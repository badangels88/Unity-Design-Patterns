using NUnit.Framework;
using Patterns.Design.Pool.Native;
using UnityEngine;

namespace Tests.TestDesign.TestPool
{
    public class NativePoolTest
    {
        [Test]
        public void TestNativePool_Instantiate_no_exception()
        {
            try
            {
                var nativePoolObject = Object.Instantiate(new GameObject());
                var nativePool = nativePoolObject.AddComponent<TestClass>();
                Assert.IsNotNull(nativePool);
            }
            catch (System.Exception ex)
            {
                Assert.Fail($"Native instantiate failed \n{ex.Message}\n{ex.StackTrace}");
            }
        }
    }

    public class TestClass : ObjectPoolNative<TestNativeItem> { }
    public class TestNativeItem : Component { }
}