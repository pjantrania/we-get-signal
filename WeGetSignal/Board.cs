namespace WeGetSignal
{
    public class Board
    {
        public string SerialNumber { get; set; }

        public bool SerialNumberOdd()
        {
            if (string.IsNullOrEmpty(this.SerialNumber))
            {
                return false;
            }

            int lastDigit;
            if (!int.TryParse(this.SerialNumber.Substring(this.SerialNumber.Length-1), out lastDigit))
            {
                return false;
            }

            return lastDigit % 2 != 0;
        }
    }
}
