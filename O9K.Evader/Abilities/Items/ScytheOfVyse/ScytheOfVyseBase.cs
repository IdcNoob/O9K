namespace O9K.Evader.Abilities.Items.ScytheOfVyse
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_sheepstick)]
    internal class ScytheOfVyseBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public ScytheOfVyseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ScytheOfVyseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}