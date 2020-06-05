namespace O9K.Evader.Abilities.Heroes.SandKing.Burrowstrike
{
    using Base;
    using Base.Evadable;
    using Base.Usable.BlinkAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.sandking_burrowstrike)]
    internal class BurrowstrikeBase : EvaderBaseAbility, IEvadable, IUsable<BlinkAbility>, IUsable<DisableAbility>
    {
        public BurrowstrikeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BurrowstrikeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        BlinkAbility IUsable<BlinkAbility>.GetUsableAbility()
        {
            return new BurrowstrikeUsableBlink(this.Ability, this.Pathfinder, this.Menu);
        }

        DisableAbility IUsable<DisableAbility>.GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}