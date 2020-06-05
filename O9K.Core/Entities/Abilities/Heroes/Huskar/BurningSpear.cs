namespace O9K.Core.Entities.Abilities.Heroes.Huskar
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.huskar_burning_spear)]
    public class BurningSpear : OrbAbility, IHarass
    {
        public BurningSpear(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool CanHitSpellImmuneEnemy
        {
            get
            {
                //todo change
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_huskar_5);
                if (talent?.Level > 0)
                {
                    return true;
                }

                return base.CanHitSpellImmuneEnemy;
            }
        }
    }
}