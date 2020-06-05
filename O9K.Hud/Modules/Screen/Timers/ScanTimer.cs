namespace O9K.Hud.Modules.Screen.Timers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Data;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    using SharpDX;

    using Drawer = Helpers.Drawer;

    internal class ScanTimer : IHudModule
    {
        private readonly IContext9 context;

        private readonly MenuSwitcher enabled;

        private readonly MenuSwitcher hide;

        private readonly IMinimap minimap;

        private readonly MenuSwitcher showOnMap;

        private readonly MenuSwitcher showOnMinimap;

        private readonly MenuSwitcher showRemaining;

        private readonly MenuVectorSlider textPosition;

        private readonly MenuSlider textSize;

        private Team ownerTeam;

        private Vector3 scanPosition;

        private ParticleEffect scanRadius;

        private bool showEnabled = false;

        private Sleeper sleeper;

        [ImportingConstructor]
        public ScanTimer(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.minimap = minimap;

            var timersMenu = hudMenu.ScreenMenu.GetOrAdd(new Menu("Timers"));
            timersMenu.AddTranslation(Lang.Ru, "Таймеры");
            timersMenu.AddTranslation(Lang.Cn, "计时 器");

            var menu = timersMenu.Add(new Menu("Scan timer"));
            menu.AddTranslation(Lang.Ru, "Таймер скана");
            menu.AddTranslation(Lang.Cn, "扫描计时器");

            this.enabled = menu.Add(new MenuSwitcher("Enabled", false));
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTranslation(Lang.Cn, "启用");

            this.showRemaining = menu.Add(new MenuSwitcher("Remaining time")).SetTooltip("Show remaining time or ready time");
            this.showRemaining.AddTranslation(Lang.Ru, "Оставшееся время");
            this.showRemaining.AddTooltipTranslation(Lang.Ru, "Показывать оставшееся время или время готовности");
            this.showRemaining.AddTranslation(Lang.Cn, "剩余时间");
            this.showRemaining.AddTooltipTranslation(Lang.Cn, "显示剩余时间或准备时间");

            this.hide = menu.Add(new MenuSwitcher("Auto hide")).SetTooltip("Hide timer if scan is ready");
            this.hide.AddTranslation(Lang.Ru, "Прятать автоматически");
            this.hide.AddTooltipTranslation(Lang.Ru, "Спрятать, если скан готов");
            this.hide.AddTranslation(Lang.Cn, "自动隐藏");
            this.hide.AddTooltipTranslation(Lang.Cn, "如果扫描准备就绪，则隐藏计时器");

            var settings = menu.Add(new Menu("Settings"));
            settings.AddTranslation(Lang.Ru, "Настройки");
            settings.AddTranslation(Lang.Cn, "设置");

            this.textSize = settings.Add(new MenuSlider("Size", 15, 10, 35));
            this.textSize.AddTranslation(Lang.Ru, "Размер");
            this.textSize.AddTranslation(Lang.Cn, "大小");

            this.textPosition = new MenuVectorSlider(settings, Hud.Info.ScanPosition + new Vector2(0, 10));

            var mapMenu = hudMenu.MapMenu.Add(new Menu("Scan"));
            mapMenu.AddTranslation(Lang.Ru, "Скан");
            mapMenu.AddTranslation(Lang.Cn, "扫描");

            this.showOnMap = mapMenu.Add(new MenuSwitcher("Show on map")).SetTooltip("Show enemy scan position");
            this.showOnMap.AddTranslation(Lang.Ru, "Показывать на карте");
            this.showOnMap.AddTooltipTranslation(Lang.Ru, "Показать позицию скана противника на карте");
            this.showOnMap.AddTranslation(Lang.Cn, "地图上显示");
            this.showOnMap.AddTooltipTranslation(Lang.Cn, "显示敌人的扫描位置");

            this.showOnMinimap = mapMenu.Add(new MenuSwitcher("Show on minimap")).SetTooltip("Show enemy scan position");
            this.showOnMinimap.AddTranslation(Lang.Ru, "Показывать на миникарте");
            this.showOnMinimap.AddTooltipTranslation(Lang.Ru, "Показать позицию скана противника на миникарте");
            this.showOnMinimap.AddTranslation(Lang.Cn, "小地图上显示");
            this.showOnMinimap.AddTooltipTranslation(Lang.Cn, "显示敌人的扫描位置");
        }

        public void Activate()
        {
            this.context.Renderer.TextureManager.LoadFromDota("o9k.scan", @"panorama\images\hud\reborn\icon_scan_on_dire_psd.vtex_c");
            this.ownerTeam = EntityManager9.Owner.Team;
            this.sleeper = new Sleeper(this.ownerTeam == Team.Radiant ? Game.ScanCooldownDire : Game.ScanCooldownRadiant);

            this.enabled.ValueChange += this.EnabledOnValueChange;
            this.showOnMap.ValueChange += this.ShowOnMapOnValueChange;
            this.showOnMinimap.ValueChange += this.ShowOnMinimapOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            this.context.Renderer.Draw -= this.OnDrawTimer;
            this.context.Renderer.Draw -= this.OnDrawPosition;
            this.showOnMap.ValueChange -= this.ShowOnMapOnValueChange;
            this.showOnMinimap.ValueChange -= this.ShowOnMinimapOnValueChange;
            ObjectManager.OnAddEntity -= this.OnAddEntity;
            this.textPosition.Dispose();
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.OnDrawTimer;
            }
            else
            {
                this.context.Renderer.Draw -= this.OnDrawTimer;
            }
        }

        private void OnAddEntity(EntityEventArgs args)
        {
            try
            {
                if (this.sleeper)
                {
                    return;
                }

                var unit = args.Entity as Unit;
                if (unit == null || unit.Team == this.ownerTeam || unit.DayVision != 0 || unit.Name != "npc_dota_thinker")
                {
                    return;
                }

                if (unit.IsVisible && unit.Modifiers.All(x => x.Name != "modifier_radar_thinker"))
                {
                    return;
                }

                this.scanPosition = unit.Position;

                if (this.showOnMap)
                {
                    this.scanRadius = new ParticleEffect("particles/ui_mouseactions/drag_selected_ring.vpcf", this.scanPosition);
                    this.scanRadius.SetControlPoint(1, new Vector3(255, 0, 0));
                    this.scanRadius.SetControlPoint(2, new Vector3(-GameData.ScanRadius, 255, 0));
                }

                this.sleeper.Sleep(this.ownerTeam == Team.Radiant ? Game.ScanCooldownDire : Game.ScanCooldownRadiant);

                this.context.Renderer.Draw += this.OnDrawPosition;

                UpdateManager.BeginInvoke(
                    () =>
                        {
                            this.context.Renderer.Draw -= this.OnDrawPosition;
                            this.scanRadius?.Dispose();
                        },
                    GameData.ScanActiveTime * 1000);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDrawPosition(IRenderer renderer)
        {
            try
            {
                if (this.showOnMinimap)
                {
                    var minimapPosition = this.minimap.WorldToMinimap(this.scanPosition, 25 * Hud.Info.ScreenRatio);
                    renderer.DrawTexture("o9k.scan", minimapPosition);
                }

                if (this.showOnMap)
                {
                    var mapPosition = this.minimap.WorldToScreen(this.scanPosition, 35 * Hud.Info.ScreenRatio);
                    if (mapPosition.IsZero)
                    {
                        return;
                    }

                    renderer.DrawTexture("o9k.scan", mapPosition);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDrawTimer(IRenderer renderer)
        {
            try
            {
                var cd = this.ownerTeam == Team.Radiant ? Game.ScanCooldownDire : Game.ScanCooldownRadiant;

                if (cd > 0)
                {
                    if (!this.showRemaining)
                    {
                        cd += Game.GameTime;
                    }

                    Drawer.DrawTextWithBackground(TimeSpan.FromSeconds(cd).ToString(@"m\:ss"), this.textSize, this.textPosition, renderer);
                }
                else if (!this.hide)
                {
                    Drawer.DrawTextWithBackground("Ready", this.textSize, this.textPosition, renderer);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void ShowOnMapOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                if (!this.showEnabled)
                {
                    ObjectManager.OnAddEntity += this.OnAddEntity;
                    this.showEnabled = true;
                }
            }
            else
            {
                if (!this.showOnMinimap)
                {
                    ObjectManager.OnAddEntity -= this.OnAddEntity;
                    this.showEnabled = false;
                }
            }
        }

        private void ShowOnMinimapOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                if (!this.showEnabled)
                {
                    ObjectManager.OnAddEntity += this.OnAddEntity;
                    this.showEnabled = true;
                }
            }
            else
            {
                if (!this.showOnMap)
                {
                    ObjectManager.OnAddEntity -= this.OnAddEntity;
                    this.showEnabled = false;
                }
            }
        }
    }
}