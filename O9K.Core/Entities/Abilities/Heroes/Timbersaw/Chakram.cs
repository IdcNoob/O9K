namespace O9K.Core.Entities.Abilities.Heroes.Timbersaw
{
    using System;
    using System.Linq;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.shredder_chakram)]
    [AbilityId(AbilityId.shredder_chakram_2)]
    public class Chakram : CircleAbility, INuke
    {
        public Chakram(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "pass_damage");
        }

        public ReturnChakram ReturnChakram { get; private set; }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return base.GetRawDamage(unit, remainingHealth) * 2;
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            return this.ReturnChakram.UseAbility(queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == (this.Id == AbilityId.shredder_chakram
                                  ? AbilityId.shredder_return_chakram
                                  : AbilityId.shredder_return_chakram_2) && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.ReturnChakram));
            }

            this.ReturnChakram = (ReturnChakram)EntityManager9.AddAbility(ability);
        }
    }
}