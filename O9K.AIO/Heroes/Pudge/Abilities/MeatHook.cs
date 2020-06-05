namespace O9K.AIO.Heroes.Pudge.Abilities
{
    using System;
    using System.Linq;

    using AIO.Abilities;
    using AIO.Abilities.Menus;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using TargetManager;

    internal class MeatHook : NukeAbility
    {
        private Unit9 lastTarget;

        private float rotation;

        private float rotationTime;

        public MeatHook(ActiveAbility ability)
            : base(ability)
        {
            this.breakShields = this.breakShields.Concat(new[] { "modifier_templar_assassin_refraction_absorb" }).ToArray();
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var target = targetManager.Target;
            var predictionInput = this.Ability.GetPredictionInput(target);
            var output = this.Ability.GetPredictionOutput(predictionInput);

            if (output.HitChance <= HitChance.Impossible)
            {
                return false;
            }

            if (target.Distance(this.Owner) < 300 || target.GetImmobilityDuration() > 0)
            {
                return true;
            }

            if (target.Equals(this.lastTarget))
            {
                if (Math.Abs(this.rotation - target.BaseUnit.NetworkRotationRad) > 0.1)
                {
                    this.rotationTime = Game.RawGameTime;
                    this.rotation = target.BaseUnit.NetworkRotationRad;
                    return false;
                }

                var menu = comboMenu.GetAbilitySettingsMenu<MeatHookMenu>(this);
                if (this.rotationTime + (menu.Delay / 1000f) > Game.RawGameTime)
                {
                    return false;
                }
            }
            else
            {
                this.lastTarget = target;
                this.rotationTime = Game.RawGameTime;
                this.rotation = target.BaseUnit.NetworkRotationRad;
                return false;
            }

            return true;
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new MeatHookMenu(this.Ability, simplifiedName);
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (target.HasModifier("modifier_templar_assassin_refraction_absorb") && target.Distance(this.Owner) < 300)
            {
                return false;
            }

            return base.ShouldCast(targetManager);
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!base.UseAbility(targetManager, comboSleeper, aoe))
            {
                return false;
            }

            if (this.lastTarget != null)
            {
                this.lastTarget.RefreshUnitState();
                this.lastTarget = null;
            }

            var delay = this.Ability.GetHitTime(targetManager.Target);
            this.OrbwalkSleeper.Sleep(delay + (this.Owner.Distance(targetManager.Target) / this.Ability.Speed));
            comboSleeper.Sleep(delay + 0.1f);

            return true;
        }
    }
}