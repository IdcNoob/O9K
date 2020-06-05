namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.Bottle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Settings;

    [AbilityId(AbilityId.item_bottle)]
    internal class BottleAbility : HealthRestoreAbility, IDisposable
    {
        private readonly Sleeper bottleRefillingSleeper = new Sleeper();

        private readonly BottleSettings settings;

        public BottleAbility(IHealthRestore healthRestore, GroupSettings settings)
            : base(healthRestore)
        {
            this.settings = new BottleSettings(settings.Menu, healthRestore);
        }

        public void Dispose()
        {
            UpdateManager.Unsubscribe(this.OnUpdate);
        }

        public override void Enabled(bool enabled)
        {
            base.Enabled(enabled);

            if (enabled)
            {
                UpdateManager.Subscribe(this.OnUpdate, 500);
            }
            else
            {
                UpdateManager.Unsubscribe(this.OnUpdate);
            }
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (!this.bottleRefillingSleeper)
            {
                return false;
            }

            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner))
                .OrderBy(x => x.GetModifier(this.HealthRestore.RestoreModifierName)?.RemainingTime)
                .ToList();

            foreach (var ally in allies)
            {
                if (!this.settings.IsHeroEnabled(ally.Name) && !this.settings.SelfOnly)
                {
                    continue;
                }

                if (!ally.CanBeHealed)
                {
                    continue;
                }

                if (ally.HealthPercentage > 99 && ally.ManaPercentage > 99)
                {
                    continue;
                }

                var selfTarget = ally.Equals(this.Owner);
                if (!selfTarget && this.settings.SelfOnly)
                {
                    continue;
                }

                if (!this.Ability.CanHit(ally))
                {
                    continue;
                }

                if (selfTarget)
                {
                    return this.Ability.UseAbility();
                }

                return this.Ability.UseAbility(ally);
            }

            return false;
        }

        private void OnUpdate()
        {
            try
            {
                if (this.Owner.Distance(EntityManager9.AllyFountain) > 1300)
                {
                    return;
                }

                this.bottleRefillingSleeper.Sleep(2);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}