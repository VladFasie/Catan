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

            var resources = GenerateResourceTypes(config).Shuffle();
            var numbers = GenerateNumbers(config).Shuffle();
            var cells = MatchCellsWithNumbers(resources, numbers);

            return new Map
            {
                Size = size,
                Cells = cells.AsReadOnly()
            };
        }

        private static ICollection<ResourceType> GenerateResourceTypes(IMapConfig config)
        {
            var result = new List<ResourceType>();

            foreach (var cellType in config.NumberOfResources.Keys)
            {
                var number = config.NumberOfResources[cellType];
                for (var i = 0; i < number; ++i)
                    result.Add(cellType);
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

        private static List<Cell> MatchCellsWithNumbers(IEnumerable<ResourceType> resources, IEnumerable<int> numbers)
        {
            var cellCount = resources.Count();
            var numberCount = numbers.Count();
            if (cellCount <= numberCount)
                throw new Exception("number of cells must be greather than number of numbers");

            var result = new List<Cell>();

            var idx = 0;
            foreach (var resource in resources)
            {
                if (resource == ResourceType.Desert)
                    result.Add(new Cell(resource, 0));
                else
                    result.Add(new Cell(resource, numbers.ElementAt(idx++)));
            }

            return result;
        }
    }
}
