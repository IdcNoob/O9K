namespace O9K.Evader.Abilities.Heroes.ChaosKnight.Phantasm
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.chaos_knight_phantasm)]
    internal class PhantasmBase : EvaderBaseAbility, IEvadable
    {
        public PhantasmBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PhantasmEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}