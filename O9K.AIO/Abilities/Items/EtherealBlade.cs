namespace O9K.AIO.Abilities.Items
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Helpers;

    using TargetManager;

    internal class EtherealBlade : DebuffAbility
    {
        public EtherealBlade(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            return this.UseAbility(targetManager, comboSleeper, false);
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(targetManager.Target))
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(targetManager.Target);

            if (this.Ability is IDisable disable)
            {
                targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, hitTime);
            }

            comboSleeper.Sleep(hitTime);
            this.Sleeper.Sleep(hitTime);
            return true;
        }
    }
}