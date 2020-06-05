namespace O9K.Evader.Abilities.Heroes.Io.Tether
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.wisp_tether)]
    internal class TetherBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public TetherBase(Ability9 ability)
            : base(ability)
        {
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkTargetableAbility(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}