namespace O9K.AIO.Heroes.TemplarAssassin.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.TemplarAssassin;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Ensage;

    using TargetManager;

    internal class TrapExplode : DebuffAbility
    {
        private readonly SelfTrap trap;

        public TrapExplode(ActiveAbility ability)
            : base(ability)
        {
            this.trap = (SelfTrap)ability;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;
            var modifier = target.GetModifier(this.Debuff.DebuffModifierName);

            if (modifier?.RemainingTime > 2.5f)
            {
                return false;
            }

            if (target.IsDarkPactProtected)
            {
                return false;
            }

            if (target.IsInvulnerable || target.IsRooted || target.IsStunned || target.IsHexed)
            {
                return false;
            }

            if (targetManager.TargetSleeper.IsSleeping)
            {
                return false;
            }

            if (this.trap.IsFullyCharged)
            {
                return true;
            }

            if (this.Owner.Distance(target) > this.Ability.Radius * 0.6f && target.GetAngle(this.Owner.Position) > 2)
            {
                return true;
            }

            return false;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var templar = this.Owner.Owner;
            var closestTrap = EntityManager9.Units
                .Where(x => x.Name == "npc_dota_templar_assassin_psionic_trap" && x.IsControllable && x.IsAlive)
                .OrderBy(x => x.Distance(templar))
                .FirstOrDefault();

            if (closestTrap != null && closestTrap == this.Owner)
            {
                var explodeAbility = templar.Abilities.FirstOrDefault(x => x.Id == AbilityId.templar_assassin_trap) as ActiveAbility;
                if (explodeAbility?.CanBeCasted() == true && explodeAbility.UseAbility())
                {
                    comboSleeper.Sleep(0.1f);
                    this.OrbwalkSleeper.Sleep(0.1f);
                    targetManager.TargetSleeper.Sleep(0.3f);
                    return true;
                }
            }

            if (!base.UseAbility(targetManager, comboSleeper, aoe))
            {
                return false;
            }

            targetManager.TargetSleeper.Sleep(0.3f);
            return true;
        }
    }
}