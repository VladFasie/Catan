using Infrastructure.Extensions;
using Infrastructure.MapConfigs;
using System;
using System.Collections.Generic;
using System.Linq;

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
                new Tuple<int, int>(0, -2),
                new Tuple<int, int>(0, 2),
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
        private static readonly List<Tuple<int, int>> _settlementToCellOffset = _cellToSettlementOffset;
        private static readonly List<Tuple<int, int>> _settlementToSettlementPartialOffset = new List<Tuple<int, int>>
        {
            new Tuple<int, int>(0, -1),
            new Tuple<int, int>(0, 1)
        };

        public static Tuple<int, int> CellCoordinatesByIndexAndSize(int idx, MapSize size)
        {
            var config = ConfigByMapSize(size);
            return config.CellCoordinates[idx];
        }

        public static int CellIndexByCoordinates(Tuple<int, int> coord, MapSize size)
        {
            var config = ConfigByMapSize(size);
            return config.CellCoordinates.IndexOf(coord);
        }
        public static bool SettlementCoordinatesAreValid(Tuple<int, int> coord, MapSize size)
        {
            var config = ConfigByMapSize(size);
            return config.SettlementsCoordinates.Contains(coord);
        }

        #region Neighbour
        public static IEnumerable<Tuple<int, int>> NeighbourCellsCoordinatesForCell(Tuple<int, int> cell, MapSize size)
        {
            var config = ConfigByMapSize(size);
            var offsets = ApplyOffsets(cell, _cellToCellOffset);

            return offsets.Intersect(config.CellCoordinates);
        }

        public static IEnumerable<Tuple<int, int>> NeighbourSettlementsCoordinatesForCell(Tuple<int, int> cell, MapSize size)
        {
            var config = ConfigByMapSize(size);
            var offsets = ApplyOffsets(cell, _cellToSettlementOffset);

            return offsets.Intersect(config.SettlementsCoordinates);
        }

        public static IEnumerable<Tuple<int, int>> NeighbourCellsCoordinatesForSettlement(Tuple<int, int> settlement, MapSize size)
        {
            var config = ConfigByMapSize(size);
            var offsets = ApplyOffsets(settlement, _settlementToCellOffset);

            return offsets.Intersect(config.CellCoordinates);
        }

        public static IEnumerable<Tuple<int, int>> NeighbourSettlementsCoordinatesForSettlement(Tuple<int, int> settlement, MapSize size)
        {
            var config = ConfigByMapSize(size);
            var offsets = ApplyOffsets(settlement, _settlementToSettlementPartialOffset);

            var result = offsets.Intersect(config.CellCoordinates);

            var left = new Tuple<int, int>(settlement.Item1 - 1, settlement.Item2);
            var right = new Tuple<int, int>(settlement.Item1 - 1, settlement.Item2);

            var hasCellInLeft = config.CellCoordinates.IndexOf(left) != -1;
            var hasCellInRight = config.CellCoordinates.IndexOf(left) != -1;

            if (!hasCellInLeft)
            {
                var possibleLeftSettlement = new Tuple<int, int>(settlement.Item1 - 2, settlement.Item2);
                var settlementExists = config.SettlementsCoordinates.IndexOf(possibleLeftSettlement) != -1;
                if (settlementExists)
                    result.Append(possibleLeftSettlement);
            }

            if (!hasCellInLeft)
            {
                var possibleRightSettlement = new Tuple<int, int>(settlement.Item1 + 2, settlement.Item2);
                var settlementExists = config.SettlementsCoordinates.IndexOf(possibleRightSettlement) != -1;
                if (settlementExists)
                    result.Append(possibleRightSettlement);
            }

            return result;
        }
        #endregion

        private static IMapConfig ConfigByMapSize(MapSize size)
        {
            if (size == MapSize.Small)
                return _smallConfig;

            return _bigConfig;
        }

        private static List<Tuple<int, int>> ApplyOffsets(Tuple<int, int> coord, List<Tuple<int, int>> offsets)
        {
            var result = new List<Tuple<int, int>>();
            foreach (var offset in offsets)
            {
                var newCoord = new Tuple<int, int>(coord.Item1 + offset.Item1, coord.Item2 + offset.Item2);
                result.Add(newCoord);
            }
            return result;
        }
    }
}
