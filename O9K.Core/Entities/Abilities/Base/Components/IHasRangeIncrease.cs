namespace O9K.Core.Entities.Abilities.Base.Components
{
    using Entities.Units;

    using Helpers.Range;

    public interface IHasRangeIncrease
    {
        bool IsRangeIncreasePermanent { get; }

        bool IsValid { get; }

        RangeIncreaseType RangeIncreaseType { get; }

        string RangeModifierName { get; }

        float GetRangeIncrease(Unit9 unit, RangeIncreaseType type);
    }
}