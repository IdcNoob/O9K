namespace O9K.Core.Entities.Abilities.Heroes.PhantomLancer
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.phantom_lancer_phantom_edge)]
    public class PhantomRush : ToggleAbility
    {
        private readonly SpecialData rangeData;

        public PhantomRush(Ability baseAbility)
            : base(baseAbility)
        {
            this.rangeData = new SpecialData(baseAbility, "max_distance");
        }

        public override float Radius
        {
            get
            {
                // for hud range
                return this.Range;
            }
        }

        public override float Range
        {
            get
            {
                return this.rangeData.GetValue(this.Level);
            }
        }
    }
}