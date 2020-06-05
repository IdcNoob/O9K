namespace O9K.AIO.Heroes.Dynamic.Abilities.Blinks
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;

    using SharpDX;

    internal class OldBlinkAbility : OldUsableAbility
    {
        public OldBlinkAbility(IBlink ability)
            : base(ability)
        {
            this.Blink = ability;
        }

        public IBlink Blink { get; }

        public override bool ShouldCast(Unit9 target)
        {
            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (target.HasModifier("modifier_pudge_meat_hook"))
            {
                return false;
            }

            return true;
        }

        public override bool Use(Unit9 target)
        {
            if (!this.Ability.UseAbility(target))
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetHitTime(target.Position));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target.Position) + 0.5f);

            return true;
        }

        public bool Use(Vector3 position)
        {
            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetHitTime(position) + 0.1f);
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(position) + 0.5f);

            return true;
        }
    }
}