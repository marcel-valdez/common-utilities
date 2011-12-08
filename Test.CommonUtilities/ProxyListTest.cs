using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CommonUtilities.Test
{
    
    
    /// <summary>
    ///This is a test class for ProxyListTest and is intended
    ///to contain all ProxyListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProxyListTest
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
        ///A test for ProxyList`2 Constructor
        ///</summary>
        public void ProxyListConstructorTestHelper<TProxy, TSource>(IList<TSource> sourceList, TProxy extraItem)
            where TProxy : TSource
        {
            ProxyList<TProxy, TSource> target = new ProxyList<TProxy, TSource>(sourceList);
            if (!target.IsReadOnly)
            {
                target.Add(extraItem);
            }

            Assert.AreEqual(sourceList.Count, target.Count);
        }

        [TestMethod()]
        public void ProxyListConstructorTest()
        {
            ProxyListConstructorTestHelper<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper[] { new GenericParameterHelper(10) }, new GenericParameterHelper(5));
        }
    }
}
