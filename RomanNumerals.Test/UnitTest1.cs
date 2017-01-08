using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Utilites;

using RomanNumeralContract;

namespace RomanNumerals.Test
{
    [TestClass]
    public class UnitTest1
    {

        private IRomanNumeralService RomaNumeralService { get; set; }


        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitialize]
        public void Setup()
        {
            SystemDateTime.SetTime(new DateTime(2017, 1, 1, 12, 0, 0)); // Always perform unit test with same date/time settings
            RomaNumeralService = new RomanNumerals.RomanNumeralsService();
        }


        [TestCleanup]
        public void Teardown()
        {
            RomaNumeralService = null;
            SystemDateTime.Reset();
        }


        [TestMethod]
        public void Test_Parse_1()
        {
            Assert.AreEqual<int>(10, RomaNumeralService.Parse("X"));
        }


        [TestMethod]
        public void Test_ToRomanNumber_1()
        {
            Assert.AreEqual<string>("X", RomaNumeralService.ToRomanNumber(10));
        }


        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Test_ToRoman_zero()
        {
            RomaNumeralService.ToRomanNumber(0);
        }


        [TestMethod]
        public void Test_ToRoman_zero_HasErrorMessage()
        {
            try
            {
                RomaNumeralService.ToRomanNumber(0);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsFalse(String.IsNullOrWhiteSpace(ex.Message));
            }
        }


        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Test_ToRoman_4000()
        {
            RomaNumeralService.ToRomanNumber(4000);
        }


        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Test_Parse_Fail_4567()
        {
            Assert.AreEqual<int>(10, RomaNumeralService.Parse("MMMMDLXVII")); // 4567 outside valid range.
        }


        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Test_Parse_Fail_1999Perm()
        {
            Assert.AreEqual<int>(10, RomaNumeralService.Parse("XCMCMIX")); // 1999 permutated
        }


        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Test_Parse_Fail_2444Perm()
        {
            Assert.AreEqual<int>(10, RomaNumeralService.Parse("XLMMCDIV")); // 2444 permutated
        }


        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Test_Parse_Fail_Garbage_AAAA()
        {
            Assert.AreEqual<int>(10, RomaNumeralService.Parse("AAAAA"));
        }


        [TestMethod]
        public void Test_Parse_blanksArount_MCMXCIX()
        {
            Assert.AreEqual<int>(1999, RomaNumeralService.Parse(" MCMXCIX "));
        }


        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Test_Parse_Fail_MCM_XCIX()
        {
            Assert.AreEqual<int>(1999, RomaNumeralService.Parse("MCM_XCIX"));
        }


        [TestMethod]
        public void Test_Parse_HasErrorMessage_2444Perm()
        {
            try
            {
                RomaNumeralService.Parse("XLMMCDIV"); // 2444 permutated
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsFalse(String.IsNullOrWhiteSpace(ex.Message));
            }
        }


        [TestMethod]
        public void Test_ToRomanNumber_1897()
        {
            Assert.AreEqual<string>("MDCCCXCVII", RomaNumeralService.ToRomanNumber(1897));
        }


        [TestMethod]
        public void Test_ToRomanNumber_acceptance()
        {
            Assert.AreEqual<string>("MCMXCIX", RomaNumeralService.ToRomanNumber(1999));
            Assert.AreEqual<string>("MMCDXLIV", RomaNumeralService.ToRomanNumber(2444));
            Assert.AreEqual<string>("XC", RomaNumeralService.ToRomanNumber(90));
        }


        [TestMethod]
        public void Test_Parse_Acceptance()
        {
            Assert.AreEqual<int>(1999, RomaNumeralService.Parse("MCMXCIX"));
            Assert.AreEqual<int>(2444, RomaNumeralService.Parse("MMCDXLIV"));
            Assert.AreEqual<int>(90, RomaNumeralService.Parse("XC"));
        }


    }
}
