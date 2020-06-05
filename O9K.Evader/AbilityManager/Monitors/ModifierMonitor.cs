namespace O9K.Evader.AbilityManager.Monitors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities.Base;
    using Abilities.Base.Evadable;
    using Abilities.Base.Evadable.Components;

    using Core.Entities.Heroes;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;

    using Metadata;

    using Settings;

    internal class ModifierMonitor : IDisposable
    {
        private readonly Dictionary<string, AbilityId> abilityModifiers = new Dictionary<string, AbilityId>
        {
            // fix
            { "modifier_juggernaut_omnislash", AbilityId.juggernaut_omni_slash },
            { "modifier_cold_feet", AbilityId.ancient_apparition_cold_feet },
            { "modifier_chen_test_of_faith_teleport", AbilityId.chen_holy_persuasion },
            { "modifier_dark_willow_debuff_fear", AbilityId.dark_willow_terrorize },
            { "modifier_dragonknight_breathefire_reduction", AbilityId.dragon_knight_breathe_fire },
            { "modifier_morphling_adaptive_strike", AbilityId.morphling_adaptive_strike_str },
            { "modifier_shadow_demon_purge_slow", AbilityId.shadow_demon_demonic_purge },
            { "modifier_templar_assassin_trap_slow", AbilityId.templar_assassin_psionic_trap },
            { "modifier_troll_warlord_whirling_axes_slow", AbilityId.troll_warlord_whirling_axes_ranged },
            { "modifier_ursa_fury_swipes_damage_increase", AbilityId.ursa_fury_swipes },
            { "modifier_windrunner_shackle_shot", AbilityId.windrunner_shackleshot },
            { "modifier_maledict", AbilityId.witch_doctor_maledict },
            { "modifier_skeleton_king_reincarnate_slow", AbilityId.skeleton_king_reincarnation },
            { "modifier_omninight_guardian_angel", AbilityId.omniknight_guardian_angel },
            { "modifier_phantom_assassin_stiflingdagger", AbilityId.phantom_assassin_stifling_dagger },
            { "modifier_phoenix_fire_spirit_burn", AbilityId.phoenix_launch_fire_spirit },
            { "modifier_ice_blast", AbilityId.ancient_apparition_ice_blast },
            { "modifier_abaddon_frostmourne_debuff_bonus", AbilityId.abaddon_frostmourne },
            { "modifier_keeper_of_the_light_mana_leak", AbilityId.keeper_of_the_light_chakra_magic },
            { "modifier_monkey_king_quadruple_tap_bonuses", AbilityId.monkey_king_jingu_mastery },
            { "modifier_snapfire_magma_burn_slow", AbilityId.snapfire_mortimer_kisses },
            { "modifier_clumsy_net_ensnare", AbilityId.item_clumsy_net },

            { "modifier_eul_cyclone", AbilityId.item_cyclone },
            { "modifier_sheepstick_debuff", AbilityId.item_sheepstick },
            { "modifier_black_king_bar_immune", AbilityId.item_black_king_bar },
            { "modifier_bloodthorn_debuff", AbilityId.item_bloodthorn },
            { "modifier_orchid_malevolence_debuff", AbilityId.item_orchid },
            { "modifier_ghost_state", AbilityId.item_ghost },
            { "modifier_item_invisibility_edge_windwalk", AbilityId.item_invis_sword },
            { "modifier_item_silver_edge_windwalk", AbilityId.item_silver_edge },
            { "modifier_rod_of_atos_debuff", AbilityId.item_rod_of_atos },
            { "modifier_item_medallion_of_courage_armor_reduction", AbilityId.item_medallion_of_courage },
            { "modifier_item_solar_crest_armor_reduction", AbilityId.item_solar_crest },
            { "modifier_boots_of_travel_incoming", AbilityId.item_travel_boots },
            { "modifier_flask_healing", AbilityId.item_flask },
            { "modifier_clarity_potion", AbilityId.item_clarity },

            // force 
            { "item_glimmer_cape", AbilityId.ability_base },
            { "modifier_ancientapparition_coldfeet_freeze", AbilityId.ability_base },
            { "modifier_kunkka_torrent_slow", AbilityId.ability_base },
            { "modifier_razor_static_link_debuff", AbilityId.ability_base },
            { "modifier_skeleton_king_hellfire_blast", AbilityId.ability_base },
            { "modifier_item_meteor_hammer_burn", AbilityId.ability_base },
            { "modifier_bane_nightmare_invulnerable", AbilityId.ability_base },
            { "modifier_beastmaster_primal_roar_speed", AbilityId.ability_base },
            { "modifier_beastmaster_primal_roar_slow", AbilityId.ability_base },
            { "modifier_bounty_hunter_track_effect", AbilityId.ability_base },
            { "modifier_jakiro_dual_breath_slow", AbilityId.ability_base },
            { "modifier_medusa_stone_gaze", AbilityId.ability_base },
            { "modifier_medusa_stone_gaze_facing", AbilityId.ability_base },
            { "modifier_silencer_last_word_disarm", AbilityId.ability_base },
            { "modifier_templar_assassin_meld", AbilityId.ability_base },
            { "modifier_winter_wyvern_winters_curse", AbilityId.ability_base },
            { "modifier_abaddon_frostmourne_debuff", AbilityId.ability_base },
            { "modifier_huskar_inner_fire_knockback", AbilityId.ability_base },
            { "modifier_lion_finger_of_death_delay", AbilityId.ability_base },
        };

        private readonly Dictionary<string, AbilityId> abilityObstacleModifiers = new Dictionary<string, AbilityId>
        {
            { "modifier_bloodseeker_bloodbath_thinker", AbilityId.bloodseeker_blood_bath },
            { "modifier_disruptor_kinetic_field_thinker", AbilityId.disruptor_kinetic_field },
            { "modifier_disruptor_static_storm_thinker", AbilityId.disruptor_static_storm },
            { "modifier_faceless_void_chronosphere", AbilityId.faceless_void_chronosphere },
            { "modifier_invoker_sun_strike", AbilityId.invoker_sun_strike },
            { "modifier_invoker_sun_strike_cataclysm", AbilityId.invoker_sun_strike },
            { "modifier_kunkka_torrent_thinker", AbilityId.kunkka_torrent },
            { "modifier_skywrath_mage_mystic_flare", AbilityId.skywrath_mage_mystic_flare },
            //{ "modifier_dark_willow_bramble_maze_thinker", AbilityId.dark_willow_bramble_maze },
            { "modifier_spirit_breaker_charge_of_darkness_vision", AbilityId.spirit_breaker_charge_of_darkness },
            { "modifier_alchemist_unstable_concoction", AbilityId.alchemist_unstable_concoction },
            { "modifier_witch_doctor_death_ward", AbilityId.witch_doctor_death_ward },
            { "modifier_clinkz_burning_army", AbilityId.clinkz_burning_army },
            { "modifier_phoenix_sun", AbilityId.phoenix_supernova },
            { "modifier_void_spirit_dissimilate_phase", AbilityId.void_spirit_dissimilate },
            { "modifier_snapfire_firesnap_cookie_short_hop", AbilityId.snapfire_firesnap_cookie },
            { "modifier_drow_ranger_multishot", AbilityId.drow_ranger_multishot },
            { "modifier_techies_suicide_leap", AbilityId.techies_suicide },

            { "modifier_provide_vision", AbilityId.grimstroke_ink_creature }, // hack
            { "modifier_item_glimmer_cape_fade", AbilityId.item_glimmer_cape }, //hack

            //{ "modifier_leshrac_split_earth_thinker", "leshrac_split_earth" },
            //{ "modifier_lina_light_strike_array", "lina_light_strike_array" },
            //{ "modifier_enigma_black_hole_thinker", "enigma_black_hole" },
            //{ "modifier_slark_dark_pact", "slark_dark_pact" },
            //{ "modifier_storm_spirit_static_remnant_thinker", "storm_spirit_static_remnant" },
            //{ "modifier_shredder_chakram_thinker", "shredder_chakram" },
            //{ "modifier_monkey_king_spring_thinker", "monkey_king_primal_spring" },
            //{ "modifier_morphling_waveform", "morphling_waveform" }
        };

        private readonly List<EvadableAbility> evadable;

        private readonly Owner owner;

        private readonly SettingsMenu settingsMenu;

        public ModifierMonitor(IMainMenu menu, List<EvadableAbility> evadable)
        {
            this.settingsMenu = menu.Settings;
            this.evadable = evadable;
            this.owner = EntityManager9.Owner;

            Unit.OnModifierAdded += this.OnModifierAdded;
        }

        public void Dispose()
        {
            Unit.OnModifierAdded -= this.OnModifierAdded;
        }

        public AbilityId? GetAbilityId(string modifierName, string modifierTextureName)
        {
            AbilityId abilityId;

            switch (modifierName)
            {
                case "modifier_bashed":
                case "modifier_stunned":
                case "modifier_silence":
                case "modifier_invisible":
                {
                    if (!this.abilityModifiers.TryGetValue(modifierTextureName, out abilityId))
                    {
                        abilityId = this.TryParse(modifierTextureName.Split('/').Last(), modifierTextureName);
                    }

                    break;
                }
                default:
                {
                    if (!this.abilityModifiers.TryGetValue(modifierName, out abilityId))
                    {
                        var name = modifierName.Substring("modifier_".Length);
                        var index = name.LastIndexOf("_", StringComparison.Ordinal);

                        abilityId = this.TryParse(name, modifierName, index < 0);

                        if (index > 0 && abilityId == AbilityId.ability_base)
                        {
                            abilityId = this.TryParse(name.Substring(0, index), modifierName);
                        }
                    }

                    break;
                }
            }

            if (abilityId == AbilityId.ability_base)
            {
                //Console.WriteLine("cant parse: " + modifierName + " " + modifierTextureName);
                return null;
            }

            return abilityId;
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                var modifier = args.Modifier;
                var name = modifier.Name;

                if (this.abilityObstacleModifiers.TryGetValue(name, out var id))
                {
                    if (!(this.evadable.Find(x => x.Ability.Id == id) is IModifierObstacle modifierObstacleAbility))
                    {
                        return;
                    }

                    if (sender.Team == this.owner.Team && !modifierObstacleAbility.AllyModifierObstacle)
                    {
                        return;
                    }

                    modifierObstacleAbility.AddModifierObstacle(modifier, sender);
                    return;
                }

                if (!this.settingsMenu.ModifierCounter || modifier.IsHidden)
                {
                    return;
                }

                var modifierOwner = EntityManager9.GetUnit(sender.Handle);
                if (modifierOwner == null)
                {
                    return;
                }

                if ((!modifierOwner.IsHero || !modifierOwner.IsImportant) && name != "modifier_boots_of_travel_incoming")
                {
                    return;
                }

                var abilityId = this.GetAbilityId(name, modifier.TextureName);
                if (abilityId == null)
                {
                    return;
                }

                //todo can be improved ?
                var ability = this.evadable.Where(x => x.Ability.Id == abilityId.Value)
                    .OfType<IModifierCounter>()
                    .OrderBy(x => x.Owner.Distance(modifierOwner))
                    .FirstOrDefault();

                if (ability?.ModifierCounterEnabled?.IsEnabled != true)
                {
                    return;
                }

                if ((ability.ModifierAllyCounter && !modifierOwner.IsAlly(ability.Owner))
                    || (ability.ModifierEnemyCounter && modifierOwner.IsAlly(ability.Owner)))
                {
                    ability.AddModifier(modifier, modifierOwner);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private AbilityId TryParse(string name, string fullName, bool forceCache = true)
        {
            if (Enum.TryParse<AbilityId>(name, out var abilityId))
            {
                this.abilityModifiers[fullName] = abilityId;
            }
            else if (forceCache)
            {
                this.abilityModifiers[fullName] = AbilityId.ability_base;
            }

            return abilityId;
        }
    }
}