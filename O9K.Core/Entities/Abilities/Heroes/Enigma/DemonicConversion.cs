namespace O9K.Core.Entities.Abilities.Heroes.Enigma
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.enigma_demonic_conversion)]
    public class DemonicConversion : RangedAbility
    {
        public DemonicConversion(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool TargetsEnemy { get; } = false;
    }
}