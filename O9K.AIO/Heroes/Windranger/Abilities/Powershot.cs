namespace O9K.AIO.Heroes.Windranger.Abilities
{
    using System;
    using System.Collections.Generic;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    using BasePowershot = Core.Entities.Abilities.Heroes.Windranger.Powershot;

    internal class Powershot : NukeAbility, IDisposable
    {
        private readonly BasePowershot powershot;

        private Vector3 castPosition;

        public Powershot(ActiveAbility ability)
            : base(ability)
        {
            this.powershot = (BasePowershot)ability;
            Player.OnExecuteOrder += this.OnExecuteOrder;
        }

        public Shackleshot Shackleshot { get; set; }

        public bool CancelChanneling(TargetManager targetManager)
        {
            if (!this.Ability.IsChanneling || !this.Ability.BaseAbility.IsChanneling)
            {
                return false;
            }

            var target = targetManager.Target;
            if (target.IsStunned || target.IsRooted)
            {
                return false;
            }

            var polygon = new Polygon.Rectangle(
                this.Owner.Position,
                this.Owner.Position.Extend2D(this.castPosition, this.Ability.Range),
                this.Ability.Radius - 75);

            var input = this.Ability.GetPredictionInput(target);
            input.Delay = this.powershot.ChannelTime - this.Ability.BaseAbility.ChannelTime;
            var output = this.Ability.GetPredictionOutput(input);

            if (!polygon.IsInside(output.TargetPosition) || this.powershot.GetCurrentDamage(target) > target.Health)
            {
                return this.Owner.BaseUnit.Stop();
            }

            return false;
        }

        public void Dispose()
        {
            Player.OnExecuteOrder -= this.OnExecuteOrder;
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var target = targetManager.Target;

            if (this.Ability.GetDamage(target) > target.Health)
            {
                return true;
            }

            if (usableAbilities.Count > 0)
            {
                return target.GetImmobilityDuration() > 0.4f;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var input = this.Ability.GetPredictionInput(target);

            if (this.Shackleshot?.Ability.TimeSinceCasted < 0.5f)
            {
                input.Delay -= this.Ability.ActivationDelay;
            }

            var output = this.Ability.GetPredictionOutput(input);

            if (!this.Ability.UseAbility(output.CastPosition))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            if (args.IsQueued || args.OrderId != OrderId.AbilityLocation)
            {
                return;
            }

            if (args.Ability.Handle == this.Ability.Handle)
            {
                this.castPosition = args.TargetPosition;
            }
        }
    }
}