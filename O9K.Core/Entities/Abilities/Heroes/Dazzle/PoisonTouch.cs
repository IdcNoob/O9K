namespace O9K.Core.Entities.Abilities.Heroes.Dazzle
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.dazzle_poison_touch)]
    public class PoisonTouch : RangedAbility, IDebuff
    {
        public PoisonTouch(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
        }

        public string DebuffModifierName { get; set; }
    }
}