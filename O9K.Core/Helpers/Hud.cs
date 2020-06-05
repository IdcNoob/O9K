namespace O9K.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Logger;

    using SharpDX;

    public static class Hud
    {
        private static readonly Dictionary<string, float> Messages = new Dictionary<string, float>();

        public static Vector3 CameraPosition
        {
            get
            {
                return Player.CameraPosition;
            }
            set
            {
                Game.ExecuteCommand($"dota_camera_set_lookatpos {value.X} {value.Y}");
            }
        }

        public static void CenterCameraOnHero(bool enabled = true)
        {
            Game.ExecuteCommand((enabled ? "+" : "-") + "dota_camera_center_on_hero");
        }

        public static void DisplayWarning(string text, float time = 10)
        {
            try
            {
                if (Messages.ContainsKey(text))
                {
                    Messages[text] = Game.RawGameTime + time;
                    return;
                }

                Messages.Add(text, Game.RawGameTime + time);

                if (Messages.Count == 1)
                {
                    Drawing.OnDraw += OnDraw;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static bool IsPositionOnScreen(Vector3 position)
        {
            if (position.Z == 0)
            {
                //todo get proper Z

                if (Drawing.WorldToScreen(position.SetZ(128), out _) && Drawing.WorldToScreen(position.SetZ(384), out _))
                {
                    return true;
                }
            }
            else
            {
                if (Drawing.WorldToScreen(position, out _))
                {
                    return true;
                }
            }

            return false;
        }

        private static void OnDraw(EventArgs args)
        {
            try
            {
                if (Messages.Count == 0)
                {
                    Drawing.OnDraw -= OnDraw;
                }

                var position = new Vector2(Info.ScreenSize.X * 0.13f, Info.ScreenSize.Y * 0.05f);

                foreach (var message in Messages.ToList())
                {
                    var text = message.Key;
                    var time = message.Value;

                    if (Game.RawGameTime > time)
                    {
                        Messages.Remove(text);
                        continue;
                    }

                    position += new Vector2(0, 35);

#pragma warning disable 618
                    Drawing.DrawText(text, "Calibri", position, new Vector2(33 * Info.ScreenRatio), Color.OrangeRed, FontFlags.DropShadow);
#pragma warning restore 618
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static class Info
        {
            public static Vector2 GlyphPosition { get; } = new Vector2(Drawing.Width * 0.16f, Drawing.Height * 0.965f);

            public static Vector2 ScanPosition { get; } = new Vector2(Drawing.Width * 0.16f, Drawing.Height * 0.925f);

            public static float ScreenRatio { get; } = Drawing.Height / 1080f;

            public static Vector2 ScreenSize { get; } = new Vector2(Drawing.Width, Drawing.Height);
        }
    }
}