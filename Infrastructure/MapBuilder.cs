using Infrastructure.Extensions;
using Infrastructure.MapConfigs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public static class MapBuilder
    {
        public static Map GetMap(MapSize size)
        {
            IMapConfig config = null;
            if (size == MapSize.Small)
                config = new SmallMapConfig();
            else if (size == MapSize.Big)
                config = new BigMapConfig();

            var cells = GenerateCells(config).Shuffle();
            var numbers = GenerateNumbers(config).Shuffle();
            MatchCellsWithNumbers(cells, numbers);

            return new Map
            {
                Size = size,
                Cells = cells.ToList()
            };
        }

        private static ICollection<Cell> GenerateCells(IMapConfig config)
        {
            var result = new List<Cell>();

            foreach (var cellType in config.NumberOfResources.Keys)
            {
                var number = config.NumberOfResources[cellType];
                for (var i = 0; i < number; ++i)
                    result.Add(new Cell
                    {
                        Type = cellType
                    });
            }

            return result;
        }

        private static ICollection<int> GenerateNumbers(IMapConfig config)
        {
            var result = new List<int>();

            foreach (var number in config.NumbersOfNumbers.Keys)
            {
                var count = config.NumbersOfNumbers[number];
                for (var i = 0; i < count; ++i)
                    result.Add(number);
            }

            return result;
        }

        private static void MatchCellsWithNumbers(IEnumerable<Cell> cells, IEnumerable<int> numbers)
        {
            var cellCount = cells.Count();
            var numberCount = numbers.Count();

            if (cellCount <= numberCount)
                throw new Exception("number of cells must be greather than number of numbers");

            var idx = 0;
            foreach (var cell in cells)
            {
                if (cell.Type == ResourceType.Desert)
                    continue;

                cell.Number = numbers.ElementAt(idx++);
            }
        }
    }
}
