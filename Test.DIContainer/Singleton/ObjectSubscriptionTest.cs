
namespace DIContainer.Test.Singleton
{
    using DIContainer.Descriptors;
    using DIContainer.Extentions;
    using NUnit.Framework;
    using System;
    using System.Linq;

    public class ObjectSubscriptionTest : SingletonBase
    {
        [Test]
        public override void AddSingletonToCollection()
        {
            Assert.IsFalse(collection.Any(), "The collection should be null on initialization.");

            _ = collection.AddSingleton<SimpleTestingObject>();

            Assert.AreEqual(1, collection.Count(), "The collection should have an item after Singleton added.");

            Assert.AreEqual(typeof(SimpleTestingObject), collection.First().ServiceType, "The service type to target should be equal.");
            Assert.AreEqual(typeof(SimpleTestingObject), collection.First().ImplementationType, "The service type to target should be equal.");
            Assert.AreEqual(ServiceLifetime.Singleton, collection.First().Lifetime, "The registered service should be registered as Singleton.");
        }

        [Test]
        public override void ObjectProvidedAreTheSame()
        {
            _ = collection.AddSingleton<SimpleTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<SimpleTestingObject>();

            Assert.NotNull(object1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), object1.GetType(), "The types should be the same.");
        }

        [Test]
        public override void RightSingletonObjectCreatedAndResolved()
        {
            _ = collection.AddSingleton<SimpleTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<SimpleTestingObject>();
            var object2 = provider.GetService<SimpleTestingObject>();

            Assert.AreSame(object1, object2, "Both objects should be the same on init.");

            object1.TestInt = 2;
            object2.TestStr = "test";

            Assert.AreSame(object1, object2, "Both objects should be the same after edit.");
        }

        [Test]
        public override void AddMultipleSingletonToCollection()
        {
            Assert.IsFalse(collection.Any(), "The collection should be null on initialization.");

            _ = collection.AddSingleton<SimpleTestingObject>();
            _ = collection.AddSingleton<ClassParameterTestingObject>();

            Assert.AreEqual(2, collection.Count(), "The collection should have an item after Singleton added.");
        }

        [Test]
        public override void RightSingletonObjectCreatedAndResolvedWithParams()
        {
            _ = collection.AddSingleton<SimpleTestingObject>();
            _ = collection.AddSingleton<ClassParameterTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<SimpleTestingObject>();

            Assert.NotNull(object1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), object1.GetType(), "The types should be the same.");

            var object2 = provider.GetService<ClassParameterTestingObject>();

            Assert.NotNull(object2, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(ClassParameterTestingObject), object2.GetType(), "The types should be the same.");
        }

        [Test]
        public override void ObjectProvidedAreTheSameWithParams()
        {
            _ = collection.AddSingleton<SimpleTestingObject>();
            _ = collection.AddSingleton<ClassParameterTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<ClassParameterTestingObject>();
            var object2 = provider.GetService<ClassParameterTestingObject>();
            var object3 = provider.GetService<SimpleTestingObject>();

            Assert.AreSame(object1, object2, "Both base objects should be the same on init.");
            Assert.AreSame(object1.SimpleTestObj, object2.SimpleTestObj, "Both inner object should be the same on init.");
            Assert.AreSame(object1.SimpleTestObj, object3, "Both the inner objects and external object should be the same on init.");

            object1.TestInt = 2;
            object2.TestStr = "test";
            object1.SimpleTestObj.TestInt = 5;
            object3.TestStr = "nono";

            Assert.AreSame(object1, object2, "Both base objects should be the same after edit.");
            Assert.AreSame(object1.SimpleTestObj, object2.SimpleTestObj, "Both inner object should be the same after edit.");
            Assert.AreSame(object1.SimpleTestObj, object3, "Both the inner objects and external object should be the same after edit.");
        }

        [Test]
        public override void HasMissingReference()
        {
            _ = collection.AddSingleton<ClassParameterTestingObject>();

            var provider = collection.BuildServiceProvider();

            Assert.Throws<Exception>(delegate { provider.GetService<ClassParameterTestingObject>(); }, "Should throw an exception if one on the parameter wanted is not referenced in the collection.");
        }
    }
}
