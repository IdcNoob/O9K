namespace O9K.Evader.Abilities.Heroes.Windranger.Shackleshot
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.windrunner_shackleshot)]
    internal class ShackleshotBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public ShackleshotBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShackleshotEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}