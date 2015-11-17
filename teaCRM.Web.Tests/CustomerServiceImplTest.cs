﻿using Spring.Context.Support;
using teaCRM.Service.CRM;
using teaCRM.Service.CRM.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using teaCRM.Entity;
using System.Collections.Generic;

namespace teaCRM.Web.Tests
{
    
    
    /// <summary>
    ///This is a test class for CustomerServiceImplTest and is intended
    ///to contain all CustomerServiceImplTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CustomerServiceImplTest
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
        ///A test for CustomerServiceImpl Context
        ///</summary>
        [TestMethod()]
        public void CustomerServiceImplContextTest()
        {
            var target = ContextRegistry.GetContext().GetObject("customerService");
            Assert.AreNotEqual(target,null);
        }

        /// <summary>
        ///A test for GetTrashOperating
        ///</summary>
        [TestMethod()]
        public void GetTrashOperatingTest()
        {
            ICustomerService target = (ICustomerService)ContextRegistry.GetContext().GetObject("customerService");
            string compNum ="10000"; // TODO: Initialize to an appropriate value
            int myappId =11; // TODO: Initialize to an appropriate value
            List<TFunOperating> expected = null; // TODO: Initialize to an appropriate value
            List<TFunOperating> actual;
            actual = target.GetTrashOperating(compNum, myappId);
            Assert.AreNotEqual(expected, actual);
         
        }
    }
}
