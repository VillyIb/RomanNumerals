using System;
using System.Text;

using RomanNumeralContract;

namespace RomanNumerals
{
    public class RomanNumeralsService : IRomanNumeralService
    {

        public int Parse(string romanNumber)
        {
            var result = 0;

            var current = romanNumber.Trim();
            foreach (var converter in RomanDecade.RomanDecadeList)
            {
                var singleDigitValue = converter.GetValueOfFirstRomanDigit(current);
                var sizeOfRomanDigit = converter.GetRomanDigit(singleDigitValue).Length;
                var remaining = current.Substring(sizeOfRomanDigit);
                result += singleDigitValue;
                current = remaining;
            }

            var t1 = ToRomanNumber(result);
            if (current.Length > 0) throw new ArgumentOutOfRangeException(nameof(romanNumber), String.Format("Parse '{0}', stopped parsing after '{1}'", romanNumber, t1));

            if (result == 0) throw new ArgumentOutOfRangeException(nameof(romanNumber), String.Format("Parse '{0}', result 0 is not valid", romanNumber));
            
            if (!(romanNumber.Trim().Equals(t1, StringComparison.OrdinalIgnoreCase))) throw new ArgumentOutOfRangeException(nameof(romanNumber), String.Format("Parse '{0}', not matching '{1}", romanNumber, t1));

            return result;
        }


        public string ToRomanNumber(int value)
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value), "min 1");
            if (value > 3999) throw new ArgumentOutOfRangeException(nameof(value), "max 3999");

            var result = new StringBuilder();

            foreach (var converter in RomanDecade.RomanDecadeList)
            {
                var singelDigitValue = converter.GetSingleDigitValue(value);
                result.Append(converter.GetRomanDigit(singelDigitValue));
            }

            return result.ToString();
        }
    }
}
