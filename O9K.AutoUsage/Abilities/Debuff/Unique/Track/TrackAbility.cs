namespace O9K.AutoUsage.Abilities.Debuff.Unique.Track
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    [AbilityId(AbilityId.bounty_hunter_track)]
    internal class TrackAbility : DebuffAbility
    {
        private readonly TrackSettings settings;

        public TrackAbility(IDebuff debuff, GroupSettings settings)
            : base(debuff)
        {
            this.settings = new TrackSettings(settings.Menu, debuff);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).ToList();
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner)).OrderBy(x => x.Health).ToList();

            foreach (var enemy in enemies)
            {
                if (!this.Ability.CanHit(enemy))
                {
                    continue;
                }

                if (allies.Any(x => this.Check(x, enemy)))
                {
                    return true;
                }
            }

            return false;
        }

        private bool Check(Unit9 ally, Unit9 enemy)
        {
            if (enemy.BaseUnit.HasModifier(this.Debuff.DebuffModifierName))
            {
                return false;
            }

            if (enemy.IsBlockingAbilities)
            {
                return false;
            }

            if (!this.settings.ForceInvisibility || !enemy.CanBecomeInvisible)
            {
                if (enemy.HealthPercentage > this.settings.HpThreshold)
                {
                    return false;
                }

                if (!this.settings.OnSight && (!this.settings.OnAttack || !ally.IsAttackingHero()))
                {
                    return false;
                }
            }

            return this.Ability.UseAbility(enemy);
        }
    }
}