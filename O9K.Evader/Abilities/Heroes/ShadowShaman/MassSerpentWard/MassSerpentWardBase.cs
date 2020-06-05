namespace O9K.Evader.Abilities.Heroes.ShadowShaman.MassSerpentWard
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shadow_shaman_mass_serpent_ward)]
    internal class MassSerpentWardBase : EvaderBaseAbility, IEvadable
    {
        public MassSerpentWardBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MassSerpentWardEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}