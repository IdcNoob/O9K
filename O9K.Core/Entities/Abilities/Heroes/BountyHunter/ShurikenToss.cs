namespace O9K.Core.Entities.Abilities.Heroes.BountyHunter
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.bounty_hunter_shuriken_toss)]
    public class ShurikenToss : RangedAbility, INuke
    {
        public ShurikenToss(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "bonus_damage");
        }
    }
}