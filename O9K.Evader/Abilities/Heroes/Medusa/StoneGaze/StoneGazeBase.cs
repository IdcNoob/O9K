namespace O9K.Evader.Abilities.Heroes.Medusa.StoneGaze
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.medusa_stone_gaze)]
    internal class StoneGazeBase : EvaderBaseAbility, IEvadable
    {
        public StoneGazeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new StoneGazeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}