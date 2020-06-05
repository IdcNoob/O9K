namespace O9K.Core.Entities.Abilities.Heroes.CentaurWarrunner
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.centaur_double_edge)]
    public class DoubleEdge : RangedAbility, INuke
    {
        private readonly SpecialData strengthDamageData;

        public DoubleEdge(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "edge_damage");
            this.strengthDamageData = new SpecialData(baseAbility, "strength_damage");
        }

        public override float CastRange
        {
            get
            {
                return base.CastRange + 100;
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return new Damage
            {
                [this.DamageType] = this.DamageData.GetValue(this.Level)
                                    + ((this.Owner.TotalStrength * this.strengthDamageData.GetValue(this.Level)) / 100)
            };
        }
    }
}