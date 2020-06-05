namespace O9K.Evader.Abilities.Heroes.Huskar.InnerFire
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.huskar_inner_fire)]
    internal class InnerFireBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public InnerFireBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new InnerFireEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}