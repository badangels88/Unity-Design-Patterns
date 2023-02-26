using NUnit.Framework;
using Patterns.Design.Singleton;

namespace Tests.TestDesign.TestSingleton
{
    public class SingletonTest
    {
        /// <summary>
        /// Tests that the TestMonoBehaviourSingleton instance is not null even if it is not instantiated
        /// </summary>
        [Test]
        public void TestMonoBehaviourSingleton_Instance_Is_Not_Null()
        {
            Assert.IsNotNull(TestMonoBehaviourSingleton.Instance);
        }
    }

    public class TestMonoBehaviourSingleton : Singleton<TestMonoBehaviourSingleton>
    {

    }
}