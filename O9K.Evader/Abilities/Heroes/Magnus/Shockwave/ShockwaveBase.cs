namespace O9K.Evader.Abilities.Heroes.Magnus.Shockwave
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.magnataur_shockwave)]
    internal class ShockwaveBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ShockwaveBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShockwaveEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}