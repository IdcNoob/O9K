namespace O9K.Core.Entities.Abilities.Heroes.Alchemist
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.alchemist_unstable_concoction)]
    public class UnstableConcoction : ActiveAbility
    {
        private readonly SpecialData brewExplosionTimeData;

        public UnstableConcoction(Ability baseAbility)
            : base(baseAbility)
        {
            this.brewExplosionTimeData = new SpecialData(baseAbility, "brew_explosion");
        }

        public float BrewExplosion
        {
            get
            {
                return this.brewExplosionTimeData.GetValue(this.Level);
            }
        }
    }
}