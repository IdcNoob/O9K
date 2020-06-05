namespace O9K.Hud.Modules.Map.Predictions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    internal class Courier : IHudModule
    {
        private readonly IContext9 context;

        private readonly IMinimap minimap;

        private readonly MenuSwitcher showOnMap;

        private readonly MenuSwitcher showOnMinimap;

        private readonly List<Unit9> units = new List<Unit9>();

        private Team ownerTeam;

        [ImportingConstructor]
        public Courier(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.minimap = minimap;

            var predictionsMenu = hudMenu.MapMenu.GetOrAdd(new Menu("Predictions"));
            predictionsMenu.AddTranslation(Lang.Ru, "Предположения");
            predictionsMenu.AddTranslation(Lang.Cn, "预测");

            var menu = predictionsMenu.Add(new Menu("Courier"));
            menu.AddTranslation(Lang.Ru, "Курьер");
            menu.AddTranslation(Lang.Cn, "信使");

            this.showOnMap = menu.Add(new MenuSwitcher("Show on map")).SetTooltip("Show predicted position on map");
            this.showOnMap.AddTranslation(Lang.Ru, "Показывать на карте");
            this.showOnMap.AddTooltipTranslation(Lang.Ru, "Показывать предполагаемую позицию на карте");
            this.showOnMap.AddTranslation(Lang.Cn, "地图上显示");
            this.showOnMap.AddTooltipTranslation(Lang.Cn, "在地图上显示预测位置");

            this.showOnMinimap = menu.Add(new MenuSwitcher("Show on minimap")).SetTooltip("Show predicted position on minimap");
            this.showOnMinimap.AddTranslation(Lang.Ru, "Показывать на миникарте");
            this.showOnMinimap.AddTooltipTranslation(Lang.Ru, "Показывать предполагаемую позицию на миникарте");
            this.showOnMinimap.AddTranslation(Lang.Cn, "小地图上显示");
            this.showOnMinimap.AddTooltipTranslation(Lang.Cn, "在小地图上显示预测位置");
        }

        public void Activate()
        {
            if (Game.GameMode == GameMode.Turbo)
            {
                return;
            }

            this.context.Renderer.TextureManager.LoadFromDota(
                "o9k.courier",
                @"panorama\images\hud\reborn\icon_courier_standard_psd.vtex_c");
            this.ownerTeam = EntityManager9.Owner.Team;

            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.UnitRemoved += this.OnUnitRemoved;
            this.context.Renderer.Draw += this.OnDraw;
        }

        public void Dispose()
        {
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            this.context.Renderer.Draw -= this.OnDraw;
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                foreach (var courier in this.units)
                {
                    if (!courier.IsValid || !courier.IsAlive)
                    {
                        continue;
                    }

                    var position = courier.Position;

                    if (this.showOnMinimap)
                    {
                        var minimapPosition = this.minimap.WorldToMinimap(position, 20 * Hud.Info.ScreenRatio);
                        if (minimapPosition.IsZero)
                        {
                            continue;
                        }

                        renderer.DrawTexture("o9k.courier", minimapPosition);
                    }

                    if (this.showOnMap && !courier.IsVisible)
                    {
                        var mapPosition = this.minimap.WorldToScreen(position, 40 * Hud.Info.ScreenRatio);
                        if (mapPosition.IsZero)
                        {
                            continue;
                        }

                        renderer.DrawTexture("o9k.courier", mapPosition);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                // ignore
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitAdded(Unit9 unit)
        {
            try
            {
                if (!unit.IsCourier || unit.Team == this.ownerTeam)
                {
                    return;
                }

                this.units.Add(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitRemoved(Unit9 unit)
        {
            try
            {
                if (!unit.IsCourier || unit.Team == this.ownerTeam)
                {
                    return;
                }

                this.units.Remove(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}