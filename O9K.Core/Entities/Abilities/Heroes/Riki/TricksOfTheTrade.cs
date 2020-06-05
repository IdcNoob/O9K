namespace O9K.Core.Entities.Abilities.Heroes.Riki
{
    using System;
    using System.Linq;

    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.riki_tricks_of_the_trade)]
    public class TricksOfTheTrade : CircleAbility, IChanneled, IShield
    {
        private readonly SpecialData attackCountData;

        private readonly SpecialData attackCountScepterData;

        private readonly SpecialData castRangeScepterTimeData;

        private readonly SpecialData channelScepterTimeData;

        private readonly SpecialData channelTimeData;

        private readonly SpecialData damageMultiplierData;

        private CloakAndDagger cloakAndDagger;

        public TricksOfTheTrade(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.attackCountData = new SpecialData(baseAbility, "attack_count");
            this.attackCountScepterData = new SpecialData(baseAbility, "scepter_attacks");
            this.damageMultiplierData = new SpecialData(baseAbility, "damage_pct");
            this.channelScepterTimeData = new SpecialData(baseAbility, "scepter_duration");
            this.castRangeScepterTimeData = new SpecialData(baseAbility, "scepter_cast_range");
            this.channelTimeData = new SpecialData(baseAbility, (Func<uint, float>)baseAbility.GetChannelTime);
        }

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public float AttackCount
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.attackCountScepterData.GetValue(this.Level);
                }

                return this.attackCountData.GetValue(this.Level);
            }
        }

        public float AttackRate
        {
            get
            {
                return this.ChannelTime / this.AttackCount;
            }
        }

        public override float CastRange
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.castRangeScepterTimeData.GetValue(this.Level);
                }

                return base.CastRange;
            }
        }

        public float ChannelTime
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.channelScepterTimeData.GetValue(this.Level);
                }

                return this.channelTimeData.GetValue(this.Level);
            }
        }

        public bool IsActivatesOnChannelStart { get; } = true;

        public string ShieldModifierName { get; } = "modifier_riki_tricks_of_the_trade_phase";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var autoAttackDamage = this.Owner.GetRawAttackDamage(unit);
            var damage = autoAttackDamage + this.cloakAndDagger?.GetRawDamage(unit);

            damage[this.DamageType] *= (this.damageMultiplierData.GetValue(this.Level) / 100f);

            return damage;
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.riki_backstab && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                return;
            }

            this.cloakAndDagger = (CloakAndDagger)EntityManager9.AddAbility(ability);
        }
    }
}