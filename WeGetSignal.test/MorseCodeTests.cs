namespace WeGetSignal.test
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class MorseCodeTests
    {

        [TestCase("shell")]
        [TestCase("halls")]
        [TestCase("slick")]
        [TestCase("vector")]
        [TestCase("bistro")]
        public void TestMorseParsing(string testString)
        {
            // Arrange
            var morse = Morse.FromUnencodedString(testString);

            // Act and Assert
            Assert.IsTrue(morse.GetPossibleStrings().Contains(testString, StringComparer.OrdinalIgnoreCase));
        }
    }
}
