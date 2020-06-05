namespace O9K.Evader.Abilities.Heroes.Slardar.CorrosiveHaze
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.slardar_amplify_damage)]
    internal class CorrosiveHazeBase : EvaderBaseAbility, IEvadable
    {
        public CorrosiveHazeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CorrosiveHazeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}