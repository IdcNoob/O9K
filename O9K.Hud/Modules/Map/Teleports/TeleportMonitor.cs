namespace O9K.Hud.Modules.Map.Teleports
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class TeleportMonitor : IHudModule
    {
        private const float TeleportCheckDuration = 25;

        private const float TeleportCheckRadius = 1150;

        private readonly Dictionary<Color, uint> colors = new Dictionary<Color, uint>
        {
            { Color.FromArgb((int)(255 * 0.2f), (int)(255 * 0.46f), (int)(255 * 1f)), 0 },
            { Color.FromArgb((int)(255 * 0.4f), (int)(255 * 1f), (int)(255 * 0.75f)), 1 },
            { Color.FromArgb((int)(255 * 0.75f), (int)(255 * 0f), (int)(255 * 0.75f)), 2 },
            { Color.FromArgb((int)(255 * 0.95f), (int)(255 * 0.94f), (int)(255 * 0.04f)), 3 },
            { Color.FromArgb((int)(255 * 1f), (int)(255 * 0.42f), (int)(255 * 0f)), 4 },
            { Color.FromArgb((int)(255 * 1f), (int)(255 * 0.53f), (int)(255 * 0.76f)), 5 },
            { Color.FromArgb((int)(255 * 0.63f), (int)(255 * 0.71f), (int)(255 * 0.28f)), 6 },
            { Color.FromArgb((int)(255 * 0.4f), (int)(255 * 0.85f), (int)(255 * 0.97f)), 7 },
            { Color.FromArgb((int)(255 * 0f), (int)(255 * 0.51f), (int)(255 * 0.13f)), 8 },
            { Color.FromArgb((int)(255 * 0.64f), (int)(255 * 0.41f), (int)(255 * 0f)), 9 }
        };

        private readonly IContext9 context;

        private readonly IMinimap minimap;

        private readonly MenuSwitcher showOnMap;

        private readonly MenuSwitcher showOnMinimap;

        private readonly List<Teleport> teleports = new List<Teleport>();

        private readonly MultiSleeper<Vector3> teleportSleeper = new MultiSleeper<Vector3>();

        private Team ownerTeam;

        private IRenderManager renderManager;

        [ImportingConstructor]
        public TeleportMonitor(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.minimap = minimap;

            var menu = hudMenu.MapMenu.Add(new Menu("Teleports"));
            menu.AddTranslation(Lang.Ru, "Телепорты");
            menu.AddTranslation(Lang.Cn, "回城");

            this.showOnMap = menu.Add(new MenuSwitcher("Show on map")).SetTooltip("Show enemy teleport locations");
            this.showOnMap.AddTranslation(Lang.Ru, "Показывать на карте");
            this.showOnMap.AddTooltipTranslation(Lang.Ru, "Показывать телепорты врагов");
            this.showOnMap.AddTranslation(Lang.Cn, "地图上显示");
            this.showOnMap.AddTooltipTranslation(Lang.Cn, "显示敌人的传送地点");

            this.showOnMinimap = menu.Add(new MenuSwitcher("Show on minimap")).SetTooltip("Show enemy teleport locations");
            this.showOnMinimap.AddTranslation(Lang.Ru, "Показывать на миникарте");
            this.showOnMinimap.AddTooltipTranslation(Lang.Ru, "Показывать телепорты врагов");
            this.showOnMinimap.AddTranslation(Lang.Cn, "小地图上显示");
            this.showOnMinimap.AddTooltipTranslation(Lang.Cn, "显示敌人的传送地点");
        }

        public void Activate()
        {
            this.ownerTeam = EntityManager9.Owner.Team;
            this.renderManager = this.context.Renderer;
            Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
        }

        public void Dispose()
        {
            Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
        }

        private void AddTeleport(Teleport tp)
        {
            if (tp.HeroId == HeroId.npc_dota_hero_base)
            {
                return;
            }

            if (this.teleports.Count == 0)
            {
                this.renderManager.Draw += this.OnDraw;
            }

            this.teleports.Add(tp);
        }

        private void CheckTeleport(ParticleEffect particle, bool start)
        {
            try
            {
                if (!particle.IsValid)
                {
                    return;
                }

                var colorCp = particle.GetControlPoint(2);
                var color = Color.FromArgb(
                    (int)(255 * Math.Round(colorCp.X, 2)),
                    (int)(255 * Math.Round(colorCp.Y, 2)),
                    (int)(255 * Math.Round(colorCp.Z, 2)));

                if (!this.colors.TryGetValue(color, out var id))
                {
                    return;
                }

                var player = ObjectManager.GetPlayerById(id);
                if (player == null || player.Hero == null || player.Team == this.ownerTeam)
                {
                    return;
                }

                var hero = (Hero9)EntityManager9.GetUnit(player.Hero.Handle);
                if (hero == null || (hero.IsVisible && start))
                {
                    return;
                }

                var position = particle.GetControlPoint(0);
                var heroId = hero.Id;
                var duration = 3f;

                //  if (hero.Abilities.Any(x => x.Id == AbilityId.item_travel_boots && x.CanBeCasted(false)))
                {
                    duration = this.GetDuration(heroId, position, start);
                }

                this.AddTeleport(new Teleport(particle, heroId, position, duration, start));

                if (start)
                {
                    hero.ChangeBasePosition(position);
                }
                else
                {
                    UpdateManager.BeginInvoke(() => this.SetPosition(particle, position, hero), (int)(duration * 1000) - 200);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private float GetDuration(HeroId heroId, Vector3 position, bool start)
        {
            if (start)
            {
                var end = this.teleports.LastOrDefault(x => x.HeroId == heroId);
                if (end == null)
                {
                    return 3f;
                }

                return end.RemainingDuration;
            }

            var duration = 3f;

            if (EntityManager9.EnemyFountain.Distance2D(position) < 1000)
            {
                return duration;
            }

            if (EntityManager9.RadiantOutpost.Distance2D(position) < 1000 || EntityManager9.DireOutpost.Distance2D(position) < 1000)
            {
                duration = 6f;
            }

            var sleepers = this.teleportSleeper.Count(x => x.Value.IsSleeping && x.Key.Distance2D(position) < TeleportCheckRadius);
            if (sleepers > 0)
            {
                duration += (sleepers * 0.5f) + 1.5f;
            }

            this.teleportSleeper[position] = new Sleeper(TeleportCheckDuration);

            return duration;
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                for (var i = this.teleports.Count - 1; i > -1; i--)
                {
                    var teleport = this.teleports[i];

                    if (!teleport.IsValid)
                    {
                        this.teleports.RemoveAt(i);

                        if (this.teleports.Count == 0)
                        {
                            this.renderManager.Draw -= this.OnDraw;
                        }

                        continue;
                    }

                    if (this.showOnMinimap)
                    {
                        teleport.DrawOnMinimap(renderer, this.minimap);
                    }

                    if (this.showOnMap)
                    {
                        teleport.DrawOnMap(renderer, this.minimap);
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

        private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            switch (args.Name)
            {
                case "particles/items2_fx/teleport_start.vpcf":
                case "particles/econ/items/tinker/boots_of_travel/teleport_start_bots.vpcf":
                case "particles/econ/events/ti10/teleport/teleport_start_ti10.vpcf":
                case "particles/econ/events/ti10/teleport/teleport_start_ti10_lvl2.vpcf":
                case "particles/econ/events/ti10/teleport/teleport_start_ti10_lvl3.vpcf":
                    UpdateManager.BeginInvoke(() => this.CheckTeleport(args.ParticleEffect, true), 100);
                    break;
                case "particles/items2_fx/teleport_end.vpcf":
                case "particles/econ/items/tinker/boots_of_travel/teleport_end_bots.vpcf":
                case "particles/econ/events/ti10/teleport/teleport_end_ti10.vpcf":
                case "particles/econ/events/ti10/teleport/teleport_end_ti10_lvl2.vpcf":
                case "particles/econ/events/ti10/teleport/teleport_end_ti10_lvl3.vpcf":
                    UpdateManager.BeginInvoke(() => this.CheckTeleport(args.ParticleEffect, false));
                    break;
                //default:
                //{
                //    var name = args.ParticleEffect.Name;
                //    if ((name.Contains("teleport_start") || name.Contains("teleport_end")) && !name.Contains("furion"))
                //    {
                //        Logger.Error("TP Particle", name);
                //    }
                //    break;
                //}
            }
        }

        private void SetPosition(ParticleEffect particle, Vector3 position, Unit9 unit)
        {
            if (!particle.IsValid)
            {
                return;
            }

            // delay position update to make sure hero is teleported
            UpdateManager.BeginInvoke(() => unit.ChangeBasePosition(position), 500);
        }
    }
}