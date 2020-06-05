namespace O9K.Hud.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Renderer.Texture;

    using Modules;

    internal class TextureLoader : IHudModule
    {
        private readonly IContext9 context;

        private readonly HashSet<string> loaded = new HashSet<string>
        {
            nameof(AbilityId.courier_take_stash_and_transfer_items),
            nameof(AbilityId.courier_transfer_items_to_other_player),
        };

        private ITextureManager textureManager;

        [ImportingConstructor]
        public TextureLoader(IContext9 context)
        {
            this.context = context;
        }

        public void Activate()
        {
            this.textureManager = this.context.Renderer.TextureManager;

            this.LoadTextures();

            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.AbilityAdded += this.OnAbilityAdded;
        }

        public void Dispose()
        {
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
        }

        private void LoadAbilityTexture(AbilityId id)
        {
            this.textureManager.LoadAbilityFromDota(id);
            this.textureManager.LoadAbilityFromDota(id, true);
        }

        private void LoadTextures()
        {
            foreach (var player in ObjectManager.GetEntities<Player>())
            {
                var id = player.SelectedHeroId;
                if (id == HeroId.npc_dota_hero_base)
                {
                    continue;
                }

                this.textureManager.LoadHeroFromDota(id);
                this.textureManager.LoadHeroFromDota(id, true);
                this.textureManager.LoadHeroFromDota(id, false, true);

                this.loaded.Add(id.ToString());
            }

            this.textureManager.LoadFromDota("o9k.x", @"panorama\images\hud\reborn\ping_icon_retreat_psd.vtex_c");
            this.textureManager.LoadFromDota(
                "rune_arcane_rounded",
                @"panorama\images\spellicons\rune_arcane_png.vtex_c",
                TextureProperties.Round);
            this.textureManager.LoadFromDota(
                "rune_arcane_rounded",
                @"panorama\images\spellicons\rune_doubledamage_png.vtex_c",
                TextureProperties.Round);
            this.textureManager.LoadFromDota(
                "rune_haste_rounded",
                @"panorama\images\spellicons\rune_haste_png.vtex_c",
                TextureProperties.Round);
            this.textureManager.LoadFromDota(
                "rune_invis_rounded",
                @"panorama\images\spellicons\rune_invis_png.vtex_c",
                TextureProperties.Round);
            this.textureManager.LoadFromDota(
                "rune_regen_rounded",
                @"panorama\images\spellicons\rune_regen_png.vtex_c",
                TextureProperties.Round);
            this.textureManager.LoadFromDota("npc_dota_roshan", @"panorama\images\heroes\npc_dota_hero_roshan_png.vtex_c");
            this.textureManager.LoadFromDota(
                "npc_dota_roshan_rounded",
                @"panorama\images\heroes\npc_dota_hero_roshan_png.vtex_c",
                TextureProperties.Round);

            this.textureManager.LoadAbilityFromDota("item_bottle");
            this.textureManager.LoadAbilityFromDota("item_bottle_arcane");
            this.textureManager.LoadAbilityFromDota("item_bottle_bounty");
            this.textureManager.LoadAbilityFromDota("item_bottle_doubledamage");
            this.textureManager.LoadAbilityFromDota("item_bottle_haste");
            this.textureManager.LoadAbilityFromDota("item_bottle_illusion");
            this.textureManager.LoadAbilityFromDota("item_bottle_invisibility");
            this.textureManager.LoadAbilityFromDota("item_bottle_regeneration");

            this.LoadAbilityTexture(AbilityId.item_smoke_of_deceit);
            this.LoadAbilityTexture(AbilityId.item_ward_sentry);
            this.LoadAbilityTexture(AbilityId.item_ward_observer);
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (this.loaded.Contains(ability.TextureName))
                {
                    return;
                }

                if (!ability.IsTalent)
                {
                    this.textureManager.LoadAbilityFromDota(ability.TextureName);
                    this.textureManager.LoadAbilityFromDota(ability.TextureName, true);
                }

                this.loaded.Add(ability.TextureName);
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
                if (this.loaded.Contains(unit.TextureName))
                {
                    return;
                }

                if (unit is Hero9 hero)
                {
                    this.textureManager.LoadHeroFromDota(hero.TextureName);
                    this.textureManager.LoadHeroFromDota(hero.TextureName, true);
                    this.textureManager.LoadHeroFromDota(hero.TextureName, false, true);
                }
                else if (unit.IsUnit && !unit.IsCreep)
                {
                    this.textureManager.LoadUnitFromDota(unit.DefaultName);
                }

                this.loaded.Add(unit.TextureName);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}