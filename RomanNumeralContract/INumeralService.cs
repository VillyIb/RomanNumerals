namespace RomanNumeralContract
{

    /// <summary>
    /// General NumeralService Contract.
    /// </summary>
    public interface INumeralService
    {

        /// <summary>
        /// Converts the specified string representation of a Number to its integer equivalent 
        /// and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryParse(string s, out int result);


        /// <summary>
        /// Converts the specified integer value to a string representing the value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string ToString(int value);
    }
}
