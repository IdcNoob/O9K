namespace O9K.AutoUsage.Abilities.LinkensBreak
{
    using System;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Helpers;

    using Settings;

    internal class AutoUsageLinkensBreakGroup<TType, TAbility> : AutoUsageGroup<TType, TAbility>
        where TType : class, IActiveAbility where TAbility : UsableAbility
    {
        public AutoUsageLinkensBreakGroup(MultiSleeper sleeper, GroupSettings settings)
            : base(sleeper, settings)
        {
        }

        public override void AddAbility(Ability9 ability)
        {
            var type = ability as TType;
            if (type == null)
            {
                return;
            }

            if (!(ability is ActiveAbility active) || !active.TargetsEnemy || !active.BreaksLinkens)
            {
                return;
            }

            var usableAbility = (TAbility)Activator.CreateInstance(typeof(TAbility), type, this.Settings);
            this.Abilities.Add(usableAbility);
            this.Settings.AddAbility(usableAbility);

            if (this.Settings.GroupEnabled)
            {
                this.Handler.IsEnabled = true;
            }
        }
    }
}