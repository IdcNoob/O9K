namespace O9K.AutoUsage.Abilities
{
    using System.Collections.Generic;

    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Units;

    public abstract class UsableAbility
    {
        protected UsableAbility(IActiveAbility ability)
        {
            this.Ability = ability;
            this.Owner = ability.Owner;
            this.OwnerHandle = ability.Owner.Handle;
        }

        public IActiveAbility Ability { get; }

        public bool IsEnabled { get; private set; }

        public Unit9 Owner { get; }

        public uint OwnerHandle { get; }

        public virtual void Enabled(bool enabled)
        {
            this.IsEnabled = enabled;
        }

        public abstract bool UseAbility(List<Unit9> heroes);
    }
}