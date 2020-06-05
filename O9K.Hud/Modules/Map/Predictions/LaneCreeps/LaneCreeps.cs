namespace O9K.Hud.Modules.Map.Predictions.LaneCreeps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Helpers;

    using LaneData;

    using MainMenu;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class LaneCreeps : IHudModule
    {
        private readonly IContext9 context;

        private readonly List<CreepWave> creepWaves = new List<CreepWave>();

        private readonly MenuSwitcher enabled;

        private readonly Sleeper mergeSleeper = new Sleeper();

        private readonly IMinimap minimap;

        private readonly MenuSwitcher showOnMap;

        private readonly MenuSwitcher showOnMinimap;

        private readonly Sleeper spawnSleeper = new Sleeper();

        private LanePaths lanePaths;

        private Team ownerTeam;

        [ImportingConstructor]
        public LaneCreeps(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.minimap = minimap;

            var predictionsMenu = hudMenu.MapMenu.GetOrAdd(new Menu("Predictions"));
            predictionsMenu.AddTranslation(Lang.Ru, "Предположения");
            predictionsMenu.AddTranslation(Lang.Cn, "预测");

            var menu = predictionsMenu.Add(new Menu("Lane creeps"));
            menu.AddTranslation(Lang.Ru, "Крипы");
            menu.AddTranslation(Lang.Cn, "线上小兵");

            this.enabled = menu.Add(new MenuSwitcher("Enabled"));
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTranslation(Lang.Cn, "启用");

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
            this.ownerTeam = EntityManager9.Owner.Team;
            this.lanePaths = new LanePaths();

            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            this.context.Renderer.Draw -= this.OnDraw;
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            EntityManager9.UnitMonitor.UnitDied -= this.OnUnitRemoved;
            Entity.OnBoolPropertyChange -= this.OnBoolPropertyChange;
            UpdateManager.Unsubscribe(this.OnUpdate);
            this.creepWaves.Clear();
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                if (e.OldValue)
                {
                    //todo delete
                    if (AppDomain.CurrentDomain.GetAssemblies()
                        .Any(x => !x.GlobalAssemblyCache && x.GetName().Name.Contains("PredictedCreepsLocation")))
                    {
                        Hud.DisplayWarning("O9K.Hud // PredictedCreepsLocation is already included in O9K.Hud");
                    }
                }

                EntityManager9.UnitAdded += this.OnUnitAdded;
                EntityManager9.UnitRemoved += this.OnUnitRemoved;
                EntityManager9.UnitMonitor.UnitDied += this.OnUnitRemoved;
                Entity.OnBoolPropertyChange += this.OnBoolPropertyChange;
                UpdateManager.Subscribe(this.OnUpdate);
                this.context.Renderer.Draw += this.OnDraw;
            }
            else
            {
                this.context.Renderer.Draw -= this.OnDraw;
                EntityManager9.UnitAdded -= this.OnUnitAdded;
                EntityManager9.UnitRemoved -= this.OnUnitRemoved;
                EntityManager9.UnitMonitor.UnitDied -= this.OnUnitRemoved;
                Entity.OnBoolPropertyChange -= this.OnBoolPropertyChange;
                UpdateManager.Unsubscribe(this.OnUpdate);
                this.creepWaves.Clear();
            }
        }

        private void OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
        {
            try
            {
                if (args.OldValue == args.NewValue || (!args.OldValue && args.NewValue) || args.PropertyName != "m_bIsWaitingToSpawn")
                {
                    return;
                }

                if (this.spawnSleeper)
                {
                    return;
                }

                var unit = EntityManager9.GetUnit(sender.Handle);
                if (unit == null || !unit.IsLaneCreep || unit.Team != this.ownerTeam)
                {
                    return;
                }

                foreach (var creepWave in this.creepWaves.Where(x => !x.IsSpawned))
                {
                    creepWave.Spawn();
                }

                this.spawnSleeper.Sleep(25);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                foreach (var wave in this.creepWaves)
                {
                    if (!wave.IsSpawned || wave.IsVisible)
                    {
                        continue;
                    }

                    var position = wave.PredictedPosition;
                    var count = wave.Creeps.Count.ToString();

                    if (this.showOnMinimap)
                    {
                        var size = 19 * Hud.Info.ScreenRatio;
                        var minimapPosition = this.minimap.WorldToMinimap(position, size);

                        renderer.DrawText(minimapPosition + new Size2F(size, 0), count, Color.OrangeRed, RendererFontFlags.Left, size);
                    }

                    if (this.showOnMap)
                    {
                        var size = 32 * Hud.Info.ScreenRatio;
                        var mapPosition = this.minimap.WorldToScreen(position, size);

                        if (mapPosition.IsZero)
                        {
                            continue;
                        }

                        renderer.DrawText(mapPosition + new Size2F(size, 0), count, Color.OrangeRed, RendererFontFlags.Left, size);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                // ignored
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
                if (!unit.IsLaneCreep || unit.Team == this.ownerTeam)
                {
                    return;
                }

                if (unit.BaseUnit.IsSpawned || !unit.BaseUnit.IsAlive || unit.IsVisible)
                {
                    return;
                }

                var lane = this.lanePaths.GetCreepLane(unit);
                if (lane == LanePosition.Unknown)
                {
                    return;
                }

                var wave = this.creepWaves.SingleOrDefault(x => !x.IsSpawned && x.Lane == lane);
                if (wave == null)
                {
                    this.creepWaves.Add(wave = new CreepWave(lane, this.lanePaths.GetLanePath(lane)));
                }

                wave.Creeps.Add(unit);
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
                if (!unit.IsLaneCreep || unit.Team == this.ownerTeam)
                {
                    return;
                }

                var wave = this.creepWaves.Find(x => x.Creeps.Contains(unit));
                if (wave == null)
                {
                    return;
                }

                wave.Creeps.Remove(unit);

                if (wave.IsValid)
                {
                    return;
                }

                this.creepWaves.Remove(wave);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdate()
        {
            try
            {
                var mergeCheck = !this.mergeSleeper.IsSleeping;

                for (var i = this.creepWaves.Count - 1; i > -1; i--)
                {
                    var wave = this.creepWaves[i];

                    if (!wave.IsSpawned)
                    {
                        continue;
                    }

                    if (!wave.IsValid)
                    {
                        this.creepWaves.RemoveAt(i);
                        continue;
                    }

                    if (mergeCheck)
                    {
                        var merge = this.creepWaves.Find(
                            x => x.IsSpawned && x.Lane == wave.Lane && !x.Equals(wave)
                                 && x.PredictedPosition.Distance2D(wave.PredictedPosition) < 500);

                        if (merge != null)
                        {
                            merge.Creeps.AddRange(wave.Creeps);
                            this.creepWaves.RemoveAt(i);
                            continue;
                        }
                    }

                    wave.Update();
                }

                if (mergeCheck)
                {
                    this.mergeSleeper.Sleep(2);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}