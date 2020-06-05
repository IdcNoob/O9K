namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_bloodstone)]
    public class Bloodstone : ActiveAbility, IHealthRestore
    {
        private readonly SpecialData regen;

        public Bloodstone(Ability baseAbility)
            : base(baseAbility)
        {
            this.regen = new SpecialData(baseAbility, "mana_cost_percentage");
        }

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_item_bloodstone_active";

        public bool RestoresAlly { get; } = false;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)(unit.Mana * (this.regen.GetValue(this.Level) / 100f));
        }
    }
}