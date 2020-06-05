namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.tusk_walrus_kick)]
    public class WalrusKick : RangedAbility, INuke
    {
        public WalrusKick(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public override float CastRange
        {
            get
            {
                return base.CastRange + 100;
            }
        }
    }
}