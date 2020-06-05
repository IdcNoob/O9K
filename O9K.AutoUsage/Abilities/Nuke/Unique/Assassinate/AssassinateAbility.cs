namespace O9K.AutoUsage.Abilities.Nuke.Unique.Assassinate
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Prediction.Data;

    using Ensage;

    using Settings;

    [AbilityId(AbilityId.sniper_assassinate)]
    internal class AssassinateAbility : NukeAbility
    {
        private readonly AssassinateSettings settings;

        public AssassinateAbility(INuke nuke, GroupSettings settings)
            : base(nuke)
        {
            this.settings = new AssassinateSettings(settings.Menu, nuke);
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

                if (enemy.IsReflectingDamage)
                {
                    continue;
                }

                if (enemy.Distance(this.Owner) < this.settings.MinCastRange)
                {
                    continue;
                }

                if (enemy.IsInvulnerable)
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

                return this.Ability.UseAbility(enemy, enemies, HitChance.Medium);
            }

            return false;
        }
    }
}