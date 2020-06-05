namespace O9K.AIO.Heroes.Dynamic.Abilities.Buffs.Unique
{
    using AIO.Modes.Combo;

    using Blinks;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.item_manta)]
    internal class MantaStyleBuff : OldBuffAbility
    {
        public MantaStyleBuff(IBuff ability)
            : base(ability)
        {
        }

        protected override bool ShouldCastBuff(Unit9 target, BlinkAbilityGroup blinks, ComboModeMenu menu)
        {
            var distance = this.Ability.Owner.Distance(target);
            var attackRange = this.Ability.Owner.GetAttackRange(target);

            if (distance > attackRange)
            {
                return false;
            }

            return true;
        }
    }
}