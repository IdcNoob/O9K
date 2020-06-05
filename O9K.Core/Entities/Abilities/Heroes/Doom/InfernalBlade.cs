namespace O9K.Core.Entities.Abilities.Heroes.Doom
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.doom_bringer_infernal_blade)]
    public class InfernalBlade : OrbAbility, IHarass
    {
        public InfernalBlade(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}