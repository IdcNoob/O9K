namespace O9K.AutoUsage.Abilities.Special
{
    using System;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Helpers;
    using Core.Managers.Menu.EventArgs;

    using Ensage;

    using Settings;

    using Unique.SpeedBurst;

    internal class AutoUsageSpecialGroup<TType, TAbility> : AutoUsageGroup<TType, TAbility>
        where TType : class, IActiveAbility where TAbility : UsableAbility
    {
        private readonly SpeedBurstAbility speedBurstAbility = new SpeedBurstAbility();

        public AutoUsageSpecialGroup(MultiSleeper sleeper, GroupSettings settings)
            : base(sleeper, settings)
        {
            settings.AddSettingsMenu();
            settings.AbilityToggler.AddAbility(AbilityId.courier_burst);

            if (settings.GroupEnabled && settings.AbilityToggler.IsEnabled(nameof(AbilityId.courier_burst)))
            {
                this.SpeedBurst(true);
            }
        }

        public override void AddAbility(Ability9 ability)
        {
            var type = ability as TType;
            if (type == null)
            {
                return;
            }

            if (!this.UniqueAbilities.TryGetValue(ability.Id, out var uniqueType))
            {
                return;
            }

            var usableAbility = (TAbility)Activator.CreateInstance(uniqueType, type, this.Settings);
            this.Abilities.Add(usableAbility);
            this.Settings.AddAbility(usableAbility);

            if (this.Settings.GroupEnabled)
            {
                this.Handler.IsEnabled = true;
            }
        }

        protected override void AbilityTogglerOnValueChange(object sender, AbilityEventArgs e)
        {
            if (e.Ability == nameof(AbilityId.courier_burst))
            {
                this.SpeedBurst(e.NewValue && this.Settings.GroupEnabled);
            }
            else
            {
                base.AbilityTogglerOnValueChange(sender, e);
            }
        }

        protected override void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            base.EnabledOnValueChange(sender, e);
            this.SpeedBurst(e.NewValue && this.Settings.AbilityToggler.IsEnabled(nameof(AbilityId.courier_burst)));
        }

        private void SpeedBurst(bool enable)
        {
            if (enable)
            {
                this.speedBurstAbility.Activate();
            }
            else
            {
                this.speedBurstAbility.Deactivate();
            }
        }
    }
}