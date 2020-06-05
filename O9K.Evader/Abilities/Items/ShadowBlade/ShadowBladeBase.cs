namespace O9K.Evader.Abilities.Items.ShadowBlade
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_invis_sword)]
    internal class ShadowBladeBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>, IUsable<DodgeAbility>
    {
        public ShadowBladeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShadowBladeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            return new CounterInvisibilityAbility(this.Ability, this.Menu);
        }

        DodgeAbility IUsable<DodgeAbility>.GetUsableAbility()
        {
            return new DodgeAbility(this.Ability, this.Menu);
        }
    }
}