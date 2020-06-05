namespace O9K.Evader.Abilities.Heroes.Puck.PhaseShift
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.puck_phase_shift)]
    internal class PhaseShiftBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public PhaseShiftBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new PhaseShiftUsable(this.Ability, this.ActionManager, this.Menu);
        }
    }
}