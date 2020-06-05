namespace O9K.Evader.Abilities.Heroes.Leshrac.SplitEarth
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.leshrac_split_earth)]
    internal class SplitEarthBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public SplitEarthBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SplitEarthEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}