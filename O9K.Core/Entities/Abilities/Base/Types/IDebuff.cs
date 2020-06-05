namespace O9K.Core.Entities.Abilities.Base.Types
{
    using Components.Base;

    public interface IDebuff : IActiveAbility
    {
        string DebuffModifierName { get; }
    }
}