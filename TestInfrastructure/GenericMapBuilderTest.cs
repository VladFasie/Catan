using Infrastructure;
using Infrastructure.MapConfigs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestInfrastructure
{
    public static class GenericMapBuilderTest
    {
        public static void Cells_Type(Map map, MapSize size)
        {
            IMapConfig config;
            if (size == MapSize.Small)
                config = new SmallMapConfig();
            else
                config = new BigMapConfig();

            var grainActual = 0;
            var woodActual = 0;
            var clayActual = 0;
            var oreActual = 0;
            var woolActual = 0;
            var desertActual = 0;

            foreach (var cell in map.Cells)
                switch (cell.Type)
                {
                    case ResourceType.Grain: grainActual++; break;
                    case ResourceType.Wood: woodActual++; break;
                    case ResourceType.Clay: clayActual++; break;
                    case ResourceType.Ore: oreActual++; break;
                    case ResourceType.Wool: woolActual++; break;
                    case ResourceType.Desert: desertActual++; break;
                    default: break;
                }

            Assert.AreEqual(config.NumberOfResources[ResourceType.Grain], grainActual);
            Assert.AreEqual(config.NumberOfResources[ResourceType.Wood], woodActual);
            Assert.AreEqual(config.NumberOfResources[ResourceType.Clay], clayActual);
            Assert.AreEqual(config.NumberOfResources[ResourceType.Ore], oreActual);
            Assert.AreEqual(config.NumberOfResources[ResourceType.Wool], woolActual);
            Assert.AreEqual(config.NumberOfResources[ResourceType.Desert], desertActual);
        }

        public static void Two_And_Twelve_Appears_Exactly(Builder sut, int times)
        {
            // arrange
            var twos = 0;
            var twelves = 0;

            // act
            var map = sut.Build();

            // assert
            foreach (var cell in map.Cells)
                switch (cell.Number)
                {
                    case 2: twos++; break;
                    case 12: twelves++; break;
                    default: break;
                }
            Assert.AreEqual(times, twos);
            Assert.AreEqual(times, twelves);
        }

        public static void Each_Number_Except_Two_And_Seven_And_Twelve_Appears_Exactly(Builder sut, int times)
        {
            // arrange
            var counter = new int[13];

            // act
            var map = sut.Build();

            // assert
            foreach (var cell in map.Cells)
                counter[cell.Number]++;
            for (var i = 3; i <= 11; ++i)
                if (i != 7)
                    Assert.AreEqual(times, counter[i]);
        }

        public static void Seven_Does_Not_Appears(Map map)
        {
            // assert
            foreach (var cell in map.Cells)
                if (cell.Number == 7)
                    Assert.Fail();
        }
    }
}
