using System;
using RomanNumeralContract;
using RomanNumerals;
using RomanNumeralsTest.UnitTest;

namespace RomanNumeralsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IRomanNumeralService testtarget = new RomanNumberService();

            var testrunner = new DevelopmentTest(testtarget);

            if (testrunner.Execute())
            {
                var msg = String.Format("Testing passed {0} tests without errors", testrunner.TestCount);
                Console.WriteLine(msg);
            }
            else
            {
                Console.WriteLine("Test had errors");
                Console.WriteLine(testrunner.TestLog.ToString());
            }

            Console.WriteLine("press Enter to end program");
            Console.ReadLine();
        }
    }
}
