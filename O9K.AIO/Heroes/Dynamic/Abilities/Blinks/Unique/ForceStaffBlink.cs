namespace O9K.AIO.Heroes.Dynamic.Abilities.Blinks.Unique
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.item_force_staff)]
    [AbilityId(AbilityId.item_hurricane_pike)]
    internal class ForceStaffBlink : OldBlinkAbility
    {
        public ForceStaffBlink(IBlink ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(Unit9 target)
        {
            return target.Equals(this.Ability.Owner);
        }

        public override bool Use(Unit9 target)
        {
            if (!this.Ability.UseAbility(target))
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetHitTime(target.Position));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target) + 0.5f);

            return true;
        }
    }
}