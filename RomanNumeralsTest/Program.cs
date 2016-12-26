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
            INumeralService testtarget = new NumberService();

            var testrunner = new DevelopmentTest(testtarget);

            if (testrunner.Execute())
            {
                Console.WriteLine(testrunner.TestLog.ToString());
                //var msg = String.Format("Testing passed {0} tests without errors", testrunner.TestCount);
                //Console.WriteLine(msg);
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
