using System;
using System.Collections.Generic;

namespace Infrastructure.MapConfigs
{
    public interface IMapConfig
    {
        IReadOnlyDictionary<ResourceType, int> NumberOfResources { get; }
        IReadOnlyList<Tuple<int, int>> CellCoordinates { get; }
        IReadOnlyList<Tuple<int, int>> SettlementsCoordinates { get; }
        IReadOnlyDictionary<int, int> NumbersOfNumbers { get; }
    }
}
