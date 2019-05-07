using System;
using System.Collections.Generic;

namespace Infrastructure.MapConfigs
{
    public interface IMapConfig
    {
        IReadOnlyDictionary<ResourceType, int> NumberOfResources { get; }
        IReadOnlyList<Tuple<int, int>> Coordinates { get; }
        IReadOnlyDictionary<int, int> NumbersOfNumbers { get; }
    }
}
