namespace O9K.AIO.Heroes.Spectre.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Managers.Entity;

    using SharpDX;

    using TargetManager;

    internal class Reality : UsableAbility
    {
        private Vector3 realityTarget;

        public Reality(ActiveAbility ability)
            : base(ability)
        {
        }

        public bool RealityUseOnFakeTarget { get; set; } = true;

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            return false;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;
            var distance = this.Owner.Distance(target);
            var hauntIllusions = EntityManager9.Heroes.Where(
                    x => x.Name == this.Owner.Name && x.IsAlive && x.IsIllusion && !x.IsInvulnerable && x.IsAlly(this.Owner)
                         && x.HasModifier("modifier_spectre_haunt"))
                .ToList();

            if (hauntIllusions.Count == 0)
            {
                return false;
            }

            if (this.RealityUseOnFakeTarget)
            {
                this.RealityUseOnFakeTarget = false;

                var fakeTarget = targetManager.EnemyHeroes.Where(x => !x.Equals(target)).OrderBy(x => x.Distance(target)).FirstOrDefault();

                if (fakeTarget != null)
                {
                    this.realityTarget = fakeTarget.Position;
                    return true;
                }
            }

            if (hauntIllusions.All(x => x.Distance(target) + 100 > distance))
            {
                return false;
            }

            this.realityTarget = target.Position;
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(this.realityTarget))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay();
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.2f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}