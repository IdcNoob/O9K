namespace O9K.Evader.Abilities.Heroes.Enigma.Malefice
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.enigma_malefice)]
    internal class MaleficeBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public MaleficeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MaleficeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}