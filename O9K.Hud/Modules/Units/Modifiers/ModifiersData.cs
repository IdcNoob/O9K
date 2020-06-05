namespace O9K.Hud.Modules.Units.Modifiers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    internal partial class Modifiers
    {
        internal enum ModifierType
        {
            Temporary,

            TemporaryHidden,

            TemporaryNoTime,

            StackHidden,

            Aura,

            ChargeCounter,

            Permanent
        }

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        [SuppressMessage("ReSharper", "CommentTypo")]
        private readonly Dictionary<string, ModifierType> data = new Dictionary<string, ModifierType>
        {
            { "modifier_rune_arcane", ModifierType.Temporary },
            { "modifier_rune_doubledamage", ModifierType.Temporary },
            { "modifier_rune_haste", ModifierType.Temporary },
            { "modifier_rune_invis", ModifierType.Temporary },
            { "modifier_rune_regen", ModifierType.Temporary },

            { "modifier_truesight", ModifierType.Aura },
            //{ "modifier_fountain_aura_buff", ModifierType.TemporaryNoTime },
            //{ "modifier_fountain_glyph", ModifierType.Temporary },
            { "modifier_tower_aura_bonus", ModifierType.TemporaryNoTime },

            { "modifier_stunned", ModifierType.Temporary },
            { "modifier_silence", ModifierType.Temporary },
            { "modifier_bashed", ModifierType.Temporary },
            { "modifier_knockback", ModifierType.Temporary },
            { "modifier_invisible", ModifierType.Temporary }, //riki inivis no time

            { "modifier_abaddon_aphotic_shield", ModifierType.Temporary },
            { "modifier_abaddon_frostmourne_debuff", ModifierType.Temporary },
            { "modifier_abaddon_frostmourne_debuff_bonus", ModifierType.Temporary },
            { "modifier_abaddon_borrowed_time", ModifierType.Temporary },
            { "modifier_abaddon_borrowed_time_damage_redirect", ModifierType.Aura },

            { "modifier_alchemist_acid_spray", ModifierType.TemporaryNoTime },
            //{ "modifier_alchemist_goblins_greed", ModifierType.ChargeCounter },
            { "modifier_alchemist_chemical_rage", ModifierType.Temporary },

            { "modifier_cold_feet", ModifierType.TemporaryNoTime },
            { "modifier_ancientapparition_coldfeet_freeze", ModifierType.Temporary },
            { "modifier_ice_vortex", ModifierType.TemporaryNoTime },
            { "modifier_chilling_touch_slow", ModifierType.Temporary },
            { "modifier_ice_blast", ModifierType.Temporary },

            { "modifier_antimage_counterspell", ModifierType.Temporary },

            { "modifier_arc_warden_flux", ModifierType.Temporary },
            { "modifier_arc_warden_magnetic_field_attack_speed", ModifierType.TemporaryNoTime },
            { "modifier_arc_warden_spark_wraith_purge", ModifierType.Temporary },

            { "modifier_axe_berserkers_call", ModifierType.Temporary },
            { "modifier_axe_berserkers_call_armor", ModifierType.Temporary },
            { "modifier_axe_battle_hunger", ModifierType.Temporary },
            { "modifier_axe_culling_blade_boost", ModifierType.Temporary },

            { "modifier_bane_enfeeble", ModifierType.Temporary },
            { "modifier_bane_nightmare", ModifierType.Temporary },
            { "modifier_bane_nightmare_invulnerable", ModifierType.Temporary },
            { "modifier_bane_fiends_grip", ModifierType.Temporary },

            { "modifier_batrider_sticky_napalm", ModifierType.Temporary },
            { "modifier_flamebreak_damage", ModifierType.Temporary },
            { "modifier_batrider_firefly", ModifierType.Temporary },
            { "modifier_batrider_flaming_lasso_self", ModifierType.Temporary },
            { "modifier_batrider_flaming_lasso", ModifierType.Temporary },

            { "modifier_beastmaster_axe_stack_counter", ModifierType.Temporary },
            { "modifier_beastmaster_inner_beast", ModifierType.Aura },
            { "modifier_beastmaster_primal_roar_speed", ModifierType.Temporary },
            { "modifier_beastmaster_primal_roar_slow", ModifierType.Temporary },
            { "modifier_beastmaster_boar_poison_effect", ModifierType.Temporary },

            { "modifier_bloodseeker_bloodrage", ModifierType.Temporary },
            { "modifier_bloodseeker_thirst_speed", ModifierType.TemporaryNoTime },
            { "modifier_bloodseeker_thirst_vision", ModifierType.TemporaryNoTime },
            { "modifier_bloodseeker_rupture", ModifierType.Temporary },
            //{ "modifier_bloodseeker_rupture_charge_counter", ModifierType.ChargeCounter },

            { "modifier_bounty_hunter_wind_walk", ModifierType.Temporary },
            { "modifier_bounty_hunter_track", ModifierType.Temporary },
            { "modifier_bounty_hunter_track_effect", ModifierType.TemporaryNoTime },

            { "modifier_brewmaster_thunder_clap", ModifierType.Temporary },
            { "modifier_brewmaster_cinder_brew", ModifierType.Temporary },
            { "modifier_brewmaster_drunken_brawler", ModifierType.Temporary },
            { "modifier_brewmaster_primal_split_duration", ModifierType.Temporary },
            { "modifier_brewmaster_fire_permanent_immolation", ModifierType.TemporaryNoTime },
            { "modifier_brewmaster_storm_cyclone", ModifierType.Temporary },

            { "modifier_bristleback_viscous_nasal_goo", ModifierType.Temporary },
            { "modifier_bristleback_quill_spray", ModifierType.Temporary },
            // { "modifier_bristleback_warpath", ModifierType.TemporaryHidden },

            { "modifier_broodmother_spawn_spiderlings", ModifierType.Temporary },
            { "modifier_broodmother_incapacitating_bite_orb", ModifierType.Temporary },
            //{ "modifier_broodmother_spin_web_charge_counter", ModifierType.ChargeCounter },
            { "modifier_broodmother_spin_web", ModifierType.TemporaryNoTime },
            { "modifier_broodmother_insatiable_hunger", ModifierType.Temporary },

            //{ "modifier_centaur_return_counter", ModifierType.ChargeCounter },
            { "modifier_centaur_return_bonus_damage", ModifierType.Temporary },
            //{ "modifier_centaur_return", ModifierType.Aura }, //todo hide on self
            { "modifier_centaur_stampede", ModifierType.Temporary },

            { "modifier_chaos_knight_reality_rift_buff", ModifierType.Temporary },

            { "modifier_chen_penitence", ModifierType.Temporary },
            { "modifier_chen_divine_favor", ModifierType.Aura },
            { "modifier_chen_test_of_faith_teleport", ModifierType.Temporary },

            { "modifier_clinkz_strafe", ModifierType.Temporary },
            { "modifier_clinkz_wind_walk", ModifierType.Temporary },

            { "modifier_rattletrap_battery_assault", ModifierType.Temporary },
            { "modifier_rattletrap_cog_push", ModifierType.Temporary },

            { "modifier_crystal_maiden_crystal_nova", ModifierType.Temporary },
            { "modifier_crystal_maiden_frostbite", ModifierType.Temporary },
            //{ "modifier_crystal_maiden_brilliance_aura_effect", ModifierType.Aura },
            { "modifier_crystal_maiden_freezing_field_slow", ModifierType.TemporaryNoTime },

            { "modifier_dark_seer_ion_shell", ModifierType.Temporary },
            { "modifier_dark_seer_surge", ModifierType.Temporary },

            { "modifier_dark_willow_bramble_maze", ModifierType.Temporary },
            { "modifier_dark_willow_shadow_realm_buff", ModifierType.Temporary },
            { "modifier_dark_willow_cursed_crown", ModifierType.Temporary },
            { "modifier_dark_willow_bedlam", ModifierType.Temporary },
            { "modifier_dark_willow_debuff_fear", ModifierType.Temporary },

            { "modifier_dazzle_poison_touch", ModifierType.Temporary },
            { "modifier_dazzle_shallow_grave", ModifierType.Temporary },
            { "modifier_dazzle_bad_juju_armor", ModifierType.Temporary },

            { "modifier_death_prophet_silence", ModifierType.Temporary },
            //{ "modifier_death_prophet_spirit_siphon_charge_counter", ModifierType.ChargeCounter },
            { "modifier_death_prophet_spirit_siphon_slow", ModifierType.Temporary },
            { "modifier_death_prophet_exorcism", ModifierType.Temporary },

            { "modifier_disruptor_thunder_strike", ModifierType.Temporary },
            { "modifier_disruptor_static_storm", ModifierType.TemporaryNoTime },

            { "modifier_doom_bringer_devour", ModifierType.Temporary },
            { "modifier_doom_bringer_scorched_earth_effect", ModifierType.Temporary },
            { "modifier_doom_bringer_infernal_blade_burn", ModifierType.Temporary },
            { "modifier_doom_bringer_doom", ModifierType.Temporary },

            { "modifier_dragonknight_breathefire_reduction", ModifierType.Temporary },
            { "modifier_dragon_knight_corrosive_breath", ModifierType.Temporary },
            { "modifier_dragon_knight_corrosive_breath_dot", ModifierType.Temporary },

            { "modifier_drow_ranger_frost_arrows_slow", ModifierType.Temporary },
            { "modifier_drowranger_wave_of_silence", ModifierType.Temporary },
            //{ "modifier_drow_ranger_trueshot_aura", ModifierType.Aura },

            { "modifier_earth_spirit_boulder_smash_debuff", ModifierType.Temporary },
            { "modifier_earth_spirit_geomagnetic_grip_debuff", ModifierType.Temporary },
            { "modifier_earth_spirit_magnetize", ModifierType.Temporary },
            { "modifier_earthspirit_petrify", ModifierType.Temporary },
            //{ "modifier_earth_spirit_stone_caller_charge_counter", ModifierType.ChargeCounter },

            { "modifier_earthshaker_enchant_totem", ModifierType.Temporary },
            { "modifier_earthshaker_fissure_stun", ModifierType.Temporary },

            { "modifier_elder_titan_echo_stomp", ModifierType.Temporary },
            { "modifier_elder_titan_echo_stomp_magic_immune", ModifierType.Temporary },
            { "modifier_elder_titan_natural_order_magic_resistance", ModifierType.Aura },
            { "modifier_elder_titan_natural_order_armor", ModifierType.Aura },
            { "modifier_elder_titan_earth_splitter", ModifierType.Temporary },

            { "modifier_ember_spirit_searing_chains", ModifierType.Temporary },
            { "modifier_ember_spirit_flame_guard", ModifierType.Temporary },
            { "modifier_ember_spirit_fire_remnant_timer", ModifierType.Temporary },
            //{ "modifier_ember_spirit_fire_remnant_charge_counter", ModifierType.ChargeCounter },
            //{ "modifier_ember_spirit_sleight_of_fist_charge_counter", ModifierType.ChargeCounter },

            { "modifier_enchantress_untouchable_slow", ModifierType.TemporaryNoTime },
            { "modifier_enchantress_enchant_slow", ModifierType.Temporary },
            { "modifier_enchantress_natures_attendants", ModifierType.Temporary },

            { "modifier_enigma_malefice", ModifierType.Temporary },
            { "modifier_enigma_black_hole_pull", ModifierType.TemporaryNoTime },

            { "modifier_faceless_void_time_dilation_slow", ModifierType.Temporary },
            { "modifier_faceless_void_timelock_freeze", ModifierType.Temporary },
            { "modifier_faceless_void_chronosphere_freeze", ModifierType.TemporaryNoTime },

            { "modifier_grimstroke_dark_artistry_slow", ModifierType.Temporary },
            { "modifier_grimstroke_ink_creature_debuff", ModifierType.Temporary },
            { "modifier_grimstroke_spirit_walk_buff", ModifierType.Temporary },
            { "modifier_grimstroke_scepter_buff", ModifierType.Temporary },
            { "modifier_grimstroke_soul_chain", ModifierType.Temporary },

            { "modifier_gyrocopter_rocket_barrage", ModifierType.Temporary },
            { "modifier_gyrocopter_flak_cannon", ModifierType.Temporary },
            { "modifier_gyrocopter_call_down_slow", ModifierType.Temporary },
            //{ "modifier_gyrocopter_homing_missile_charge_counter", ModifierType.ChargeCounter },

            { "modifier_huskar_inner_fire_disarm", ModifierType.Temporary },
            { "modifier_huskar_burning_spear_debuff", ModifierType.Temporary },
            { "modifier_huskar_life_break_slow", ModifierType.Temporary },

            { "modifier_invoker_cold_snap", ModifierType.Temporary },
            { "modifier_invoker_cold_snap_freeze", ModifierType.Temporary },
            { "modifier_invoker_ghost_walk_self", ModifierType.Temporary },
            { "modifier_invoker_ghost_walk_enemy", ModifierType.TemporaryNoTime },
            { "modifier_invoker_tornado", ModifierType.Temporary },
            { "modifier_forged_spirit_melting_strike_debuff", ModifierType.Temporary },
            { "modifier_invoker_ice_wall_slow_debuff", ModifierType.TemporaryNoTime },
            { "modifier_invoker_alacrity", ModifierType.Temporary },
            { "modifier_invoker_chaos_meteor_burn", ModifierType.Temporary },
            { "modifier_invoker_deafening_blast_disarm", ModifierType.Temporary },

            { "modifier_wisp_spirits_slow", ModifierType.Temporary },
            { "modifier_wisp_tether", ModifierType.TemporaryNoTime },
            { "modifier_wisp_tether_haste", ModifierType.TemporaryNoTime },
            { "modifier_wisp_spirits", ModifierType.Temporary },
            { "modifier_wisp_overcharge", ModifierType.Temporary },
            { "modifier_wisp_relocate_return", ModifierType.TemporaryNoTime },

            { "modifier_jakiro_dual_breath_slow", ModifierType.Temporary },
            { "modifier_jakiro_ice_path_stun", ModifierType.Temporary },
            { "modifier_jakiro_liquid_fire_burn", ModifierType.Temporary },
            { "modifier_jakiro_macropyre_burn", ModifierType.TemporaryNoTime },

            { "modifier_juggernaut_blade_fury", ModifierType.Temporary },
            { "modifier_juggernaut_healing_ward_heal", ModifierType.TemporaryNoTime },
            { "modifier_juggernaut_omnislash", ModifierType.Temporary },

            { "modifier_keeper_of_the_light_blinding_light", ModifierType.Temporary },
            { "modifier_keeper_of_the_light_mana_leak", ModifierType.Temporary },
            { "modifier_keeper_of_the_light_will_o_wisp", ModifierType.Temporary },

            { "modifier_kunkka_torrent", ModifierType.Temporary },
            { "modifier_kunkka_torrent_slow", ModifierType.Temporary },
            { "modifier_kunkka_x_marks_the_spot", ModifierType.Temporary },
            { "modifier_kunkka_ghost_ship_damage_absorb", ModifierType.Temporary },

            { "modifier_legion_commander_overwhelming_odds", ModifierType.Temporary },
            { "modifier_legion_commander_press_the_attack", ModifierType.Temporary },
            { "modifier_legion_commander_duel", ModifierType.Temporary },

            { "modifier_leshrac_diabolic_edict", ModifierType.Temporary },
            { "modifier_leshrac_lightning_storm_slow", ModifierType.Temporary },
            { "modifier_leshrac_pulse_nova", ModifierType.TemporaryNoTime },

            { "modifier_lich_frostnova_slow", ModifierType.Temporary },
            { "modifier_lich_frost_shield", ModifierType.Temporary },
            { "modifier_lich_frost_shield_slow", ModifierType.Temporary },
            { "modifier_lich_sinister_gaze", ModifierType.Temporary },
            { "modifier_lich_chainfrost_slow", ModifierType.Temporary },
            { "modifier_lich_attack_slow_debuff", ModifierType.Temporary },

            { "modifier_life_stealer_rage", ModifierType.Temporary },
            { "modifier_life_stealer_open_wounds", ModifierType.Temporary },
            { "modifier_life_stealer_infest", ModifierType.TemporaryNoTime },

            // { "modifier_lina_fiery_soul", ModifierType.TemporaryHidden },

            { "modifier_lion_impale", ModifierType.Temporary },
            { "modifier_lion_voodoo", ModifierType.Temporary },
            { "modifier_lion_mana_drain", ModifierType.Temporary },

            { "modifier_lone_druid_spirit_link", ModifierType.Temporary },
            { "modifier_lone_druid_true_form_battle_cry", ModifierType.Temporary },
            { "modifier_lone_druid_savage_roar", ModifierType.Temporary },
            { "modifier_lone_druid_spirit_bear_defender", ModifierType.Aura },
            { "modifier_lone_druid_spirit_bear_entangle_effect", ModifierType.Temporary },

            { "modifier_luna_lunar_blessing_aura", ModifierType.Aura },
            { "modifier_luna_eclipse", ModifierType.TemporaryNoTime },

            { "modifier_lycan_howl", ModifierType.Temporary },
            { "modifier_lycan_feral_impulse", ModifierType.Aura },
            { "modifier_lycan_shapeshift", ModifierType.Temporary },

            { "modifier_magnataur_shockwave", ModifierType.Temporary },
            { "modifier_magnataur_empower", ModifierType.Temporary },
            { "modifier_magnataur_skewer_slow", ModifierType.Temporary },

            { "modifier_mars_spear_stun", ModifierType.Temporary },

            { "modifier_medusa_mana_shield", ModifierType.TemporaryNoTime },
            { "modifier_medusa_stone_gaze", ModifierType.Temporary },
            { "modifier_medusa_stone_gaze_stone", ModifierType.Temporary },
            { "modifier_medusa_stone_gaze_facing", ModifierType.Temporary },

            { "modifier_meepo_earthbind", ModifierType.Temporary },

            { "modifier_mirana_leap_buff", ModifierType.Temporary },
            //{ "modifier_mirana_leap_charge_counter", ModifierType.ChargeCounter },
            { "modifier_mirana_moonlight_shadow", ModifierType.Temporary },

            { "modifier_monkey_king_boundless_strike_stun", ModifierType.Temporary },
            { "modifier_monkey_king_spring_slow", ModifierType.Temporary },
            { "modifier_monkey_king_quadruple_tap_counter", ModifierType.Temporary },
            { "modifier_monkey_king_quadruple_tap_bonuses", ModifierType.Temporary },
            { "modifier_monkey_king_transform", ModifierType.TemporaryNoTime },

            { "modifier_morphling_adaptive_strike", ModifierType.Temporary },
            { "modifier_morphling_morph_agi", ModifierType.TemporaryNoTime },
            { "modifier_morphling_morph_str", ModifierType.TemporaryNoTime },
            { "modifier_morphling_replicate_manager", ModifierType.Temporary },
            //{ "modifier_morphling_waveform_charge_counter", ModifierType.ChargeCounter },

            { "modifier_furion_wrathofnature_spawn", ModifierType.Temporary },

            { "modifier_naga_siren_ensnare", ModifierType.Temporary },
            { "modifier_naga_siren_rip_tide", ModifierType.Temporary },
            { "modifier_naga_siren_song_of_the_siren_aura", ModifierType.Temporary },
            { "modifier_naga_siren_song_of_the_siren", ModifierType.TemporaryNoTime },

            { "modifier_necrolyte_sadist_active", ModifierType.Temporary },
            { "modifier_necrolyte_sadist_aura_effect", ModifierType.TemporaryNoTime },
            { "modifier_necrolyte_heartstopper_aura", ModifierType.Aura },
            { "modifier_necrolyte_heartstopper_aura_counter", ModifierType.Temporary },
            { "modifier_necrolyte_reapers_scythe", ModifierType.Temporary },

            { "modifier_night_stalker_void", ModifierType.Temporary },
            { "modifier_night_stalker_crippling_fear_aura", ModifierType.Temporary },
            { "modifier_night_stalker_crippling_fear", ModifierType.TemporaryNoTime },
            { "modifier_night_stalker_darkness", ModifierType.Temporary },

            { "modifier_nyx_assassin_impale", ModifierType.Temporary },
            { "modifier_nyx_assassin_spiked_carapace", ModifierType.Temporary },
            { "modifier_nyx_assassin_burrow", ModifierType.TemporaryNoTime },
            { "modifier_nyx_assassin_vendetta", ModifierType.Temporary },

            { "modifier_ogre_magi_ignite", ModifierType.Temporary },
            { "modifier_ogre_magi_bloodlust", ModifierType.Temporary },

            { "modifier_omniknight_repel", ModifierType.Temporary },
            { "modifier_omniknight_degen_aura_effect", ModifierType.Aura },
            { "modifier_omninight_guardian_angel", ModifierType.Temporary },

            { "modifier_oracle_fortunes_end_purge", ModifierType.Temporary },
            { "modifier_oracle_fates_edict", ModifierType.Temporary },
            { "modifier_oracle_purifying_flames", ModifierType.Temporary },
            { "modifier_oracle_false_promise_timer", ModifierType.Temporary },

            //{ "modifier_obsidian_destroyer_arcane_orb_buff_counter", ModifierType.Temporary },
            //{ "modifier_obsidian_destroyer_arcane_orb_debuff_counter", ModifierType.Temporary },
            { "modifier_obsidian_destroyer_astral_imprisonment_prison", ModifierType.Temporary },
            { "modifier_obsidian_destroyer_equilibrium_active", ModifierType.Temporary },
            { "modifier_obsidian_destroyer_equilibrium_debuff", ModifierType.Temporary },

            { "modifier_pangolier_shield_crash_buff", ModifierType.Temporary },
            { "modifier_pangolier_luckyshot_disarm", ModifierType.Temporary },
            { "modifier_pangolier_gyroshell", ModifierType.Temporary },

            { "modifier_phantom_assassin_stiflingdagger", ModifierType.Temporary },
            { "modifier_phantom_assassin_phantom_strike", ModifierType.Temporary },
            { "modifier_phantom_assassin_blur_active", ModifierType.Temporary },
            //{ "modifier_special_bonus_corruption_debuff", ModifierType.Temporary },

            { "modifier_phantom_lancer_spirit_lance", ModifierType.Temporary },
            { "modifier_phantom_lancer_phantom_edge_boost", ModifierType.Temporary },

            { "modifier_phoenix_icarus_dive", ModifierType.Temporary },
            { "modifier_phoenix_fire_spirit_count", ModifierType.Temporary },
            { "modifier_phoenix_fire_spirit_burn", ModifierType.Temporary },
            { "modifier_phoenix_sun_ray", ModifierType.Temporary },
            { "modifier_phoenix_sun", ModifierType.Temporary },
            { "modifier_phoenix_sun_debuff", ModifierType.TemporaryNoTime },

            { "modifier_puck_phase_shift", ModifierType.Temporary },
            { "modifier_puck_coiled", ModifierType.Temporary },

            { "modifier_pudge_meat_hook", ModifierType.TemporaryNoTime },
            { "modifier_pudge_rot", ModifierType.TemporaryNoTime },
            // { "modifier_pudge_flesh_heap", ModifierType.ChargeCounter },
            { "modifier_pudge_dismember", ModifierType.Temporary },

            { "modifier_pugna_life_drain", ModifierType.TemporaryNoTime },
            { "modifier_pugna_decrepify", ModifierType.Temporary },
            { "modifier_pugna_nether_ward_aura", ModifierType.Aura },

            { "modifier_queenofpain_shadow_strike", ModifierType.Temporary },
            { "modifier_queenofpain_scream_of_pain_fear", ModifierType.Temporary },

            { "modifier_razor_plasma_field_slow", ModifierType.Temporary },
            { "modifier_razor_static_link_buff", ModifierType.Temporary },
            { "modifier_razor_static_link_debuff", ModifierType.Temporary },
            { "modifier_razor_eye_of_the_storm", ModifierType.TemporaryNoTime },

            { "modifier_riki_smoke_screen", ModifierType.TemporaryNoTime },
            //{ "modifier_riki_tricks_of_the_trade_phase", ModifierType.Temporary }, //hidden

            { "modifier_rubick_fade_bolt_debuff", ModifierType.Temporary },
            { "modifier_rubick_spell_steal", ModifierType.Temporary },

            { "modifier_sandking_impale", ModifierType.Temporary },
            { "modifier_sand_king_caustic_finale_orb", ModifierType.Temporary },
            { "modifier_sand_king_epicenter", ModifierType.TemporaryNoTime },
            { "modifier_sand_king_epicenter_slow", ModifierType.Temporary },

            { "modifier_shadow_demon_disruption", ModifierType.Temporary },
            { "modifier_shadow_demon_soul_catcher", ModifierType.Temporary },
            { "modifier_shadow_demon_shadow_poison", ModifierType.Temporary },
            { "modifier_shadow_demon_purge_slow", ModifierType.Temporary },
            //{ "modifier_shadow_demon_disruption_charge_counter", ModifierType.ChargeCounter },
            //{ "modifier_shadow_demon_demonic_purge_charge_counter", ModifierType.ChargeCounter },

            { "modifier_nevermore_shadowraze_debuff", ModifierType.Temporary },
            //{ "modifier_nevermore_necromastery", ModifierType.ChargeCounter },
            { "modifier_nevermore_presence", ModifierType.Aura },
            { "modifier_nevermore_requiem", ModifierType.Temporary },

            { "modifier_shadow_shaman_voodoo", ModifierType.Temporary },
            { "modifier_shadow_shaman_shackles", ModifierType.Temporary },

            { "modifier_silencer_curse_of_the_silent", ModifierType.Temporary },
            { "modifier_silencer_last_word", ModifierType.Temporary },
            { "modifier_silencer_global_silence", ModifierType.Temporary },
            //{ "modifier_silencer_int_steal", ModifierType.ChargeCounter },

            { "modifier_skywrath_mage_concussive_shot_slow", ModifierType.Temporary },
            { "modifier_skywrath_mage_ancient_seal", ModifierType.Temporary },

            { "modifier_slardar_sprint", ModifierType.Temporary },
            { "modifier_slardar_sprint_river", ModifierType.TemporaryNoTime },
            // { "modifier_slardar_bash_active", ModifierType.ChargeCounter },
            { "modifier_slithereen_crush", ModifierType.Temporary },
            { "modifier_slardar_amplify_damage", ModifierType.Temporary },

            //{ "modifier_slark_dark_pact", ModifierType.Temporary }, // hidden
            //{ "modifier_slark_essence_shift", ModifierType.Temporary },
            //{ "modifier_slark_essence_shift_buff", ModifierType.Temporary },
            //{ "modifier_slark_essence_shift_debuff", ModifierType.Temporary },
            //{ "modifier_slark_essence_shift_permanent_buff", ModifierType.Temporary },
            { "modifier_slark_pounce_leash", ModifierType.Temporary },
            { "modifier_slark_shadow_dance", ModifierType.TemporaryNoTime },

            //{ "modifier_sniper_shrapnel_charge_counter", ModifierType.ChargeCounter },
            { "modifier_sniper_shrapnel_slow", ModifierType.TemporaryNoTime },
            { "modifier_sniper_take_aim_bonus", ModifierType.TemporaryNoTime },
            { "modifier_sniper_assassinate", ModifierType.Temporary },

            { "modifier_spectre_spectral_dagger", ModifierType.Temporary },
            //{ "modifier_spectre_spectral_dagger_in_path", ModifierType.TemporaryNoTime }, // no time while on path
            { "modifier_spectre_desolate_blind", ModifierType.Temporary },

            //{ "modifier_spirit_breaker_charge_of_darkness_vision", ModifierType.TemporaryNoTime }, // hidden
            { "modifier_spirit_breaker_bulldoze", ModifierType.Temporary },

            { "modifier_storm_spirit_overload", ModifierType.TemporaryNoTime },
            { "modifier_storm_spirit_overload_debuff", ModifierType.Temporary },
            { "modifier_storm_spirit_electric_vortex_pull", ModifierType.Temporary },
            //{ "modifier_storm_spirit_ball_lightning", ModifierType.TemporaryNoTime },

            { "modifier_sven_warcry", ModifierType.Temporary },
            { "modifier_sven_gods_strength", ModifierType.Temporary },
            { "modifier_sven_gods_strength_child", ModifierType.Aura },

            { "modifier_techies_stasis_trap_stunned", ModifierType.Temporary },

            { "modifier_templar_assassin_refraction_absorb", ModifierType.Temporary },
            { "modifier_templar_assassin_refraction_damage", ModifierType.Temporary },
            { "modifier_templar_assassin_meld", ModifierType.TemporaryNoTime },
            { "modifier_templar_assassin_trap_slow", ModifierType.Temporary },
            //{ "modifier_templar_assassin_psionic_trap_counter", ModifierType.ChargeCounter },

            { "modifier_terrorblade_reflection_slow", ModifierType.Temporary },
            { "modifier_terrorblade_metamorphosis", ModifierType.Temporary },
            { "modifier_terrorblade_fear", ModifierType.Temporary },

            { "modifier_tidehunter_gush", ModifierType.Temporary },
            { "modifier_tidehunter_anchor_smash", ModifierType.Temporary },
            { "modifier_tidehunter_ravage", ModifierType.Temporary },

            { "modifier_shredder_whirling_death_debuff", ModifierType.Temporary },
            // { "modifier_shredder_reactive_armor_stack", ModifierType.ChargeCounter },
            { "modifier_shredder_chakram_debuff", ModifierType.TemporaryNoTime },

            { "modifier_tinker_laser_blind", ModifierType.Temporary },
            { "modifier_tinker_rearm", ModifierType.Temporary },

            { "modifier_tiny_avalanche_stun", ModifierType.Temporary },
            // { "modifier_tiny_craggy_exterior", ModifierType.ChargeCounter },

            { "modifier_treant_leech_seed", ModifierType.Temporary },
            { "modifier_treant_living_armor", ModifierType.Temporary },
            { "modifier_treant_overgrowth", ModifierType.Temporary },

            { "modifier_troll_warlord_berserkers_rage_ensnare", ModifierType.Temporary },
            { "modifier_troll_warlord_whirling_axes_slow", ModifierType.Temporary },
            { "modifier_troll_warlord_whirling_axes_blind", ModifierType.Temporary },
            //{ "modifier_troll_warlord_fervor", ModifierType.ChargeCounter },
            { "modifier_troll_warlord_battle_trance", ModifierType.Temporary },

            { "modifier_tusk_tag_team_aura", ModifierType.Temporary },
            { "modifier_tusk_walrus_punch_slow", ModifierType.Temporary },
            { "modifier_tusk_walrus_kick_slow", ModifierType.Temporary },

            { "modifier_abyssal_underlord_firestorm_burn", ModifierType.Temporary },
            { "modifier_abyssal_underlord_pit_of_malice_ensare", ModifierType.Temporary },
            { "modifier_abyssal_underlord_atrophy_aura_effect", ModifierType.Aura },
            { "modifier_abyssal_underlord_atrophy_aura_scepter", ModifierType.Aura },
            //{ "modifier_abyssal_underlord_dark_rift", ModifierType.Temporary }, //hidden

            { "modifier_undying_decay_buff", ModifierType.Temporary },
            // { "modifier_undying_tombstone_zombie_deathstrike_slow_counter", ModifierType.TemporaryNoTime }, // hidden
            { "modifier_undying_flesh_golem", ModifierType.Temporary },
            { "modifier_undying_flesh_golem_plague_aura", ModifierType.Aura },

            { "modifier_ursa_earthshock", ModifierType.Temporary },
            { "modifier_ursa_overpower", ModifierType.Temporary },
            { "modifier_ursa_enrage", ModifierType.Temporary },

            { "modifier_vengefulspirit_wave_of_terror", ModifierType.Temporary },
            { "modifier_vengefulspirit_command_aura_effect", ModifierType.Aura },
            //{ "modifier_vengefulspirit_nether_swap_charge_counter", ModifierType.ChargeCounter },

            { "modifier_venomancer_venomous_gale", ModifierType.Temporary },
            { "modifier_venomancer_poison_sting", ModifierType.Temporary },
            { "modifier_venomancer_poison_nova", ModifierType.Temporary },

            { "modifier_viper_poison_attack_slow", ModifierType.Temporary },
            { "modifier_viper_nethertoxin", ModifierType.TemporaryNoTime },
            { "modifier_viper_corrosive_skin_slow", ModifierType.Temporary },
            { "modifier_viper_viper_strike_slow", ModifierType.Temporary },

            { "modifier_visage_grave_chill_buff", ModifierType.Temporary },
            { "modifier_visage_grave_chill_debuff", ModifierType.Temporary },
            //{ "modifier_visage_gravekeepers_cloak", ModifierType.TemporaryNoTime }, // hiden
            { "modifier_visage_summon_familiars_stone_form_buff", ModifierType.Temporary },

            { "modifier_warlock_fatal_bonds", ModifierType.Temporary },
            { "modifier_warlock_shadow_word", ModifierType.Temporary },
            { "modifier_warlock_upheaval", ModifierType.Temporary },
            { "modifier_warlock_golem_permanent_immolation_debuff", ModifierType.TemporaryNoTime },

            { "modifier_weaver_swarm", ModifierType.TemporaryNoTime },
            { "modifier_weaver_shukuchi", ModifierType.Temporary },

            { "modifier_windrunner_shackle_shot", ModifierType.Temporary },
            { "modifier_windrunner_windrun", ModifierType.Temporary },
            { "modifier_windrunner_windrun_slow", ModifierType.TemporaryNoTime },
            { "modifier_windrunner_focusfire", ModifierType.Temporary },
            // { "modifier_windrunner_windrun_charge_counter", ModifierType.ChargeCounter },

            { "modifier_winter_wyvern_arctic_burn_flight", ModifierType.Temporary },
            { "modifier_winter_wyvern_splinter_blast_slow", ModifierType.Temporary },
            { "modifier_winter_wyvern_cold_embrace", ModifierType.Temporary },
            { "modifier_winter_wyvern_winters_curse_aura", ModifierType.Temporary },
            { "modifier_winter_wyvern_winters_curse", ModifierType.TemporaryNoTime },

            { "modifier_voodoo_restoration_aura", ModifierType.Aura },
            { "modifier_maledict_dot", ModifierType.Temporary },

            { "modifier_skeleton_king_hellfire_blast", ModifierType.Temporary },
            { "modifier_skeleton_king_vampiric_aura_buff", ModifierType.Aura },
            //{ "modifier_skeleton_king_mortal_strike", ModifierType.ChargeCounter },
            { "modifier_skeleton_king_reincarnate_slow", ModifierType.Temporary },

            { "modifier_void_spirit_aether_remnant_pull", ModifierType.Temporary },
            { "modifier_void_spirit_resonant_pulse_physical_buff", ModifierType.Temporary },
            { "modifier_void_spirit_astral_step_debuff", ModifierType.Temporary },

            { "modifier_snapfire_scatterblast_disarm", ModifierType.Temporary },
            { "modifier_snapfire_lil_shredder_buff", ModifierType.Temporary },
            { "modifier_snapfire_lil_shredder_debuff", ModifierType.Temporary },
            { "modifier_snapfire_mortimer_kisses", ModifierType.Temporary },
            { "modifier_snapfire_magma_burn_slow", ModifierType.Temporary },
            { "modifier_snapfire_gobble_up_belly_has_unit", ModifierType.Temporary },

            { "modifier_teleporting", ModifierType.Temporary },
            { "modifier_clarity_potion", ModifierType.Temporary },
            { "modifier_smoke_of_deceit", ModifierType.Temporary },
            { "modifier_tango_heal", ModifierType.Temporary },
            { "modifier_flask_healing", ModifierType.Temporary },
            { "modifier_item_dustofappearance", ModifierType.Temporary },
            { "modifier_bottle_regeneration", ModifierType.Temporary },
            { "modifier_item_orb_of_venom_slow", ModifierType.Temporary },
            { "modifier_blight_stone_buff", ModifierType.Temporary },
            { "modifier_item_shadow_amulet_fade", ModifierType.TemporaryNoTime },
            { "modifier_ghost_state", ModifierType.Temporary },
            { "modifier_item_soul_ring_buff", ModifierType.Temporary },
            { "modifier_item_phase_boots_active", ModifierType.Temporary },
            { "modifier_item_mask_of_madness_berserk", ModifierType.Temporary },
            // { "modifier_item_moon_shard_consumed", ModifierType.TemporaryNoTime },
            { "modifier_item_ring_of_basilius_aura_bonus", ModifierType.Aura },
            { "modifier_item_buckler_effect", ModifierType.Temporary },
            { "modifier_item_urn_heal", ModifierType.Temporary },
            { "modifier_item_urn_damage", ModifierType.Temporary },
            { "modifier_item_medallion_of_courage_armor_reduction", ModifierType.Temporary },
            { "modifier_item_medallion_of_courage_armor_addition", ModifierType.Temporary },
            { "modifier_item_ancient_janggo_active", ModifierType.Temporary },
            { "modifier_item_vladmir_aura", ModifierType.Aura },
            { "modifier_item_mekansm_aura", ModifierType.Aura },
            { "modifier_item_spirit_vessel_heal", ModifierType.Temporary },
            { "modifier_item_spirit_vessel_damage", ModifierType.Temporary },
            { "modifier_item_pipe_aura", ModifierType.Aura },
            { "modifier_item_pipe_barrier", ModifierType.Temporary },
            { "modifier_item_guardian_greaves_aura", ModifierType.Aura },
            //  { "modifier_item_glimmer_cape_fade", ModifierType.Temporary }, //invisible
            { "modifier_item_veil_of_discord_debuff", ModifierType.Temporary },
            { "modifier_eul_cyclone", ModifierType.Temporary },
            { "modifier_rod_of_atos_debuff", ModifierType.Temporary },
            { "modifier_item_solar_crest_armor_addition", ModifierType.Temporary },
            { "modifier_item_solar_crest_armor_reduction", ModifierType.Temporary },
            { "modifier_orchid_malevolence_debuff", ModifierType.Temporary },
            { "modifier_item_nullifier_mute", ModifierType.Temporary },
            { "modifier_sheepstick_debuff", ModifierType.Temporary },
            { "modifier_item_hood_of_defiance_barrier", ModifierType.Temporary },
            { "modifier_item_blade_mail_reflect", ModifierType.Temporary },
            { "modifier_item_aeon_disk_buff", ModifierType.Temporary },
            { "modifier_item_crimson_guard_extra", ModifierType.Temporary },
            { "modifier_item_lotus_orb_active", ModifierType.Temporary },
            { "modifier_black_king_bar_immune", ModifierType.Temporary },
            { "modifier_item_shivas_guard_aura", ModifierType.Aura },
            { "modifier_item_shivas_guard_blast", ModifierType.Temporary },
            { "modifier_item_bloodstone_active", ModifierType.Temporary },
            { "modifier_item_sphere_target", ModifierType.Temporary },
            { "modifier_item_assault_negative_armor", ModifierType.Aura },
            { "modifier_item_armlet_unholy_strength", ModifierType.TemporaryNoTime },
            { "modifier_item_meteor_hammer_burn", ModifierType.Temporary },
            { "modifier_item_invisibility_edge_windwalk", ModifierType.Temporary },
            { "modifier_item_ethereal_blade_ethereal", ModifierType.Temporary },
            { "modifier_item_radiance_debuff", ModifierType.Aura },
            { "modifier_item_silver_edge_windwalk", ModifierType.Temporary },
            { "modifier_bloodthorn_debuff", ModifierType.Temporary },
            { "modifier_item_diffusal_blade_slow", ModifierType.Temporary },
            { "modifier_heavens_halberd_debuff", ModifierType.Temporary },
            { "modifier_desolator_buff", ModifierType.Temporary },
            { "modifier_item_satanic_unholy", ModifierType.Temporary },
            { "modifier_item_skadi_slow", ModifierType.Temporary },
            { "modifier_item_mjollnir_static", ModifierType.Temporary },

            { "modifier_clumsy_net_ensnare", ModifierType.Temporary },
            { "modifier_royal_jelly", ModifierType.Aura },
            { "modifier_item_ocean_heart", ModifierType.TemporaryNoTime },
            { "modifier_item_essence_ring_active", ModifierType.Temporary },
            { "modifier_item_spider_legs_active", ModifierType.Temporary },
            { "modifier_orb_of_destruction_debuff", ModifierType.Temporary },
            { "modifier_item_spy_gadget", ModifierType.TemporaryNoTime },
            { "modifier_item_princes_knife_hex", ModifierType.Temporary },
            { "modifier_havoc_hammer_slow", ModifierType.Temporary },
            { "modifier_minotaur_horn_immune", ModifierType.Temporary },
            { "modifier_woodland_striders_active", ModifierType.Temporary },
        };

        private readonly HashSet<string> unitNames = new HashSet<string>
        {
            "npc_dota_phoenix_sun",
            "npc_dota_brewmaster_earth_1",
            "npc_dota_brewmaster_earth_2",
            "npc_dota_brewmaster_earth_3",
            "npc_dota_visage_familiar1",
            "npc_dota_visage_familiar2",
            "npc_dota_visage_familiar3",
        };
    }
}