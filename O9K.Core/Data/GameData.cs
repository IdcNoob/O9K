namespace O9K.Core.Data
{
    using System;
    using System.Collections.Generic;

    using Ensage;

    using Helpers;

    using SharpDX;

    public static class GameData
    {
        public const int AegisExpirationTime = 5 * 60;

        public const int BountyRuneRespawnTime = 5 * 60;

        public const int BuybackCooldown = 8 * 60;

        public const int CreepSpeed = 325;

        public const int DefaultZoom = 1200;

        public const int ExperienceRange = 1500;

        public const int GlyphDuration = 6;

        public const int JungleCreepsSpawnStartTime = 1 * 60;

        public const int MaxHeroLevel = 30;

        public const int OutpostExperienceTime = 10 * 60;

        public const int RoshanMaxRespawnTime = 11 * 60;

        public const int RoshanMinRespawnTime = 8 * 60;

        public const int RuneRespawnTime = 2 * 60;

        public const int ScanActiveTime = 8;

        public const int ScanRadius = 900;

        internal const int HealthGainPerStrength = 20;

        internal const int MaxAttackSpeed = 700;

        internal const int MaxMovementSpeed = 550;

        internal const int MinAttackSpeed = 20;

        public static IReadOnlyDictionary<AbilityId, int> AbilityVision { get; } = new Dictionary<AbilityId, int>
        {
            { AbilityId.shredder_timber_chain, 100 },
            { AbilityId.rattletrap_hookshot, 100 },
            { AbilityId.weaver_the_swarm, 100 },
            { AbilityId.grimstroke_dark_artistry, 160 },
            { AbilityId.tusk_ice_shards, 200 },
            { AbilityId.tiny_toss_tree, 200 },
            { AbilityId.batrider_flamebreak, 300 },
            { AbilityId.medusa_mystic_snake, 300 },
            { AbilityId.oracle_fortunes_end, 300 },
            { AbilityId.meepo_earthbind, 300 },
            { AbilityId.kunkka_ghostship, 400 },
            { AbilityId.venomancer_venomous_gale, 400 },
            { AbilityId.shadow_demon_shadow_poison, 400 },
            { AbilityId.disruptor_glimpse, 400 },
            { AbilityId.phantom_assassin_stifling_dagger, 450 },
            { AbilityId.ancient_apparition_ice_blast, 550 },
            { AbilityId.terrorblade_metamorphosis, 575 },
            { AbilityId.razor_plasma_field, 800 },

            { AbilityId.invoker_tornado, (int)new SpecialData(AbilityId.invoker_tornado, "vision_distance").GetValue(1) },
            {
                AbilityId.snapfire_mortimer_kisses,
                (int)new SpecialData(AbilityId.snapfire_mortimer_kisses, "projectile_vision").GetValue(1)
            },
            { AbilityId.sven_storm_bolt, (int)new SpecialData(AbilityId.sven_storm_bolt, "vision_radius").GetValue(1) },
            {
                AbilityId.arc_warden_spark_wraith,
                (int)new SpecialData(AbilityId.arc_warden_spark_wraith, "wraith_vision_radius").GetValue(1)
            },
            { AbilityId.mars_spear, (int)new SpecialData(AbilityId.mars_spear, "spear_vision").GetValue(1) },
            {
                AbilityId.skywrath_mage_concussive_shot,
                (int)new SpecialData(AbilityId.skywrath_mage_concussive_shot, "shot_vision").GetValue(1)
            },
            {
                AbilityId.vengefulspirit_wave_of_terror,
                (int)new SpecialData(AbilityId.vengefulspirit_wave_of_terror, "vision_aoe").GetValue(1)
            },
            { AbilityId.skywrath_mage_arcane_bolt, (int)new SpecialData(AbilityId.skywrath_mage_arcane_bolt, "bolt_vision").GetValue(1) },
            {
                AbilityId.storm_spirit_ball_lightning,
                (int)new SpecialData(AbilityId.storm_spirit_ball_lightning, "ball_lightning_vision_radius").GetValue(1)
            },
            { AbilityId.windrunner_powershot, (int)new SpecialData(AbilityId.windrunner_powershot, "vision_radius").GetValue(1) },
            { AbilityId.puck_illusory_orb, (int)new SpecialData(AbilityId.puck_illusory_orb, "orb_vision").GetValue(1) },
            { AbilityId.mirana_arrow, (int)new SpecialData(AbilityId.mirana_arrow, "arrow_vision").GetValue(1) },
            { AbilityId.invoker_chaos_meteor, (int)new SpecialData(AbilityId.invoker_chaos_meteor, "vision_distance").GetValue(1) },
            { AbilityId.rattletrap_rocket_flare, (int)new SpecialData(AbilityId.rattletrap_rocket_flare, "vision_radius").GetValue(1) },
            { AbilityId.lich_chain_frost, (int)new SpecialData(AbilityId.lich_chain_frost, "vision_radius").GetValue(1) },
        };

        public static string DisplayTime
        {
            get
            {
                return TimeSpan.FromSeconds(Game.GameTime).ToString(@"mm\:ss");
            }
        }

        public static Vector3 RoshanPosition { get; } = new Vector3(-2806, 2281, 160);

        internal static int HurricanePikeBonusAttackSpeed { get; } =
            (int)new SpecialData(AbilityId.item_hurricane_pike, "bonus_attack_speed").GetValue(1);
    }
}