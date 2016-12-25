using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RomanNumeralContract;

namespace RomanNumerals
{
    public class RomanNumberService : IRomanNumeralService
    {
        #region Implementation of IRomanNumeralService

        /// <summary>
        /// Converts the specified string representation of a Roman Number to its integer equivalent 
        /// and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="roman"></param>
        /// <returns></returns>
        public bool TryParse(out int value, string roman)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the specified integer value to a string representing the value as Roman Number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ToRoman(int value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
