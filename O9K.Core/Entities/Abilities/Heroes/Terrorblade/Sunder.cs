namespace O9K.Core.Entities.Abilities.Heroes.Terrorblade
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.terrorblade_sunder)]
    public class Sunder : RangedAbility, IHealthRestore
    {
        public Sunder(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = false;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return 0;
        }
    }
}