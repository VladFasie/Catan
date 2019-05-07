using System;
using System.Collections.Generic;

namespace Infrastructure.MapConfigs
{
    public sealed class SmallMapConfig : IMapConfig
    {
        
        public IReadOnlyDictionary<ResourceType, int> NumberOfResources => _numberOfResources;

        public IReadOnlyList<Tuple<int, int>> Coordinates => _coordinates;

        public IReadOnlyDictionary<int, int> NumbersOfNumbers => _numbersOfNumbers;

        #region private fields
        private static readonly IReadOnlyDictionary<ResourceType, int> _numberOfResources =
            new Dictionary<ResourceType, int>
            {
                [ResourceType.Desert] = 1,
                [ResourceType.Wood] = 4,
                [ResourceType.Clay] = 3,
                [ResourceType.Wool] = 4,
                [ResourceType.Ore] = 3,
                [ResourceType.Grain] = 4
            };

        private static readonly IReadOnlyList<Tuple<int, int>> _coordinates =
            new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-4, 2), new Tuple<int, int>(-4, 0), new Tuple<int, int>(-4, -2),
                new Tuple<int, int>(-2, 3), new Tuple<int, int>(-2, 1), new Tuple<int, int>(-2, -1), new Tuple<int, int>(-2, -3),
                new Tuple<int, int>(0, -4), new Tuple<int, int>(0, 2), new Tuple<int, int>(0, 0), new Tuple<int, int>(0, -2), new Tuple<int, int>(0, -4),
                new Tuple<int, int>(2, 3), new Tuple<int, int>(2, 1), new Tuple<int, int>(2, -1), new Tuple<int, int>(2, -3),
                new Tuple<int, int>(4, 2), new Tuple<int, int>(4, 0), new Tuple<int, int>(4, -2)
            };

        private static readonly IReadOnlyDictionary<int, int> _numbersOfNumbers =
            new Dictionary<int, int>
            {
                [2] = 1,
                [3] = 2,
                [4] = 2,
                [5] = 2,
                [6] = 2,
                [8] = 2,
                [9] = 2,
                [10] = 2,
                [11] = 2,
                [12] = 1,
            };
        #endregion
    }
}
