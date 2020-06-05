namespace O9K.AIO.Heroes.Dynamic.Abilities.Harasses
{
    using Base;

    using Core.Entities.Abilities.Base.Types;

    internal class HarassAbilityGroup : OldAbilityGroup<IHarass, OldHarassAbility>
    {
        public HarassAbilityGroup(BaseHero baseHero)
            : base(baseHero)
        {
        }
    }
}