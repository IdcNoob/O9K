namespace O9K.AutoUsage.Abilities.Shield.Unique.MagneticField
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Abilities.Heroes.ArcWarden;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    [AbilityId(AbilityId.arc_warden_magnetic_field)]
    internal class MagneticFieldAbility : ShieldAbility
    {
        private static readonly Sleeper Sleeper = new Sleeper();

        private readonly MagneticField field;

        private readonly MagneticFieldSettings settings;

        public MagneticFieldAbility(IShield shield, GroupSettings settings)
            : base(shield)
        {
            this.field = (MagneticField)shield;
            this.settings = new MagneticFieldSettings(settings.Menu, shield);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (Sleeper.IsSleeping)
            {
                return false;
            }

            var lowMp = this.Owner.ManaPercentage < this.settings.MpThreshold;

            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).OrderBy(x => x.Health).ToList();
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner) && this.settings.IsEnemyHeroEnabled(x.Name))
                .ToList();

            if (this.settings.UseOnTowers)
            {
                allies.AddRange(EntityManager9.Units.Where(x => x.IsTower && !x.IsInvulnerable && x.IsAlive && x.IsAlly(this.Owner)));
            }

            if (this.settings.UseOnRax)
            {
                allies.AddRange(EntityManager9.Units.Where(x => x.IsBarrack && !x.IsInvulnerable && x.IsAlive && x.IsAlly(this.Owner)));
            }

            foreach (var ally in allies)
            {
                if (!this.settings.IsAllyHeroEnabled(ally.Name) && !ally.IsTower && !ally.IsBarrack && !this.settings.SelfOnly)
                {
                    continue;
                }

                var healthPercentage = ally.HealthPercentage;

                if (healthPercentage > this.settings.HpThreshold || (lowMp && healthPercentage > this.settings.CriticalHpThreshold))
                {
                    continue;
                }

                var selfTarget = ally.Equals(this.Owner);

                if (selfTarget && !this.Shield.ShieldsOwner)
                {
                    continue;
                }

                if (!selfTarget && (!this.Shield.ShieldsAlly || this.settings.SelfOnly))
                {
                    continue;
                }

                if (!this.Ability.CanHit(ally, allies, this.settings.AlliesCount))
                {
                    continue;
                }

                if (ally.BaseUnit.HasModifier(this.Shield.ShieldModifierName))
                {
                    continue;
                }

                if (enemies.Count(x => x.Distance(ally) < this.settings.Distance) < this.settings.EnemiesCount)
                {
                    continue;
                }

                var closestEnemy = enemies.OrderBy(x => x.Distance(ally)).FirstOrDefault();

                if (!ally.IsHero && closestEnemy?.CanAttack(ally) != true)
                {
                    continue;
                }

                if (closestEnemy != null)
                {
                    var position = ally.Position.Extend2D(closestEnemy.Position, -this.field.Radius);

                    if (ally.IsBarrack)
                    {
                        var barracks = allies.Where(x => x.IsBarrack && x.Distance(ally) < this.Shield.Radius + (x.HullRadius * 2))
                            .ToList();

                        if (barracks.Count > 1)
                        {
                            position = barracks.GetCenterPosition().Extend2D(EntityManager9.AllyFountain, this.Shield.Radius);
                        }
                    }

                    if (this.Owner.Distance(position) < this.Ability.CastRange && this.Ability.UseAbility(position))
                    {
                        Sleeper.Sleep(1);
                        return true;
                    }

                    return false;
                }

                return this.Ability.UseAbility(ally, allies, HitChance.Medium, this.settings.AlliesCount);
            }

            return false;
        }
    }
}