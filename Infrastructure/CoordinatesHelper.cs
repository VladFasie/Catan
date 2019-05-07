using Infrastructure.Extensions;
using Infrastructure.MapConfigs;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public static class CoordinatesHelper
    {
        private static readonly IMapConfig _smallConfig = new SmallMapConfig();
        private static readonly IMapConfig _bigConfig = new BigMapConfig();
        private static readonly List<Tuple<int, int>> _cellToCellOffset = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-2, -1),
                new Tuple<int, int>(-2, 1),
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(2, -1),
                new Tuple<int, int>(2, 1)
            };
        private static readonly List<Tuple<int, int>> _cellToSettlementOffset = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(-1, 0),
                new Tuple<int, int>(-1, 1),
                new Tuple<int, int>(1, -1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(1, 1),
            };
        private static readonly List<Tuple<int, int>> _settlementToCellOffset = new List<Tuple<int, int>>();

        public static List<Cell> NeighbourCells(Tuple<int, int> cell, Map map)
        {
            var result = new List<Cell>();
            IMapConfig config = ConfigByMapSize(map);

            foreach (var t in _cellToCellOffset)
            {
                var newCoord = new Tuple<int, int>(cell.Item1 + t.Item1, cell.Item2 + t.Item2);
                var idx = config.CellCoordinates.IndexOf(newCoord);
                if (idx != -1)
                    result.Add(map.Cells[idx]);
            }

            return result;
        }

        public static List<Tuple<int, int>> NeighbourSettlementsCoordinates(Tuple<int, int> cell, Map map)
        {
            var result = new List<Tuple<int, int>>();
            IMapConfig config = ConfigByMapSize(map);

            foreach (var t in _cellToSettlementOffset)
            {
                var newCoord = new Tuple<int, int>(cell.Item1 + t.Item1, cell.Item2 + t.Item2);
                var idx = config.CellCoordinates.IndexOf(newCoord);
                if (idx != -1)
                    result.Add(newCoord);
            }

            return result;
        }

        private static IMapConfig ConfigByMapSize(Map map)
        {
            if (map.Size == MapSize.Small)
                return _smallConfig;

            return _bigConfig;
        }
    }
}
