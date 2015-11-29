namespace WeGetSignal.test
{
    using NUnit.Framework;

    using WC = WireColor;
    using WP = WirePosition;

    [TestFixture]
    public class SimpleWiresTests
    {
        private const string OddSerial = "ABC123";
        private const string EvenSerial = "ABC456";

        // 3 wires
        [TestCase(WP.Second, EvenSerial, WC.Blue, WC.Black, WC.Yellow, TestName = "ThreeWires_NoRed_CutSecond")]
        [TestCase(WP.Third, OddSerial, WC.Red, WC.Blue, WC.White, TestName = "ThreeWires_LastWireWhite_CutLast")]
        [TestCase(WP.Second, EvenSerial, WC.Blue, WC.Blue, WC.Red, TestName = "ThreeWires_MultipleBlues_CutLastBlue")]
        [TestCase(WP.Third, EvenSerial, WC.Red, WC.Blue, WC.Yellow, TestName = "ThreeWires_Otherwise_CutLast")]
                  
        // 4 wires
        [TestCase(WP.Third, OddSerial, WC.Red, WC.Black, WC.Red, WC.Yellow, TestName = "FourWires_MultipleRedAndOddSerial_CutLastRed")]
        [TestCase(WP.First, EvenSerial, WC.Black, WC.Blue, WC.White, WC.Yellow, TestName = "FourWires_LastWireYellowAndNoRed_CutFirst")]
        [TestCase(WP.First, EvenSerial, WC.Red, WC.Blue, WC.Black, WC.Yellow, TestName = "FourWires_OneBlue_CutFirst")]
        [TestCase(WP.Fourth, EvenSerial, WC.Red, WC.Yellow, WC.Yellow, WC.Red, TestName = "FourWires_MultipleYellows_CutLast")]
        [TestCase(WP.Second, OddSerial, WC.Red, WC.Yellow, WC.Black, WC.Black, TestName = "FourWires_Otherwise_CutSecond")]
                  
        // 5 wires
        [TestCase(WP.Fourth, OddSerial, WC.Red, WC.Black, WC.Blue, WC.Yellow, WC.Black, TestName = "FiveWires_LastWireBlackAndOddSerial_CutFourth")]
        [TestCase(WP.First, EvenSerial, WC.Red, WC.Black, WC.Yellow, WC.Blue, WC.Yellow, TestName = "FiveWires_OneRedAndMultipleYellows_CutFirst")]
        [TestCase(WP.Second, OddSerial, WC.Blue, WC.Yellow, WC.Yellow, WC.Blue, WC.White, TestName = "FiveWires_NoBlack_CutSecond")]
        [TestCase(WP.First, EvenSerial, WC.Red, WC.White, WC.Black, WC.Blue, WC.Red, TestName = "FiveWires_Otherwise_CutFirst")]
                  
        // 6 wires
        [TestCase(WP.Third, OddSerial, WC.Red, WC.Blue, WC.Black, WC.Red, WC.Blue, WC.Black, TestName = "SixWires_NoYellowAndOddSerial_CutThird")]
        [TestCase(WP.Fourth, OddSerial, WC.Yellow, WC.White, WC.White, WC.Blue, WC.Black, WC.Red, TestName = "SixWires_OneYellowAndMultipleWhites_CutFourth")]
        [TestCase(WP.Sixth, EvenSerial, WC.Yellow, WC.White, WC.Blue, WC.Black, WC.Black, WC.Blue, TestName = "SixWires_NoRed_CutLast")]
        [TestCase(WP.Fourth, OddSerial, WC.Red, WC.Yellow, WC.White, WC.Blue, WC.Blue, WC.Black, TestName = "SixWires_Otherwise_CutFourth")]
        public void SolveSimpleWires(WirePosition expected, string serialNumber, params WireColor[] wires)
        {
            // Arrange
            var bomb = new Board { SerialNumber = serialNumber };
            var wireTest = new SimpleWires { WireList = wires, Board = bomb };

            // Act
            WirePosition actual = wireTest.GetWireToCut();

            // Assert
            Assert.AreEqual(expected, actual,
                string.Format("Expected: [{0}] in position [{1}], Actual: [{2}] in position [{3}]",
                wires[(int)expected],
                expected,
                wires[(int)actual],
                actual));
        }
    }
}
