namespace O9K.AutoUsage.Abilities.Nuke.Unique.Purification
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Settings;

    [AbilityId(AbilityId.omniknight_purification)]
    internal class PurificationAbility : NukeAbility
    {
        private readonly NukeSettings settings;

        public PurificationAbility(INuke nuke, GroupSettings settings)
            : base(nuke)
        {
            this.settings = new NukeSettings(settings.Menu, nuke);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var enemies = heroes.Where(x => x.IsEnemy(this.Owner)).OrderByDescending(x => x.Health).ToList();

            for (var i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];

                if (!this.CheckEnemy(enemy))
                {
                    continue;
                }

                var enemyPosition = enemy.GetPredictedPosition(this.Ability.GetHitTime(enemy));
                if (this.Owner.Distance(enemyPosition) > this.Ability.CastRange + this.Ability.Radius)
                {
                    return false;
                }

                var ally = EntityManager9.Units.FirstOrDefault(
                    x => x.IsUnit && x.IsAlive && !x.IsInvulnerable && x.IsAlly(this.Owner)
                         && x.Distance(this.Owner) < this.Ability.CastRange && x.Distance(enemyPosition) < this.Ability.Radius);

                if (ally == null)
                {
                    continue;
                }

                if (this.settings.EnemiesCount > 1)
                {
                    var possibleEnemies = new List<Unit9>();

                    for (var j = i + 1; j < enemies.Count; j++)
                    {
                        var remainingEnemy = enemies[j];

                        if (this.CheckEnemy(remainingEnemy))
                        {
                            possibleEnemies.Add(remainingEnemy);
                        }
                    }

                    if (!this.Ability.CanHit(enemy, possibleEnemies, this.settings.EnemiesCount))
                    {
                        continue;
                    }
                }

                return this.Ability.UseAbility(ally);
            }

            return false;
        }

        private bool CheckEnemy(Unit9 enemy)
        {
            if (!this.settings.IsHeroEnabled(enemy.Name))
            {
                return false;
            }

            if (enemy.IsReflectingDamage)
            {
                return false;
            }

            if (this.settings.OnImmobileOnly)
            {
                var immobileDuration = enemy.GetImmobilityDuration();
                if (immobileDuration <= 0)
                {
                    return false;
                }

                var time = this.Ability.GetHitTime(enemy.Position);
                if (time * 0.8f > immobileDuration)
                {
                    return false;
                }

                if (enemy.IsInvulnerable && immobileDuration > time * 0.9f)
                {
                    return false;
                }
            }
            else if (enemy.IsInvulnerable)
            {
                return false;
            }

            if (enemy.IsMagicImmune && !this.Ability.PiercesMagicImmunity(enemy))
            {
                return false;
            }

            if (enemy.Health > this.Nuke.GetDamage(enemy))
            {
                return false;
            }

            return true;
        }
    }
}