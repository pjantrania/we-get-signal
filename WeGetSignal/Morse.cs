using System.Collections.Generic;
using System.Linq;

namespace WeGetSignal
{
    public class Morse : Module
    {
        public static readonly Dictionary<char, Morse> CharacterToMorse = new Dictionary<char, Morse>
        {
            {'A', Morse.FromEncodedString("SL") },
            {'B', Morse.FromEncodedString("LSSS") },
            {'C', Morse.FromEncodedString("LSLS") },
            {'D', Morse.FromEncodedString("LSS") },
            {'E', Morse.FromEncodedString("S") },
            {'F', Morse.FromEncodedString("SSLS") },
            {'G', Morse.FromEncodedString("LLS") },
            {'H', Morse.FromEncodedString("SSSS") },
            {'I', Morse.FromEncodedString("SS") },
            {'J', Morse.FromEncodedString("SLLL") },
            {'K', Morse.FromEncodedString("LSL") },
            {'L', Morse.FromEncodedString("SLSS") },
            {'M', Morse.FromEncodedString("LL") },
            {'N', Morse.FromEncodedString("LS") },
            {'O', Morse.FromEncodedString("LLL") },
            {'P', Morse.FromEncodedString("SLLS") },
            {'Q', Morse.FromEncodedString("LLSL") },
            {'R', Morse.FromEncodedString("SLS") },
            {'S', Morse.FromEncodedString("SSS") },
            {'T', Morse.FromEncodedString("L") },
            {'U', Morse.FromEncodedString("SSL") },
            {'V', Morse.FromEncodedString("SSSL") },
            {'W', Morse.FromEncodedString("SLL") },
            {'X', Morse.FromEncodedString("LSSL") },
            {'Y', Morse.FromEncodedString("LSLL") },
            {'Z', Morse.FromEncodedString("LLSS") },
            {'1', Morse.FromEncodedString("SLLLL") },
            {'2', Morse.FromEncodedString("SSLLL") },
            {'3', Morse.FromEncodedString("SSSLL") },
            {'4', Morse.FromEncodedString("SSSSL") },
            {'5', Morse.FromEncodedString("SSSSS") },
            {'6', Morse.FromEncodedString("LSSSS") },
            {'7', Morse.FromEncodedString("LLSSS") },
            {'8', Morse.FromEncodedString("LLLSS") },
            {'9', Morse.FromEncodedString("LLLLS") },
            {'0', Morse.FromEncodedString("LLLLL") }
        };

        public static readonly Dictionary<Morse, char> MorseToCharacter = 
            CharacterToMorse.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        private const char Dot = 'S';
        private const char Dash = 'L';
        private const int MaxCodeLength = 5; 
        private readonly List<byte> code = new List<byte>();

        public IList<byte> Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code.Clear();
                this.code.AddRange(value);
            }
        }

        public static Morse FromUnencodedString(string unencodedString)
        {
            var bytes = new List<byte>(unencodedString.Length * MaxCodeLength);
            foreach (char c in unencodedString.ToUpper().ToCharArray())
            {
                bytes.AddRange(CharacterToMorse[c].Code);
            }

            return new Morse { Code = bytes };
        }

        public static Morse FromEncodedString(string encodedString)
        {
            return new Morse { Code = GetMorseBytesFromEncodedString(encodedString) };
        }

        public Morse()
        {
        }

        public IEnumerable<string> GetPossibleStrings()
        {
            return GetPossibleStrings(string.Empty, this.code.ToArray());
        }

        public override bool Equals(object obj)
        {
            if (obj is Morse)
            {
                return this.code.SequenceEqual((obj as Morse).Code);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (byte i in this.code)
            {
                hash += i;
                hash <<= 1;
            }

            return hash;
        }

        private static IEnumerable<string> GetPossibleStrings(string prefix, byte[] code)
        {
            Queue<KeyValuePair<string, byte[]>> queue = new Queue<KeyValuePair<string, byte[]>>();
            queue.Enqueue(new KeyValuePair<string, byte[]>(prefix, code));
            while (queue.Any())
            {
                var kvp = queue.Dequeue();
                string prefixSoFar = kvp.Key;
                var remainingBytes = kvp.Value;
                var intermediateCode = new Morse { Code = remainingBytes };
                for (int i = 0; i <= remainingBytes.Count(); i++)
                {
                    intermediateCode = new Morse { Code = remainingBytes.Take(i).ToList() };
                    if (MorseToCharacter.ContainsKey(intermediateCode))
                    {
                        queue.Enqueue(new KeyValuePair<string, byte[]>(prefixSoFar + MorseToCharacter[intermediateCode],
                            remainingBytes.Skip(i).ToArray()));
                    }
                }

                if (!MorseToCharacter.ContainsKey(intermediateCode))
                {
                    yield return prefixSoFar;
                }
            }
        }

        private static IList<byte> GetMorseBytesFromEncodedString(string encodedString)
        {
            return encodedString.ToCharArray()
                .Select(c => c == Dot
                    ? (byte)MorseCode.Dot
                    : (byte)MorseCode.Dash).ToList();
        }
    }

    public enum MorseCode : byte
    {
        Dot,
        Dash
    }
}
