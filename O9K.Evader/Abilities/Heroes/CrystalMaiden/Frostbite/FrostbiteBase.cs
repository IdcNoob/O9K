namespace O9K.Evader.Abilities.Heroes.CrystalMaiden.Frostbite
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.crystal_maiden_frostbite)]
    internal class FrostbiteBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public FrostbiteBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FrostbiteEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}