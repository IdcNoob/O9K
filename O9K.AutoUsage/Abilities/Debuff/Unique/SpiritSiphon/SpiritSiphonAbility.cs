namespace O9K.AutoUsage.Abilities.Debuff.Unique.SpiritSiphon
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Abilities.Heroes.DeathProphet;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    [AbilityId(AbilityId.death_prophet_spirit_siphon)]
    internal class SpiritSiphonAbility : DebuffAbility
    {
        private readonly SpiritSiphonSettings settings;

        private readonly SpiritSiphon spiritSiphon;

        public SpiritSiphonAbility(IDebuff debuff, GroupSettings settings)
            : base(debuff)
        {
            this.settings = new SpiritSiphonSettings(settings.Menu, debuff);
            this.spiritSiphon = (SpiritSiphon)debuff;
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (this.Owner.HealthPercentage > this.settings.HpThreshold)
            {
                return false;
            }

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

                if (!this.Ability.CanHit(enemy))
                {
                    continue;
                }

                if (this.Check(this.Owner, enemy))
                {
                    return true;
                }
            }

            return false;
        }

        private bool Check(Unit9 ally, Unit9 enemy)
        {
            if (this.settings.MaxCastRange > 0 && enemy.Distance(ally) > this.settings.MaxCastRange)
            {
                return false;
            }

            if (enemy.BaseUnit.HasModifier(this.spiritSiphon.DebuffModifierName))
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

            return this.Ability.UseAbility(enemy);
        }
    }
}