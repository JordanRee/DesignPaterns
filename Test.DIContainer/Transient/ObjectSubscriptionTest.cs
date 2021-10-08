
using DIContainer.Descriptors;
using DIContainer.Extentions;
using NUnit.Framework;
using System.Linq;

namespace DIContainer.Test.Transient
{
    public class ObjectSubscriptionTest : TransientBase
    {
        [Test]
        public override void AddTransientToCollection()
        {
            Assert.IsFalse(collection.Any(), "The collection should be null on initialization.");

            _ = collection.AddTransient<SimpleTestingObject>();

            Assert.AreEqual(1, collection.Count(), "The collection should have an item after Singleton added.");

            Assert.AreEqual(typeof(SimpleTestingObject), collection.First().ServiceType, "The service type to target should be equal.");
            Assert.AreEqual(typeof(SimpleTestingObject), collection.First().ImplementationType, "The service type to target should be equal.");
            Assert.AreEqual(ServiceLifetime.Transient, collection.First().Lifetime, "The registered service should be registered as Transient.");
        }

        [Test]
        public override void RightTransientObjectCreatedAndResolvedByProvider()
        {
            _ = collection.AddTransient<SimpleTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<SimpleTestingObject>();

            Assert.NotNull(object1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), object1.GetType(), "The types should be the same.");
        }

        [Test]
        public override void ObjectProvidedAreNotTheSame()
        {
            _ = collection.AddTransient<SimpleTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<SimpleTestingObject>();
            var object2 = provider.GetService<SimpleTestingObject>();

            Assert.AreNotSame(object1, object2, "Both objects shouldn't be the same on init.");

            object1.TestInt = 2;
            object2.TestStr = "test";

            Assert.AreNotSame(object1, object2, "Both objects shouldn't be the same after edit.");
        }

        [Test]
        public override void AddMultipleTransientToCollection()
        {
            Assert.IsFalse(collection.Any(), "The collection should be null on initialization.");

            _ = collection.AddTransient<SimpleTestingObject>();
            _ = collection.AddTransient<ClassParameterTestingObject>();

            Assert.AreEqual(2, collection.Count(), "The collection should have an item after Singleton added.");
        }

        [Test]
        public override void RightTransientObjectCreatedAndResolvedWithParams()
        {
            _ = collection.AddTransient<SimpleTestingObject>();
            _ = collection.AddTransient<ClassParameterTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<SimpleTestingObject>();

            Assert.NotNull(object1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), object1.GetType(), "The types should be the same.");

            var object2 = provider.GetService<ClassParameterTestingObject>();

            Assert.NotNull(object2, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(ClassParameterTestingObject), object2.GetType(), "The types should be the same.");
        }

        [Test]
        public override void ObjectProvidedAreNotTheSameWithParams()
        {
            _ = collection.AddTransient<SimpleTestingObject>();
            _ = collection.AddTransient<ClassParameterTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<ClassParameterTestingObject>();
            var object2 = provider.GetService<ClassParameterTestingObject>();
            var object3 = provider.GetService<SimpleTestingObject>();

            Assert.AreNotSame(object1, object2, "Both base objects shouldn't be the same on init.");
            Assert.AreNotSame(object1.SimpleTestObj, object2.SimpleTestObj, "Both inner object shouldn't be the same on init.");
            Assert.AreNotSame(object1.SimpleTestObj, object3, "Both the inner objects and external object shouldn't be the same on init.");

            object1.TestInt = 2;
            object2.TestInt = 2;
            object1.TestStr = "test";
            object2.TestStr = "test";
            object1.SimpleTestObj.TestInt = 5;
            object2.SimpleTestObj.TestInt = 5;
            object1.SimpleTestObj.TestStr = "nono";
            object2.SimpleTestObj.TestStr = "nono";
            object3.TestInt = 5;
            object3.TestStr = "nono";

            Assert.AreNotSame(object1, object2, "Both base objects shouldn't be the same after edit.");
            Assert.AreNotSame(object1.SimpleTestObj, object2.SimpleTestObj, "Both inner object shouldn't be the same after edit.");
            Assert.AreNotSame(object1.SimpleTestObj, object3, "Both the inner objects and external object shouldn't be the same after edit.");
        }
    }
}
