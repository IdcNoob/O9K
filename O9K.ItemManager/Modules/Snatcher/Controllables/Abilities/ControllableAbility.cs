namespace O9K.ItemManager.Modules.Snatcher.Controllables.Abilities
{
    using Core.Data;
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Helpers;

    internal class ControllableAbility
    {
        public ControllableAbility(ActiveAbility ability)
        {
            this.Ability = ability;
            this.Handle = ability.Handle;
        }

        public ActiveAbility Ability { get; }

        public bool CanBeCasted
        {
            get
            {
                return !this.Sleeper.IsSleeping && this.Ability.IsValid && this.Ability.CanBeCasted();
            }
        }

        public uint Handle { get; }

        protected Sleeper Sleeper { get; } = new Sleeper();

        public virtual void UseAbility(Unit9 roshan)
        {
            if (roshan?.IsVisible == true)
            {
                if (this.Ability.Owner.Distance(roshan.Position) > this.Ability.CastRange)
                {
                    return;
                }

                if (this.Ability.GetDamage(roshan) < roshan.Health)
                {
                    return;
                }

                this.Ability.UseAbility(roshan);
            }
            else
            {
                if (this.Ability.Owner.Distance(GameData.RoshanPosition) > this.Ability.CastRange)
                {
                    return;
                }

                this.Ability.UseAbility(GameData.RoshanPosition);
            }

            this.Sleeper.Sleep(0.5f);
        }
    }
}