namespace O9K.Core.Entities.Abilities.Heroes.VengefulSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.vengefulspirit_magic_missile)]
    public class MagicMissile : RangedAbility, IDisable, INuke
    {
        public MagicMissile(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "magic_missile_speed");
            this.DamageData = new SpecialData(baseAbility, "magic_missile_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override bool CanHitSpellImmuneEnemy
        {
            get
            {
                if (this.Owner.GetAbilityById(AbilityId.special_bonus_unique_vengeful_spirit_3)?.Level > 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}