namespace O9K.Core.Entities.Abilities.Heroes.Tiny
{
    using Base;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.tiny_toss_tree)]
    public class TreeThrow : LineAbility
    {
        public TreeThrow(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "splash_radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public override bool BreaksLinkens { get; } = false;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return this.Owner.GetRawAttackDamage(unit);
        }
    }
}