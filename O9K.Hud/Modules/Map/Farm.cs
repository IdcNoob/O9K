namespace O9K.Hud.Modules.Map
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Heroes;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;
    using Core.Managers.Particle;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    using SharpDX;

    internal class Farm : IHudModule
    {
        private readonly HashSet<string> attackParticles = new HashSet<string>
        {
            "particles/generic_gameplay/generic_hit_blood.vpcf",
            "particles/neutral_fx/neutral_item_drop_lvl0.vpcf",
            "particles/neutral_fx/neutral_item_drop_lvl1.vpcf",
            "particles/neutral_fx/neutral_item_drop_lvl2.vpcf",
            "particles/neutral_fx/neutral_item_drop_lvl3.vpcf",
            "particles/neutral_fx/neutral_item_drop_lvl4.vpcf",
            "particles/neutral_fx/neutral_item_drop_lvl5.vpcf"
        };

        private readonly Dictionary<Vector3, Sleeper> attacks = new Dictionary<Vector3, Sleeper>();

        private readonly IContext9 context;

        private readonly IMinimap minimap;

        private readonly MenuSwitcher showOnMap;

        private readonly MenuSwitcher showOnMinimap;

        private Owner owner;

        [ImportingConstructor]
        public Farm(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.minimap = minimap;

            var menu = hudMenu.MapMenu.Add(new Menu("Farm"));
            menu.AddTranslation(Lang.Ru, "Фарм");
            menu.AddTranslation(Lang.Cn, "打钱");

            this.showOnMap = menu.Add(new MenuSwitcher("Show on map")).SetTooltip("Show when enemy attacks neutrals/roshan");
            this.showOnMap.AddTranslation(Lang.Ru, "Показывать на карте");
            this.showOnMap.AddTooltipTranslation(Lang.Ru, "Показывать когда враг атакует нейтралов/рошана");
            this.showOnMap.AddTranslation(Lang.Cn, "地图上显示");
            this.showOnMap.AddTooltipTranslation(Lang.Cn, "显示敌人何时袭击野区/肉山");

            this.showOnMinimap = menu.Add(new MenuSwitcher("Show on minimap")).SetTooltip("Show when enemy attacks neutrals/roshan");
            this.showOnMinimap.AddTranslation(Lang.Ru, "Показывать на миникарте");
            this.showOnMinimap.AddTooltipTranslation(Lang.Ru, "Показывать когда враг атакует нейтралов/рошана");
            this.showOnMinimap.AddTranslation(Lang.Cn, "小地图上显示");
            this.showOnMinimap.AddTooltipTranslation(Lang.Cn, "显示敌人何时袭击野区/肉山");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;
            this.context.Renderer.TextureManager.LoadFromDota("o9k.attack", @"panorama\images\hud\reborn\ping_icon_attack_psd.vtex_c");

            this.context.ParticleManger.ParticleAdded += this.OnParticleAdded;
        }

        public void Dispose()
        {
            this.context.Renderer.Draw -= this.OnDraw;
            this.context.ParticleManger.ParticleAdded -= this.OnParticleAdded;
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                foreach (var attack in this.attacks.ToList())
                {
                    if (!attack.Value.IsSleeping)
                    {
                        this.attacks.Remove(attack.Key);

                        if (this.attacks.Count == 0)
                        {
                            this.context.Renderer.Draw -= this.OnDraw;
                        }

                        continue;
                    }

                    if (this.showOnMinimap)
                    {
                        var position = this.minimap.WorldToMinimap(attack.Key, 20 * Hud.Info.ScreenRatio);
                        renderer.DrawTexture("o9k.attack", position);
                    }

                    if (this.showOnMap)
                    {
                        var position = this.minimap.WorldToScreen(attack.Key, 40 * Hud.Info.ScreenRatio);
                        if (position.IsZero)
                        {
                            continue;
                        }

                        renderer.DrawTexture("o9k.attack", position);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                //ignore
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnParticleAdded(Particle particle)
        {
            try
            {
                if (!particle.Released || particle.SenderHandle == null)
                {
                    return;
                }

                if (!this.attackParticles.Contains(particle.Name))
                {
                    return;
                }

                var position = particle.GetControlPoint(0);

                if (particle.SenderHandle == this.owner.PlayerHandle)
                {
                    var droppedItem = ObjectManager.GetEntities<PhysicalItem>().FirstOrDefault(x => x.Distance2D(position) < 300);
                    if (droppedItem != null)
                    {
                        return;
                    }
                }
                else if (particle.Sender.Team != Team.Neutral || particle.Sender.IsVisible)
                {
                    return;
                }

                var sleeper = this.attacks.FirstOrDefault(x => x.Key.Distance2D(position) < 500).Value;
                if (sleeper != null)
                {
                    sleeper.Sleep(3);
                    return;
                }

                if (this.attacks.Count == 0)
                {
                    this.context.Renderer.Draw += this.OnDraw;
                }

                this.attacks[position] = new Sleeper(3);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}