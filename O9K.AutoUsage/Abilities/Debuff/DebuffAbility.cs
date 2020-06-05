namespace O9K.AutoUsage.Abilities.Debuff
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Prediction.Data;

    using Ensage.SDK.Extensions;

    using Settings;

    internal class DebuffAbility : UsableAbility
    {
        private readonly DebuffSettings settings;

        public DebuffAbility(IDebuff debuff)
            : base(debuff)
        {
            this.Debuff = debuff;
        }

        public DebuffAbility(IDebuff debuff, GroupSettings settings)
            : base(debuff)
        {
            this.Debuff = debuff;
            this.settings = new DebuffSettings(settings.Menu, debuff);
        }

        protected IDebuff Debuff { get; }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).ToList();
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner))
                .OrderByDescending(x => x.Equals(this.Owner.Target))
                .ThenBy(x => x.Distance(this.Owner))
                .ToList();

            foreach (var enemy in enemies)
            {
                if (!this.settings.IsHeroEnabled(enemy.Name))
                {
                    continue;
                }

                if (!this.Ability.CanHit(enemy, enemies, this.settings.EnemiesCount))
                {
                    continue;
                }

                if (this.settings.SelfOnly)
                {
                    if (this.Check(this.Owner, enemy, enemies))
                    {
                        return true;
                    }
                }
                else
                {
                    if (allies.Any(x => this.Check(x, enemy, enemies)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool Check(Unit9 ally, Unit9 enemy, List<Unit9> enemies)
        {
            if (this.settings.MaxCastRange > 0 && enemy.Distance(ally) > this.settings.MaxCastRange)
            {
                return false;
            }

            if (enemy.BaseUnit.HasModifier(this.Debuff.DebuffModifierName))
            {
                return false;
            }

            if (!this.settings.OnSight && (!this.settings.OnAttack || !ally.IsAttackingHero()))
            {
                return false;
            }

            if (this.Ability.UnitTargetCast && enemy.IsBlockingAbilities)
            {
                return false;
            }

            return this.Ability.UseAbility(enemy, enemies, HitChance.Medium, this.settings.EnemiesCount);
        }
    }
}