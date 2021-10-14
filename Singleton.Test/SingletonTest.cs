
namespace Singleton.Test
{
    using NUnit.Framework;

    public class SingletonTest
    {
        [TearDown]
        public void TearDown() 
            => Singleton.ResetSingleton();

        [Test]
        public void GetDefaultObjectOnInit()
        {
            var obj = Singleton.GetSingleton();

            Assert.NotNull(obj, "The object isn't supposed to be null on init.");
        }

        [Test]
        public void GetSameSingletonInstance()
        {
            var object1 = Singleton.GetSingleton();
            var object2 = Singleton.GetSingleton();

            Assert.AreSame(object1, object2, "Both objects are supposed to be a reference to the same instance on init.");
            Assert.AreEqual(object1.TestStr, object2.TestStr, "Both objects are supposed have the same values on init.");
            Assert.AreEqual(object1.TestInt, object2.TestInt, "Both objects are supposed have the same values on init.");

            object1.TestInt = 2;
            object2.TestStr = "changed";

            Assert.AreSame(object1, object2, "Both objects are supposed to be a reference to the same instance after changes.");
            Assert.AreEqual(object1.TestStr, object2.TestStr, "Both objects are supposed have the same values after changes.");
            Assert.AreEqual(object1.TestInt, object2.TestInt, "Both objects are supposed have the same values after changes.");

            var object3 = Singleton.GetSingleton();

            Assert.AreSame(object1, object3, "Both objects are supposed to be a reference to the same instance.");
        }

        [Test]
        public void SingletonInitialisationNoParams()
        {
            var obj = Singleton.GetSingleton();

            Assert.AreEqual("default", obj.TestStr, "The string should have the default values on singleton init.");
            Assert.AreEqual(0, obj.TestInt, "The int should have the default values on singleton init.");
        }

        [Test]
        public void SingletonInitialisationWithParams()
        {
            var testTxt = "text";
            var testInt = 20;

            var obj = Singleton.GetSingleton(testTxt, testInt);

            Assert.AreEqual(testTxt, obj.TestStr, "The string values should be the same on singleton init.");
            Assert.AreEqual(testInt, obj.TestInt, "The int values should be the same on singleton init.");
        }

        [Test]
        public void SingletonReset()
        {
            var testTxt = "text";
            var testInt = 20;

            var object1 = Singleton.GetSingleton(testTxt, testInt);

            Assert.AreEqual(testTxt, object1.TestStr, "The string values should be the same on singleton init.");
            Assert.AreEqual(testInt, object1.TestInt, "The int values should be the same on singleton init.");

            Singleton.ResetSingleton();

            var object2 = Singleton.GetSingleton(testTxt, testInt);

            Assert.AreNotSame(object1, object2, "The objets shouldn't be the same.");
            Assert.AreEqual(testTxt, object2.TestStr, "The string should have the default values on singleton init.");
            Assert.AreEqual(testInt, object2.TestInt, "The int should have the default values on singleton init.");
        }
    }
}
