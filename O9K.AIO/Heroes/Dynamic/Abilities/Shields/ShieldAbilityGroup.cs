namespace O9K.AIO.Heroes.Dynamic.Abilities.Shields
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Modes.Combo;

    using Base;

    using Blinks;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;

    internal class ShieldAbilityGroup : OldAbilityGroup<IShield, OldShieldAbility>
    {
        public ShieldAbilityGroup(BaseHero baseHero)
            : base(baseHero)
        {
        }

        public BlinkAbilityGroup Blinks { get; set; }

        protected override HashSet<AbilityId> Ignored { get; } = new HashSet<AbilityId>
        {
            AbilityId.item_ethereal_blade,
            AbilityId.dark_willow_shadow_realm,
            AbilityId.puck_phase_shift,
            AbilityId.obsidian_destroyer_astral_imprisonment,
            AbilityId.bane_nightmare,
            AbilityId.chen_holy_persuasion,
            AbilityId.dazzle_shallow_grave,
            AbilityId.earth_spirit_petrify,
            AbilityId.life_stealer_assimilate,
            AbilityId.naga_siren_song_of_the_siren,
            AbilityId.necrolyte_sadist,
            AbilityId.omniknight_guardian_angel,
            AbilityId.oracle_false_promise,
            AbilityId.oracle_fates_edict,
            AbilityId.phoenix_supernova,
            AbilityId.pugna_decrepify,
            AbilityId.shadow_demon_disruption,
            AbilityId.slark_shadow_dance,
            AbilityId.vengefulspirit_nether_swap,
            AbilityId.weaver_time_lapse,
            AbilityId.winter_wyvern_cold_embrace,
            AbilityId.item_cyclone,
            AbilityId.item_sphere,
        };

        public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
        {
            foreach (var ability in this.Abilities)
            {
                if (!ability.Ability.IsValid)
                {
                    continue;
                }

                if (except.Contains(ability.Ability.Id))
                {
                    continue;
                }

                if (!ability.CanBeCasted(ability.Shield.Owner, target, this.Blinks, menu))
                {
                    continue;
                }

                if (ability.Use(ability.Shield.Owner))
                {
                    return true;
                }
            }

            return false;
        }

        protected override bool IsIgnored(Ability9 ability)
        {
            if (base.IsIgnored(ability))
            {
                return true;
            }

            return ability is IDisable disable && disable.IsInvulnerability();
        }
    }
}