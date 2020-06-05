namespace O9K.Core.Managers.Menu
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Items;

    using Logger;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal sealed class MenuSerializer
    {
        public MenuSerializer(params JsonConverter[] converters)
        {
            this.Settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DefaultValueHandling = DefaultValueHandling.Include | DefaultValueHandling.Populate,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = converters,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            this.JsonSerializer = JsonSerializer.Create(this.Settings);
            this.ConfigDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "game", "O9K");

            try
            {
                Directory.CreateDirectory(this.ConfigDirectory);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public string ConfigDirectory { get; }

        public JsonSerializer JsonSerializer { get; }

        public JsonSerializerSettings Settings { get; }

        public JToken Deserialize(MenuItem menuItem)
        {
            var file = Path.Combine(this.ConfigDirectory, menuItem.Name + ".json");

            if (!File.Exists(file))
            {
                return null;
            }

            try
            {
                var json = File.ReadAllText(file);
                return JToken.Parse(json);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        public void Serialize(MainMenu mainMenu)
        {
            var menus = mainMenu.MenuItems.ToList();
            menus.Add(mainMenu);

            foreach (var menu in menus)
            {
                var sb = new StringBuilder();
                using (var sw = new StringWriter(sb))
                {
                    using (var writer = new JsonTextWriter(sw))
                    {
                        writer.Formatting = Formatting.Indented;
                        writer.WriteStartObject();
                        this.Serialize(writer, menu);
                        writer.WriteEndObject();
                    }
                }

                var file = Path.Combine(this.ConfigDirectory, menu.Name + ".json");
                File.WriteAllText(file, sb.ToString());
            }
        }

        private void Serialize(JsonWriter writer, MenuItem menuItem)
        {
            if (menuItem is Menu menu && !menu.IsMainMenu)
            {
                var saved = new List<string>();

                writer.WritePropertyName(menuItem.Name);
                writer.WriteStartObject();

                foreach (var item in menu.MenuItems.ToList())
                {
                    this.Serialize(writer, item);
                    saved.Add(item.Name);
                }

                if (menu.Token != null)
                {
                    foreach (var item in menu.Token.ToObject<JObject>())
                    {
                        if (saved.Contains(item.Key))
                        {
                            continue;
                        }

                        writer.WritePropertyName(item.Key);
                        this.JsonSerializer.Serialize(writer, item.Value);
                    }
                }

                writer.WriteEndObject();
            }
            else
            {
                var value = menuItem.GetSaveValue();
                if (value == null)
                {
                    return;
                }

                writer.WritePropertyName(menuItem.Name);
                this.WritePropertyValue(writer, value);
            }
        }

        private void WritePropertyValue(JsonWriter writer, object propertyValue)
        {
            var propertyType = propertyValue.GetType();
            if (propertyType.IsArray && propertyValue is object[] values)
            {
                writer.WriteStartArray();
                foreach (var value in values)
                {
                    this.JsonSerializer.Serialize(writer, value);
                }

                writer.WriteEnd();
            }
            else
            {
                this.JsonSerializer.Serialize(writer, propertyValue);
            }
        }
    }
}