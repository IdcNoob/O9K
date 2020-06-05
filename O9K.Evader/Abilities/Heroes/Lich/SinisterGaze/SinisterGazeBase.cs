namespace O9K.Evader.Abilities.Heroes.Lich.SinisterGaze
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lich_sinister_gaze)]
    internal class SinisterGazeBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public SinisterGazeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SinisterGazeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}