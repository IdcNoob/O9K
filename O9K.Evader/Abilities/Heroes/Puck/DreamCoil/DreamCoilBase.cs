namespace O9K.Evader.Abilities.Heroes.Puck.DreamCoil
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.puck_dream_coil)]
    internal class DreamCoilBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public DreamCoilBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DreamCoilEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}