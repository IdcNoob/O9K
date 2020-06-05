namespace O9K.Core.Entities.Abilities.Base
{
    using Components;

    using Ensage;

    public abstract class AutoCastAbility : RangedAbility, IToggleable
    {
        protected AutoCastAbility(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool BreaksLinkens { get; } = false;

        public virtual bool Enabled
        {
            get
            {
                return this.BaseAbility.IsAutoCastEnabled;
            }
            set
            {
                var result = false;
                if (value)
                {
                    if (!this.Enabled)
                    {
                        result = this.BaseAbility.ToggleAutocastAbility();
                    }
                }
                else
                {
                    if (this.Enabled)
                    {
                        result = this.BaseAbility.ToggleAutocastAbility();
                    }
                }

                if (result)
                {
                    this.ActionSleeper.Sleep(0.1f);
                }
            }
        }
    }
}