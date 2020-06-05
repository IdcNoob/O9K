namespace O9K.AIO.Heroes.Tusk.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using TargetManager;

    internal class Kick : NukeAbility
    {
        public Kick(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            if (!base.ShouldConditionCast(targetManager, menu, usableAbilities))
            {
                return false;
            }

            var ally = EntityManager9.Heroes
                .Where(x => !x.Equals(this.Owner) && x.IsAlly(this.Owner) && x.IsAlive && x.Distance(this.Owner) < 1500)
                .OrderBy(x => x.Distance(this.Owner))
                .FirstOrDefault();
            var allyPosition = ally == null ? EntityManager9.AllyFountain : ally.Position;

            if (this.Owner.Position.AngleBetween(targetManager.Target.Position, allyPosition) > 30)
            {
                if (this.Owner.Move(targetManager.Target.Position.Extend2D(allyPosition, -100)))
                {
                    this.OrbwalkSleeper.Sleep(0.1f);
                    this.Sleeper.Sleep(0.1f);
                    return false;
                }

                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(targetManager.Target, HitChance.Low))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            if (this.Ability is IDisable disable)
            {
                targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, this.Ability.GetHitTime(targetManager.Target));
            }

            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}