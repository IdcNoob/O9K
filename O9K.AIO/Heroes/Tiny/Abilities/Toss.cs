namespace O9K.AIO.Heroes.Tiny.Abilities
{
    using System.Linq;

    using AIO.Abilities;
    using AIO.Abilities.Menus;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Managers.Entity;

    using TargetManager;

    internal class Toss : NukeAbility
    {
        private Unit9 tossTarget;

        public Toss(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
        {
            if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
            {
                return false;
            }

            var menu = comboMenu.GetAbilitySettingsMenu<TossMenu>(this);

            if (menu.TossToAlly)
            {
                var grabTarget = EntityManager9.Units
                    .Where(
                        x => x.IsUnit && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && !x.Equals(this.Owner)
                             && x.Distance(this.Owner) < this.Ability.Radius)
                    .OrderBy(x => x.Distance(this.Owner))
                    .FirstOrDefault();

                if (grabTarget != null && grabTarget == targetManager.Target)
                {
                    this.tossTarget = targetManager.AllyHeroes.Find(
                        x => menu.TossAlly.IsEnabled(x.Name) && !x.Equals(this.Owner) && x.Distance(this.Owner) < this.Ability.CastRange);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new TossMenu(this.Ability, simplifiedName);
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (this.tossTarget == null)
            {
                this.tossTarget = targetManager.Target;
            }

            if (this.tossTarget.HasModifier("modifier_tiny_toss"))
            {
                return false;
            }

            if (!this.Ability.UseAbility(this.tossTarget))
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
            this.tossTarget = null;
            return true;
        }
    }
}