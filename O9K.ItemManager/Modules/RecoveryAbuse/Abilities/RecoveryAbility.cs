namespace O9K.ItemManager.Modules.RecoveryAbuse.Abilities
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    internal class RecoveryAbility
    {
        private readonly Sleeper usedSleeper = new Sleeper();

        public RecoveryAbility(Ability9 ability)
        {
            this.Ability = (ActiveAbility)ability;
            this.Owner = ability.Owner;
            this.Handle = ability.Handle;
            this.Id = ability.Id;
            this.Name = ability.Name;

            this.ManaRestore = ability as IManaRestore;
            this.HealthRestore = ability as IHealthRestore;
            this.InstantRestore = this.ManaRestore?.InstantRestore == true || this.HealthRestore?.InstantRestore == true;
        }

        public ActiveAbility Ability { get; }

        public float Delay
        {
            get
            {
                return this.Ability.CastPoint;
            }
        }

        public uint Handle { get; }

        public AbilityId Id { get; }

        public bool InstantRestore { get; }

        public string Name { get; }

        public virtual Unit9 Owner { get; }

        public virtual Attribute PowerTreadsAttribute
        {
            get
            {
                if (this.Ability.ManaCost > 0)
                {
                    return Attribute.Intelligence;
                }

                var missingHp = this.RestoresHealth;
                var missingMp = this.RestoresMana;

                if (missingHp && missingMp)
                {
                    return Attribute.Agility;
                }

                var primaryAttribute = this.Owner.PrimaryAttribute;

                if (missingMp && primaryAttribute == Attribute.Intelligence)
                {
                    return Attribute.Strength;
                }

                if (missingHp && primaryAttribute == Attribute.Strength)
                {
                    return Attribute.Agility;
                }

                return Attribute.Invalid;
            }
        }

        public bool RestoresHealth
        {
            get
            {
                if (this.HealthRestore == null)
                {
                    return false;
                }

                if (this.Owner.MaximumHealth - this.Owner.Health <= this.HealthRestore.GetHealthRestore(this.Owner))
                {
                    return false;
                }

                return true;
            }
        }

        public bool RestoresMana
        {
            get
            {
                if (this.ManaRestore == null)
                {
                    return false;
                }

                if (this.Owner.MaximumMana - this.Owner.Mana <= this.ManaRestore.GetManaRestore(this.Owner))
                {
                    return false;
                }

                return true;
            }
        }

        protected IHealthRestore HealthRestore { get; }

        protected IManaRestore ManaRestore { get; }

        public virtual bool CanBeCasted()
        {
            if (this.usedSleeper)
            {
                return false;
            }

            if (!this.Ability.IsValid)
            {
                return false;
            }

            if (!this.Ability.CanBeCasted())
            {
                return false;
            }

            if (!this.ShouldUse())
            {
                return false;
            }

            return true;
        }

        public virtual bool CanPickUpItems()
        {
            if (this.ManaRestore?.InstantRestore == false)
            {
                var modifier = this.Owner.GetModifier(this.ManaRestore.RestoreModifierName);
                if (modifier?.RemainingTime > 0.15f || this.usedSleeper)
                {
                    return false;
                }
            }

            if (this.HealthRestore?.InstantRestore == false)
            {
                var modifier = this.Owner.GetModifier(this.HealthRestore.RestoreModifierName);
                if (modifier?.RemainingTime > 0.15f || this.usedSleeper)
                {
                    return false;
                }
            }

            return true;
        }

        public virtual bool ShouldUse()
        {
            if (this.ManaRestore != null)
            {
                if (this.ManaRestore.InstantRestore && this.Owner.HasModifier(this.ManaRestore.RestoreModifierName))
                {
                    return false;
                }

                if (this.Owner.MaximumMana - this.Owner.Mana > 50)
                {
                    return true;
                }
            }

            if (this.HealthRestore != null)
            {
                if (this.HealthRestore.InstantRestore && this.Owner.HasModifier(this.HealthRestore.RestoreModifierName))
                {
                    return false;
                }

                if (this.Owner.MaximumHealth - this.Owner.Health > 50)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool UseAbility()
        {
            if (this.ManaRestore?.InstantRestore == false)
            {
                var modifier = this.Owner.GetModifier(this.ManaRestore.RestoreModifierName);
                if (modifier?.RemainingTime > 0.1f)
                {
                    return false;
                }
            }

            if (this.HealthRestore != null)
            {
                var modifier = this.Owner.GetModifier(this.HealthRestore.RestoreModifierName);
                if (modifier?.RemainingTime > 0.1f)
                {
                    return false;
                }
            }

            if (this.Ability.NoTargetCast)
            {
                if (this.Ability.UseAbility())
                {
                    this.usedSleeper.Sleep(0.3f);
                    return true;
                }

                return false;
            }

            if (this.Ability.UseAbility(this.Owner))
            {
                this.usedSleeper.Sleep(0.3f);
                return true;
            }

            return false;
        }
    }
}