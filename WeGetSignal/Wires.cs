namespace WeGetSignal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SimpleWires : Module
    {
        private const int MinWires = 3;
        private const int MaxWires = 6;
        private readonly List<WireColor> wireList = new List<WireColor>(MaxWires);
        private readonly IList<Func<WirePosition>> solvingMethods;

        public IList<WireColor> WireList
        {
            get { return wireList; }
            set
            {
                wireList.Clear();
                wireList.AddRange(value);
            }
        }

        public SimpleWires()
        {
            this.solvingMethods = new Func<WirePosition>[]
            {
                this.SolveThreeWires,
                this.SolveFourWires,
                this.SolveFiveWires,
                this.SolveSixWires
            };
        }

        public WirePosition GetWireToCut()
        {
            if (this.wireList.Count < MinWires || this.wireList.Count > MaxWires)
            {
                throw new InvalidOperationException(string.Format("There must be between {0} and {1} wires.", MinWires, MaxWires));
            }

            return this.solvingMethods[this.wireList.Count - MinWires]();
        }

        private WirePosition SolveSixWires()
        {
            if (!this.wireList.Any(w => w == WireColor.Yellow) && this.Board.SerialNumberOdd())
            {
                return WirePosition.Third;
            }
            else if (this.wireList.Count(w => w == WireColor.Yellow) == 1 && this.wireList.Count(w => w == WireColor.White) > 1)
            {
                return WirePosition.Fourth;
            }
            else if (!this.wireList.Any(w => w == WireColor.Red))
            {
                return WirePosition.Sixth;
            }
            else
            {
                return WirePosition.Fourth;
            }
        }

        private WirePosition SolveFiveWires()
        {
            if (this.wireList.Last() == WireColor.Black && this.Board.SerialNumberOdd())
            {
                return WirePosition.Fourth;
            }
            else if (this.wireList.Count(w => w == WireColor.Red) == 1 && this.wireList.Count(w => w == WireColor.Yellow) > 1)
            {
                return WirePosition.First;
            }
            else if (!this.wireList.Any(w => w == WireColor.Black))
            {
                return WirePosition.Second;
            }
            else
            {
                return WirePosition.First;
            }
        }

        private WirePosition SolveFourWires()
        {
            if (this.wireList.Count(w => w == WireColor.Red) > 1 && this.Board.SerialNumberOdd())
            {
                return (WirePosition)this.wireList.LastIndexOf(WireColor.Red);
            }
            else if (this.wireList.Last() == WireColor.Yellow && !this.wireList.Any(w => w == WireColor.Red))
            {
                return WirePosition.First;
            }
            else if (this.wireList.Count(w => w == WireColor.Blue) == 1)
            {
                return WirePosition.First;
            }
            else if (this.wireList.Count(w => w == WireColor.Yellow) > 1)
            {
                return WirePosition.Fourth;
            }
            else
            {
                return WirePosition.Second;
            }
        }

        private WirePosition SolveThreeWires()
        {
            if (!this.wireList.Any(w => w == WireColor.Red))
            {
                return WirePosition.Second;
            }
            else if (this.wireList.Last() == WireColor.White)
            {
                return  WirePosition.Third;
            }
            else if (this.wireList.Count(w => w == WireColor.Blue) > 1)
            {
                return (WirePosition)this.wireList.LastIndexOf(WireColor.Blue);
            }
            else
            {
                return WirePosition.Third;
            }
        }
    }

    public enum WireColor
    {
        Red,
        White,
        Blue,
        Yellow,
        Black
    }

    public enum WirePosition
    {
        First = 0,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth
    }
}
