using System;
using System.Text;
using RomanNumeralContract;
using Utilites;
// ReSharper disable RedundantJumpStatement

namespace RomanNumeralsTest.Test
{
    public class DevelopmentTest
    {
        private readonly IRomanNumberService NumeralService;


        public StringBuilder TestLog { get; protected set; }


        private string CurrentTest { get; set; }


        public int TestCount { get; protected set; }


        public int PassedTestCount { get; protected set; }


        public bool BreakOnException { get; set; }


        /// <summary>
        /// Prepare Test Setup.
        /// </summary>
        private void Setup(string testname, bool breakOnException = false)
        {
            BreakOnException = breakOnException;
            CurrentTest = testname;
            TestLog.AppendFormat("\r\n{0:yyyy-MM-dd HH:mm} Starting test '{1}'", SystemDateTime.Now, CurrentTest);

            // E.g. Database preparation
        }


        /// <summary>
        /// Clenup after Test.
        /// </summary>
        private void Teardown()
        {
            // E.g. Database Cleanup
            TestLog.AppendFormat("\r\n{0:yyyy-MM-dd HH:mm} Finished test '{1}'\r\n\n", SystemDateTime.Now, CurrentTest);
        }


        public void Assert(bool expectedTrue, string reference = "")
        {
            TestCount++;

            if (expectedTrue)
            {
                PassedTestCount++;
            }
            else
            {
                TestLog.AppendFormat("\r\n{0:yyyy-MM-dd HH:mm}   Failed test '{1}' - {2}", SystemDateTime.Now, CurrentTest, reference);

                if (BreakOnException) { throw new ApplicationException(CurrentTest); }
            }
        }


        public void AssertFail(bool expectedFalse, string reference = "")
        {
            TestCount++;

            if (expectedFalse == false)
            {
                PassedTestCount++;
            }
            else
            {
                TestLog.AppendFormat("\r\n{0:yyyy-MM-dd HH:mm}   Failed test '{1}' - {2}", SystemDateTime.Now, CurrentTest, reference);

                if (BreakOnException) { throw new ApplicationException(CurrentTest); }
            }
        }


        public bool Execute()
        {
            // Switch individual test on/off by providing "True"|"False"

            do // Single pass with option to use break.
            {
                if (bool.Parse("true"))  // ! Compiler doesn't complain about fixed value using 'bool.Parse("xxx")'
                {
                    try
                    {
                        Setup("First Test", BreakOnException = false);

                        int result;
                        NumeralService.TryParse("IV", out result);

                        // TDD very first test with empty implementation is expexted to throw exception.

                        //public bool TryParse(string roman, out int value)
                        //{
                        //    throw new NotImplementedException();
                        //}

                        Assert(true, "First test");
                    }
                    catch (NotImplementedException)
                    {
                        Assert(false, "NotImplementedException");
                        break;
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                        if (BreakOnException) { break; }
                    }

                    finally
                    {
                        Teardown();
                    }
                }


                if (bool.Parse("false"))
                {
                    try
                    {
                        Setup("Test Encode - decode", BreakOnException = true);

                        // ReSharper disable once TooWideLocalVariableScope
                        int t1;
                        for (var test = 1; test < 4000; test++)
                        {
                            var r6 = NumeralService.ToString(test);
                            var n6 = NumeralService.TryParse(r6, out t1) ? t1 : -1;
                            Assert(test == n6, test.ToString());
                        }
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                        if (BreakOnException) { break; }
                    }

                    finally
                    {
                        Teardown();
                    }
                }

                if (bool.Parse("true"))
                {
                    try
                    {
                        Setup("Acceptance test Encode");

                        // Third party online converter see:http://www.onlineconversion.com/roman_numerals_advanced.htm

                        Assert("MCMXCIX".Equals(NumeralService.ToString(1999), StringComparison.OrdinalIgnoreCase), "1999");
                        Assert("MMCDXLIV".Equals(NumeralService.ToString(2444), StringComparison.OrdinalIgnoreCase), "2444");
                        Assert("XC".Equals(NumeralService.ToString(90), StringComparison.OrdinalIgnoreCase), "90");
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                        if (BreakOnException) { break; }
                    }

                    finally
                    {
                        Teardown();
                    }
                }

                if (bool.Parse("true"))
                {
                    try
                    {
                        Setup("Acceptance Test Decode");

                        int t1;
                        Assert(1999 == (NumeralService.TryParse("MCMXCIX", out t1) ? t1 : -1), "1999");
                        Assert(2444 == (NumeralService.TryParse("MMCDXLIV", out t1) ? t1 : -1), "2444");
                        Assert(90 == (NumeralService.TryParse("XC", out t1) ? t1 : -1), "90");
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                        if (BreakOnException) { break; }
                    }

                    finally
                    {
                        Teardown();
                    }
                }

                if (bool.Parse("true"))
                {
                    try
                    {
                        Setup("Test lower boundary");
                        NumeralService.ToString(0); // expected ArgumentOutOfRangeException
                        Assert(false, "0");
                    }

                    catch (ArgumentOutOfRangeException)
                    {
                        Assert(true);
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                        if (BreakOnException) { break; }
                    }

                    finally
                    {
                        Teardown();
                    }
                }

                if (bool.Parse("true"))
                {
                    try
                    {
                        Setup("Test upper boundary");
                        NumeralService.ToString(4000); // expected ArgumentOutOfRangeException
                        Assert(false, "4000");
                    }

                    catch (ArgumentOutOfRangeException)
                    {
                        Assert(true);
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                        if (BreakOnException) { break; }
                    }

                    finally
                    {
                        Teardown();
                    }
                }

                if (bool.Parse("true"))
                {
                    try
                    {
                        Setup("Test garbage");
                        var garbageRoman = "MMMMDLXVII"; // 4567 outside valid range.
                        int t1;
                        AssertFail(NumeralService.TryParse(garbageRoman, out t1), "MMMMDLXVII");
                        AssertFail(NumeralService.TryParse("XCMCMIX", out t1), "XCMCMIX"); // 1999 permutated
                        AssertFail(NumeralService.TryParse("XLMMCDIV", out t1), "XLMMCDIV"); // 2444 permutated
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                        if (BreakOnException) { break; }
                    }

                    finally
                    {
                        Teardown();
                    }
                }

                if (bool.Parse("true"))
                {
                    try
                    {
                        SystemDateTime.SetTime(new DateTime(1897, 12, 12));

                        Setup("Test year");
                        var year = SystemDateTime.Today.Year;
                        Assert("MDCCCXCVII".Equals(NumeralService.ToString(year)), "1897");
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                        if (BreakOnException) { break; }
                    }

                    finally
                    {
                        Teardown();
                        SystemDateTime.Reset();
                    }
                }

            } while (false);

            TestLog.AppendFormat(
                "\r\nPerfomed {0} tests, Passed {1} testes"
                , TestCount
                , PassedTestCount
            );

            return PassedTestCount == TestCount;

        }

        // Using interface and dependency injection decouples the actual implementation from this project.
        // This test can be uset to test any implementation of interface "IRomanNummeralService".

        public DevelopmentTest(IRomanNumberService numeralService)
        {
            if (numeralService == null) { throw new ArgumentNullException(nameof(numeralService)); }

            NumeralService = numeralService;

            TestLog = new StringBuilder();
        }
    }
}
