using Infrastructure.PlayerDetails;

namespace Infrastructure
{
    public static class BuildingsCosts
    {
        public static readonly ReadonlyResourceBag Road = new ReadonlyResourceBag(new ResourceBag { Clay = 1, Wood = 1 });
        public static readonly ReadonlyResourceBag Village = new ReadonlyResourceBag(new ResourceBag { Clay = 1, Wood = 1, Grain = 1, Wool = 1 });
        public static readonly ReadonlyResourceBag City = new ReadonlyResourceBag(new ResourceBag { Ore = 3, Grain = 2});

        public static bool CanBuildRoad(ReadonlyResourceBag resources)
        {
            return resources.Clay >= Road.Clay && resources.Wood >= Road.Wood;
        }

        public static bool CanBuildVillage(ReadonlyResourceBag resources)
        {
            return resources.Clay >= Village.Clay && resources.Wood >= Village.Wood && resources.Grain >= Village.Grain && resources.Wool >= Village.Wool;
        }

        public static bool CanBuildCIty(ReadonlyResourceBag resources)
        {
            return resources.Ore >= City.Ore && resources.Grain >= City.Grain;
        }
    }
}
