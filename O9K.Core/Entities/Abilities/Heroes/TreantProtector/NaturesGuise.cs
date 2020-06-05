namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.treant_natures_guise)]
    public class NaturesGuise : PassiveAbility
    {
        private bool isInvisibility;

        public NaturesGuise(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool IsInvisibility
        {
            get
            {
                if (this.isInvisibility)
                {
                    return true;
                }

                return this.isInvisibility = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_treant_4)?.Level > 0;
            }
        }
    }
}