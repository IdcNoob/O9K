namespace O9K.Core.Managers.Jungle
{
    using System.Collections.Generic;

    using Camp;

    public interface IJungleManager
    {
        IEnumerable<IJungleCamp> JungleCamps { get; }
    }
}