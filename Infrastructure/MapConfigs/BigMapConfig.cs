using System;
using System.Collections.Generic;

namespace Infrastructure.MapConfigs
{
    public class BigMapConfig : IMapConfig
    {
        public IReadOnlyDictionary<ResourceType, int> NumberOfResources => throw new NotImplementedException();

        public IReadOnlyList<Tuple<int, int>> Coordinates => throw new NotImplementedException();

        public IReadOnlyDictionary<int, int> NumbersOfNumbers => throw new NotImplementedException();
    }
}
