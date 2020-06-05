namespace O9K.Core.Entities.Abilities.Heroes.Lina
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.lina_laguna_blade)]
    public class LagunaBlade : RangedAbility, INuke
    {
        public LagunaBlade(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "damage_delay");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public override bool CanHitSpellImmuneEnemy
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return true;
                }

                return false;
            }
        }

        public override DamageType DamageType
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return DamageType.Pure;
                }

                return base.DamageType;
            }
        }
    }
}