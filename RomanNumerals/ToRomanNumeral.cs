using System;
using System.Text;

namespace RomanNumerals
{
    /// <summary>
    /// Convert interger between 1 and 3999 to RomanNumeral.
    /// </summary>
    public static class ToRomanNumeral
    {
        /// <summary>
        /// Returns string representation of value as Roman Number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(int value)
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value), "min 1");
            if (value > 3999) throw new ArgumentOutOfRangeException(nameof(value), "max 3999");

            var result = new StringBuilder();
          
            foreach (var converter in RomanDecade.RomanDecadeList)
            {
                var singelDigitValue = converter.GetSingleDigitValue(value);
                result.Append( converter.GetRomanDigit(singelDigitValue));
            }
            
            return result.ToString();
        }


    }
}
