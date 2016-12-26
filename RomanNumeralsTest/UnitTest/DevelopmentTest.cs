using System;
using System.Text;

using RomanNumeralContract;
using Utilites;

namespace RomanNumeralsTest.UnitTest
{
    public class DevelopmentTest
    {
        private readonly INumeralService znumeralService;

        public StringBuilder TestLog { get; protected set; }

        private string CurrentTest { get; set; }

        public int TestCount { get; protected set; }

        public int PassedTestCount { get; protected set; }

        /// <summary>
        /// Prepare Test Setup.
        /// </summary>
        private void Setup(string testname)
        {
            CurrentTest = testname;
            TestCount++;
            TestLog.AppendFormat("\r\n{0:yyyy-MM-dd HH:mm} Starting test {1}", SystemDateTime.Now, CurrentTest);

            // E.g. Database preparation
        }

        /// <summary>
        /// Clenaup after Test.
        /// </summary>
        private void Teardown()
        {
            // E.g. Database Cleanup
            TestLog.AppendFormat("\r\n{0:yyyy-MM-dd HH:mm} Finished test {1}\r\n\n", SystemDateTime.Now, CurrentTest);
        }


        public void Assert(bool expectedTrue)
        {
            if (!(expectedTrue))
            {
                throw new ApplicationException(CurrentTest);
            }
        }


        public bool Execute()
        {
            // Switch individual test on/off by providing "True"|"False"

            do // Single pass with option to use break.
            {

                // test Alfa
                if (bool.Parse("false"))  // ! Compiler doesn't complain about fixed value using 'bool.Parse("xxx")'
                {
                    try
                    {
                        Setup("Alfa");

                        int result;
                        znumeralService.TryParse("IV", out result);

                        // TDD very first test with empty implementation is expexted to throw exception.
                        
                        //public bool TryParse(out int value, string roman)
                        //{
                        //    throw new NotImplementedException();
                        //}

                        TestLog.AppendFormat("\r\n*** Failed: expected to throw exception");
                    }
                    catch (NotImplementedException)
                    {
                        PassedTestCount++;
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                    }

                    finally
                    {
                        Teardown();
                    }
                }

                // test Bravo
                if (bool.Parse("true"))
                {
                    try
                    {
                        Setup("Bravo");

PassedTestCount++;
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                    }

                    finally
                    {
                        Teardown();
                    }
                }

                // test Charlie
                if (bool.Parse("true"))
                {
                    try
                    {
                        Setup("Charlie");

PassedTestCount++;
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                    }

                    finally
                    {
                        Teardown();
                    }
                }

                // test Delta
                if (bool.Parse("true"))
                {
                    try
                    {
                        Setup("Delta");

PassedTestCount++;
                    }

                    catch (Exception ex)
                    {
                        TestLog.AppendFormat("\r\n*** Failed: {0}", ex);
                    }

                    finally
                    {
                        Teardown();
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

        public DevelopmentTest(INumeralService _NumeralService)
{
    if (_NumeralService == null) { throw new ArgumentNullException("_NumeralService"); }

    znumeralService = _NumeralService;

    TestLog = new StringBuilder();
}
    }
}
