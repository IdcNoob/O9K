namespace O9K.Evader.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;

    internal static partial class Abilities
    {
        public static IEnumerable<AbilityId> AllyPurge { get; } = new[]
        {
            AbilityId.item_lotus_orb,
            AbilityId.brewmaster_storm_dispel_magic, //todo check target
            AbilityId.satyr_trickster_purge,
            AbilityId.omniknight_repel,
            AbilityId.oracle_fortunes_end,
            AbilityId.slark_dark_pact,
            AbilityId.abaddon_aphotic_shield,
            //AbilityId.legion_commander_press_the_attack,
            //AbilityId.item_guardian_greaves,
            AbilityId.item_cyclone,
        };

        public static IEnumerable<AbilityId> AllyStrongPurge { get; } = new[]
        {
            AbilityId.abaddon_aphotic_shield,
            AbilityId.omniknight_repel,
            AbilityId.legion_commander_press_the_attack,
            //    AbilityId.abaddon_borrowed_time,
            AbilityId.ursa_enrage,
            AbilityId.grimstroke_spirit_walk,
            //    AbilityId.oracle_false_promise, //todo add as purge with some logic?
        };

        public static IEnumerable<AbilityId> Blink { get; } = new[]
        {
            AbilityId.ember_spirit_activate_fire_remnant,
            AbilityId.item_blink,
            AbilityId.void_spirit_astral_step,
            AbilityId.sandking_burrowstrike,
            AbilityId.wisp_tether,
            AbilityId.antimage_blink,
            AbilityId.queenofpain_blink,
            AbilityId.faceless_void_time_walk,
            AbilityId.monkey_king_tree_dance,
            AbilityId.pangolier_swashbuckle,
            AbilityId.morphling_waveform,
            AbilityId.magnataur_skewer,
            AbilityId.item_flicker,
            AbilityId.riki_blink_strike,
            AbilityId.phantom_assassin_phantom_strike,
            AbilityId.mirana_leap,
            AbilityId.slark_pounce,
            AbilityId.item_force_staff,
            AbilityId.item_hurricane_pike,
            AbilityId.item_force_boots,
        };

        public static IEnumerable<AbilityId> Break { get; } = new[] { AbilityId.viper_nethertoxin, };

        public static IEnumerable<AbilityId> ChannelDisable { get; } = new[]
        {
            AbilityId.zuus_lightning_bolt,
            AbilityId.disruptor_glimpse,
            AbilityId.luna_lucent_beam,
            AbilityId.void_spirit_resonant_pulse,
            //  AbilityId.viper_nethertoxin, //todo add talent silence

            //todo add more mini stuns ?

            AbilityId.puck_dream_coil,
            AbilityId.silencer_global_silence,
            AbilityId.zuus_cloud,
        };

        public static IEnumerable<AbilityId> ClarityNukes { get; } = new[]
        {
            AbilityId.ancient_apparition_chilling_touch,
            AbilityId.arc_warden_flux,
            AbilityId.axe_battle_hunger,
            AbilityId.bane_brain_sap,
            AbilityId.beastmaster_wild_axes,
            AbilityId.bounty_hunter_shuriken_toss,
            AbilityId.rattletrap_rocket_flare,
            AbilityId.crystal_maiden_crystal_nova,
            AbilityId.dark_seer_vacuum,
            AbilityId.dazzle_poison_touch,
            AbilityId.death_prophet_carrion_swarm,
            AbilityId.disruptor_thunder_strike,
            AbilityId.dragon_knight_breathe_fire,
            AbilityId.elder_titan_ancestral_spirit,
            AbilityId.ember_spirit_sleight_of_fist,
            AbilityId.grimstroke_dark_artistry,
            AbilityId.invoker_cold_snap,
            AbilityId.invoker_tornado,
            AbilityId.jakiro_dual_breath,
            AbilityId.keeper_of_the_light_illuminate,
            AbilityId.legion_commander_overwhelming_odds,
            AbilityId.leshrac_lightning_storm,
            AbilityId.lich_frost_nova,
            AbilityId.lina_dragon_slave,
            AbilityId.luna_lucent_beam,
            AbilityId.magnataur_shockwave,
            AbilityId.mars_gods_rebuke,
            AbilityId.medusa_mystic_snake,
            AbilityId.monkey_king_boundless_strike,
            AbilityId.morphling_adaptive_strike_agi,
            AbilityId.morphling_adaptive_strike_str,
            AbilityId.night_stalker_void,
            AbilityId.nyx_assassin_mana_burn,
            AbilityId.nyx_assassin_impale,
            AbilityId.ogre_magi_ignite,
            AbilityId.ogre_magi_fireblast,
            AbilityId.phantom_assassin_stifling_dagger,
            AbilityId.phantom_lancer_spirit_lance,
            AbilityId.puck_illusory_orb,
            AbilityId.pugna_nether_blast,
            AbilityId.queenofpain_shadow_strike,
            AbilityId.razor_plasma_field,
            AbilityId.rubick_fade_bolt,
            AbilityId.shadow_demon_shadow_poison,
            AbilityId.shadow_shaman_ether_shock,
            AbilityId.silencer_curse_of_the_silent,
            AbilityId.skywrath_mage_concussive_shot,
            AbilityId.skywrath_mage_arcane_bolt,
            AbilityId.snapfire_scatterblast,
            AbilityId.sniper_shrapnel,
            AbilityId.spectre_spectral_dagger,
            AbilityId.terrorblade_reflection,
            AbilityId.tinker_heat_seeking_missile,
            AbilityId.tinker_laser,
            AbilityId.tiny_toss,
            AbilityId.treant_natures_grasp,
            AbilityId.troll_warlord_whirling_axes_ranged,
            AbilityId.tusk_ice_shards,
            AbilityId.undying_decay,
            AbilityId.undying_soul_rip,
            AbilityId.vengefulspirit_wave_of_terror,
            AbilityId.venomancer_venomous_gale,
            AbilityId.viper_nethertoxin,
            AbilityId.visage_soul_assumption,
            AbilityId.zuus_arc_lightning,
        };

        public static IEnumerable<AbilityId> Disable { get; } = new[]
        {
            AbilityId.item_abyssal_blade,
            AbilityId.item_sheepstick,
            AbilityId.lion_voodoo,
            AbilityId.item_cyclone,
            AbilityId.shadow_shaman_voodoo,
            AbilityId.invoker_cold_snap,
            AbilityId.lone_druid_savage_roar,
            AbilityId.lone_druid_savage_roar_bear,
            AbilityId.rubick_telekinesis,

            // disable
            AbilityId.void_spirit_aether_remnant,
            AbilityId.doom_bringer_infernal_blade,
            AbilityId.dragon_knight_dragon_tail,
            AbilityId.leshrac_split_earth,
            AbilityId.ember_spirit_searing_chains,
            AbilityId.centaur_hoof_stomp,
            AbilityId.batrider_flaming_lasso,
            AbilityId.earthshaker_fissure,
            AbilityId.axe_berserkers_call,
            AbilityId.jakiro_ice_path,
            AbilityId.monkey_king_boundless_strike,
            AbilityId.ogre_magi_fireblast,
            AbilityId.ogre_magi_unrefined_fireblast,
            AbilityId.slardar_slithereen_crush,
            AbilityId.storm_spirit_electric_vortex,
            AbilityId.tusk_walrus_punch,
            AbilityId.tusk_walrus_kick,
            AbilityId.visage_summon_familiars_stone_form,
            AbilityId.centaur_khan_war_stomp,
            AbilityId.lich_sinister_gaze,
            AbilityId.mars_spear,
            AbilityId.snapfire_firesnap_cookie,

            //silence

            AbilityId.void_spirit_resonant_pulse,
            AbilityId.puck_waning_rift,
            AbilityId.item_bloodthorn,
            AbilityId.item_orchid,
            AbilityId.skywrath_mage_ancient_seal,
            AbilityId.death_prophet_silence,
            // AbilityId.earth_spirit_geomagnetic_grip, //todo add ?
            AbilityId.night_stalker_crippling_fear,
            AbilityId.enigma_malefice,

            // disable projectile
            AbilityId.brewmaster_earth_hurl_boulder,
            AbilityId.chaos_knight_chaos_bolt,
            //  AbilityId.earth_spirit_boulder_smash, //todo add ?
            AbilityId.lion_impale,
            AbilityId.morphling_adaptive_strike_str,
            AbilityId.nyx_assassin_impale,
            AbilityId.sandking_burrowstrike,
            AbilityId.sven_storm_bolt,
            AbilityId.tiny_avalanche,
            AbilityId.vengefulspirit_magic_missile,
            AbilityId.windrunner_shackleshot,
            AbilityId.skeleton_king_hellfire_blast,
            AbilityId.mud_golem_hurl_boulder,
            AbilityId.invoker_tornado,
            AbilityId.drow_ranger_wave_of_silence,
            AbilityId.rattletrap_battery_assault,
            AbilityId.shadow_shaman_shackles,
            AbilityId.pudge_dismember,
        };

        public static IEnumerable<AbilityId> EnemyPurge { get; } = new[]
        {
            AbilityId.item_nullifier,
            AbilityId.necronomicon_archer_purge,
            AbilityId.oracle_fortunes_end,
            AbilityId.satyr_trickster_purge,
            AbilityId.brewmaster_storm_dispel_magic,
            AbilityId.item_cyclone,
            AbilityId.invoker_tornado,

            // AbilityId.sven_storm_bolt // todo add talent purge
        };

        /* possible counters
           * 
           * item_smoke_of_deceit
           * invoker_ice_wall
           * wisp_overcharge
           * juggernaut_healing_ward
           * juggernaut_omni_slash
           * medusa_mana_shield
           * mirana_arrow
           * naga_siren_mirror_image
           * omniknight_guardian_angel
           * oracle_purifying_flames
           * phoenix_icarus_dive
           * pugna_nether_ward
           * razor_eye_of_the_storm
           * slardar_amplify_damage
           * terrorblade_sunder
           * shredder_timber_chain
           * tiny_toss
           * weaver_time_lapse
           * item_mjollnir
           * dark_willow_terrorize
           *
           */
        public static IEnumerable<AbilityId> EnemyStrongPurge { get; } = new[] { AbilityId.shadow_demon_demonic_purge };

        public static IEnumerable<AbilityId> FlaskNukes { get; } = new[]
        {
            //   AbilityId.abaddon_death_coil, //todo
            //   AbilityId.oracle_fortunes_end,
            //   AbilityId.dark_willow_shadow_realm,
            // AbilityId.pangolier_swashbuckle,
            //   AbilityId.winter_wyvern_splinter_blast,

            AbilityId.ancient_apparition_chilling_touch,
            AbilityId.arc_warden_flux,
            AbilityId.axe_battle_hunger,
            AbilityId.bane_brain_sap,
            AbilityId.beastmaster_wild_axes,
            AbilityId.bounty_hunter_shuriken_toss,
            AbilityId.broodmother_spawn_spiderlings,
            AbilityId.rattletrap_rocket_flare,
            AbilityId.crystal_maiden_crystal_nova,
            AbilityId.dark_seer_vacuum,
            AbilityId.dazzle_poison_touch,
            AbilityId.death_prophet_carrion_swarm,
            AbilityId.disruptor_thunder_strike,
            AbilityId.dragon_knight_breathe_fire,
            AbilityId.earthshaker_fissure,
            AbilityId.elder_titan_ancestral_spirit,
            AbilityId.ember_spirit_sleight_of_fist,
            AbilityId.grimstroke_dark_artistry,
            AbilityId.invoker_cold_snap,
            AbilityId.invoker_tornado,
            AbilityId.jakiro_dual_breath,
            AbilityId.keeper_of_the_light_illuminate,
            AbilityId.legion_commander_overwhelming_odds,
            AbilityId.leshrac_lightning_storm,
            AbilityId.lich_frost_nova,
            AbilityId.lina_dragon_slave,
            AbilityId.luna_lucent_beam,
            AbilityId.magnataur_shockwave,
            AbilityId.mars_gods_rebuke,
            AbilityId.mars_spear,
            AbilityId.medusa_mystic_snake,
            AbilityId.monkey_king_boundless_strike,
            AbilityId.morphling_adaptive_strike_agi,
            AbilityId.morphling_adaptive_strike_str,
            AbilityId.night_stalker_void,
            AbilityId.nyx_assassin_mana_burn,
            AbilityId.nyx_assassin_impale,
            AbilityId.ogre_magi_ignite,
            AbilityId.ogre_magi_fireblast,
            AbilityId.phantom_assassin_stifling_dagger,
            AbilityId.phantom_lancer_spirit_lance,
            AbilityId.puck_illusory_orb,
            AbilityId.pugna_nether_blast,
            AbilityId.queenofpain_shadow_strike,
            AbilityId.razor_plasma_field,
            AbilityId.rubick_fade_bolt,
            AbilityId.shadow_demon_shadow_poison,
            AbilityId.nevermore_shadowraze1,
            AbilityId.nevermore_shadowraze2,
            AbilityId.nevermore_shadowraze3,
            AbilityId.shadow_shaman_ether_shock,
            AbilityId.silencer_curse_of_the_silent,
            AbilityId.skywrath_mage_concussive_shot,
            AbilityId.skywrath_mage_arcane_bolt,
            AbilityId.snapfire_scatterblast,
            AbilityId.sniper_shrapnel,
            AbilityId.spectre_spectral_dagger,
            AbilityId.sven_storm_bolt,
            AbilityId.terrorblade_reflection,
            AbilityId.tidehunter_gush,
            AbilityId.tinker_heat_seeking_missile,
            AbilityId.tinker_laser,
            AbilityId.tiny_toss,
            AbilityId.treant_natures_grasp,
            AbilityId.troll_warlord_whirling_axes_ranged,
            AbilityId.tusk_ice_shards,
            AbilityId.abyssal_underlord_firestorm,
            AbilityId.undying_decay,
            AbilityId.undying_soul_rip,
            AbilityId.vengefulspirit_wave_of_terror,
            AbilityId.venomancer_venomous_gale,
            AbilityId.viper_nethertoxin,
            AbilityId.visage_soul_assumption,
            AbilityId.witch_doctor_paralyzing_cask,
            AbilityId.skeleton_king_hellfire_blast,
            AbilityId.zuus_arc_lightning,
            AbilityId.zuus_lightning_bolt,
        };

        public static IEnumerable<AbilityId> FullAoeDisable { get; } = new[]
        {
            AbilityId.enigma_black_hole, AbilityId.faceless_void_chronosphere, AbilityId.winter_wyvern_winters_curse,
        };

        public static IEnumerable<AbilityId> Heal { get; } = new[]
        {
            AbilityId.item_essence_ring,
            AbilityId.troll_warlord_battle_trance,
            AbilityId.terrorblade_sunder,
            AbilityId.dazzle_shallow_grave,
            AbilityId.oracle_false_promise,
            AbilityId.item_mekansm,
            AbilityId.item_guardian_greaves,
            AbilityId.dazzle_shadow_wave,
            AbilityId.abaddon_death_coil,
            AbilityId.omniknight_purification,
            AbilityId.chen_hand_of_god,
            AbilityId.item_magic_stick,
            AbilityId.item_magic_wand,
            AbilityId.item_cheese,
            AbilityId.item_greater_faerie_fire,
            AbilityId.item_faerie_fire,
            //   AbilityId.clinkz_death_pact, // todo add
            AbilityId.necrolyte_death_pulse,
            AbilityId.abaddon_borrowed_time,
            AbilityId.item_bloodstone,
        };

        public static IEnumerable<AbilityId> InstantBlink { get; } = new[]
        {
            AbilityId.ember_spirit_activate_fire_remnant,
            AbilityId.item_blink,
            AbilityId.antimage_blink,
            AbilityId.queenofpain_blink,
            AbilityId.void_spirit_astral_step,
        };

        public static IEnumerable<AbilityId> Invisibility { get; } = new[]
        {
            AbilityId.phantom_assassin_blur,
            AbilityId.bounty_hunter_wind_walk,
            AbilityId.invoker_ghost_walk,
            AbilityId.clinkz_wind_walk,
            AbilityId.sandking_sand_storm,
            AbilityId.templar_assassin_meld,
            AbilityId.weaver_shukuchi,
            AbilityId.item_glimmer_cape,
            AbilityId.item_invis_sword,
            AbilityId.item_silver_edge,
            AbilityId.nyx_assassin_vendetta,
            //  AbilityId.brewmaster_storm_wind_walk //todo add ?
        };

        public static IEnumerable<AbilityId> Invulnerability { get; } = new[]
        {
            AbilityId.shadow_demon_disruption,
            AbilityId.obsidian_destroyer_astral_imprisonment,
            AbilityId.earth_spirit_petrify,
            AbilityId.brewmaster_storm_cyclone,
            AbilityId.life_stealer_assimilate,
            AbilityId.item_cyclone,
            AbilityId.bane_nightmare,
        };

        public static IEnumerable<AbilityId> MagicImmunity { get; } = new[]
        {
            AbilityId.juggernaut_blade_fury,
            AbilityId.life_stealer_rage,
            AbilityId.item_minotaur_horn,
            AbilityId.oracle_fates_edict,
            AbilityId.item_black_king_bar,
        };

        public static IEnumerable<AbilityId> MagicShield { get; } = new[]
        {
            AbilityId.ember_spirit_flame_guard,
            AbilityId.item_hood_of_defiance,
            AbilityId.abaddon_aphotic_shield,
            AbilityId.item_glimmer_cape,
            AbilityId.bristleback_bristleback,
        };

        public static IEnumerable<AbilityId> PhysDisable { get; } = new[]
        {
            AbilityId.invoker_deafening_blast,
            AbilityId.item_heavens_halberd,
            AbilityId.tinker_laser,
            AbilityId.huskar_inner_fire,
            AbilityId.item_ethereal_blade,
            AbilityId.pugna_decrepify,
            AbilityId.oracle_fates_edict,
            AbilityId.keeper_of_the_light_blinding_light,
            AbilityId.snapfire_scatterblast,

            // AbilityId.phoenix_launch_fire_spirit, //todo add?
        };

        public static IEnumerable<AbilityId> PhysShield { get; } = new[]
        {
            AbilityId.item_ghost,
            AbilityId.item_ethereal_blade,
            AbilityId.necrolyte_sadist,
            AbilityId.pugna_decrepify,
            AbilityId.item_solar_crest,
            AbilityId.abaddon_aphotic_shield,
            AbilityId.treant_living_armor,
            AbilityId.item_medallion_of_courage,
            AbilityId.ogre_magi_frost_armor,
            AbilityId.lich_frost_shield,
            AbilityId.sven_warcry,
            AbilityId.bristleback_bristleback,
        };

        public static IEnumerable<AbilityId> ProactiveAbilityDisable { get; } = new[]
        {
            AbilityId.item_abyssal_blade,
            AbilityId.item_sheepstick,
            AbilityId.lion_voodoo,
            AbilityId.shadow_shaman_voodoo,
            AbilityId.item_cyclone,
            AbilityId.sandking_burrowstrike,
            AbilityId.lone_druid_savage_roar,
            AbilityId.lone_druid_savage_roar_bear,
            AbilityId.rubick_telekinesis,
            AbilityId.puck_waning_rift,
            AbilityId.item_bloodthorn,
            AbilityId.item_orchid,
            AbilityId.skywrath_mage_ancient_seal,
            AbilityId.tiny_avalanche,
        };

        public static IEnumerable<AbilityId> ProactiveBlink { get; } = new[]
        {
            AbilityId.ember_spirit_activate_fire_remnant, AbilityId.item_blink, AbilityId.nyx_assassin_burrow,
        };

        public static IEnumerable<AbilityId> ProactiveHexCounter { get; } = new[]
        {
            AbilityId.antimage_counterspell,
            AbilityId.item_sphere,
            AbilityId.templar_assassin_refraction,
            AbilityId.slark_dark_pact,
            AbilityId.slark_shadow_dance,
            AbilityId.dark_willow_shadow_realm,
            AbilityId.item_lotus_orb,
            //   AbilityId.puck_phase_shift,
            AbilityId.spirit_breaker_bulldoze,
            AbilityId.nyx_assassin_spiked_carapace,
            AbilityId.juggernaut_blade_fury,
            AbilityId.life_stealer_rage,
            AbilityId.item_black_king_bar,
            AbilityId.phantom_lancer_doppelwalk,
            AbilityId.alchemist_chemical_rage,
            AbilityId.morphling_morph_str,
            AbilityId.ursa_enrage,
            AbilityId.item_manta,
            AbilityId.item_hood_of_defiance,
            AbilityId.item_pipe,
            AbilityId.item_glimmer_cape,
            AbilityId.bounty_hunter_wind_walk,
            AbilityId.clinkz_wind_walk,
            AbilityId.item_invis_sword,
            AbilityId.item_silver_edge,
            AbilityId.windrunner_windrun,
            AbilityId.weaver_shukuchi,
            AbilityId.templar_assassin_meld,
            AbilityId.nyx_assassin_vendetta,
            AbilityId.sven_warcry,
            AbilityId.item_blade_mail,
            AbilityId.item_crimson_guard,
            AbilityId.item_armlet,
        };

        public static IEnumerable<AbilityId> ProactiveItemDisable { get; } = new[]
        {
            AbilityId.item_abyssal_blade,
            AbilityId.item_sheepstick,
            AbilityId.lion_voodoo,
            AbilityId.shadow_shaman_voodoo,
            AbilityId.item_cyclone,
            AbilityId.sandking_burrowstrike,
            AbilityId.lone_druid_savage_roar,
            AbilityId.lone_druid_savage_roar_bear,
            AbilityId.rubick_telekinesis,
            AbilityId.tiny_avalanche,
        };

        public static IEnumerable<AbilityId> ProactiveSilenceCounter { get; } = new[]
        {
            AbilityId.antimage_counterspell,
            AbilityId.item_sphere,
            AbilityId.templar_assassin_refraction,
            AbilityId.slark_dark_pact,
            AbilityId.slark_shadow_dance,
            AbilityId.dark_willow_shadow_realm,
            // AbilityId.puck_phase_shift,
            AbilityId.spirit_breaker_bulldoze,
            AbilityId.nyx_assassin_spiked_carapace,
            AbilityId.juggernaut_blade_fury,
            AbilityId.life_stealer_rage,
            AbilityId.phantom_lancer_doppelwalk,
            AbilityId.alchemist_chemical_rage,
            AbilityId.ursa_enrage,
            AbilityId.morphling_morph_str,
            AbilityId.bounty_hunter_wind_walk,
            AbilityId.clinkz_wind_walk,
            AbilityId.windrunner_windrun,
            AbilityId.weaver_shukuchi,
            AbilityId.templar_assassin_meld,
            AbilityId.nyx_assassin_vendetta,
            AbilityId.sven_warcry,
        };

        public static IEnumerable<AbilityId> Root { get; } = new[]
        {
            AbilityId.crystal_maiden_frostbite,
            AbilityId.item_rod_of_atos,
            AbilityId.naga_siren_ensnare,
            AbilityId.dark_troll_warlord_ensnare,
            AbilityId.ember_spirit_searing_chains,
            AbilityId.meepo_earthbind,
            AbilityId.abyssal_underlord_pit_of_malice,
            AbilityId.item_clumsy_net,
        };

        public static IEnumerable<AbilityId> Shield { get; } = new[]
        {
            AbilityId.puck_phase_shift,
            AbilityId.item_minotaur_horn,
            AbilityId.void_spirit_dissimilate,
            AbilityId.monkey_king_mischief,
            AbilityId.nyx_assassin_spiked_carapace,
            AbilityId.templar_assassin_refraction,
            AbilityId.phantom_lancer_doppelwalk,
            AbilityId.bristleback_bristleback,
        };

        public static IEnumerable<AbilityId> SimplePhysShield { get; } = new[]
        {
            AbilityId.abaddon_aphotic_shield,
            AbilityId.void_spirit_resonant_pulse,
            AbilityId.item_solar_crest,
            AbilityId.treant_living_armor,
            AbilityId.item_medallion_of_courage,
            AbilityId.ogre_magi_frost_armor,
            AbilityId.lich_frost_shield,
            AbilityId.sven_warcry,
        };

        public static IEnumerable<AbilityId> SimpleStun { get; } = new[]
        {
            AbilityId.item_abyssal_blade,
            AbilityId.item_sheepstick,
            AbilityId.lion_voodoo,
            AbilityId.shadow_shaman_voodoo,
            AbilityId.invoker_cold_snap,
            AbilityId.lone_druid_savage_roar,
            AbilityId.lone_druid_savage_roar_bear,
            AbilityId.rubick_telekinesis,
            AbilityId.leshrac_split_earth,
            AbilityId.centaur_hoof_stomp,
            AbilityId.earthshaker_fissure,
            AbilityId.axe_berserkers_call,
            AbilityId.jakiro_ice_path,
            AbilityId.monkey_king_boundless_strike,
            AbilityId.ogre_magi_fireblast,
            AbilityId.ogre_magi_unrefined_fireblast,
            AbilityId.slardar_slithereen_crush,
            AbilityId.storm_spirit_electric_vortex,
            AbilityId.tusk_walrus_punch,
            AbilityId.visage_summon_familiars_stone_form,
            AbilityId.centaur_khan_war_stomp,
            AbilityId.invoker_tornado,
            AbilityId.shadow_shaman_shackles,
            AbilityId.doom_bringer_infernal_blade,
            AbilityId.pudge_dismember,
            AbilityId.brewmaster_earth_hurl_boulder,
            AbilityId.chaos_knight_chaos_bolt,
            //  AbilityId.earth_spirit_boulder_smash, //todo add ?
            AbilityId.lion_impale,
            AbilityId.morphling_adaptive_strike_str,
            AbilityId.nyx_assassin_impale,
            AbilityId.sandking_burrowstrike,
            AbilityId.sven_storm_bolt,
            AbilityId.tiny_avalanche,
            AbilityId.vengefulspirit_magic_missile,
            AbilityId.windrunner_shackleshot,
            AbilityId.skeleton_king_hellfire_blast,
            AbilityId.mud_golem_hurl_boulder,
            AbilityId.zuus_lightning_bolt,
            AbilityId.disruptor_glimpse,
            AbilityId.luna_lucent_beam,
            AbilityId.crystal_maiden_frostbite,
            AbilityId.naga_siren_ensnare,
            AbilityId.dark_troll_warlord_ensnare,
            AbilityId.ember_spirit_searing_chains,
            AbilityId.zuus_cloud,
            AbilityId.item_clumsy_net,
        };

        public static IEnumerable<AbilityId> SlowHeal { get; } = new[]
        {
            AbilityId.enchantress_natures_attendants, AbilityId.warlock_shadow_word, AbilityId.lycan_howl,
            // AbilityId.witch_doctor_voodoo_restoration, //todo fix
            // AbilityId.death_prophet_spirit_siphon, // todo add?
        };

        public static IEnumerable<AbilityId> StrongDisable { get; } = new[]
        {
            AbilityId.disruptor_static_storm,
            AbilityId.doom_bringer_doom,
            // AbilityId.earthshaker_echo_slam, // todo fix stun range

            AbilityId.treant_overgrowth, //todo agh trees ?
            AbilityId.warlock_rain_of_chaos,
            AbilityId.winter_wyvern_winters_curse,
            AbilityId.beastmaster_primal_roar,
            AbilityId.keeper_of_the_light_will_o_wisp,
        };

        public static IEnumerable<AbilityId> StrongMagicShield { get; } = new[]
        {
            AbilityId.ember_spirit_flame_guard,
            AbilityId.item_hood_of_defiance,
            AbilityId.item_pipe,
            AbilityId.abaddon_aphotic_shield,
            AbilityId.item_glimmer_cape,
            AbilityId.oracle_fates_edict,
            AbilityId.life_stealer_infest,
            AbilityId.bristleback_bristleback,
        };

        public static IEnumerable<AbilityId> StrongPhysShield { get; } = new[]
        {
            AbilityId.windrunner_windrun, // todo mkb check ?
            AbilityId.item_ghost,
            AbilityId.item_ethereal_blade,
            AbilityId.necrolyte_sadist,
            AbilityId.pugna_decrepify,
            AbilityId.winter_wyvern_cold_embrace,
            AbilityId.void_spirit_resonant_pulse,
            AbilityId.razor_static_link, //todo ignore int heroes + some range logic ?

            AbilityId.item_solar_crest,
            AbilityId.abaddon_aphotic_shield,
            AbilityId.troll_warlord_whirling_axes_melee,
            //  AbilityId.arc_warden_magnetic_field, //todo only vs modifiers ?

            AbilityId.treant_living_armor,
            AbilityId.item_glimmer_cape,
            AbilityId.item_crimson_guard,
            AbilityId.item_shivas_guard,
            AbilityId.bristleback_bristleback,
            AbilityId.item_medallion_of_courage,
            AbilityId.ogre_magi_frost_armor,
            AbilityId.lich_frost_shield,
            AbilityId.sven_warcry,
        };

        public static IEnumerable<AbilityId> StrongShield { get; } = new[]
        {
            AbilityId.puck_phase_shift,
            AbilityId.item_minotaur_horn,
            AbilityId.void_spirit_dissimilate,
            AbilityId.nyx_assassin_spiked_carapace,
            AbilityId.templar_assassin_refraction,
            AbilityId.phantom_lancer_doppelwalk,
            AbilityId.item_cyclone,
            AbilityId.tusk_snowball,
            AbilityId.slark_dark_pact,
            AbilityId.slark_shadow_dance,
            AbilityId.juggernaut_blade_fury,
            AbilityId.life_stealer_rage,
            AbilityId.life_stealer_infest,
            AbilityId.omniknight_repel,
            AbilityId.riki_tricks_of_the_trade,
            AbilityId.ursa_enrage,
            AbilityId.item_black_king_bar,
            AbilityId.dark_willow_shadow_realm,
            AbilityId.grimstroke_spirit_walk,
            AbilityId.bristleback_bristleback,
            AbilityId.spirit_breaker_bulldoze,
            (AbilityId)419 //Swiftslash
        };

        public static IEnumerable<AbilityId> Stun { get; } = new[]
        {
            AbilityId.item_abyssal_blade,
            AbilityId.item_sheepstick,
            AbilityId.lion_voodoo,
            AbilityId.shadow_shaman_voodoo,
            AbilityId.invoker_cold_snap,
            AbilityId.lone_druid_savage_roar,
            AbilityId.lone_druid_savage_roar_bear,
            AbilityId.rubick_telekinesis,
            AbilityId.dragon_knight_dragon_tail,
            AbilityId.leshrac_split_earth,
            AbilityId.centaur_hoof_stomp,
            AbilityId.batrider_flaming_lasso,
            AbilityId.earthshaker_fissure,
            AbilityId.axe_berserkers_call,
            AbilityId.jakiro_ice_path,
            AbilityId.monkey_king_boundless_strike,
            AbilityId.ogre_magi_fireblast,
            AbilityId.ogre_magi_unrefined_fireblast,
            AbilityId.slardar_slithereen_crush,
            AbilityId.storm_spirit_electric_vortex,
            AbilityId.tusk_walrus_punch,
            AbilityId.visage_summon_familiars_stone_form,
            AbilityId.centaur_khan_war_stomp,
            AbilityId.invoker_tornado,
            AbilityId.shadow_shaman_shackles,
            AbilityId.doom_bringer_infernal_blade,
            AbilityId.pudge_dismember,
            AbilityId.brewmaster_earth_hurl_boulder,
            AbilityId.chaos_knight_chaos_bolt,
            //  AbilityId.earth_spirit_boulder_smash, //todo add ?
            AbilityId.lion_impale,
            AbilityId.morphling_adaptive_strike_str,
            AbilityId.nyx_assassin_impale,
            AbilityId.sandking_burrowstrike,
            AbilityId.sven_storm_bolt,
            AbilityId.tiny_avalanche,
            AbilityId.vengefulspirit_magic_missile,
            AbilityId.windrunner_shackleshot,
            AbilityId.skeleton_king_hellfire_blast,
            AbilityId.mud_golem_hurl_boulder,
        };

        public static IEnumerable<AbilityId> Suicide { get; } = Enumerable.Empty<AbilityId>();

        public static IEnumerable<AbilityId> Tango { get; } = new[] { AbilityId.item_tango, AbilityId.item_tango_single };

        public static IEnumerable<AbilityId> VsDisableProjectile { get; } = new[]
        {
            AbilityId.antimage_counterspell,
            AbilityId.monkey_king_mischief,
            AbilityId.enchantress_bunny_hop,
            AbilityId.item_sphere,
            AbilityId.storm_spirit_ball_lightning,
            AbilityId.ember_spirit_sleight_of_fist,
            AbilityId.item_manta,
            AbilityId.alchemist_chemical_rage,
            AbilityId.huskar_life_break,
            (AbilityId)419 //Swiftslash
        };

        public static IEnumerable<AbilityId> VsInvisibility { get; } = new[] { AbilityId.bounty_hunter_track, AbilityId.item_dust, };

        public static IEnumerable<AbilityId> VsProjectile { get; } = new[]
        {
            //AbilityId.item_sphere,
            AbilityId.enchantress_bunny_hop,
            AbilityId.monkey_king_mischief,
            AbilityId.antimage_counterspell,
            AbilityId.storm_spirit_ball_lightning,
            AbilityId.ember_spirit_sleight_of_fist,
        };
    }
}