using System;
using System.Collections.Generic;

namespace RomanNumerals
{
    public class RomanDecadeConverter
    {
        /// <summary>
        /// Base value for the current Decade: {1|10|100|1000}
        /// </summary>
        public int DecadeBase { get; private set; }


        /// <summary>
        /// Roman representation for digits for the current DecadeBase.
        /// E.g.: {"", "I", "II,...,"IX"}
        /// </summary>
        public string[] PositionEncodedRomanDigits { get; private set; }


        private int Readindex(int value)
        {
            return value / DecadeBase % 10;
        }


        public int GetPositionOfMatchingRomanDigit(string roman)
        {
            for (var index = PositionEncodedRomanDigits.Length - 1; index > 0; index--)
            {
                var prope = PositionEncodedRomanDigits[index];
                var foundAtIndex = roman.IndexOf(prope, 0, StringComparison.OrdinalIgnoreCase);
                if (foundAtIndex == 0)
                {
                    return index;
                }
            }
            return 0;
        }


        /// <summary>
        /// Returns the Roman Number for the Current Decade for the specified value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetRoman(int value)
        {
            return PositionEncodedRomanDigits[Readindex(value)];
        }


        private static readonly RomanDecadeConverter RomanM = new RomanDecadeConverter { DecadeBase = 1000, PositionEncodedRomanDigits = new[] { "", "M", "MM", "MMM" } };
        private static readonly RomanDecadeConverter RomanC = new RomanDecadeConverter { DecadeBase = 0100, PositionEncodedRomanDigits = new[] { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" } };
        private static readonly RomanDecadeConverter RomanX = new RomanDecadeConverter { DecadeBase = 0010, PositionEncodedRomanDigits = new[] { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" } };
        private static readonly RomanDecadeConverter RomanI = new RomanDecadeConverter { DecadeBase = 0001, PositionEncodedRomanDigits = new[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" } };

        public static readonly List<RomanDecadeConverter> RomanDecadeList = new List<RomanDecadeConverter> { RomanM, RomanC, RomanX, RomanI };

    }


}
