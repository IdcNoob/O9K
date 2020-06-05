namespace O9K.AIO.Abilities
{
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Heroes.Base;

    using Items;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class AbilityHelper
    {
        private readonly Sleeper comboSleeper;

        private readonly IComboModeMenu menu;

        private readonly Sleeper orbwalkSleeper;

        private readonly Unit9 unit;

        public AbilityHelper(TargetManager targetManager, IComboModeMenu comboModeMenu, ControllableUnit controllableUnit)
        {
            this.unit = controllableUnit.Owner;
            this.TargetManager = targetManager;
            this.comboSleeper = controllableUnit.ComboSleeper;
            this.orbwalkSleeper = controllableUnit.OrbwalkSleeper;
            this.menu = comboModeMenu;
        }

        public TargetManager TargetManager { get; }

        public bool CanBeCasted(
            UsableAbility ability,
            bool canHit = true,
            bool shouldCast = true,
            bool channelingCheck = true,
            bool canBeCastedCheck = true)
        {
            if (ability?.Ability.IsValid != true)
            {
                return false;
            }

            if (this.menu?.IsAbilityEnabled(ability.Ability) == false)
            {
                return false;
            }

            if (canBeCastedCheck && !ability.CanBeCasted(this.TargetManager, channelingCheck, this.menu))
            {
                return false;
            }

            if (canHit && !ability.CanHit(this.TargetManager, this.menu))
            {
                return false;
            }

            if (shouldCast && !ability.ShouldCast(this.TargetManager))
            {
                return false;
            }

            return true;
        }

        public bool CanBeCastedHidden(UsableAbility ability)
        {
            if (ability?.Ability.IsValid != true)
            {
                return false;
            }

            if (!this.menu.IsAbilityEnabled(ability.Ability))
            {
                return false;
            }

            if (ability.Ability.Owner.IsChanneling)
            {
                return false;
            }

            if (ability.Ability.Level <= 0)
            {
                return false;
            }

            if (ability.Ability.RemainingCooldown > 0)
            {
                return false;
            }

            if (ability.Ability.Owner.Mana < ability.Ability.ManaCost)
            {
                return false;
            }

            return true;
        }

        public bool CanBeCastedIfCondition(UsableAbility ability, params UsableAbility[] checkAbilities)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            if (!ability.ShouldConditionCast(
                    this.TargetManager,
                    this.menu,
                    checkAbilities.Where(x => this.CanBeCasted(x, false, false)).ToList()))
            {
                return false;
            }

            return true;
        }

        public bool ForceUseAbility(UsableAbility ability, bool ignorePrediction = false, bool aoe = true)
        {
            if (ignorePrediction)
            {
                return ability.ForceUseAbility(this.TargetManager, this.comboSleeper);
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, aoe);
        }

        public bool HasMana(params UsableAbility[] abilities)
        {
            return this.MissingMana(abilities) <= 0;
        }

        public float MissingMana(params UsableAbility[] abilities)
        {
            var manaCost = abilities.Where(x => x?.Ability.IsValid == true).Sum(x => x.Ability.ManaCost);
            return manaCost - this.unit.Mana;
        }

        public bool UseAbility(BuffAbility ability, float distance)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, distance);
        }

        public bool UseAbility(ShieldAbility ability, float distance)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, distance);
        }

        public bool UseAbility(UsableAbility ability, bool aoe = true)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, aoe);
        }

        public bool UseAbility(BlinkAbility ability, float minUseRange, float blinkToEnemyRange)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, minUseRange, blinkToEnemyRange);
        }

        public bool UseAbility(BlinkAbility ability, Vector3 position)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, position);
        }

        public bool UseAbilityIfAny(UsableAbility ability, params UsableAbility[] checkAbilities)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            if (checkAbilities.Any(x => this.CanBeCasted(x, false)))
            {
                return ability.UseAbility(this.TargetManager, this.comboSleeper, true);
            }

            return false;
        }

        public bool UseAbilityIfCondition(UsableAbility ability, params UsableAbility[] checkAbilities)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            if (!ability.ShouldConditionCast(
                    this.TargetManager,
                    this.menu,
                    checkAbilities.Where(x => this.CanBeCasted(x, false, false)).ToList()))
            {
                return false;
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, true);
        }

        public bool UseAbilityIfNone(UsableAbility ability, params UsableAbility[] checkAbilities)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            if (checkAbilities.All(x => !this.CanBeCasted(x, false)))
            {
                return ability.UseAbility(this.TargetManager, this.comboSleeper, true);
            }

            return false;
        }

        public bool UseBlinkLineCombo(BlinkAbility blink, UsableAbility ability)
        {
            if (!this.CanBeCasted(ability, false) || !this.CanBeCasted(blink))
            {
                return false;
            }

            if (!(ability.Ability is LineAbility line))
            {
                return false;
            }

            var target = this.TargetManager.Target;

            var range = line.CastRange;
            if (blink.Ability.CastRange < range)
            {
                range = blink.Ability.CastRange - 100;
            }

            if (line.Owner.Distance(target) < range)
            {
                return false;
            }

            var input = line.GetPredictionInput(target, this.TargetManager.EnemyHeroes);
            input.CastRange = blink.Ability.Range;
            input.Range = line.CastRange;
            input.UseBlink = true;

            var output = line.GetPredictionOutput(input);
            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            var blinkPosition = output.BlinkLinePosition;

            if (blink.Ability.UseAbility(blinkPosition))
            {
                if (line.UseAbility(output.CastPosition))
                {
                    var delay = ability.Ability.GetCastDelay(output.CastPosition);

                    this.comboSleeper.Sleep(delay + 0.3f);
                    this.orbwalkSleeper.Sleep(delay + 0.5f);
                    ability.Sleeper.Sleep(delay + 0.5f);
                    blink.Sleeper.Sleep(delay + 0.5f);

                    return true;
                }
            }

            return false;
        }

        public bool UseDoubleBlinkCombo(ForceStaff force, BlinkAbility blink, float minDistance = 0)
        {
            if (!this.CanBeCasted(force, false) || !this.CanBeCasted(blink, false))
            {
                return false;
            }

            var target = this.TargetManager.Target;
            var owner = force.Ability.Owner;

            if (owner.Distance(target) < minDistance || owner.Distance(target) < blink.Ability.Range)
            {
                return false;
            }

            var range = blink.Ability.Range + force.Ability.Range;

            if (owner.Distance(target) > range)
            {
                return false;
            }

            if (owner.GetAngle(target.Position) > 0.5f)
            {
                owner.BaseUnit.Move(target.Position);
                this.comboSleeper.Sleep(0.1f);
                return false;
            }

            force.Ability.UseAbility(owner);
            this.comboSleeper.Sleep(force.Ability.GetCastDelay() + 0.3f);
            return false;
        }

        public bool UseForceStaffAway(ForceStaff force, int range)
        {
            if (!this.CanBeCasted(force))
            {
                return false;
            }

            var target = this.TargetManager.Target;
            if (target.IsRanged || target.IsStunned || target.IsRooted || target.IsHexed || target.IsDisarmed)
            {
                return false;
            }

            var owner = force.Ability.Owner;
            if (target.Distance(owner) > range)
            {
                return false;
            }

            var mouse = Game.MousePosition;
            if (owner.GetAngle(mouse) > 1f)
            {
                owner.BaseUnit.Move(mouse);
                UpdateManager.BeginInvoke(() => force.Ability.UseAbility(owner), 200);
                return true;
            }

            return false;
        }

        public bool UseKillStealAbility(NukeAbility ability, bool aoe = true)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            if (ability.Ability.GetDamage(this.TargetManager.Target) < this.TargetManager.Target.Health)
            {
                return false;
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, aoe);
        }

        public bool UseMoveAbility(BlinkAbility ability)
        {
            if (!this.CanBeCasted(ability, false))
            {
                return false;
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, Game.MousePosition);
        }

        public bool UseMoveAbility(UsableAbility ability)
        {
            if (!this.CanBeCasted(ability))
            {
                return false;
            }

            return ability.UseAbility(this.TargetManager, this.comboSleeper, true);
        }
    }
}