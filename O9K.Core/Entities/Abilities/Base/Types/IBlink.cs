namespace O9K.Core.Entities.Abilities.Base.Types
{
    using Components.Base;

    public enum BlinkType
    {
        Blink,

        Leap,

        Targetable
    }

    public interface IBlink : IActiveAbility
    {
        BlinkType BlinkType { get; }

        float Range { get; }
    }
}