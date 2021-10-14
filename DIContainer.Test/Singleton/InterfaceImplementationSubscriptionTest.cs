
namespace DIContainer.Test.Singleton
{
    using DIContainer.Descriptors;
    using DIContainer.Extentions;
    using NUnit.Framework;
    using System;
    using System.Linq;

    public class InterfaceImplementationSubscriptionTest : SingletonBase
    {
        [Test]
        public override void AddSingletonToCollection()
        {
            Assert.IsFalse(collection.Any(), "The collection should be null on initialization.");

            _ = collection.AddSingleton<ISimpleTestingObject, SimpleTestingObject>();

            Assert.AreEqual(1, collection.Count(), "The collection should have an item after Singleton added.");

            Assert.AreEqual(typeof(ISimpleTestingObject), collection.First().ServiceType, "The service type to target should be equal.");
            Assert.AreEqual(typeof(SimpleTestingObject), collection.First().ImplementationType, "The service type to target should be equal.");
            Assert.AreEqual(ServiceLifetime.Singleton, collection.First().Lifetime, "The registered service should be registered as Singleton.");
        }

        [Test]
        public override void RightSingletonObjectCreatedAndResolved()
        {
            _ = collection.AddSingleton<ISimpleTestingObject, SimpleTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<ISimpleTestingObject>();

            Assert.NotNull(object1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), object1.GetType(), "The types should be the same.");
            Assert.Contains(typeof(ISimpleTestingObject), object1.GetType().GetInterfaces(), "The resolved object should contain the wanted interface.");
        }

        [Test]
        public override void ObjectProvidedAreTheSame()
        {
            _ = collection.AddSingleton<ISimpleTestingObject, SimpleTestingObject>();
            
            var provider = collection.BuildServiceProvider();
            
            var object1 = provider.GetService<ISimpleTestingObject>();
            var object2 = provider.GetService<ISimpleTestingObject>();

            Assert.AreSame(object1, object2, "Both objects should be the same on init.");

            object1.TestInt = 2;
            object2.TestStr = "test";

            Assert.AreSame(object1, object2, "Both objects should be the same after edit.");
        }

        [Test]
        public override void AddMultipleSingletonToCollection()
        {
            Assert.IsFalse(collection.Any(), "The collection should be null on initialization.");

            _ = collection.AddSingleton<ISimpleTestingObject, SimpleTestingObject>();
            _ = collection.AddSingleton<IParameterTestingObject, InterfaceParameterTestingObject>();

            Assert.AreEqual(2, collection.Count(), "The collection should have an item after Singleton added.");
        }

        [Test]
        public override void RightSingletonObjectCreatedAndResolvedWithParams()
        {
            _ = collection.AddSingleton<ISimpleTestingObject, SimpleTestingObject>();
            _ = collection.AddSingleton<IParameterTestingObject, InterfaceParameterTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<ISimpleTestingObject>();

            Assert.NotNull(object1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), object1.GetType(), "The types should be the same.");
            Assert.Contains(typeof(ISimpleTestingObject), object1.GetType().GetInterfaces(), "The resolved object should contain the wanted interface.");

            var object2 = provider.GetService<IParameterTestingObject>();

            Assert.NotNull(object2, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(InterfaceParameterTestingObject), object2.GetType(), "The types should be the same.");
            Assert.Contains(typeof(IParameterTestingObject), object2.GetType().GetInterfaces(), "The resolved object should contain the wanted interface.");
        }

        [Test]
        public override void ObjectProvidedAreTheSameWithParams()
        {
            _ = collection.AddSingleton<ISimpleTestingObject, SimpleTestingObject>();
            _ = collection.AddSingleton<IParameterTestingObject, InterfaceParameterTestingObject>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<IParameterTestingObject>();
            var object2 = provider.GetService<IParameterTestingObject>();
            var object3 = provider.GetService<ISimpleTestingObject>();

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
            _ = collection.AddSingleton<IParameterTestingObject, InterfaceParameterTestingObject>();

            var provider = collection.BuildServiceProvider();

            _ = Assert.Throws<Exception>(delegate { _ = provider.GetService<IParameterTestingObject>(); }, "Should throw an exception if one of the parameter wanted is not referenced in the collection.");
        }
    }
}