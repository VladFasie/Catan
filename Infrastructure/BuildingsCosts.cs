using System.Collections.Generic;

namespace Infrastructure
{
    public static class BuildingsCosts
    {
        public static readonly IEnumerable<Cost> Road =
            new List<Cost>
            {
                new Cost {Type = ResourceType.Wood, Quantity = 1 },
                new Cost {Type = ResourceType.Clay, Quantity = 1 }
            };

        public static readonly IEnumerable<Cost> Village =
            new List<Cost>
            {
                new Cost {Type = ResourceType.Wood, Quantity = 1 },
                new Cost {Type = ResourceType.Clay, Quantity = 1 },
                new Cost {Type = ResourceType.Grain, Quantity = 1 },
                new Cost {Type = ResourceType.Wool, Quantity = 1 }

            };

        public static readonly IEnumerable<Cost> City =
            new List<Cost>
            {
                new Cost {Type = ResourceType.Ore, Quantity = 3 },
                new Cost {Type = ResourceType.Grain, Quantity = 2 }
            };
    }
}
