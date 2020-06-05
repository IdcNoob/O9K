namespace O9K.Evader.Abilities.Heroes.Batrider.FlamingLasso
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.batrider_flaming_lasso)]
    internal class FlamingLassoBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public FlamingLassoBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FlamingLassoEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}