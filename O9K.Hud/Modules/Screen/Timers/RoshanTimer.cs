namespace O9K.Hud.Modules.Screen.Timers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Input;

    using Core.Data;
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Entities.Units.Unique;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Input.EventArgs;
    using Core.Managers.Input.Keys;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    using SharpDX;

    using Drawer = Helpers.Drawer;
    using KeyEventArgs = Core.Managers.Menu.EventArgs.KeyEventArgs;

    internal class RoshanTimer : IHudModule
    {
        private readonly MenuHoldKey altKey;

        private readonly Sleeper clickSleeper = new Sleeper();

        private readonly IContext9 context;

        private readonly MenuSwitcher enabled;

        private readonly MenuSwitcher hide;

        private readonly MenuSwitcher printTime;

        private readonly AbilityId[][] roshanDrop =
        {
            new[] { AbilityId.item_aegis },
            new[] { AbilityId.item_aegis, AbilityId.item_cheese },
            new[] { AbilityId.item_aegis, AbilityId.item_cheese, AbilityId.item_refresher_shard, AbilityId.item_ultimate_scepter_2 }
        };

        private readonly MenuHoldKey showDrop;

        private readonly MenuSwitcher showMinTime;

        private readonly MenuSwitcher showRemaining;

        private readonly MenuVectorSlider textPosition;

        private readonly MenuSlider textSize;

        private readonly ITopPanel topPanel;

        private float aegisPickUpTime = -999999;

        private Unit9 roshan;

        private float roshanKillTime = -99999;

        private int roshansKilled;

        [ImportingConstructor]
        public RoshanTimer(IContext9 context, ITopPanel topPanel, IHudMenu hudMenu)
        {
            this.context = context;
            this.topPanel = topPanel;

            var timersMenu = hudMenu.ScreenMenu.GetOrAdd(new Menu("Timers"));
            timersMenu.AddTranslation(Lang.Ru, "Таймеры");
            timersMenu.AddTranslation(Lang.Cn, "计时 器");

            var menu = timersMenu.Add(new Menu("Roshan timer"));
            menu.AddTranslation(Lang.Ru, "Таймер рошана");
            menu.AddTranslation(Lang.Cn, "肉山时间");

            this.enabled = menu.Add(new MenuSwitcher("Enabled"));
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTranslation(Lang.Cn, "启用");

            this.showRemaining = menu.Add(new MenuSwitcher("Remaining time")).SetTooltip("Show remaining time or respawn time");
            this.showRemaining.AddTranslation(Lang.Ru, "Оставшееся время");
            this.showRemaining.AddTooltipTranslation(Lang.Ru, "Показывать оставшееся время или время спавна");
            this.showRemaining.AddTranslation(Lang.Cn, "剩余时间");
            this.showRemaining.AddTooltipTranslation(Lang.Cn, "显示剩余时间或重生时间");

            this.showMinTime = menu.Add(new MenuSwitcher("Minimum time")).SetTooltip("Show minimum respawn time");
            this.showMinTime.AddTranslation(Lang.Ru, "Минимальное время");
            this.showMinTime.AddTooltipTranslation(Lang.Ru, "Показать минимальное время спавна");
            this.showMinTime.AddTranslation(Lang.Cn, "最小时间");
            this.showMinTime.AddTooltipTranslation(Lang.Cn, "显示最短重生时间");

            this.hide = menu.Add(new MenuSwitcher("Auto hide")).SetTooltip("Hide timer if roshan is spawned");
            this.hide.AddTranslation(Lang.Ru, "Прятать автоматически");
            this.hide.AddTooltipTranslation(Lang.Ru, "Спрятать, если рошан появился");
            this.hide.AddTranslation(Lang.Cn, "自动隐藏");
            this.hide.AddTooltipTranslation(Lang.Cn, "如果生成肉山，则隐藏计时器");

            this.printTime = menu.Add(new MenuSwitcher("Print time on click")).SetTooltip("Print respawn time in chat when clicked");
            this.printTime.AddTranslation(Lang.Ru, "Написать время при нажатии");
            this.printTime.AddTooltipTranslation(Lang.Ru, "Написать время возрождения в чате при нажатии");
            this.printTime.AddTranslation(Lang.Cn, "按下时的写入时间");
            this.printTime.AddTooltipTranslation(Lang.Cn, "单击时打印聊天中的重生时间");

            this.showDrop = menu.Add(new MenuHoldKey("Show drop", Key.LeftAlt)).SetTooltip("Show current/next roshan items");
            this.showDrop.AddTranslation(Lang.Ru, "Показать дроп");
            this.showDrop.AddTooltipTranslation(Lang.Ru, "Показать текущие/следующие предметы Рошана");
            this.showDrop.AddTranslation(Lang.Cn, "显示放置位置");
            this.showDrop.AddTooltipTranslation(Lang.Cn, "顯示當前/下一個肉山項目");

            var settings = menu.Add(new Menu("Settings"));
            settings.AddTranslation(Lang.Ru, "Настройки");
            settings.AddTranslation(Lang.Cn, "设置");

            this.textSize = settings.Add(new MenuSlider("Size", 15, 10, 35));
            this.textSize.AddTranslation(Lang.Ru, "Размер");
            this.textSize.AddTranslation(Lang.Cn, "大小");

            this.textPosition = new MenuVectorSlider(settings, Hud.Info.ScanPosition + new Vector2(0, -50));

            // hidden alt key
            this.altKey = menu.Add(new MenuHoldKey("alt", Key.LeftAlt));
            this.altKey.Hide();
        }

        public void Activate()
        {
            this.LoadTextures();

            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            this.context.Renderer.Draw -= this.OnDraw;
            this.context.Renderer.Draw -= this.OnDrawDrop;
            this.showDrop.ValueChange -= this.ShowDropOnValueChange;
            this.printTime.ValueChange -= this.PrintTimeOnValueChange;
            Game.OnFireEvent -= this.OnFireEvent;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            this.context.InputManager.MouseKeyUp -= this.InputManagerOnMouseKeyUp;
            this.textPosition.Dispose();
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.OnDraw;
                this.showDrop.ValueChange += this.ShowDropOnValueChange;
                this.printTime.ValueChange += this.PrintTimeOnValueChange;
                EntityManager9.UnitAdded += this.OnUnitAdded;
                Game.OnFireEvent += this.OnFireEvent;
            }
            else
            {
                this.context.Renderer.Draw -= this.OnDraw;
                this.context.Renderer.Draw -= this.OnDrawDrop;
                this.showDrop.ValueChange -= this.ShowDropOnValueChange;
                this.context.InputManager.MouseKeyUp -= this.InputManagerOnMouseKeyUp;
                this.printTime.ValueChange -= this.PrintTimeOnValueChange;
                Game.OnFireEvent -= this.OnFireEvent;
                EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
                EntityManager9.UnitAdded -= this.OnUnitAdded;
            }
        }

        private void InputManagerOnMouseKeyUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.MouseKey != MouseKey.Left || this.clickSleeper.IsSleeping)
                {
                    return;
                }

                var cd = Game.RawGameTime - this.roshanKillTime;
                if (cd > GameData.RoshanMaxRespawnTime)
                {
                    return;
                }

                var position = new Rectangle9(
                    new Vector2(this.textPosition.Value.X - this.textSize.Value, this.textPosition.Value.Y),
                    new Vector2(this.textSize.Value * 2));

                if (!position.Contains(e.ScreenPosition))
                {
                    return;
                }

                var time = (GameData.RoshanMaxRespawnTime - cd) + Game.GameTime;
                var start = TimeSpan.FromSeconds(time - (GameData.RoshanMaxRespawnTime - GameData.RoshanMinRespawnTime)).ToString("mss");
                var end = TimeSpan.FromSeconds(time).ToString("mss");

                Game.ExecuteCommand("say_team \"" + start + " " + end + " rosh\"");
                this.clickSleeper.Sleep(30);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LoadTextures()
        {
            var tm = this.context.Renderer.TextureManager;

            tm.LoadFromDota("o9k.roshan_icon", @"panorama\images\hud\icon_roshan_psd.vtex_c");

            foreach (var ids in this.roshanDrop)
            {
                foreach (var id in ids)
                {
                    tm.LoadAbilityFromDota(id, true);
                }
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            if (ability.Id != AbilityId.item_aegis || !ability.Owner.IsHero || ability.Owner.IsIllusion)
            {
                return;
            }

            this.aegisPickUpTime = -999999;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                var gameTime = Game.RawGameTime;
                var cd = gameTime - this.roshanKillTime;
                var aegisCd = gameTime - this.aegisPickUpTime;
                var showRemainingTime = this.showRemaining.IsEnabled;

                if (this.altKey)
                {
                    showRemainingTime = !showRemainingTime;
                }

                string text;

                if (cd > GameData.RoshanMaxRespawnTime)
                {
                    if (this.hide)
                    {
                        return;
                    }

                    text = "Alive";
                }
                else if (cd > GameData.RoshanMinRespawnTime)
                {
                    var time = GameData.RoshanMaxRespawnTime - cd;
                    if (!showRemainingTime)
                    {
                        time += Game.GameTime;
                    }

                    text = TimeSpan.FromSeconds(time).ToString(@"m\:ss") + "*";
                }
                else if (aegisCd <= GameData.AegisExpirationTime)
                {
                    var time = GameData.AegisExpirationTime - aegisCd;
                    if (!showRemainingTime)
                    {
                        time += Game.GameTime;
                    }

                    text = TimeSpan.FromSeconds(time).ToString(@"m\:ss") + "!";
                }
                else
                {
                    var time = GameData.RoshanMaxRespawnTime - cd;
                    if (!showRemainingTime)
                    {
                        time += Game.GameTime;
                    }

                    text = TimeSpan.FromSeconds(time).ToString(@"m\:ss");

                    if (this.showMinTime)
                    {
                        text = TimeSpan.FromSeconds(time - (GameData.RoshanMaxRespawnTime - GameData.RoshanMinRespawnTime))
                                   .ToString(@"m\:ss") + Environment.NewLine + text;
                    }
                }

                Drawer.DrawTextWithBackground(text, this.textSize, this.textPosition, renderer);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDrawDrop(IRenderer renderer)
        {
            try
            {
                var position = this.topPanel.GetTimePosition();
                position.Y += 100 * Hud.Info.ScreenRatio;
                position *= new Size2F(0.4f, 0.75f);

                var gameTime = Game.RawGameTime;
                var cd = gameTime - this.roshanKillTime;

                if (cd > GameData.RoshanMaxRespawnTime)
                {
                    renderer.DrawTexture("o9k.outline_green", position * 1.15f);
                }
                else
                {
                    renderer.DrawTexture(cd > GameData.RoshanMinRespawnTime ? "o9k.outline_green" : "o9k.outline_yellow", position * 1.15f);

                    var pct = (int)((cd / GameData.RoshanMaxRespawnTime) * 100);
                    renderer.DrawTexture("o9k.outline_black" + pct, position * 1.17f);
                }

                renderer.DrawTexture("o9k.roshan_icon", position);

                var center = position.Center + new Vector2(0, position.Height * 0.65f);
                var items = this.roshan?.IsValid == true && this.roshan.BaseInventory != null
                                ? this.roshan.BaseInventory.Items.Select(x => x.Id).ToArray()
                                : this.roshanDrop[this.roshansKilled];
                var abilitiesSize = 20 * Hud.Info.ScreenRatio;
                var gap = 4 * Hud.Info.ScreenRatio;
                var start = new Vector2(center.X - ((abilitiesSize / 2f) * items.Length) - ((gap / 2f) * (items.Length - 1)), center.Y);

                for (var i = 0; i < items.Length; i++)
                {
                    renderer.DrawTexture(
                        items[i] + "_rounded",
                        new Rectangle9(start + new Vector2((i * abilitiesSize) + (i * gap), 0), abilitiesSize, abilitiesSize));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnFireEvent(FireEventEventArgs args)
        {
            switch (args.GameEvent.Name)
            {
                case "dota_roshan_kill":
                {
                    this.roshanKillTime = Game.RawGameTime;
                    this.roshan = null;

                    if (this.roshansKilled < this.roshanDrop.Length - 1)
                    {
                        this.roshansKilled++;
                    }

                    return;
                }
                case "aegis_event":
                {
                    this.aegisPickUpTime = Game.RawGameTime;
                    EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
                    return;
                }
            }
        }

        private void OnUnitAdded(Unit9 unit)
        {
            try
            {
                if (!(unit is Roshan))
                {
                    return;
                }

                this.roshan = unit;
                this.roshanKillTime = -99999;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void PrintTimeOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.InputManager.MouseKeyUp += this.InputManagerOnMouseKeyUp;
            }
            else
            {
                this.context.InputManager.MouseKeyUp -= this.InputManagerOnMouseKeyUp;
            }
        }

        private void ShowDropOnValueChange(object sender, KeyEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.OnDrawDrop;
            }
            else
            {
                this.context.Renderer.Draw -= this.OnDrawDrop;
            }
        }
    }
}