using System;

namespace RomanNumerals
{
    public static class FromRomanNumeral
    {
        /// <summary>
        /// Converts the specified string representation of a Roman Number to its integer equivalent 
        /// and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="romanNumber"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse(string romanNumber, out int result)
        {
            result = 0;

            var current = romanNumber.Trim();
            foreach (var converter in RomanDecade.RomanDecadeList)
            {
                var singleDigitValue = converter.GetValueOfFirstRomanDigit(current);
                var sizeOfRomanDigit = converter.GetRomanDigit(singleDigitValue).Length;
                var remaining = current.Substring(sizeOfRomanDigit);
                result += singleDigitValue;
                current = remaining;
            }

            if (current.Length > 0 || result == 0) return false;

            var t1 = ToRomanNumeral.ToString(result);

            return romanNumber.Trim().Equals(t1, StringComparison.OrdinalIgnoreCase);
        }


    }
}

