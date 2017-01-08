using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Utilites;

using RomanNumeralContract;

namespace RomanNumerals.Test
{
    [TestClass]
    public class UnitTest1
    {
        [ClassInitialize]
        public static void SetupClass()
        { }


        [ClassCleanup]
        public static void TeardownClass()
        { }


        private IRomanNumeralService RomaNumeralService {get;set;}


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
        public void Test_Parse()
        {
            Assert.AreEqual<int>(10, RomaNumeralService.Parse("X"));
        }


        [TestMethod]
        public void Test_ToRomanNumber()
        {
            Assert.AreEqual<string>("X", RomaNumeralService.ToRomanNumber(10);
        }

    }
}
