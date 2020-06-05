namespace O9K.AutoUsage.Abilities.Nuke.Unique.ReapersScythe
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    using Settings;

    [AbilityId(AbilityId.necrolyte_reapers_scythe)]
    internal class ReapersScytheAbility : NukeAbility
    {
        private readonly NukeSettings settings;

        public ReapersScytheAbility(INuke nuke, GroupSettings settings)
            : base(nuke)
        {
            this.settings = new NukeSettings(settings.Menu, nuke);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var enemies = heroes.Where(x => x.IsEnemy(this.Owner)).OrderByDescending(x => x.Health).ToList();

            foreach (var enemy in enemies)
            {
                if (!this.settings.IsHeroEnabled(enemy.Name))
                {
                    continue;
                }

                if (enemy.IsReflectingDamage || enemy.CanReincarnate)
                {
                    continue;
                }

                if (this.settings.OnImmobileOnly)
                {
                    var immobileDuration = enemy.GetImmobilityDuration();
                    if (immobileDuration <= 0)
                    {
                        continue;
                    }

                    var time = this.Ability.GetHitTime(enemy.Position);
                    if (time * 0.8f > immobileDuration)
                    {
                        continue;
                    }

                    if (enemy.IsInvulnerable && immobileDuration > time * 0.9f)
                    {
                        continue;
                    }
                }
                else if (enemy.IsInvulnerable)
                {
                    continue;
                }

                if (!this.Ability.CanHit(enemy))
                {
                    continue;
                }

                if (enemy.Health > this.Nuke.GetDamage(enemy))
                {
                    continue;
                }

                if (this.Ability.UnitTargetCast && enemy.IsBlockingAbilities)
                {
                    continue;
                }

                return this.Ability.UseAbility(enemy);
            }

            return false;
        }
    }
}