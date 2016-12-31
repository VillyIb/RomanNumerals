using System;
using System.Collections.Generic;

namespace RomanNumerals
{
    public class RomanDecade
    {
        /// <summary>
        /// Base value for the current Decade: {1|10|100|1000}
        /// </summary>
        private int DecadeBase { get; set; }


        /// <summary>
        /// Roman representation for digits for the current DecadeBase.
        /// E.g.: {"", "I", "II,...,"IX"}
        /// </summary>
        private string[] PositionEncodedRomanDigits { get; set; }


        public int GetSingleDigitValue(int value)
        {
            return (value / DecadeBase % 10) * DecadeBase;
        }


        public int GetValueOfFirstRomanDigit(string roman)
        {
            for (var index = PositionEncodedRomanDigits.Length - 1; index > 0; index--)
            {
                var prope = PositionEncodedRomanDigits[index];
                var foundAtIndex = roman.IndexOf(prope, 0, StringComparison.OrdinalIgnoreCase);
                if (foundAtIndex == 0)
                {
                    return index * DecadeBase;
                }
            }
            return 0;
        }


        public string GetRomanDigit(int value)
        {
            var position = value / DecadeBase % 10;
            if (position < 0 || PositionEncodedRomanDigits.Length <= position) throw new ArgumentOutOfRangeException(nameof(position));

            return PositionEncodedRomanDigits[position];
        }


        private static readonly RomanDecade RomanM = new RomanDecade { DecadeBase = 1000, PositionEncodedRomanDigits = new[] { "", "M", "MM", "MMM" } };
        private static readonly RomanDecade RomanC = new RomanDecade { DecadeBase = 0100, PositionEncodedRomanDigits = new[] { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" } };
        private static readonly RomanDecade RomanX = new RomanDecade { DecadeBase = 0010, PositionEncodedRomanDigits = new[] { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" } };
        private static readonly RomanDecade RomanI = new RomanDecade { DecadeBase = 0001, PositionEncodedRomanDigits = new[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" } };


        public static readonly List<RomanDecade> RomanDecadeList = new List<RomanDecade> { RomanM, RomanC, RomanX, RomanI };

    }


}
