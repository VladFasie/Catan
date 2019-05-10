using Infrastructure.PlayerDetails;

namespace Infrastructure
{
    public static class BuildingsCosts
    {
        public static readonly ReadOnlyResourceBag Road = new ReadOnlyResourceBag(new ResourceBag { Clay = 1, Wood = 1 });
        public static readonly ReadOnlyResourceBag Village = new ReadOnlyResourceBag(new ResourceBag { Clay = 1, Wood = 1, Grain = 1, Wool = 1 });
        public static readonly ReadOnlyResourceBag City = new ReadOnlyResourceBag(new ResourceBag { Ore = 3, Grain = 2});

        public static bool CanBuildRoad(ReadOnlyResourceBag resources)
        {
            return resources.Clay >= Road.Clay && resources.Wood >= Road.Wood;
        }

        public static bool CanBuildVillage(ReadOnlyResourceBag resources)
        {
            return resources.Clay >= Village.Clay && resources.Wood >= Village.Wood && resources.Grain >= Village.Grain && resources.Wool >= Village.Wool;
        }

        public static bool CanBuildCIty(ReadOnlyResourceBag resources)
        {
            return resources.Ore >= City.Ore && resources.Grain >= City.Grain;
        }
    }
}
