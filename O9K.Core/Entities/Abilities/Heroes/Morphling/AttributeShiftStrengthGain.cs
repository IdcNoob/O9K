namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
    using System;
    using System.Linq;

    using Base;
    using Base.Types;

    using Data;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.morphling_morph_str)]
    public class AttributeShiftStrengthGain : ToggleAbility, IHealthRestore
    {
        private readonly SpecialData attributeGainData;

        public AttributeShiftStrengthGain(Ability baseAbility)
            : base(baseAbility)
        {
            this.attributeGainData = new SpecialData(baseAbility, "morph_rate_tooltip");
        }

        public AttributeShiftAgilityGain AttributeShiftAgilityGain { get; private set; }

        public override bool CanBeCastedWhileChanneling { get; } = true;

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_morphling_morph_str";

        public bool RestoresAlly { get; } = false;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return 0;
        }

        public float HealthGain(float seconds)
        {
            //todo move to HealthRestoreValue ?
            return this.attributeGainData.GetValue(this.Level) * seconds * GameData.HealthGainPerStrength;
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.morphling_morph_agi && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.AttributeShiftAgilityGain));
            }

            this.AttributeShiftAgilityGain = (AttributeShiftAgilityGain)EntityManager9.AddAbility(ability);
        }
    }
}