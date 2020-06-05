namespace O9K.Core.Entities.Abilities.Heroes.OgreMagi
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.ogre_magi_ignite)]
    public class Ignite : RangedAbility, IDebuff
    {
        public Ignite(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
        }

        public string DebuffModifierName { get; } = "modifier_ogre_magi_ignite";
    }
}