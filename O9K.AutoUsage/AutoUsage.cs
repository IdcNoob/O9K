namespace O9K.AutoUsage
{
    using System;
    using System.Collections.Generic;

    using Abilities;
    using Abilities.Autocast;
    using Abilities.Blink;
    using Abilities.Buff;
    using Abilities.Debuff;
    using Abilities.Disable;
    using Abilities.HealthRestore;
    using Abilities.LinkensBreak;
    using Abilities.ManaRestore;
    using Abilities.Nuke;
    using Abilities.Shield;
    using Abilities.Special;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Heroes;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;

    using Settings;

    internal class AutoUsage : IDisposable
    {
        private readonly List<IAutoUsageGroup> groups = new List<IAutoUsageGroup>();

        private readonly HashSet<AbilityId> ignoredAbilities = new HashSet<AbilityId>
        {
            AbilityId.item_flask,
            AbilityId.item_clarity
        };

        private readonly Owner myHero;

        public AutoUsage(MainSettings settings)
        {
            this.myHero = EntityManager9.Owner;

            var sleeper = new MultiSleeper();

            this.groups.Add(new AutoUsageGroup<IDisable, DisableAbility>(sleeper, settings.DisableSettings));
            this.groups.Add(new ShieldGroup<IShield, ShieldAbility>(sleeper, settings.ShieldSettings));
            this.groups.Add(new AutoUsageGroup<IBlink, BlinkAbility>(sleeper, settings.BlinkSettings));
            this.groups.Add(new AutoUsageGroup<IHealthRestore, HealthRestoreAbility>(sleeper, settings.HpRestoreSettings));
            this.groups.Add(new AutoUsageGroup<INuke, NukeAbility>(sleeper, settings.NukeSettings));
            this.groups.Add(new AutoUsageGroup<IBuff, BuffAbility>(sleeper, settings.BuffSettings));
            this.groups.Add(new AutoUsageGroup<IDebuff, DebuffAbility>(sleeper, settings.DebuffSettings));
            this.groups.Add(new AutoUsageSpecialGroup<IActiveAbility, SpecialAbility>(sleeper, settings.SpecialSettings));
            this.groups.Add(new ManaRestoreGroup<IManaRestore, ManaRestoreAbility>(sleeper, settings.MpRestoreSettings));
            this.groups.Add(new AutoUsageLinkensBreakGroup<IActiveAbility, LinkensBreakAbility>(sleeper, settings.LinkensBreakSettings));
            this.groups.Add(new AutoUsageGroup<OrbAbility, AutocastAbility>(sleeper, settings.AutocastSettings));

            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
        }

        public void Dispose()
        {
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;

            foreach (var group in this.groups)
            {
                group.Dispose();
            }
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!ability.IsControllable || ability.IsFake || !ability.Owner.IsAlly(this.myHero.Team) || !ability.Owner.IsMyControllable)
                {
                    return;
                }

                if (this.ignoredAbilities.Contains(ability.Id))
                {
                    return;
                }

                foreach (var group in this.groups)
                {
                    group.AddAbility(ability);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (!ability.IsControllable || ability.IsFake || !ability.Owner.IsAlly(this.myHero.Team))
                {
                    return;
                }

                foreach (var group in this.groups)
                {
                    group.RemoveAbility(ability);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}