namespace O9K.AutoUsage.Abilities.ManaRestore
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Helpers;

    using Ensage;

    using Settings;

    internal class ManaRestoreGroup<TType, TAbility> : AutoUsageGroup<TType, TAbility>
        where TType : class, IActiveAbility where TAbility : ManaRestoreAbility
    {
        public ManaRestoreGroup(MultiSleeper sleeper, GroupSettings settings)
            : base(sleeper, settings)
        {
        }

        public override void AddAbility(Ability9 ability)
        {
            if (ability.Id == AbilityId.item_bottle)
            {
                return;
            }

            base.AddAbility(ability);
        }
    }
}