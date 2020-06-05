namespace O9K.Core.Entities.Abilities.Heroes.Alchemist
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.alchemist_chemical_rage)]
    public class ChemicalRage : ActiveAbility, IBuff
    {
        private readonly SpecialData attackTimeData;

        public ChemicalRage(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackTimeData = new SpecialData(baseAbility, "base_attack_time");
        }

        public float AttackTime
        {
            get
            {
                return this.attackTimeData.GetValue(this.Level);
            }
        }

        public string BuffModifierName { get; } = "modifier_alchemist_chemical_rage";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}