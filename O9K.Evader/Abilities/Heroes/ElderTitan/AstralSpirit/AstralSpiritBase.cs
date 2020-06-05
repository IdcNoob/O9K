namespace O9K.Evader.Abilities.Heroes.ElderTitan.AstralSpirit
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.elder_titan_ancestral_spirit)]
    internal class AstralSpiritBase : EvaderBaseAbility, IUsable<CounterEnemyAbility>
    {
        public AstralSpiritBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}