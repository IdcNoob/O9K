namespace O9K.AutoUsage.Abilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Metadata;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu.EventArgs;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Settings;

    using AbilityEventArgs = Core.Managers.Menu.EventArgs.AbilityEventArgs;

    internal class AutoUsageGroup<TType, TAbility> : IAutoUsageGroup
        where TType : class, IActiveAbility where TAbility : UsableAbility
    {
        private readonly MultiSleeper sleeper;

        public AutoUsageGroup(MultiSleeper sleeper, GroupSettings settings)
        {
            foreach (var type in Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && typeof(TAbility).IsAssignableFrom(x)))
            {
                foreach (var attribute in type.GetCustomAttributes<AbilityIdAttribute>())
                {
                    this.UniqueAbilities.Add(attribute.AbilityId, type);
                }
            }

            this.Handler = UpdateManager.Subscribe(this.OnUpdate, 1000, false);

            this.sleeper = sleeper;
            this.Settings = settings;

            settings.GroupEnabled.ValueChange += this.EnabledOnValueChange;
            settings.UpdateRate.ValueChange += this.UpdateRateOnValueChange;
            settings.AbilityToggler.ValueChange += this.AbilityTogglerOnValueChange;
        }

        protected HashSet<TAbility> Abilities { get; } = new HashSet<TAbility>();

        protected IUpdateHandler Handler { get; }

        protected GroupSettings Settings { get; }

        protected Dictionary<AbilityId, Type> UniqueAbilities { get; } = new Dictionary<AbilityId, Type>();

        public virtual void AddAbility(Ability9 ability)
        {
            var type = ability as TType;
            if (type == null)
            {
                return;
            }

            if (!this.UniqueAbilities.TryGetValue(ability.Id, out var uniqueType))
            {
                uniqueType = typeof(TAbility);
            }

            var usableAbility = (TAbility)Activator.CreateInstance(uniqueType, type, this.Settings);
            this.Abilities.Add(usableAbility);
            this.Settings.AddAbility(usableAbility);

            if (this.Settings.GroupEnabled)
            {
                this.Handler.IsEnabled = true;
            }
        }

        public virtual void Dispose()
        {
            foreach (var ability in this.Abilities.OfType<IDisposable>())
            {
                ability.Dispose();
            }

            UpdateManager.Unsubscribe(this.Handler);
            this.Settings.GroupEnabled.ValueChange -= this.EnabledOnValueChange;
            this.Settings.UpdateRate.ValueChange -= this.UpdateRateOnValueChange;
            this.Settings.AbilityToggler.ValueChange -= this.AbilityTogglerOnValueChange;
        }

        public void RemoveAbility(Ability9 ability)
        {
            var usableAbility = this.Abilities.FirstOrDefault(x => x.Ability.Equals(ability));
            if (usableAbility == null)
            {
                return;
            }

            if (usableAbility is IDisposable disposable)
            {
                disposable.Dispose();
            }

            this.Abilities.Remove(usableAbility);

            if (this.Abilities.Count == 0)
            {
                this.Handler.IsEnabled = false;
            }
        }

        protected virtual void AbilityTogglerOnValueChange(object sender, AbilityEventArgs e)
        {
            var ability = this.Abilities.FirstOrDefault(x => x.Ability.Name == e.Ability);
            ability?.Enabled(e.NewValue);
        }

        protected virtual void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            this.Handler.IsEnabled = e.NewValue;
        }

        private void OnUpdate()
        {
            if (Game.IsPaused)
            {
                return;
            }

            try
            {
                var heroes = EntityManager9.Units.Where(x => x.IsHero && x.IsAlive && !x.IsIllusion && x.IsVisible).ToList();

                foreach (var ability in this.Abilities.OrderBy(x => this.Settings.AbilityToggler.GetPriority(x.Ability.Name)))
                {
                    if (!ability.IsEnabled)
                    {
                        continue;
                    }

                    if (this.sleeper.IsSleeping(ability.OwnerHandle) || !ability.Ability.CanBeCasted() || ability.Owner.IsCharging)
                    {
                        continue;
                    }

                    var owner = ability.Owner;
                    if (!this.Settings.UseWhenInvisible && !owner.CanUseAbilitiesInInvisibility && owner.IsInvisible)
                    {
                        continue;
                    }

                    if (ability.UseAbility(heroes))
                    {
                        this.sleeper.Sleep(ability.OwnerHandle, ability.Ability.GetCastDelay() + 0.6f);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void UpdateRateOnValueChange(object sender, SliderEventArgs e)
        {
            this.Handler.SetUpdateRate(e.NewValue);
        }
    }
}