namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_power_treads)]
    public class PowerTreads : ActiveAbility
    {
        private readonly Sleeper expectedAttributeSleeper = new Sleeper();

        private readonly Ensage.Items.PowerTreads powerTreads;

        private Attribute expectedAttribute;

        public PowerTreads(Ability baseAbility)
            : base(baseAbility)
        {
            this.powerTreads = (Ensage.Items.PowerTreads)baseAbility;
        }

        public Attribute ActiveAttribute
        {
            get
            {
                if (this.expectedAttributeSleeper)
                {
                    return this.expectedAttribute;
                }

                return this.powerTreads.ActiveAttribute;
            }
        }

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            //if (this.ActionSleeper.IsSleeping)
            //{
            //    return false;
            //}

            if (!this.IsReady)
            {
                return false;
            }

            if (!this.Owner.IsAlive || !this.Owner.CanBeHealed)
            {
                return false;
            }

            if (checkChanneling && !this.CanBeCastedWhileChanneling && this.Owner.IsChanneling)
            {
                return false;
            }

            if (this.Owner.IsStunned && (this.Owner.UnitState & UnitState.OutOfGame) == 0)
            {
                return false;
            }

            if (this.Owner.IsMuted)
            {
                return false;
            }

            return true;
        }

        public void ChangeExpectedAttribute(bool single)
        {
            if (!this.expectedAttributeSleeper)
            {
                this.expectedAttribute = this.powerTreads.ActiveAttribute;
            }

            switch (this.expectedAttribute)
            {
                case Attribute.Agility:
                    this.expectedAttribute = single ? Attribute.Strength : Attribute.Intelligence;
                    break;
                case Attribute.Strength:
                    this.expectedAttribute = single ? Attribute.Intelligence : Attribute.Agility;
                    break;
                case Attribute.Intelligence:
                    this.expectedAttribute = single ? Attribute.Agility : Attribute.Strength;
                    break;
            }

            this.expectedAttributeSleeper.Sleep(0.5f);
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            var result = this.BaseAbility.UseAbility(queue, bypass);
            if (result)
            {
                this.ChangeExpectedAttribute(true);
                // this.ActionSleeper.Sleep(0.03f);
            }

            return result;
        }

        public bool UseAbilitySimple()
        {
            return this.BaseAbility.UseAbility();
        }
    }
}