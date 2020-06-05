namespace O9K.Core.Managers.Jungle
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Camp;

    using Data;

    using Ensage;

    using Helpers;

    using SharpDX;

    [Export(typeof(IJungleManager))]
    public class JungleManager : IJungleManager
    {
        [ImportingConstructor]
        public JungleManager()
        {
            var data = new JungleCampsData();
            this.JungleCamps = data.GetJungleCamps();

            //Drawing.OnDraw += this.DebugOnDraw;
        }

        public IEnumerable<IJungleCamp> JungleCamps { get; }

        [Obsolete]
        private void DebugOnDraw(EventArgs args)
        {
            foreach (var jungleCamp in this.JungleCamps)
            {
                var drawPosition = Drawing.WorldToScreen(jungleCamp.DrawPosition);
                if (drawPosition.IsZero)
                {
                    continue;
                }

                var creepsPosition = Drawing.WorldToScreen(jungleCamp.CreepsPosition);
                if (creepsPosition.IsZero)
                {
                    continue;
                }

                var size = jungleCamp.IsSmall ? "Small" :
                           jungleCamp.IsMedium ? "Medium" :
                           jungleCamp.IsLarge ? "Large" :
                           jungleCamp.IsAncient ? "Ancient" : "?";

                Drawing.DrawText(
                    jungleCamp.Id + " // S: " + size,
                    drawPosition,
                    new Vector2(16 * Hud.Info.ScreenRatio),
                    Color.White,
                    FontFlags.None);

                Drawing.DrawText(
                    jungleCamp.Id.ToString(),
                    creepsPosition,
                    new Vector2(20 * Hud.Info.ScreenRatio),
                    Color.White,
                    FontFlags.None);
            }
        }
    }
}