using Infrastructure.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TestInfrastructure
{
    [TestClass]
    public class IEnumerableExtensionTest
    {
        private static readonly int listSize = 100;

        [TestMethod]
        public void Shuffle_Preserve_Elements()
        {
            // arrange
            var numbers = new List<int>(listSize);
            for (var i = 0; i < listSize; ++i)
                numbers.Add(i);

            // act
            var shuffledNumbers = numbers.Shuffle();

            // assert
            foreach (var x in numbers)
                Assert.IsTrue(shuffledNumbers.Contains(x));
            foreach (var x in shuffledNumbers)
                Assert.IsTrue(numbers.Contains(x));
        }
    }
}
