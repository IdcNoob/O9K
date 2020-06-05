namespace O9K.AutoUsage.Abilities.Disable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Prediction.Data;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Settings;

    internal class DisableAbility : UsableAbility
    {
        private readonly DisableSettings settings;

        public DisableAbility(IDisable disable, GroupSettings settings)
            : base(disable)
        {
            this.Disable = disable;
            this.settings = new DisableSettings(settings.Menu, disable);
        }

        protected IDisable Disable { get; }

        public override bool UseAbility(List<Unit9> heroes)
        {
            //todo improve

            if (this.Disable.AppliesUnitState == 0)
            {
                return false;
            }

            var allEnemies = heroes.Where(x => x.IsEnemy(this.Owner)).ToList();
            var enemies = new List<Unit9>();

            if (this.settings.OnAttack && this.Owner.IsAttackingHero())
            {
                enemies.Add(this.Owner.Target);
            }

            if (this.settings.OnSight)
            {
                enemies.AddRange(allEnemies.Where(x => !x.IsInvulnerable).OrderBy(x => this.Owner.Distance(x)));
            }

            if (this.settings.OnCast)
            {
                enemies.AddRange(allEnemies.Where(x => x.IsCasting));
            }

            if (this.settings.OnInitiation)
            {
                enemies.AddRange(
                    allEnemies.Where(
                        x => x.Abilities.Any(z => z is IBlink && z.Id != AbilityId.monkey_king_tree_dance && z.TimeSinceCasted < 1)));
            }

            foreach (var enemy in enemies)
            {
                if (!this.settings.IsHeroEnabled(enemy.Name))
                {
                    continue;
                }

                if (!this.Ability.CanHit(enemy, allEnemies, this.settings.EnemiesCount))
                {
                    continue;
                }

                if (this.Check(this.Owner, enemy, allEnemies, false))
                {
                    return true;
                }
            }

            enemies.Clear();

            if (this.settings.OnChannel)
            {
                foreach (var enemy in allEnemies)
                {
                    if (!enemy.IsChanneling)
                    {
                        continue;
                    }

                    var ability = enemy.Abilities.FirstOrDefault(x => x.IsChanneling);
                    if (ability == null)
                    {
                        continue;
                    }

                    if (ability.Id == AbilityId.lion_mana_drain || ability.Id == AbilityId.windrunner_powershot
                                                                || ability.Id == AbilityId.oracle_fortunes_end)
                    {
                        continue;
                    }

                    if (ability.IsItem)
                    {
                        if (this.Disable.IsRoot() && ability.Id != AbilityId.item_tpscroll)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (this.Disable.IsMute() || this.Disable.IsRoot())
                        {
                            continue;
                        }
                    }

                    if (!enemy.ChannelActivatesOnCast)
                    {
                        var time = this.Ability.GetHitTime(enemy.Position) + (this.settings.Delay / 1000f) + 0.3f;
                        if (enemy.ChannelEndTime - Game.RawGameTime > time)
                        {
                            return false;
                        }
                    }

                    enemies.Add(enemy);
                }
            }

            if (this.settings.OnChainStun)
            {
                foreach (var enemy in allEnemies)
                {
                    var immobileDuration = enemy.GetImmobilityDuration();
                    if (immobileDuration <= 0)
                    {
                        continue;
                    }

                    var time = this.Ability.GetHitTime(enemy.Position) + (this.settings.Delay / 1000f);

                    if (this.Ability.UnitTargetCast && !enemy.IsInvulnerable)
                    {
                        time = Math.Max(time, 0.2f);
                    }

                    if (time * 0.8f > immobileDuration)
                    {
                        continue;
                    }

                    if (immobileDuration > time * 0.9f)
                    {
                        continue;
                    }

                    enemies.Add(enemy);
                }
            }

            foreach (var enemy in enemies)
            {
                if (!this.settings.IsHeroEnabled(enemy.Name))
                {
                    continue;
                }

                if (!this.Ability.CanHit(enemy, allEnemies, this.settings.EnemiesCount))
                {
                    continue;
                }

                if (this.Check(this.Owner, enemy, allEnemies, true))
                {
                    return true;
                }
            }

            return false;
        }

        private bool Check(Unit9 ally, Unit9 enemy, List<Unit9> enemies, bool ignoreStatusCheck)
        {
            if (this.settings.MaxCastRange > 0 && enemy.Distance(ally) > this.settings.MaxCastRange)
            {
                return false;
            }

            if (!ignoreStatusCheck)
            {
                if (!this.settings.HexStack && enemy.IsHexed)
                {
                    return false;
                }

                if (!this.settings.SilenceStack && enemy.IsSilenced)
                {
                    return false;
                }

                if (!this.settings.DisarmStack && enemy.IsDisarmed)
                {
                    return false;
                }

                if (!this.settings.RootStack && enemy.IsRooted)
                {
                    return false;
                }

                if (!this.settings.StunStack && enemy.IsStunned)
                {
                    return false;
                }
            }

            if (enemy.IsDarkPactProtected)
            {
                return false;
            }

            if (this.Ability.UnitTargetCast && enemy.IsBlockingAbilities)
            {
                return false;
            }

            if (this.settings.Delay > 0)
            {
                UpdateManager.BeginInvoke(
                    () => this.Ability.UseAbility(enemy, enemies, HitChance.Medium, this.settings.EnemiesCount),
                    this.settings.Delay);
            }
            else
            {
                this.Ability.UseAbility(enemy, enemies, HitChance.Medium, this.settings.EnemiesCount);
            }

            enemy.SetExpectedUnitState(
                this.Disable.AppliesUnitState,
                this.Ability.GetHitTime(enemy.Position) + (this.settings.Delay / 1000f) + 0.3f);
            return true;
        }
    }
}