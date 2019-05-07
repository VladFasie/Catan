using Infrastructure.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestInfrastructure
{
    [TestClass]
    public class BigMapBuilderTest
    {
        private readonly BigMapBuilder sut;

        public BigMapBuilderTest()
        {
            sut = new BigMapBuilder();
        }

        [TestMethod]
        public void Each_Number_Except_Two_And_Seven_And_Twelve_Appears_Exactly_Three_Times()
        {
            GenericMapBuilderTest.Each_Number_Except_Two_And_Seven_And_Twelve_Appears_Exactly(sut, 3);
        }

        [TestMethod]
        public void Two_And_Twelve_Appears_Exactly_Twice()
        {
            GenericMapBuilderTest.Two_And_Twelve_Appears_Exactly(sut, 2);
        }

        [TestMethod]
        public void Cells_Type()
        {
            GenericMapBuilderTest.Cells_Type(sut);
        }

        [TestMethod]
        public void Seven_Does_Not_Appears()
        {
            GenericMapBuilderTest.Seven_Does_Not_Appears(sut);
        }
    }
}
