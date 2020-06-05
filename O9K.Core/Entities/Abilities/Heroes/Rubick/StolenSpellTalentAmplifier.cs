namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
    using System.Linq;

    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.special_bonus_unique_rubick_5)]
    public class StolenSpellTalentAmplifier : Talent /*, IHasDamageAmplify*/
    {
        private readonly float amplifierValue;

        public StolenSpellTalentAmplifier(Ability baseAbility)
            : base(baseAbility)
        {
            //todo add stolen ability check
            this.amplifierValue = this.BaseAbility.AbilitySpecialData.First(x => x.Name == "value").Value / 100;
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical | DamageType.Physical | DamageType.Pure;

        public string AmplifierModifierName { get; } = string.Empty;

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Outgoing;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = true;

        public float AmplifierValue(Unit9 target)
        {
            if (this.Level == 0)
            {
                return 0;
            }

            return this.amplifierValue;
        }
    }
}