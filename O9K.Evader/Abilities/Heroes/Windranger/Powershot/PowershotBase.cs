namespace O9K.Evader.Abilities.Heroes.Windranger.Powershot
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.windrunner_powershot)]
    internal class PowershotBase : EvaderBaseAbility, IEvadable
    {
        public PowershotBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PowershotEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}