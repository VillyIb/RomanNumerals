using System;
using RomanNumeralContract;
using RomanNumerals;
using RomanNumeralsTest.UnitTest;

namespace RomanNumeralsTest
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            INumeralService testtarget = new RomanNumeralsService();

            var testrunner = new DevelopmentTest(testtarget);

            if (testrunner.Execute())
            {
                Console.WriteLine(testrunner.TestLog.ToString());
            }
            else
            {
                Console.WriteLine("Test had errors");
                Console.WriteLine(testrunner.TestLog.ToString());
            }

            Console.WriteLine("\r\npress Enter to end program");
            Console.ReadLine();
        }
    }
}
