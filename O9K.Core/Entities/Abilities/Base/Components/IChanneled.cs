namespace O9K.Core.Entities.Abilities.Base.Components
{
    using Base;

    public interface IChanneled : IActiveAbility
    {
        float ChannelTime { get; }

        bool IsActivatesOnChannelStart { get; }
    }
}