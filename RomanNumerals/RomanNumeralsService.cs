using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RomanNumeralContract;

namespace RomanNumerals
{
    public class NumberService : INumeralService
    {
        #region Implementation of INumeralService

        /// <summary>
        /// Converts the specified string representation of a Roman Number to its integer equivalent 
        /// and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="roman"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryParse(string roman, out int result)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Converts the specified integer value to a string representing the value as Roman Number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ToString(int value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
