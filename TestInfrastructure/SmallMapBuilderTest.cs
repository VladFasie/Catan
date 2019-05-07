using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestInfrastructure
{
    [TestClass]
    public class SmallMapBuilderTest
    {
        [TestMethod]
        public void Each_Number_Except_Two_And_Seven_And_Twelve_Appears_Exactly_Twice()
        {
            GenericMapBuilderTest.Each_Number_Except_Two_And_Seven_And_Twelve_Appears_Exactly(sut, 2);
        }

        [TestMethod]
        public void Two_And_Twelve_Appears_Exactly_Once()
        {
            GenericMapBuilderTest.Two_And_Twelve_Appears_Exactly(sut, 1);
        }

        [TestMethod]
        public void Cells_Type()
        {
            GenericMapBuilderTest.Cells_Type(MapBuilder.GetMap(MapSize.Small), MapSize.Small);
        }

        [TestMethod]
        public void Seven_Does_Not_Appears()
        {
            GenericMapBuilderTest.Seven_Does_Not_Appears(MapBuilder.GetMap(MapSize.Small));
        }
    }
}
