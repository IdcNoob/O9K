namespace O9K.Core.Entities.Abilities.Base.Types
{
    using Components.Base;

    public interface IBuff : IActiveAbility
    {
        string BuffModifierName { get; }

        bool BuffsAlly { get; }

        bool BuffsOwner { get; }
    }
}