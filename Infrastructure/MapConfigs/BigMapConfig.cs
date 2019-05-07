using System;
using System.Collections.Generic;

namespace Infrastructure.MapConfigs
{
    public class BigMapConfig : IMapConfig
    {
        public IReadOnlyDictionary<ResourceType, int> NumberOfResources => throw new NotImplementedException();

        public IReadOnlyList<Tuple<int, int>> CellCoordinates => throw new NotImplementedException();
        public IReadOnlyList<Tuple<int, int>> SettlementsCoordinates => throw new NotImplementedException();

        public IReadOnlyDictionary<int, int> NumbersOfNumbers => throw new NotImplementedException();
    }
}
