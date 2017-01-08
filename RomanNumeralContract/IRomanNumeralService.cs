namespace RomanNumeralContract
{
    public interface IRomanNumeralService
    {

        int Parse(string romanNumber);


        string ToRomanNumber(int value);
    }
}
