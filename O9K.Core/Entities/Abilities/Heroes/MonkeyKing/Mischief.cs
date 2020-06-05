namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
    using System;
    using System.Linq;

    using Base;

    using Ensage;

    using Entities.Units;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.monkey_king_mischief)]
    public class Mischief : ActiveAbility
    {
        private RevertForm revertForm;

        public Mischief(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            if (!this.IsUsable)
            {
                return this.revertForm.CanBeCasted(checkChanneling);
            }

            return base.CanBeCasted(checkChanneling);
        }

        public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
        {
            if (!this.IsUsable)
            {
                return this.revertForm.UseAbility(target, queue, bypass);
            }

            return base.UseAbility(target, queue, bypass);
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            if (!this.IsUsable)
            {
                return this.revertForm.UseAbility(queue, bypass);
            }

            return base.UseAbility(queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.monkey_king_untransform && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.revertForm));
            }

            this.revertForm = (RevertForm)EntityManager9.AddAbility(ability);
        }
    }
}