namespace O9K.Hud.Modules.Map.Runes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Data;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;
    using Core.Managers.Particle;

    using Ensage;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    using SharpDX;

    internal class BountyRunes : IHudModule
    {
        private readonly IContext9 context;

        private readonly IMinimap minimap;

        private readonly List<Vector3> pickedRunes = new List<Vector3>();

        private readonly MenuSwitcher showOnMap;

        private readonly MenuSwitcher showOnMinimap;

        private Vector3[] bountySpawns;

        [ImportingConstructor]
        public BountyRunes(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.minimap = minimap;

            var runesMenu = hudMenu.MapMenu.GetOrAdd(new Menu("Runes"));
            runesMenu.AddTranslation(Lang.Ru, "Руны");
            runesMenu.AddTranslation(Lang.Cn, "神符");

            var menu = runesMenu.Add(new Menu("Bounty"));
            menu.AddTranslation(Lang.Ru, "Баунти");
            menu.AddTranslation(Lang.Cn, "赏金神符");

            this.showOnMap = menu.Add(new MenuSwitcher("Show on map", false)).SetTooltip("Show picked bounty runes");
            this.showOnMap.AddTranslation(Lang.Ru, "Показывать на карте");
            this.showOnMap.AddTooltipTranslation(Lang.Ru, "Показывать взятые баунти руны");
            this.showOnMap.AddTranslation(Lang.Cn, "地图上显示");
            this.showOnMap.AddTooltipTranslation(Lang.Cn, "显示刷新的赏金神符");

            this.showOnMinimap = menu.Add(new MenuSwitcher("Show on minimap")).SetTooltip("Show picked bounty runes");
            this.showOnMinimap.AddTranslation(Lang.Ru, "Показывать на миникарте");
            this.showOnMinimap.AddTooltipTranslation(Lang.Ru, "Показывать взятые баунти руны");
            this.showOnMinimap.AddTranslation(Lang.Cn, "小地图上显示");
            this.showOnMinimap.AddTooltipTranslation(Lang.Cn, "显示刷新的赏金神符");
        }

        public void Activate()
        {
            this.bountySpawns = ObjectManager.GetEntities<Item>()
                .Where(x => x.NetworkName == "CDOTA_Item_RuneSpawner_Bounty")
                .Select(x => x.Position)
                .ToArray();

            if (this.bountySpawns.Length == 0)
            {
                return;
            }

            this.context.ParticleManger.ParticleAdded += this.OnParticleAdded;
            UpdateManager.Subscribe(this.OnUpdate, 3000);
        }

        public void Dispose()
        {
            UpdateManager.Unsubscribe(this.OnUpdate);
            this.context.ParticleManger.ParticleAdded -= this.OnParticleAdded;
            this.context.Renderer.Draw -= this.OnDraw;
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                foreach (var pickedRune in this.pickedRunes)
                {
                    if (this.showOnMinimap)
                    {
                        var position = this.minimap.WorldToMinimap(pickedRune, 20 * Hud.Info.ScreenRatio);
                        renderer.DrawTexture("o9k.x", position);
                    }

                    if (this.showOnMap)
                    {
                        var position = this.minimap.WorldToScreen(pickedRune, 40 * Hud.Info.ScreenRatio);
                        if (position.IsZero)
                        {
                            continue;
                        }

                        renderer.DrawTexture("o9k.x", position);
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
                if (!particle.Released || particle.Name != "particles/generic_gameplay/rune_bounty_owner.vpcf")
                {
                    return;
                }

                var position = particle.GetControlPoint(0);
                var runeSpawn = Array.Find(this.bountySpawns, x => x.Distance2D(position) < 500);
                if (runeSpawn.IsZero)
                {
                    return;
                }

                if (this.pickedRunes.Contains(runeSpawn))
                {
                    return;
                }

                if (this.pickedRunes.Count == 0)
                {
                    this.context.Renderer.Draw += this.OnDraw;
                }

                this.pickedRunes.Add(runeSpawn);
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
                if (Game.GameTime % GameData.BountyRuneRespawnTime > GameData.BountyRuneRespawnTime - 5)
                {
                    this.context.Renderer.Draw -= this.OnDraw;
                    this.pickedRunes.Clear();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}