using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
namespace CommonUtilities.Test
{
    
    
    /// <summary>
    ///This is a test class for SerializableProxyListTest and is intended
    ///to contain all SerializableProxyListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SerializableProxyListTest
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
        ///A test for SerializableProxyList`2 Constructor
        ///</summary>
        public void SerializableProxyListConstructorTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            Assert.IsNotNull(target.AsSource());
        }

        [TestMethod()]
        public void SerializableProxyListConstructorTest()
        {
            SerializableProxyListConstructorTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for SerializableProxyList`2 Constructor
        ///</summary>
        public void SerializableProxyListConstructorTest1Helper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            IList<TSource> sourceList = new List<TSource>();
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>(sourceList);
        }

        [TestMethod()]
        public void SerializableProxyListConstructorTest1()
        {
            SerializableProxyListConstructorTest1Helper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        public void AddTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>(); 
            TProxy item = new TProxy();
            target.Add(item);
            Assert.AreEqual(1, target.Count);
            Assert.AreEqual(1, target.AsSource().Count);
        }

        [TestMethod()]
        public void AddTest()
        {
            AddTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for AsSource
        ///</summary>
        public void AsSourceTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            IList<TSource> expected = new List<TSource>();
            expected.Add(new TProxy().AsSource());
            IList<TSource> actual;
            target.SetSource(expected);
            actual = target.AsSource();
            CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
        }

        [TestMethod()]
        public void AsSourceTest()
        {
            AsSourceTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Clear
        ///</summary>
        public void ClearTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            TProxy item = new TProxy();
            target.Add(item);
            target.Clear();
            Assert.AreEqual(0, target.Count);
            Assert.AreEqual(0, target.AsSource().Count);
        }

        [TestMethod()]
        public void ClearTest()
        {
            ClearTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        public void ContainsTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            TProxy item = new TProxy();
            target.Add(item);

            bool actual;
            actual = target.Contains(item);
            Assert.AreEqual(true, actual);

            TProxy item2 = new TProxy();
            actual = target.Contains(item2);
            Assert.AreEqual(false, actual);

            item2.SetSource(item.AsSource());
            actual = target.Contains(item2);
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void ContainsTest()
        {
            ClearTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for CopyTo
        ///</summary>
        public void CopyToTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            target.Add(new TProxy());
            target.Add(new TProxy());

            TProxy[] array = new TProxy[3];
            int arrayIndex = 1;
            target.CopyTo(array, arrayIndex);
            Assert.IsNull(array[0]);
            Assert.IsNotNull(array[1]);
            Assert.IsNotNull(array[2]);
        }

        [TestMethod()]
        public void CopyToTest()
        {
            CopyToTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for IndexOf
        ///</summary>
        public void IndexOfTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            TProxy item0 = new TProxy();
            TProxy item1 = new TProxy();
            TProxy item2 = new TProxy();
            TProxy item3 = new TProxy();
            target.Add(item0);
            target.Add(item1);
            target.Add(item2);
            target.Add(item3);
            int expected = 2;
            int actual;
            actual = target.IndexOf(item2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            IndexOfTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Insert
        ///</summary>
        public void InsertTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            TProxy item0 = new TProxy();
            TProxy item1 = new TProxy();
            TProxy item2 = new TProxy();
            TProxy item3 = new TProxy();
            target.Add(item0);
            target.Add(item1);
            target.Add(item2);
            target.Add(item3);

            int index = 2;
            TProxy item = new TProxy();
            target.Insert(index, item);

            Assert.AreEqual(item.AsSource(), target[index].AsSource());
        }

        [TestMethod()]
        public void InsertTest()
        {
            InsertTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        public void RemoveTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            TProxy item = new TProxy(); 
            bool expected = false;
            bool actual;
            actual = target.Remove(item);
            Assert.AreEqual(expected, actual);

            expected = true;
            target.Add(item);
            actual = target.Remove(item);
            Assert.AreEqual(expected, actual);   
        }

        [TestMethod()]
        public void RemoveTest()
        {
            RemoveTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for RemoveAt
        ///</summary>
        public void RemoveAtTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            TProxy item0 = new TProxy();
            TProxy item1 = new TProxy();
            TProxy item2 = new TProxy();
            TProxy item3 = new TProxy();
            target.Add(item0);
            target.Add(item1);
            target.Add(item2);
            target.Add(item3);
            int index = 0;
            target.RemoveAt(index);
            Assert.AreNotEqual(item0, target[0]);
            CollectionAssert.DoesNotContain(target.ToArray(), item0);
        }

        [TestMethod()]
        public void RemoveAtTest()
        {
            RemoveAtTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for SetSource
        ///</summary>
        public void SetSourceTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            IList<TSource> source = new List<TSource>();
            target.SetSource(source);
            Assert.AreEqual(source, target.AsSource());
        }

        [TestMethod()]
        public void SetSourceTest()
        {
            SetSourceTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Count
        ///</summary>
        public void CountTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            target.Add(new TProxy());
            target.Add(new TProxy());
            target.Add(new TProxy());
            target.Add(new TProxy());
            int actual;
            actual = target.Count;
            Assert.AreEqual(4, actual);
        }

        [TestMethod()]
        public void CountTest()
        {
            CountTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTestHelper<TProxy, TSource>()
            where TProxy : ISerializableProxy<TSource>, new()
        {
            SerializableProxyList<TProxy, TSource> target = new SerializableProxyList<TProxy, TSource>();
            
            int index = 0; 
            TProxy expected = new TProxy();
            target.Add(expected);
            TProxy actual;
            actual = target[index];
            Assert.AreEqual(expected.AsSource(), actual.AsSource());
        }

        [TestMethod()]
        public void ItemTest()
        {
            ItemTestHelper<ProxyGenericParameterHelper, GenericParameterHelper>(); 
        }
    }

}
