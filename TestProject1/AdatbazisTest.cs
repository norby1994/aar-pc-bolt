﻿using PcBolt.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for AdatbazisTest and is intended
    ///to contain all AdatbazisTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdatbazisTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddProcesszorFoglala
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\SZTE\\2009-2010-2\\AdatbazisAlapuRendszerek\\PcBolt\\PcBolt", "/")]
        [UrlToTest("http://localhost:2006/")]
        public void AddProcesszorFoglalaTest()
        {
            string nev = string.Empty; // TODO: Initialize to an appropriate value
            Adatbazis.AddProcesszorFoglala(nev);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
