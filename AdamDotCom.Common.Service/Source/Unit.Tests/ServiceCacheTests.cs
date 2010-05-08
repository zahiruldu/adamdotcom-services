using System;
using AdamDotCom.Common.Service;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class ServiceCacheTests
    {
        public class MyTestObject{}

        [Test]
        public void ShouldTestThatKeyUsesObjectName()
        {
            var hash = ServiceCache.Hash<MyTestObject>("dude");

            Assert.IsTrue(hash.Contains("MyTestObject"));
            Assert.IsTrue(hash.Contains("dude"));
            Console.Write(hash);
        }
    }
}
