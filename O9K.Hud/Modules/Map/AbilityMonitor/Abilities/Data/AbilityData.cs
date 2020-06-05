namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data
{
    using System.Collections.Generic;

    using Core.Data;
    using Core.Helpers;

    using Ensage;

    using SharpDX;

    using UniqueAbilities.Blink;
    using UniqueAbilities.Cleave;
    using UniqueAbilities.FireRemnant;
    using UniqueAbilities.Item;
    using UniqueAbilities.Maelstorm;
    using UniqueAbilities.Poof;
    using UniqueAbilities.RemoteMines;
    using UniqueAbilities.Smoke;
    using UniqueAbilities.ThinkerAbility;
    using UniqueAbilities.Wards;
    using UniqueAbilities.Wisp;

    internal class AbilityData
    {
        // ReSharper disable StringLiteralTypo
        public Dictionary<string, AbilityFullData> Units { get; } = new Dictionary<string, AbilityFullData>
        {
            {
                "npc_dota_observer_wards", new WardAbilityData
                {
                    AbilityId = AbilityId.item_ward_observer,
                    UnitName = "npc_dota_observer_wards",
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.item_ward_observer, "vision_range").GetValue(1) + 150,
                    RangeColor = new Vector3(255, 255, 0),
                    Duration = (int)new SpecialData(AbilityId.item_ward_observer, "lifetime").GetValue(1)
                }
            },
            {
                "npc_dota_sentry_wards", new WardAbilityData
                {
                    AbilityId = AbilityId.item_ward_sentry,
                    UnitName = "npc_dota_sentry_wards",
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.item_ward_sentry, "true_sight_range").GetValue(1) + 150,
                    RangeColor = new Vector3(30, 100, 255),
                    Duration = (int)new SpecialData(AbilityId.item_ward_sentry, "lifetime").GetValue(1)
                }
            },
            {
                "npc_dota_pugna_nether_ward_1", new AbilityFullData
                {
                    AbilityId = AbilityId.pugna_nether_ward,
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.pugna_nether_ward, "radius").GetValue(1) + 150,
                    RangeColor = new Vector3(124, 252, 0),
                    Duration = (int)new SpecialData(AbilityId.pugna_nether_ward, "AbilityDuration").GetValue(1)
                }
            },
            {
                "npc_dota_pugna_nether_ward_2", new AbilityFullData
                {
                    AbilityId = AbilityId.pugna_nether_ward,
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.pugna_nether_ward, "radius").GetValue(1) + 150,
                    RangeColor = new Vector3(124, 252, 0),
                    Duration = (int)new SpecialData(AbilityId.pugna_nether_ward, "AbilityDuration").GetValue(2)
                }
            },
            {
                "npc_dota_pugna_nether_ward_3", new AbilityFullData
                {
                    AbilityId = AbilityId.pugna_nether_ward,
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.pugna_nether_ward, "radius").GetValue(1) + 150,
                    RangeColor = new Vector3(124, 252, 0),
                    Duration = (int)new SpecialData(AbilityId.pugna_nether_ward, "AbilityDuration").GetValue(3)
                }
            },
            {
                "npc_dota_pugna_nether_ward_4", new AbilityFullData
                {
                    AbilityId = AbilityId.pugna_nether_ward,
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.pugna_nether_ward, "radius").GetValue(1) + 150,
                    RangeColor = new Vector3(124, 252, 0),
                    Duration = (int)new SpecialData(AbilityId.pugna_nether_ward, "AbilityDuration").GetValue(4)
                }
            },
            {
                "npc_dota_venomancer_plague_ward_1", new AbilityFullData
                {
                    AbilityId = AbilityId.venomancer_plague_ward,
                    Duration = (int)new SpecialData(AbilityId.venomancer_plague_ward, "duration").GetValue(1)
                }
            },
            {
                "npc_dota_venomancer_plague_ward_2", new AbilityFullData
                {
                    AbilityId = AbilityId.venomancer_plague_ward,
                    Duration = (int)new SpecialData(AbilityId.venomancer_plague_ward, "duration").GetValue(2)
                }
            },
            {
                "npc_dota_venomancer_plague_ward_3", new AbilityFullData
                {
                    AbilityId = AbilityId.venomancer_plague_ward,
                    Duration = (int)new SpecialData(AbilityId.venomancer_plague_ward, "duration").GetValue(3)
                }
            },
            {
                "npc_dota_venomancer_plague_ward_4", new AbilityFullData
                {
                    AbilityId = AbilityId.venomancer_plague_ward,
                    Duration = (int)new SpecialData(AbilityId.venomancer_plague_ward, "duration").GetValue(4)
                }
            },
            {
                "npc_dota_unit_tombstone1", new AbilityFullData
                {
                    AbilityId = AbilityId.undying_tombstone,
                    Duration = (int)new SpecialData(AbilityId.undying_tombstone, "duration").GetValue(1)
                }
            },
            {
                "npc_dota_unit_tombstone2", new AbilityFullData
                {
                    AbilityId = AbilityId.undying_tombstone,
                    Duration = (int)new SpecialData(AbilityId.undying_tombstone, "duration").GetValue(2)
                }
            },
            {
                "npc_dota_unit_tombstone3", new AbilityFullData
                {
                    AbilityId = AbilityId.undying_tombstone,
                    Duration = (int)new SpecialData(AbilityId.undying_tombstone, "duration").GetValue(3)
                }
            },
            {
                "npc_dota_unit_tombstone4", new AbilityFullData
                {
                    AbilityId = AbilityId.undying_tombstone,
                    Duration = (int)new SpecialData(AbilityId.undying_tombstone, "duration").GetValue(4)
                }
            },
            {
                "npc_dota_techies_land_mine", new AbilityFullData
                {
                    AbilityId = AbilityId.techies_land_mines,
                    Duration = 99999999,
                    TimeToShow = 0,
                    ShowTimer = false,
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.techies_land_mines, "radius").GetValue(1) + 50,
                    RangeColor = new Vector3(255, 0, 0),
                }
            },
            {
                "npc_dota_techies_stasis_trap", new AbilityFullData
                {
                    AbilityId = AbilityId.techies_stasis_trap,
                    Duration = 99999999,
                    TimeToShow = 0,
                    ShowTimer = false,
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.techies_stasis_trap, "activation_radius").GetValue(1) + 50,
                    RangeColor = new Vector3(65, 105, 225),
                }
            },
            {
                "npc_dota_techies_remote_mine", new RemoteMinesAbilityData
                {
                    AbilityId = AbilityId.techies_remote_mines,
                    Duration = (int)new SpecialData(AbilityId.techies_remote_mines, "duration").GetValue(1),
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.techies_remote_mines, "radius").GetValue(1) + 50,
                    RangeColor = new Vector3(0, 255, 0),
                }
            },
            {
                "npc_dota_clinkz_skeleton_archer", new AbilityFullData
                {
                    AbilityId = AbilityId.clinkz_burning_army,
                    TimeToShow = 0,
                    Duration = (int)new SpecialData(AbilityId.clinkz_burning_army, "duration").GetValue(1),
                }
            },
            {
                "npc_dota_zeus_cloud", new AbilityFullData
                {
                    AbilityId = AbilityId.zuus_cloud,
                    TimeToShow = 0,
                    Duration = (int)new SpecialData(AbilityId.zuus_cloud, "cloud_duration").GetValue(1),
                }
            },
            // todo add ?
            //{
            //    "npc_dota_ignis_fatuus", new AbilityFullData
            //    {
            //        AbilityId = AbilityId.keeper_of_the_light_will_o_wisp,
            //        TimeToShow = 0,
            //        Duration = ?
            //    }
            //},
            {
                "npc_dota_templar_assassin_psionic_trap", new AbilityFullData
                {
                    AbilityId = AbilityId.templar_assassin_psionic_trap,
                    Duration = 99999999,
                    TimeToShow = 0,
                    ShowTimer = false
                }
            },
            {
                "npc_dota_invoker_forged_spirit", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_forge_spirit,
                    TimeToShow = 5
                }
            },
            {
                "npc_dota_juggernaut_healing_ward", new AbilityFullData
                {
                    AbilityId = AbilityId.juggernaut_healing_ward,
                    TimeToShow = 5
                }
            },
            {
                "npc_dota_stormspirit_remnant", new AbilityFullData
                {
                    AbilityId = AbilityId.storm_spirit_static_remnant,
                    TimeToShow = 5
                }
            },
            {
                "npc_dota_necronomicon_warrior_1", new AbilityFullData
                {
                    AbilityId = AbilityId.item_necronomicon_3,
                    TimeToShow = 5
                }
            },
            {
                "npc_dota_necronomicon_warrior_2", new AbilityFullData
                {
                    AbilityId = AbilityId.item_necronomicon_3,
                    TimeToShow = 5
                }
            },
            {
                "npc_dota_necronomicon_warrior_3", new AbilityFullData
                {
                    AbilityId = AbilityId.item_necronomicon_3,
                    TimeToShow = 5
                }
            },
            {
                "npc_dota_metamorphosis_fear", new AbilityFullData
                {
                    AbilityId = AbilityId.terrorblade_metamorphosis,
                    TimeToShow = 5
                }
            },
            {
                "npc_dota_plasma_field", new AbilityFullData
                {
                    AbilityId = AbilityId.razor_plasma_field,
                    TimeToShow = 5
                }
            },
            {
                "npc_dota_thinker", new ThinkerUnitAbilityData
                {
                    TimeToShow = 5
                }
            },
        };

        public Dictionary<string, AbilityFullData> Particles { get; } = new Dictionary<string, AbilityFullData>
        {
            // abilities

            {
                "particles/units/heroes/hero_abaddon/abaddon_aphotic_shield_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.abaddon_aphotic_shield,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_abaddon/abaddon_curse_counter_stack.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.abaddon_frostmourne,
                    SearchOwner = true,
                }
            },
            {
                "particles/units/heroes/hero_abaddon/abaddon_borrowed_time_heal.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.abaddon_borrowed_time,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_alchemist/alchemist_acid_spray_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.alchemist_acid_spray,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_alchemist/alchemist_unstableconc_bottles.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.alchemist_unstable_concoction,
                    ShowNotification = true
                }
            },
            {
                "particles/units/heroes/hero_alchemist/alchemist_unstable_concoction_explosion.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.alchemist_unstable_concoction_throw,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_alchemist/alchemist_chemichalrage_effect.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.alchemist_chemical_rage,
                }
            },
            {
                "particles/units/heroes/hero_ancient_apparition/ancient_apparition_cold_feet_marker.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ancient_apparition_cold_feet,
                    SearchOwner = true,
                }
            },
            {
                "particles/units/heroes/hero_ancient_apparition/ancient_ice_vortex.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ancient_apparition_ice_vortex,
                }
            },
            {
                "particles/econ/items/ancient_apparition/ancient_apparation_ti8/ancient_ice_vortex_ti8.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ancient_apparition_ice_vortex,
                }
            },
            {
                "particles/units/heroes/hero_antimage/antimage_blade_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.antimage_mana_break,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/antimage/antimage_weapon_basher_ti5/antimage_blade_hit_basher_ti_5.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.antimage_mana_break,
                    ParticleReleaseData = true,

                    Replace = true
                }
            },
            {
                "particles/econ/items/antimage/antimage_weapon_basher_ti5_gold/antimage_blade_hit_basher_ti_5_gold.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.antimage_mana_break,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_antimage/antimage_blink_start.vpcf", new BlinkAbilityData
                {
                    AbilityId = AbilityId.antimage_blink,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/antimage/antimage_ti7_golden/antimage_blink_start_ti7_golden.vpcf", new BlinkAbilityData
                {
                    AbilityId = AbilityId.antimage_blink,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/antimage/antimage_ti7/antimage_blink_start_ti7.vpcf", new BlinkAbilityData
                {
                    AbilityId = AbilityId.antimage_blink,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_arc_warden/arc_warden_flux_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.arc_warden_flux,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_arc_warden/arc_warden_magnetic_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.arc_warden_magnetic_field,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_arc_warden/arc_warden_wraith_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.arc_warden_spark_wraith,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/arc_warden/arc_warden_ti9_immortal/arc_warden_ti9_wraith_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.arc_warden_spark_wraith,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_arc_warden/arc_warden_tempest_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.arc_warden_tempest_double,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_axe/axe_beserkers_call_owner.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.axe_berserkers_call,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/axe/axe_helm_shoutmask/axe_beserkers_call_owner_shoutmask.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.axe_berserkers_call,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/axe/axe_ti9_immortal/axe_ti9_call.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.axe_berserkers_call,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/axe/axe_ti9_immortal/axe_ti9_gold_call.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.axe_berserkers_call,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_axe/axe_battle_hunger_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.axe_battle_hunger,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_axe/axe_counterhelix.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.axe_counter_helix
                }
            },
            {
                "particles/econ/items/axe/axe_cinder/axe_cinder_battle_hunger_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.axe_battle_hunger,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_axe/axe_culling_blade.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.axe_culling_blade,
                    SearchOwner = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_axe/axe_culling_blade_boost.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.axe_culling_blade,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_bane/bane_sap.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bane_brain_sap,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_batrider/batrider_stickynapalm_impact.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.batrider_sticky_napalm,
                    ParticleReleaseData = true,
                    ControlPoint = 2
                }
            },
            {
                "particles/units/heroes/hero_batrider/batrider_flamebreak.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.batrider_flamebreak,
                }
            },
            {
                "particles/units/heroes/hero_batrider/batrider_firefly_ember.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.batrider_firefly,
                }
            },
            {
                "particles/units/heroes/hero_batrider/batrider_firefly_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.batrider_firefly,
                    ParticleReleaseData = true,
                    Replace = true,
                    SearchOwner = true
                }
            },
            {
                "particles/econ/items/batrider/batrider_ti8_immortal_mount/batrider_ti8_immortal_firefly_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.batrider_firefly,
                    ParticleReleaseData = true,
                    Replace = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_batrider/batrider_flaming_lasso.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.batrider_flaming_lasso
                }
            },
            {
                "particles/units/heroes/hero_beastmaster/beastmaster_wildaxe.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.beastmaster_wild_axes
                }
            },
            {
                "particles/units/heroes/hero_beastmaster/beastmaster_call_boar.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.beastmaster_call_of_the_wild_boar,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_beastmaster/beastmaster_call_bird.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.beastmaster_call_of_the_wild_hawk,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_bloodseeker/bloodseeker_bloodritual_ring.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bloodseeker_blood_bath
                }
            },
            {
                "particles/units/heroes/hero_bounty_hunter/bounty_hunter_windwalk.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bounty_hunter_wind_walk,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_bounty_hunter/bounty_hunter_hand_r.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bounty_hunter_jinada
                }
            },
            {
                "particles/econ/items/bounty_hunter/bounty_hunter_ti9_immortal/bh_ti9_immortal_jinada_active_r.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bounty_hunter_jinada
                }
            },
            {
                "particles/units/heroes/hero_brewmaster/brewmaster_thunder_clap.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.brewmaster_thunder_clap,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_brewmaster/brewmaster_cinder_brew_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.brewmaster_cinder_brew,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_brewmaster/brewmaster_drunkenbrawler_crit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.brewmaster_drunken_brawler
                }
            },
            {
                "particles/units/heroes/hero_brewmaster/brewmaster_earth_ambient.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.brewmaster_primal_split,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_brewmaster/brewmaster_earth_death.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.brewmaster_primal_split,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_brewmaster/brewmaster_windwalk.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.brewmaster_storm_wind_walk,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_bristleback/bristleback_quill_spray.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bristleback_quill_spray,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/bristleback/bristle_spikey_spray/bristle_spikey_quill_spray.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bristleback_quill_spray,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_bristleback/bristleback_back_dmg.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bristleback_bristleback,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_bristleback/bristleback_side_dmg.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bristleback_bristleback,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_bristleback/bristleback_viscous_nasal_goo_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.bristleback_viscous_nasal_goo,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_broodmother/broodmother_spiderlings_spawn.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.broodmother_spawn_spiderlings,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_broodmother/broodmother_spin_web_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.broodmother_spin_web,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_broodmother/broodmother_hunger_buff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.broodmother_insatiable_hunger,
                }
            },
            {
                "particles/units/heroes/hero_centaur/centaur_warstomp.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.centaur_hoof_stomp,
                    ParticleReleaseData = true,
                    ControlPoint = 2,
                }
            },
            {
                "particles/units/heroes/hero_centaur/centaur_double_edge.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.centaur_double_edge,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/centaur/centaur_ti9/centaur_double_edge_ti9.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.centaur_double_edge,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_centaur/centaur_stampede.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.centaur_stampede,
                    ControlPoint = 1,
                }
            },
            {
                "particles/units/heroes/hero_chaos_knight/chaos_knight_bolt_msg.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chaos_knight_chaos_bolt,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_chaos_knight/chaos_knight_reality_rift.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chaos_knight_reality_rift,
                    Replace = true
                }
            },
            {
                "particles/econ/items/chaos_knight/chaos_knight_ti7_shield/chaos_knight_ti7_reality_rift.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chaos_knight_reality_rift,
                    Replace = true
                }
            },
            {
                "particles/econ/items/chaos_knight/chaos_knight_ti7_shield/chaos_knight_ti7_golden_reality_rift.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chaos_knight_reality_rift,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_chaos_knight/chaos_knight_crit_tgt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chaos_knight_chaos_strike,
                    ParticleReleaseData = true,
                    SearchOwner = true,
                    Replace = true,
                    RawParticlePosition = true,
                }
            },
            {
                "particles/econ/items/chaos_knight/chaos_knight_ti9_weapon/chaos_knight_ti9_weapon_crit_tgt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chaos_knight_chaos_strike,
                    ParticleReleaseData = true,
                    SearchOwner = true,
                    Replace = true,
                    RawParticlePosition = true,
                }
            },
            {
                "particles/units/heroes/hero_chen/chen_cast_1.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chen_penitence,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_chen/chen_cast_3.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chen_holy_persuasion,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_chen/chen_teleport_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chen_divine_favor,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_chen/chen_cast_4.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.chen_hand_of_god,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_clinkz/clinkz_death_pact_buff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.clinkz_death_pact,
                }
            },
            {
                "particles/units/heroes/hero_clinkz/clinkz_windwalk.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.clinkz_wind_walk,
                    ParticleReleaseData = true,
                    ShowNotification = true
                }
            },
            {
                "particles/units/heroes/hero_clinkz/clinkz_death_pact.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.clinkz_burning_army,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_rattletrap/rattletrap_battery_shrapnel.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.rattletrap_battery_assault,
                    ParticleReleaseData = true,
                    ControlPoint = 1,
                    Replace = true,
                }
            },
            {
                "particles/units/heroes/hero_rattletrap/rattletrap_cog_ambient.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.rattletrap_power_cogs,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_rattletrap/rattletrap_rocket_flare.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.rattletrap_rocket_flare,
                }
            },
            {
                "particles/econ/items/clockwerk/clockwerk_paraflare/clockwerk_para_rocket_flare.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.rattletrap_rocket_flare,
                }
            },
            {
                "particles/units/heroes/hero_rattletrap/rattletrap_hookshot.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.rattletrap_hookshot,
                }
            },
            {
                "particles/units/heroes/hero_crystalmaiden/maiden_crystal_nova.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.crystal_maiden_crystal_nova,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/crystal_maiden/crystal_maiden_cowl_of_ice/maiden_crystal_nova_cowlofice.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.crystal_maiden_crystal_nova,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_crystalmaiden/maiden_frostbite.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.crystal_maiden_frostbite,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/crystal_maiden/ti7_immortal_shoulder/cm_ti7_immortal_frostbite_proj.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.crystal_maiden_frostbite,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_crystalmaiden/maiden_freezing_field_snow.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.crystal_maiden_freezing_field,
                }
            },
            {
                "particles/econ/items/crystal_maiden/crystal_maiden_maiden_of_icewrack/maiden_freezing_field_snow_arcana1.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.crystal_maiden_freezing_field,
                }
            },
            {
                "particles/units/heroes/hero_dark_seer/dark_seer_vacuum.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dark_seer_vacuum,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_dark_seer/dark_seer_ion_shell_damage.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dark_seer_ion_shell,
                    Replace = true,
                    ParticleReleaseData = true,
                    TimeToShow = 1
                }
            },
            {
                "particles/units/heroes/hero_dark_willow/dark_willow_bramble_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dark_willow_bramble_maze,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_dark_willow/dark_willow_shadow_realm.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dark_willow_shadow_realm,
                }
            },
            {
                "particles/units/heroes/hero_dark_willow/dark_willow_ley_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dark_willow_cursed_crown,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/dark_willow/dark_willow_ti8_immortal_head/dw_crimson_ti8_immortal_cursed_crown_cast.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.dark_willow_cursed_crown,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/dark_willow/dark_willow_ti8_immortal_head/dw_ti8_immortal_cursed_crown_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dark_willow_cursed_crown,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_dark_willow/dark_willow_wisp_aoe.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dark_willow_bedlam,
                }
            },
            {
                "particles/units/heroes/hero_dark_willow/dark_willow_wisp_spell_channel.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dark_willow_terrorize,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_dazzle/dazzle_shadow_wave.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dazzle_shadow_wave,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/dazzle/dazzle_ti9/dazzle_shadow_wave_ti9.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dazzle_shadow_wave,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_death_prophet/death_prophet_carrion_swarm.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.death_prophet_carrion_swarm
                }
            },
            {
                "particles/econ/items/death_prophet/death_prophet_acherontia/death_prophet_acher_swarm.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.death_prophet_carrion_swarm
                }
            },
            {
                "particles/units/heroes/hero_death_prophet/death_prophet_silence_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.death_prophet_silence,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_death_prophet/death_prophet_spiritsiphon.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.death_prophet_spirit_siphon
                }
            },
            {
                "particles/units/heroes/hero_death_prophet/death_prophet_spirit_glow.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.death_prophet_exorcism,
                    Replace = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_disruptor/disruptor_thunder_strike_buff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.disruptor_thunder_strike,
                }
            },
            {
                "particles/econ/items/disruptor/disruptor_ti8_immortal_weapon/disruptor_ti8_immortal_thunder_strike_buff.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.disruptor_thunder_strike,
                }
            },
            {
                "particles/units/heroes/hero_doom_bringer/doom_bringer_devour.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.doom_bringer_devour,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/doom/doom_ti8_immortal_arms/doom_ti8_immortal_devour.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.doom_bringer_devour,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_doom_bringer/doom_bringer_scorched_earth_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.doom_bringer_scorched_earth,
                    Replace = true,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_doom_bringer/doom_infernal_blade_impact.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.doom_bringer_infernal_blade,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_dragon_knight/dragon_knight_dragon_tail_dragonform.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dragon_knight_dragon_tail,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_dragon_knight/dragon_knight_dragon_tail_knightform.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.dragon_knight_dragon_tail,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_earth_spirit/espirit_bouldersmash_caster.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earth_spirit_boulder_smash,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_earth_spirit/espirit_rollingboulder.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earth_spirit_rolling_boulder,
                }
            },
            {
                "particles/econ/items/earth_spirit/earth_spirit_ti6_boulder/espirit_ti6_rollingboulder.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earth_spirit_rolling_boulder
                }
            },
            {
                "particles/units/heroes/hero_earth_spirit/espirit_geomagentic_grip_caster.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earth_spirit_geomagnetic_grip,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_earthshaker/earthshaker_fissure.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earthshaker_fissure,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/earthshaker/earthshaker_ti9/earthshaker_fissure_ti9.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earthshaker_fissure,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_earthshaker/earthshaker_totem_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earthshaker_enchant_totem,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/earthshaker/earthshaker_arcana/earthshaker_arcana_totem_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earthshaker_enchant_totem,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/earthshaker/earthshaker_totem_ti6/earthshaker_totem_ti6_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earthshaker_enchant_totem,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/earthshaker/earthshaker_arcana/earthshaker_arcana_totem_leap.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earthshaker_enchant_totem,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_earthshaker/earthshaker_totem_leap_blur.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earthshaker_enchant_totem,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/earthshaker/earthshaker_totem_ti6/earthshaker_totem_ti6_leap_blur.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.earthshaker_enchant_totem,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_elder_titan/elder_titan_echo_stomp_physical.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.elder_titan_echo_stomp,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_elder_titan/elder_titan_ancestral_spirit_ambient.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.elder_titan_ancestral_spirit,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_elder_titan/elder_titan_earth_splitter.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.elder_titan_earth_splitter,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_ember_spirit/ember_spirit_searing_chains_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ember_spirit_searing_chains,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_ember_spirit/ember_spirit_sleight_of_fist_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ember_spirit_sleight_of_fist,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_ember_spirit/ember_spirit_flameguard.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ember_spirit_flame_guard,
                }
            },
            {
                "particles/econ/items/ember_spirit/ember_ti9/ember_ti9_flameguard.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ember_spirit_flame_guard,
                }
            },
            {
                "particles/units/heroes/hero_ember_spirit/emberspirit_flame_shield_aoe_impact.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ember_spirit_flame_guard,
                    Replace = true,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_ember_spirit/ember_spirit_fire_remnant.vpcf", new FireRemnantAbilityData
                {
                    AbilityId = AbilityId.ember_spirit_fire_remnant,
                    StartControlPoint = 1,
                    Duration = (int)new SpecialData(AbilityId.ember_spirit_fire_remnant, "duration").GetValue(1) + 3,
                }
            },
            {
                "particles/units/heroes/hero_ember_spirit/ember_spirit_remnant_dash.vpcf", new FireRemnantAbilityData
                {
                    //removes fire remnants display
                    AbilityId = AbilityId.ember_spirit_fire_remnant,
                }
            },
            {
                "particles/units/heroes/hero_enchantress/enchantress_enchant.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.enchantress_enchant,
                    ControlPoint = 1,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_enchantress/enchantress_natures_attendants_heal.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.enchantress_natures_attendants,
                    Replace = true,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_enigma/enigma_demonic_conversion.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.enigma_demonic_conversion,
                    SearchOwner = true,
                    Replace = true,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_enigma/enigma_midnight_pulse.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.enigma_midnight_pulse,
                }
            },
            {
                "particles/units/heroes/hero_faceless_void/faceless_void_time_walk_preimage.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.faceless_void_time_walk,
                    ParticleReleaseData = true,
                    ControlPoint = 1,
                }
            },
            {
                "particles/units/heroes/hero_faceless_void/faceless_void_time_lock_bash.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.faceless_void_time_lock,
                    SearchOwner = true,
                    Replace = true,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_grimstroke/grimstroke_cast2_ground.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.grimstroke_dark_artistry,
                }
            },
            {
                "particles/units/heroes/hero_grimstroke/grimstroke_cast_phantom.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.grimstroke_ink_creature,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_grimstroke/grimstroke_cast_ink_swell.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.grimstroke_spirit_walk,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_grimstroke/grimstroke_ink_swell_aoe.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.grimstroke_spirit_walk,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_grimstroke/grimstroke_ink_swell_tick_damage.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.grimstroke_spirit_walk,
                    ParticleReleaseData = true,
                    Replace = true,
                }
            },
            {
                "particles/units/heroes/hero_gyrocopter/gyro_rocket_barrage.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.gyrocopter_rocket_barrage,
                    Replace = true,
                    ParticleReleaseData = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/units/heroes/hero_gyrocopter/gyro_calldown_first.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.gyrocopter_call_down,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_huskar/huskar_inner_fire.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.huskar_inner_fire,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_huskar/huskar_life_break.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.huskar_life_break,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/econ/items/huskar/huskar_searing_dominator/huskar_searing_life_break.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.huskar_life_break,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_invoker/invoker_quas_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/econ/items/invoker/invoker_apex/invoker_apex_quas_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/econ/items/invoker/invoker_ti6/invoker_ti6_quas_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/units/heroes/hero_invoker/invoker_wex_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/econ/items/invoker/invoker_ti6/invoker_ti6_exort_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/econ/items/invoker/invoker_apex/invoker_apex_wex_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/units/heroes/hero_invoker/invoker_exort_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/econ/items/invoker/invoker_apex/invoker_apex_exort_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/econ/items/invoker/invoker_ti6/invoker_ti6_wex_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/units/heroes/hero_invoker_kid/invoker_kid_quas_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/units/heroes/hero_invoker_kid/invoker_kid_wex_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/units/heroes/hero_invoker_kid/invoker_kid_exort_orb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_wex,
                    Replace = true,
                    TimeToShow = 2
                }
            },
            {
                "particles/units/heroes/hero_invoker/invoker_cold_snap.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_cold_snap,
                    Replace = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_invoker/invoker_ice_wall.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_ice_wall,
                }
            },
            {
                "particles/units/heroes/hero_invoker/invoker_emp.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_emp,
                }
            },
            {
                "particles/units/heroes/hero_invoker/invoker_alacrity_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_alacrity,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/invoker/invoker_ti7/invoker_ti7_alacrity_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_alacrity,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_invoker/invoker_chaos_meteor_fly.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.invoker_chaos_meteor,
                    ParticleReleaseData = true,
                }
            },
            //{
            //    "particles/units/heroes/hero_wisp/wisp_tether.vpcf", new AbilityFullData
            //    {
            //        AbilityId = AbilityId.wisp_tether,
            //    }
            //},
            //{
            //    "particles/econ/items/wisp/wisp_tether_ti7.vpcf", new AbilityFullData
            //    {
            //        AbilityId = AbilityId.wisp_tether,
            //    }
            //},
            //{
            //    "particles/units/heroes/hero_wisp/wisp_guardian_explosion_small.vpcf", new AbilityFullData
            //    {
            //        AbilityId = AbilityId.wisp_spirits,
            //        SearchOwner = true,
            //        ParticleReleaseData = true,
            //        Replace = true
            //    }
            //},
            //{
            //    "particles/econ/items/wisp/wisp_guardian_explosion_small_ti7.vpcf", new AbilityFullData
            //    {
            //        AbilityId = AbilityId.wisp_spirits,
            //        SearchOwner = true,
            //        ParticleReleaseData = true,
            //        Replace = true
            //    }
            //},
            //{
            //    "particles/units/heroes/hero_wisp/wisp_overcharge.vpcf", new AbilityFullData
            //    {
            //        AbilityId = AbilityId.wisp_overcharge,
            //    }
            //},
            //{
            //    "particles/econ/items/wisp/wisp_overcharge_ti7.vpcf", new AbilityFullData
            //    {
            //        AbilityId = AbilityId.wisp_overcharge,
            //    }
            //},
            {
                "particles/units/heroes/hero_wisp/wisp_relocate_marker_endpoint.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.wisp_relocate,
                }
            },
            {
                "particles/econ/items/wisp/wisp_relocate_marker_ti7_endpoint.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.wisp_relocate,
                }
            },
            {
                "particles/units/heroes/hero_wisp/wisp_ambient_entity_tentacles.vpcf", new WispUnitData
                {
                    Replace = true,
                }
            },
            {
                "particles/units/heroes/hero_jakiro/jakiro_dual_breath_ice.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.jakiro_dual_breath,
                }
            },
            {
                "particles/econ/items/jakiro/jakiro_ti8_immortal_head/jakiro_ti8_dual_breath_ice.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.jakiro_dual_breath,
                }
            },
            {
                "particles/units/heroes/hero_jakiro/jakiro_ice_path.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.jakiro_ice_path,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/jakiro/jakiro_ti7_immortal_head/jakiro_ti7_immortal_head_ice_path.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.jakiro_ice_path,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_jakiro/jakiro_liquid_fire_explosion.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.jakiro_liquid_fire,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_jakiro/jakiro_liquid_fire_ready.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.jakiro_liquid_fire,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_juggernaut/juggernaut_blade_fury_tgt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.juggernaut_blade_fury,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/juggernaut/jugg_ti8_sword/juggernaut_crimson_blade_fury_abyssal_tgt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.juggernaut_blade_fury,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/juggernaut/jugg_ti8_sword/juggernaut_blade_fury_abyssal_tgt_golden.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.juggernaut_blade_fury,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_juggernaut/juggernaut_crit_tgt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.juggernaut_blade_dance,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/juggernaut/jugg_arcana/juggernaut_arcana_crit_tgt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.juggernaut_blade_dance,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/juggernaut/jugg_arcana/juggernaut_arcana_v2_crit_tgt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.juggernaut_blade_dance,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_keeper_of_the_light/keeper_of_the_light_illuminate_charge.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.keeper_of_the_light_illuminate
                }
            },
            {
                "particles/units/heroes/hero_keeper_of_the_light/keeper_of_the_light_blinding_light_aoe.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.keeper_of_the_light_blinding_light,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_keeper_of_the_light/keeper_chakra_magic.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.keeper_of_the_light_chakra_magic,
                    SearchOwner = true,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_kunkka/kunkka_spell_torrent_splash.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.kunkka_torrent,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/kunkka/kunkka_weapon_whaleblade_retro/kunkka_spell_torrent_retro_splash_whaleblade.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.kunkka_torrent,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/kunkka/divine_anchor/hero_kunkka_dafx_skills/kunkka_spell_torrent_splash_fxset.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.kunkka_torrent,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_kunkka/kunkka_spell_x_spot.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.kunkka_x_marks_the_spot
                }
            },
            {
                "particles/econ/items/kunkka/divine_anchor/hero_kunkka_dafx_skills/kunkka_spell_x_spot_fxset.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.kunkka_x_marks_the_spot
                }
            },
            {
                "particles/units/heroes/hero_kunkka/kunkka_spell_tidebringer.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.kunkka_tidebringer,
                    ParticleReleaseData = true,
                    RawParticlePosition = true
                }
            },
            {
                "particles/units/heroes/hero_legion_commander/legion_commander_odds_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.legion_commander_overwhelming_odds,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_legion_commander/legion_commander_press_hero.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.legion_commander_press_the_attack,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_legion_commander/legion_commander_press.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.legion_commander_press_the_attack,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_legion_commander/legion_commander_courage_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.legion_commander_moment_of_courage,
                    Replace = true,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_leshrac/leshrac_split_earth.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.leshrac_split_earth,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_leshrac/leshrac_diabolic_edict.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.leshrac_diabolic_edict,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/leshrac/leshrac_ti9_immortal_head/leshrac_ti9_immortal_edict.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.leshrac_diabolic_edict,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_leshrac/leshrac_lightning_bolt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.leshrac_lightning_storm,
                    SearchOwner = true,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_leshrac/leshrac_pulse_nova.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.leshrac_pulse_nova,
                    SearchOwner = true,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_lich/lich_frost_nova.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lich_frost_nova,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_lich/lich_ice_age_dmg.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lich_frost_shield,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_life_stealer/life_stealer_open_wounds_impact.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.life_stealer_open_wounds,
                    SearchOwner = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_life_stealer/life_stealer_infest_emerge_bloody.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.life_stealer_infest,
                    SearchOwner = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_lina/lina_spell_dragon_slave_impact.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lina_dragon_slave,
                    SearchOwner = true,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/lina/lina_head_headflame/lina_spell_dragon_slave_impact_headflame.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lina_dragon_slave,
                    SearchOwner = true,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_lina/lina_spell_light_strike_array.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lina_light_strike_array,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/lina/lina_ti7/lina_spell_light_strike_array_ti7_gold.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lina_light_strike_array,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/lina/lina_ti7/lina_spell_light_strike_array_ti7.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lina_light_strike_array,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_lina/lina_spell_laguna_blade.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lina_laguna_blade,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/lina/lina_ti6/lina_ti6_laguna_blade.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lina_laguna_blade,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_lion/lion_spell_impale_staff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lion_impale,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_lion/lion_spell_mana_drain.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lion_mana_drain,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/lion/lion_demon_drain/lion_spell_mana_drain_demon.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lion_mana_drain,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_lion/lion_spell_finger_of_death.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lion_finger_of_death,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_rubick/rubick_finger_of_death.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lion_finger_of_death,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/lion/lion_ti8/lion_spell_finger_ti8.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lion_finger_of_death,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/lion/lion_ti8/lion_spell_finger_death_arcana.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lion_finger_of_death,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_lone_druid/lone_druid_spiritlink_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lone_druid_spirit_link,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_lone_druid/lone_druid_savage_roar.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lone_druid_savage_roar,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_lone_druid/lone_druid_battle_cry_buff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lone_druid_true_form_battle_cry,
                }
            },
            {
                "particles/units/heroes/hero_lone_druid/lone_druid_true_form.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lone_druid_true_form,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_lone_druid/true_form_lone_druid.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lone_druid_true_form,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_lone_druid/lone_druid_bear_entangle.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lone_druid_spirit_bear_entangle,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_luna/luna_lucent_beam_precast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.luna_lucent_beam,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_lycan/lycan_summon_wolves_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lycan_summon_wolves,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_lycan/lycan_howl_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lycan_howl,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_lycan/lycan_shapeshift_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.lycan_shapeshift,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_magnataur/magnataur_shockwave_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.magnataur_shockwave,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/magnataur/shock_of_the_anvil/magnataur_shockanvil_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.magnataur_shockwave,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_magnataur/magnataur_empower_cleave_effect.vpcf", new CleaveAbilityData
                {
                    AbilityId = AbilityId.magnataur_empower,
                    ParticleReleaseData = true,
                    ControlPoint = 2,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_magnataur/magnataur_skewer.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.magnataur_skewer,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_mars/mars_shield_bash.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.mars_gods_rebuke,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_medusa/medusa_mystic_snake_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.medusa_mystic_snake,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_medusa/medusa_mana_shield_end.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.medusa_mana_shield,
                    TimeToShow = 3,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_medusa/medusa_mana_shield_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.medusa_mana_shield,
                    TimeToShow = 3,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_meepo/meepo_earthbind_projectile_fx.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.meepo_earthbind,
                }
            },
            {
                "particles/units/heroes/hero_meepo/meepo_poof_end.vpcf", new PoofAbilityData
                {
                    AbilityId = AbilityId.meepo_poof,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/meepo/meepo_colossal_crystal_chorus/meepo_divining_rod_poof_end.vpcf", new PoofAbilityData
                {
                    AbilityId = AbilityId.meepo_poof,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_mirana/mirana_starfall_attack.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.mirana_starfall,
                    SearchOwner = true,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/mirana/mirana_starstorm_bow/mirana_starstorm_starfall_attack.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.mirana_starfall,
                    SearchOwner = true,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_mirana/mirana_leap_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.mirana_leap,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_mirana/mirana_moonlight_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.mirana_invis,
                    ShowNotification = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_monkey_king/monkey_king_strike_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.monkey_king_boundless_strike,
                }
            },
            {
                "particles/econ/items/monkey_king/ti7_weapon/mk_ti7_golden_immortal_strike_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.monkey_king_boundless_strike,
                }
            },
            {
                "particles/econ/items/monkey_king/ti7_weapon/mk_ti7_immortal_strike_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.monkey_king_boundless_strike,
                }
            },
            {
                "particles/units/heroes/hero_monkey_king/monkey_king_jump_trail.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.monkey_king_tree_dance,
                    Replace = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_monkey_king/monkey_king_spring.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.monkey_king_primal_spring,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/monkey_king/arcana/water/monkey_king_spring_arcana_water.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.monkey_king_primal_spring,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/monkey_king/arcana/fire/monkey_king_spring_arcana_fire.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.monkey_king_primal_spring,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_monkey_king/monkey_king_quad_tap_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.monkey_king_jingu_mastery,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_monkey_king/monkey_king_disguise.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.monkey_king_mischief,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_morphling/morphling_waveform_dmg.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.morphling_waveform,
                    SearchOwner = true,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/morphling/morphling_crown_of_tears/morphling_crown_waveform_dmg.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.morphling_waveform,
                    SearchOwner = true,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_morphling/morphling_adaptive_strike.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.morphling_adaptive_strike_agi,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_morphling/morphling_adaptive_strike_str.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.morphling_adaptive_strike_str,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_siren/naga_siren_riptide.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.naga_siren_rip_tide,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/naga/naga_ti8_immortal_tail/naga_ti8_immortal_riptide.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.naga_siren_rip_tide,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_furion/furion_sprout.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.furion_sprout,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_furion/furion_teleport.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.furion_teleportation,
                    ShowNotification = true
                }
            },
            {
                "particles/econ/items/natures_prophet/natures_prophet_weapon_sufferwood/furion_teleport_sufferwood.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.furion_teleportation,
                    ShowNotification = true
                }
            },
            {
                "particles/units/heroes/hero_furion/furion_teleport_end.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.furion_teleportation,
                    ControlPoint = 1,
                }
            },
            {
                "particles/econ/items/natures_prophet/natures_prophet_weapon_sufferwood/furion_teleport_end_sufferwood.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.furion_teleportation,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_furion/furion_force_of_nature_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.furion_force_of_nature,
                    ControlPoint = 1,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_furion/furion_wrath_of_nature_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.furion_wrath_of_nature,
                    ControlPoint = 1,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/natures_prophet/natures_prophet_ti9_immortal/natures_prophet_ti9_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.furion_wrath_of_nature,
                    ControlPoint = 1,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_necrolyte/necrolyte_sadist.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.necrolyte_sadist,
                }
            },
            {
                "particles/units/heroes/hero_night_stalker/nightstalker_void_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.night_stalker_void,
                    SearchOwner = true,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_night_stalker/nightstalker_crippling_fear_aura.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.night_stalker_crippling_fear,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_night_stalker/nightstalker_dark_buff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.night_stalker_darkness,
                }
            },
            {
                "particles/units/heroes/hero_nyx_assassin/nyx_assassin_impale_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nyx_assassin_impale,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/nyx_assassin/nyx_assassin_ti6/nyx_assassin_impale_hit_ti6.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nyx_assassin_impale,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_nyx_assassin/nyx_assassin_vendetta_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nyx_assassin_vendetta,
                    ShowNotification = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_nyx_assassin/nyx_assassin_vendetta.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nyx_assassin_vendetta,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_nyx_assassin/nyx_assassin_burrow.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nyx_assassin_burrow,
                }
            },
            {
                "particles/units/heroes/hero_nyx_assassin/nyx_assassin_burrow_water.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nyx_assassin_burrow,
                }
            },
            {
                "particles/units/heroes/hero_nyx_assassin/nyx_assassin_burrow_exit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nyx_assassin_unburrow,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_nyx_assassin/nyx_assassin_burrow_exit_water.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nyx_assassin_unburrow,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_ogre_magi/ogre_magi_fireblast_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ogre_magi_fireblast,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/ogre_magi/ogre_magi_arcana/ogre_magi_arcana_fireblast_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ogre_magi_fireblast,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_ogre_magi/ogre_magi_ignite_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ogre_magi_ignite,
                }
            },
            {
                "particles/econ/items/ogre_magi/ogre_magi_arcana/ogre_magi_arcana_ignite_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ogre_magi_ignite,
                }
            },
            {
                "particles/units/heroes/hero_ogre_magi/ogre_magi_bloodlust_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ogre_magi_bloodlust,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_omniknight/omniknight_purification_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.omniknight_purification,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/omniknight/hammer_ti6_immortal/omniknight_purification_immortal_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.omniknight_purification,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_omniknight/omniknight_repel_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.omniknight_repel,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/omniknight/omni_ti8_head/omniknight_repel_cast_ti8.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.omniknight_repel,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_oracle/oracle_fortune_channel.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.oracle_fortunes_end,
                }
            },
            {
                "particles/econ/items/oracle/oracle_fortune_ti7/oracle_fortune_ti7_channel.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.oracle_fortunes_end,
                }
            },
            {
                "particles/units/heroes/hero_oracle/oracle_fatesedict_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.oracle_fates_edict,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_oracle/oracle_fatesedict_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.oracle_fates_edict,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_oracle/oracle_purifyingflames_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.oracle_purifying_flames,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_oracle/oracle_false_promise_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.oracle_false_promise,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_oracle/oracle_false_promise_heal.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.oracle_false_promise,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_obsidian_destroyer/obsidian_destroyer_prison_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.obsidian_destroyer_astral_imprisonment,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_obsidian_destroyer/obsidian_destroyer_matter_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.obsidian_destroyer_equilibrium,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_pangolier/pangolier_swashbuckler.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pangolier_swashbuckle,
                    Replace = true
                }
            },
            {
                "particles/econ/items/pangolier/pangolier_immortal_musket/pangolier_immortal_swashbuckler.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pangolier_swashbuckle,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_pangolier/pangolier_tailthump.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pangolier_shield_crash,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/pangolier/pangolier_ti8_immortal/pangolier_ti8_immortal_shield_crash.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pangolier_shield_crash,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_pangolier/pangolier_gyroshell_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pangolier_gyroshell,
                }
            },
            {
                "particles/units/heroes/hero_pangolier/pangolier_luckyshot_disarm_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pangolier_lucky_shot,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_phantom_assassin/phantom_assassin_phantom_strike_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_assassin_phantom_strike,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_phantom_assassin/phantom_assassin_active_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_assassin_blur,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/phantom_assassin/phantom_assassin_weapon_generic/phantom_assassin_ambient_blade_generic.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_assassin_coup_de_grace,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_phantom_assassin/phantom_assassin_crit_impact.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_assassin_coup_de_grace,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/phantom_assassin/phantom_assassin_arcana_elder_smith/phantom_assassin_crit_arcana_swoop.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_assassin_coup_de_grace,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_phantom_assassin/phantom_assassin_crit_impact_mechanical.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_assassin_coup_de_grace,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/phantom_assassin/phantom_assassin_arcana_elder_smith/phantom_assassin_crit_mechanical_arcana_swoop.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_assassin_coup_de_grace,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_phantom_lancer/phantomlancer_spiritlance_caster.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_lancer_spirit_lance,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/phantom_lancer/phantom_lancer_immortal_ti6/phantom_lancer_immortal_ti6_spiritlance_cast.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_lancer_spirit_lance,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_phantom_lancer/phantom_lancer_doppleganger_aoe.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phantom_lancer_doppelwalk,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_phoenix/phoenix_icarus_dive.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phoenix_icarus_dive,
                }
            },
            {
                "particles/units/heroes/hero_phoenix/phoenix_fire_spirit_launch.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phoenix_launch_fire_spirit,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_phoenix/phoenix_sunray.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phoenix_sun_ray
                }
            },
            {
                "particles/econ/items/phoenix/phoenix_solar_forge/phoenix_sunray_solar_forge.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phoenix_sun_ray
                }
            },
            {
                "particles/units/heroes/hero_phoenix/phoenix_sunray_beam_friend.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phoenix_sun_ray,
                    Replace = true,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/phoenix/phoenix_solar_forge/phoenix_sunray_beam_friend_solar_forge.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phoenix_sun_ray,
                    Replace = true,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_phoenix/phoenix_supernova_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.phoenix_supernova,
                    ParticleReleaseData = true,
                    TimeToShow = 6,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_pudge/pudge_meathook.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pudge_meat_hook,
                }
            },
            {
                "particles/econ/items/pudge/pudge_trapper_beam_chain/pudge_nx_meathook.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pudge_meat_hook,
                }
            },
            {
                "particles/econ/items/pudge/pudge_hook_whale/pudge_meathook_whale.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pudge_meat_hook,
                }
            },
            {
                "particles/econ/items/pudge/pudge_ti6_immortal/pudge_ti6_meathook.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pudge_meat_hook,
                }
            },
            {
                "particles/econ/items/pudge/pudge_ti6_immortal_gold/pudge_ti6_meathook_gold.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pudge_meat_hook,
                }
            },
            {
                "particles/econ/items/pudge/pudge_ti6_immortal/pudge_ti6_witness_meathook.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pudge_meat_hook,
                }
            },
            {
                "particles/econ/items/pudge/pudge_scorching_talon/pudge_scorching_talon_meathook.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pudge_meat_hook,
                }
            },
            {
                "particles/econ/items/pudge/pudge_dragonclaw/pudge_meathook_dragonclaw.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pudge_meat_hook,
                }
            },
            {
                "particles/econ/items/pudge/pudge_harvester/pudge_meathook_harvester.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pudge_meat_hook,
                }
            },
            {
                "particles/units/heroes/hero_pugna/pugna_netherblast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pugna_nether_blast,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/pugna/pugna_ti9_immortal/pugna_ti9_immortal_netherblast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pugna_nether_blast,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_pugna/pugna_life_drain.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pugna_life_drain,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_pugna/pugna_life_give.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.pugna_life_drain,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_queenofpain/queen_shadow_strike_body.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.queenofpain_shadow_strike,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/queen_of_pain/qop_ti8_immortal/queen_ti8_shadow_strike_body.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.queenofpain_shadow_strike,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_queenofpain/queen_blink_start.vpcf", new BlinkAbilityData
                {
                    AbilityId = AbilityId.queenofpain_blink,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_razor/razor_storm_lightning_strike.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.razor_eye_of_the_storm,
                    Replace = true,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_riki/riki_smokebomb.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.riki_smoke_screen,
                    SearchOwner = true,
                }
            },
            {
                "particles/units/heroes/hero_riki/riki_backstab.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.riki_permanent_invisibility,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_rubick/rubick_telekinesis.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.rubick_telekinesis,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_rubick/rubick_fade_bolt_head.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.rubick_fade_bolt,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/rubick/rubick_ti8_immortal/rubick_ti8_immortal_fade_bolt_head.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.rubick_fade_bolt,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_sandking/sandking_burrowstrike.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sandking_burrowstrike,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/sand_king/sandking_barren_crown/sandking_rubyspire_burrowstrike.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sandking_burrowstrike,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_sandking/sandking_sandstorm.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sandking_sand_storm
                }
            },
            {
                "particles/units/heroes/hero_sandking/sandking_caustic_finale_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sandking_caustic_finale,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/econ/items/sand_king/sandking_ti7_arms/sandking_ti7_caustic_finale_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sandking_caustic_finale,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_sandking/sandking_caustic_finale_explode.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sandking_caustic_finale,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/econ/items/sand_king/sandking_ti7_arms/sandking_ti7_caustic_finale_explode.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sandking_caustic_finale,
                    ParticleReleaseData = true,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_sandking/sandking_epicenter_tell.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sandking_epicenter,
                    ShowNotification = true
                }
            },
            {
                "particles/units/heroes/hero_shadow_demon/shadow_demon_disruption.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shadow_demon_disruption
                }
            },
            {
                "particles/units/heroes/hero_shadow_demon/shadow_demon_soul_catcher_v2_projected_ground.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shadow_demon_soul_catcher,
                    ControlPoint = 2
                }
            },
            {
                "particles/units/heroes/hero_shadow_demon/shadow_demon_shadow_poison_release.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shadow_demon_shadow_poison_release,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/shadow_demon/sd_ti7_shadow_poison/sd_ti7_shadow_poison_release.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shadow_demon_shadow_poison_release,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_nevermore/nevermore_shadowraze_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nevermore_shadowraze3,
                    SearchOwner = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_nevermore/nevermore_necro_souls.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nevermore_necromastery,
                    ControlPoint = 1,
                    Replace = true
                }
            },
            {
                "particles/econ/items/shadow_fiend/sf_fire_arcana/sf_fire_arcana_necro_souls.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nevermore_necromastery,
                    ControlPoint = 1,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_nevermore/nevermore_wings.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nevermore_requiem,
                    RawParticlePosition = true
                }
            },
            {
                "particles/econ/items/shadow_fiend/sf_fire_arcana/sf_fire_arcana_wings.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.nevermore_requiem,
                    RawParticlePosition = true
                }
            },
            {
                "particles/units/heroes/hero_shadowshaman/shadowshaman_ether_shock.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shadow_shaman_ether_shock,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/shadow_shaman/shadow_shaman_ti8/shadow_shaman_ti8_ether_shock.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shadow_shaman_ether_shock,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/shadow_shaman/shadow_shaman_ti8/shadow_shaman_crimson_ti8_ether_shock.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shadow_shaman_ether_shock,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_shadowshaman/shadowshaman_shackle.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shadow_shaman_shackles,
                }
            },
            {
                "particles/units/heroes/hero_silencer/silencer_curse_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.silencer_curse_of_the_silent,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_silencer/silencer_last_word_status_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.silencer_last_word,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_silencer/silencer_global_silence.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.silencer_global_silence,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_skywrath_mage/skywrath_mage_concussive_shot_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.skywrath_mage_concussive_shot,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_skywrath_mage/skywrath_mage_ancient_seal_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.skywrath_mage_ancient_seal,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_slark/slark_dark_pact_pulses_body.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.slark_dark_pact,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/slark/slark_head_immortal/slark_immortal_dark_pact_pulses_body.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.slark_dark_pact,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_slark/slark_pounce_trail.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.slark_pounce,
                }
            },
            {
                "particles/econ/items/slark/slark_ti6_blade/slark_ti6_pounce_trail.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.slark_pounce,
                }
            },
            {
                "particles/econ/items/slark/slark_ti6_blade/slark_ti6_pounce_trail_gold.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.slark_pounce,
                }
            },
            {
                "particles/units/heroes/hero_snapfire/hero_snapfire_shotgun_impact.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.snapfire_scatterblast,
                    SearchOwner = true,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_snapfire/hero_snapfire_cookie_receive.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.snapfire_firesnap_cookie,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_sniper/sniper_headshot_slow.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.snapfire_lil_shredder,
                    SearchOwner = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_snapfire/snapfire_lizard_blobs_arced.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.snapfire_mortimer_kisses,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_sniper/sniper_shrapnel_launch.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sniper_shrapnel,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_spectre/spectre_desolate.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.spectre_desolate,
                    ParticleReleaseData = true,
                    Replace = true,
                }
            },
            {
                "particles/units/heroes/hero_spirit_breaker/spirit_breaker_greater_bash.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.spirit_breaker_greater_bash,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/spirit_breaker/spirit_breaker_weapon_ti8/spirit_breaker_bash_ti8.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.spirit_breaker_greater_bash,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_stormspirit/stormspirit_overload_discharge.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.storm_spirit_overload,
                    ParticleReleaseData = true,
                    SearchOwner = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/storm_spirit/strom_spirit_ti8/storm_sprit_ti8_overload_discharge.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.storm_spirit_overload,
                    ParticleReleaseData = true,
                    SearchOwner = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_stormspirit/stormspirit_ball_lightning.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.storm_spirit_ball_lightning,
                    Replace = true
                }
            },
            {
                "particles/econ/items/storm_spirit/storm_spirit_orchid_hat/stormspirit_orchid_ball_lightning.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.storm_spirit_ball_lightning,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_sven/sven_spell_storm_bolt_lightning.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sven_storm_bolt,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_sven/sven_spell_great_cleave.vpcf", new CleaveAbilityData
                {
                    AbilityId = AbilityId.sven_great_cleave,
                    ParticleReleaseData = true,
                    ControlPoint = 2,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_sven/sven_spell_great_cleave_crit.vpcf", new CleaveAbilityData
                {
                    AbilityId = AbilityId.sven_great_cleave,
                    ParticleReleaseData = true,
                    ControlPoint = 2,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_sven/sven_spell_great_cleave_gods_strength.vpcf", new CleaveAbilityData
                {
                    AbilityId = AbilityId.sven_great_cleave,
                    ParticleReleaseData = true,
                    ControlPoint = 1,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_sven/sven_spell_great_cleave_gods_strength_crit.vpcf", new CleaveAbilityData
                {
                    AbilityId = AbilityId.sven_great_cleave,
                    ParticleReleaseData = true,
                    ControlPoint = 1,
                    Replace = true
                }
            },
            //{
            //    "particles/econ/items/sven/sven_ti7_sword/sven_ti7_sword_spell_great_cleave.vpcf", new CleaveAbilityData
            //    {
            //        AbilityId = AbilityId.sven_great_cleave,
            //        ParticleReleaseData = true,
            //        ControlPoint = 1,
            //        Replace = true
            //    }
            //},
            //{
            //    "particles/econ/items/sven/sven_ti7_sword/sven_ti7_sword_spell_great_cleave_gods_strength.vpcf", new CleaveAbilityData
            //    {
            //        AbilityId = AbilityId.sven_great_cleave,
            //        ParticleReleaseData = true,
            //        ControlPoint = 1,
            //        Replace = true
            //    }
            //},
            //{
            //    "particles/econ/items/sven/sven_ti7_sword/sven_ti7_sword_spell_great_cleave_gods_strength_crit.vpcf", new CleaveAbilityData
            //    {
            //        AbilityId = AbilityId.sven_great_cleave,
            //        ParticleReleaseData = true,
            //        ControlPoint = 1,
            //        Replace = true
            //    }
            //},
            //{
            //    "particles/econ/items/sven/sven_ti7_sword/sven_ti7_sword_spell_great_cleave_crit.vpcf", new CleaveAbilityData
            //    {
            //        AbilityId = AbilityId.sven_great_cleave,
            //        ParticleReleaseData = true,
            //        ControlPoint = 1,
            //        Replace = true
            //    }
            //},
            {
                "particles/units/heroes/hero_sven/sven_spell_warcry.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sven_warcry,
                    ParticleReleaseData = true,
                    ControlPoint = 2
                }
            },
            {
                "particles/econ/items/sven/sven_warcry_ti5/sven_spell_warcry_ti_5.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sven_warcry,
                    ParticleReleaseData = true,
                    ControlPoint = 2
                }
            },
            {
                "particles/units/heroes/hero_sven/sven_warcry_buff_shield_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sven_warcry,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_sven/sven_spell_gods_strength.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.sven_gods_strength,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_techies/techies_blast_off.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.techies_suicide,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_techies/techies_remote_mine_plant.vpcf", new RemoteMinesAbilityData
                {
                    AbilityId = AbilityId.techies_remote_mines,
                    ControlPoint = 1,
                    Duration = (int)new SpecialData(AbilityId.techies_remote_mines, "duration").GetValue(1),
                    ShowRange = true,
                    Range = (int)new SpecialData(AbilityId.techies_remote_mines, "radius").GetValue(1) + 50,
                    RangeColor = new Vector3(0, 255, 0),
                }
            },
            {
                "particles/units/heroes/hero_techies/techies_remote_mines_detonate.vpcf", new RemoteMinesAbilityData
                {
                    AbilityId = AbilityId.techies_remote_mines,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/techies/techies_arcana/techies_remote_mine_plant_arcana.vpcf", new RemoteMinesAbilityData
                {
                    AbilityId = AbilityId.techies_remote_mines,
                    ControlPoint = 1,
                    Duration = (int)new SpecialData(AbilityId.techies_remote_mines, "duration").GetValue(1)
                }
            },
            {
                "particles/econ/items/techies/techies_arcana/techies_remote_mines_detonate_arcana.vpcf", new RemoteMinesAbilityData
                {
                    AbilityId = AbilityId.techies_remote_mines,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_templar_assassin/templar_assassin_refraction.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.templar_assassin_refraction,
                }
            },
            {
                "particles/econ/items/lanaya/ta_ti9_immortal_shoulders/ta_ti9_refraction.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.templar_assassin_refraction,
                }
            },
            {
                "particles/units/heroes/hero_templar_assassin/templar_assassin_refract_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.templar_assassin_refraction,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/lanaya/ta_ti9_immortal_shoulders/ta_ti9_refract_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.templar_assassin_refraction,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_templar_assassin/templar_assassin_psi_blade.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.templar_assassin_psi_blades,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/econ/items/templar_assassin/templar_assassin_focal/ta_focal_psi_blade.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.templar_assassin_psi_blades,
                    Replace = true,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_terrorblade/terrorblade_reflection_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.terrorblade_reflection,
                }
            },
            {
                "particles/units/heroes/hero_terrorblade/terrorblade_sunder.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.terrorblade_sunder,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_tidehunter/tidehunter_anchor_hero.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tidehunter_anchor_smash,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_tidehunter/tidehunter_spell_ravage.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tidehunter_ravage,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_shredder/shredder_whirling_death.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_whirling_death,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_shredder/shredder_timber_chain_tree.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_timber_chain,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_shredder/shredder_reactive_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_reactive_armor,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_shredder/shredder_chakram_stay.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_chakram,
                    Replace = true
                }
            },
            {
                "particles/econ/items/shredder/hero_shredder_icefx/shredder_chakram_stay_ice.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_chakram,
                    Replace = true
                }
            },
            {
                "particles/econ/items/timbersaw/timbersaw_ti9/timbersaw_ti9_chakram_stay.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_chakram,
                    Replace = true
                }
            },
            {
                "particles/econ/items/timbersaw/timbersaw_ti9_gold/timbersaw_ti9_chakram_gold_stay.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_chakram,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_shredder/shredder_chakram_return.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_return_chakram,
                    ControlPoint = 1,
                    Replace = true
                }
            },
            {
                "particles/econ/items/shredder/hero_shredder_icefx/shredder_chakram_return_ice.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_return_chakram,
                    ControlPoint = 1,
                    Replace = true
                }
            },
            {
                "particles/econ/items/timbersaw/timbersaw_ti9/timbersaw_ti9_chakram_return.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_return_chakram,
                    RawParticlePosition = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/timbersaw/timbersaw_ti9_gold/timbersaw_ti9_chakram_gold_return.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.shredder_return_chakram,
                    RawParticlePosition = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_tinker/tinker_missile_dud.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tinker_heat_seeking_missile,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_tinker/tinker_rearm.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tinker_rearm,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_tiny/tiny_avalanche_lvl1.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tiny_avalanche,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_tiny/tiny_avalanche_lvl2.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tiny_avalanche,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_tiny/tiny_avalanche_lvl3.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tiny_avalanche,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_tiny/tiny_avalanche_lvl4.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tiny_avalanche,
                    Replace = true
                }
            },
            {
                "particles/econ/items/tiny/tiny_prestige/tiny_prestige_avalanche.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tiny_avalanche,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_tiny/tiny_craggy_cleave.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tiny_toss_tree,
                    ParticleReleaseData = true,
                    SearchOwner = true,
                    Replace = true
                }
            },
            {
                "particles/econ/items/tiny/tiny_prestige/tiny_prestige_tree_melee_hit.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tiny_toss_tree,
                    ParticleReleaseData = true,
                    SearchOwner = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_treant/treant_leech_seed.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.treant_leech_seed,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_treant/treant_livingarmor.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.treant_living_armor,
                }
            },
            {
                "particles/econ/items/treant_protector/ti7_shoulder/treant_ti7_livingarmor.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.treant_living_armor,
                }
            },
            {
                "particles/units/heroes/hero_treant/treant_eyesintheforest.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.treant_eyes_in_the_forest,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_treant/treant_overgrowth_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.treant_overgrowth,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_troll_warlord/troll_warlord_whirling_axe_melee.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.troll_warlord_whirling_axes_melee,
                    SearchOwner = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_tusk/tusk_ice_shards.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tusk_ice_shards,
                    ControlPoint = 1,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_tusk/tusk_snowball.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tusk_snowball,
                }
            },
            {
                "particles/units/heroes/hero_tusk/tusk_tag_team.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tusk_tag_team,
                    ControlPoint = 2
                }
            },
            {
                "particles/units/heroes/hero_tusk/tusk_walruspunch_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tusk_walrus_punch,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_tusk/tusk_walruskick_tgt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tusk_walrus_kick,
                }
            },
            {
                "particles/econ/items/tuskarr/tusk_ti9_immortal/tusk_ti9_walruspunch_tgt.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.tusk_walrus_kick,
                }
            },
            {
                "particles/units/heroes/heroes_underlord/abyssal_underlord_firestorm_wave.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.abyssal_underlord_firestorm,
                    Replace = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/heroes_underlord/underlord_pit_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.abyssal_underlord_pit_of_malice,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/underlord/underlord_ti8_immortal_weapon/underlord_crimson_ti8_immortal_pitofmalice_cast.vpcf",
                new AbilityFullData
                {
                    AbilityId = AbilityId.abyssal_underlord_pit_of_malice,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/heroes_underlord/abyssal_underlord_darkrift_target.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.abyssal_underlord_dark_rift,
                    SearchOwner = true,
                    TimeToShow = 5,
                    ShowNotification = true
                }
            },
            {
                "particles/units/heroes/hero_undying/undying_decay.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.undying_decay,
                    ParticleReleaseData = true,
                    ControlPoint = 2
                }
            },
            {
                "particles/econ/items/undying/undying_pale_augur/undying_pale_augur_decay.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.undying_decay,
                    ParticleReleaseData = true,
                    ControlPoint = 2
                }
            },
            {
                "particles/units/heroes/hero_undying/undying_soul_rip_damage.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.undying_soul_rip,
                    ControlPoint = 2,
                    ParticleReleaseData = true,
                    Replace = true,
                }
            },
            {
                "particles/units/heroes/hero_undying/undying_soul_rip_heal.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.undying_soul_rip,
                    ParticleReleaseData = true,
                    Replace = true,
                }
            },
            {
                "particles/units/heroes/hero_ursa/ursa_overpower_buff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ursa_overpower,
                }
            },
            {
                "particles/units/heroes/hero_ursa/ursa_enrage_buff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.ursa_enrage,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_venomancer/venomancer_venomous_gale_mouth.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.venomancer_venomous_gale,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/venomancer/veno_ti8_immortal_head/veno_ti8_immortal_gale_mouth.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.venomancer_venomous_gale,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_venomancer/venomancer_poison_nova.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.venomancer_poison_nova,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_viper/viper_poison_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.viper_poison_attack,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_viper/viper_nethertoxin_proj.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.viper_nethertoxin,
                }
            },
            {
                "particles/econ/items/viper/viper_immortal_tail_ti8/viper_immortal_ti8_nethertoxin_proj.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.viper_nethertoxin,
                }
            },
            {
                "particles/units/heroes/hero_viper/viper_viper_strike_warmup.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.viper_viper_strike,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_visage/visage_grave_chill_caster.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.visage_grave_chill,
                }
            },
            {
                "particles/units/heroes/hero_visage/visage_soul_assumption_beams.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.visage_soul_assumption,
                    ParticleReleaseData = true,
                    RawParticlePosition = true
                }
            },
            {
                "particles/units/heroes/hero_visage/visage_summon_familiars.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.visage_summon_familiars,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_void_spirit/aether_remnant/void_spirit_aether_remnant_watch.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.void_spirit_aether_remnant
                }
            },
            {
                "particles/units/heroes/hero_void_spirit/dissimilate/void_spirit_dissimilate_dmg.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.void_spirit_dissimilate,
                    ParticleReleaseData = true
                }
            },
            {
                "particles/units/heroes/hero_void_spirit/pulse/void_spirit_pulse.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.void_spirit_resonant_pulse
                }
            },
            {
                "particles/units/heroes/hero_void_spirit/astral_step/void_spirit_astral_step.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.void_spirit_astral_step,
                    ControlPoint = 1,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_warlock/warlock_fatal_bonds_hit_parent.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.warlock_fatal_bonds,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_warlock/warlock_shadow_word_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.warlock_shadow_word,
                    SearchOwner = true
                }
            },
            {
                "particles/econ/items/warlock/warlock_ti9/warlock_ti9_shadow_word_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.warlock_shadow_word,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_warlock/warlock_upheaval.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.warlock_upheaval,
                    RawParticlePosition = true
                }
            },
            {
                "particles/econ/items/warlock/warlock_staff_hellborn/warlock_upheaval_hellborn.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.warlock_upheaval,
                    RawParticlePosition = true
                }
            },
            {
                "particles/units/heroes/hero_warlock/warlock_rain_of_chaos_staff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.warlock_rain_of_chaos,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_weaver/weaver_swarm_debuff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.weaver_the_swarm,
                    Replace = true
                }
            },
            {
                "particles/units/heroes/hero_weaver/weaver_shukuchi.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.weaver_shukuchi,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/weaver/weaver_immortal_ti6/weaver_immortal_ti6_shukuchi.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.weaver_shukuchi,
                    ControlPoint = 1,
                }
            },
            {
                "particles/econ/items/weaver/weaver_immortal_ti6/weaver_immortal_ti6_shukuchi_portal.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.weaver_shukuchi,
                }
            },
            {
                "particles/units/heroes/hero_windrunner/windrunner_shackleshot_single.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.windrunner_shackleshot,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_windrunner/windrunner_shackleshot_pair.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.windrunner_shackleshot,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_windrunner/windrunner_shackleshot_pair_tree.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.windrunner_shackleshot,
                    SearchOwner = true
                }
            },
            {
                "particles/units/heroes/hero_winter_wyvern/wyvern_arctic_burn_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.winter_wyvern_arctic_burn,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_winter_wyvern/wyvern_cold_embrace_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.winter_wyvern_cold_embrace,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_winter_wyvern/wyvern_cold_embrace_buff.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.winter_wyvern_cold_embrace,
                }
            },
            {
                "particles/units/heroes/hero_witchdoctor/witchdoctor_maledict_aoe.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.witch_doctor_maledict,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/witch_doctor/wd_ti8_immortal_head/wd_ti8_immortal_maledict_aoe.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.witch_doctor_maledict,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_skeletonking/skeletonking_hellfireblast_warmup.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.skeleton_king_hellfire_blast,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_skeletonking/wraith_king_vampiric_aura_lifesteal.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.skeleton_king_vampiric_aura,
                    ParticleReleaseData = true,
                    Replace = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/units/heroes/hero_skeletonking/wraith_king_reincarnate.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.skeleton_king_reincarnation,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_zuus/zuus_arc_lightning_head.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.zuus_arc_lightning,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/zeus/zeus_ti8_immortal_arms/zeus_ti8_immortal_arc_head.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.zuus_arc_lightning,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_zuus/zuus_lightning_bolt_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.zuus_lightning_bolt,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/units/heroes/hero_zuus/zuus_thundergods_wrath_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.zuus_thundergods_wrath,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/zeus/arcana_chariot/zeus_arcana_thundergods_wrath_start.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.zuus_thundergods_wrath,
                    ControlPoint = 1
                }
            },
            {
                "particles/econ/items/zeus/arcana_chariot/zeus_arcana_thundergods_wrath.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.zuus_thundergods_wrath,
                    ControlPoint = 1
                }
            },

            // items

            {
                "particles/items2_fx/smoke_of_deceit.vpcf", new SmokeAbilityData
                {
                    AbilityId = AbilityId.item_smoke_of_deceit,
                    ParticleReleaseData = true,
                    ShowNotification = true
                }
            },
            {
                "particles/items_fx/blink_dagger_start.vpcf", new BlinkItemAbilityData
                {
                    AbilityId = AbilityId.item_blink,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/zeus/arcana_chariot/zeus_arcana_blink_start.vpcf", new BlinkItemAbilityData
                {
                    AbilityId = AbilityId.item_blink,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/earthshaker/earthshaker_arcana/earthshaker_arcana_blink_start.vpcf", new BlinkItemAbilityData
                {
                    AbilityId = AbilityId.item_blink,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/items/earthshaker/earthshaker_arcana/earthshaker_arcana_blink_start_v2.vpcf", new BlinkItemAbilityData
                {
                    AbilityId = AbilityId.item_blink,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/items_fx/battlefury_cleave.vpcf", new CleaveAbilityData
                {
                    AbilityId = AbilityId.item_bfury,
                    ControlPoint = 2,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },
            {
                "particles/items2_fx/refresher.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.item_refresher,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/items2_fx/vanguard_active_launch.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.item_crimson_guard,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/items2_fx/vanguard_active_impact.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.item_crimson_guard,
                    ParticleReleaseData = true,
                    ControlPoint = 1,
                    Replace = true
                }
            },
            {
                "particles/items2_fx/pipe_of_insight.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.item_hood_of_defiance,
                    ControlPoint = 1
                }
            },
            {
                "particles/items2_fx/pipe_of_insight_launch.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.item_pipe,
                    ParticleReleaseData = true,
                    ControlPoint = 1
                }
            },
            {
                "particles/items_fx/dagon.vpcf", new NoOwnerItemData
                {
                    AbilityId = AbilityId.item_dagon,
                    ParticleReleaseData = true,
                    RangeCheck = 1000,
                }
            },
            {
                "particles/items3_fx/glimmer_cape_initial_flash.vpcf", new NoOwnerItemData
                {
                    AbilityId = AbilityId.item_glimmer_cape,
                    ParticleReleaseData = true,
                    RangeCheck = 1000,
                }
            },
            {
                "particles/items2_fx/veil_of_discord.vpcf", new NoOwnerItemData
                {
                    AbilityId = AbilityId.item_veil_of_discord,
                    ParticleReleaseData = true,
                    RangeCheck = 1500,
                }
            },
            {
                "particles/items2_fx/hand_of_midas.vpcf", new NoOwnerItemData
                {
                    AbilityId = AbilityId.item_hand_of_midas,
                    ParticleReleaseData = true,
                    ControlPoint = 1,
                    RangeCheck = 800,
                }
            },
            {
                "particles/items2_fx/shivas_guard_impact.vpcf", new NoOwnerItemData
                {
                    AbilityId = AbilityId.item_shivas_guard,
                    SearchOwner = true,
                    ParticleReleaseData = true,
                    Replace = true,
                    RangeCheck = 1000,
                }
            },
            {
                "particles/items_fx/bloodstone_heal.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.item_bloodstone,
                    ControlPoint = 1
                }
            },
            {
                "particles/items4_fx/meteor_hammer_cast.vpcf", new AbilityFullData
                {
                    AbilityId = AbilityId.item_meteor_hammer,
                    ControlPoint = 1
                }
            },
            {
                "particles/items_fx/chain_lightning.vpcf", new MaelstormAbilityData
                {
                    AbilityId = AbilityId.item_maelstrom,
                    ParticleReleaseData = true,
                    Replace = true
                }
            },

            // TI9 Items

            {
                "particles/econ/events/ti9/blink_dagger_ti9_start.vpcf", new BlinkItemAbilityData
                {
                    AbilityId = AbilityId.item_blink,
                    ParticleReleaseData = true,
                }
            },
            {
                "particles/econ/events/ti9/blink_dagger_ti9_start_lvl2.vpcf", new BlinkItemAbilityData
                {
                    AbilityId = AbilityId.item_blink,
                    ParticleReleaseData = true,
                }
            },
        };

        public Dictionary<AbilityId, AbilityFullData> AbilityUnitVision { get; } = new Dictionary<AbilityId, AbilityFullData>
        {
            {
                AbilityId.shredder_timber_chain, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.shredder_timber_chain],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.rattletrap_hookshot, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.rattletrap_hookshot],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.weaver_the_swarm, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.weaver_the_swarm],
                    IgnoreUnitAbility = true,
                }
            },
            {
                AbilityId.grimstroke_dark_artistry, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.grimstroke_dark_artistry],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.tusk_ice_shards, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.tusk_ice_shards]
                }
            },
            {
                AbilityId.tiny_toss_tree, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.tiny_toss_tree]
                }
            },
            {
                AbilityId.batrider_flamebreak, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.batrider_flamebreak],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.medusa_mystic_snake, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.medusa_mystic_snake],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.oracle_fortunes_end, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.oracle_fortunes_end],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.meepo_earthbind, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.meepo_earthbind],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.kunkka_ghostship, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.kunkka_ghostship],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.venomancer_venomous_gale, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.venomancer_venomous_gale],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.shadow_demon_shadow_poison, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.shadow_demon_shadow_poison],
                }
            },
            {
                AbilityId.disruptor_glimpse, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.disruptor_glimpse],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.phantom_assassin_stifling_dagger, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.phantom_assassin_stifling_dagger]
                }
            },
            {
                AbilityId.ancient_apparition_ice_blast, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.ancient_apparition_ice_blast]
                }
            },
            {
                AbilityId.mirana_arrow, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.mirana_arrow],
                    ShowNotification = true
                }
            },
            {
                AbilityId.arc_warden_spark_wraith, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.arc_warden_spark_wraith]
                }
            },
            {
                AbilityId.invoker_tornado, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.invoker_tornado]
                }
            },
            {
                AbilityId.puck_illusory_orb, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.puck_illusory_orb]
                }
            },
            {
                AbilityId.skywrath_mage_arcane_bolt, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.skywrath_mage_arcane_bolt]
                }
            },
            {
                AbilityId.windrunner_powershot, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.windrunner_powershot]
                }
            },
            {
                AbilityId.vengefulspirit_wave_of_terror, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.vengefulspirit_wave_of_terror]
                }
            },
            {
                AbilityId.mars_spear, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.mars_spear]
                }
            },
            {
                AbilityId.skywrath_mage_concussive_shot, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.skywrath_mage_concussive_shot],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.lich_chain_frost, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.lich_chain_frost],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.sven_storm_bolt, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.sven_storm_bolt],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.storm_spirit_ball_lightning, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.storm_spirit_ball_lightning],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.invoker_chaos_meteor, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.invoker_chaos_meteor],
                    IgnoreUnitAbility = true
                }
            },
            {
                AbilityId.rattletrap_rocket_flare, new AbilityFullData
                {
                    Vision = GameData.AbilityVision[AbilityId.rattletrap_rocket_flare],
                    IgnoreUnitAbility = true
                }
            },
        };
    }
}