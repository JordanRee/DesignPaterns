
namespace DIContainer.Test.Scoped
{
    using DIContainer.Descriptors;
    using DIContainer.Extentions;
    using NUnit.Framework;
    using System;
    using System.Linq;

    public class FactorySubscriptionTest : ScopedBase
    {

        [Test]
        public override void AddScopedToCollection()
        {
            Assert.IsFalse(collection.Any(), "The collection should be null on initialization.");

            _ = collection.AddScoped((s) => new SimpleTestingObject());

            Assert.AreEqual(1, collection.Count(), "The collection should have an item after Scoped added.");

            Assert.AreEqual(typeof(SimpleTestingObject), collection.First().ServiceType, "The service type to target should be equal.");
            Assert.AreEqual(typeof(SimpleTestingObject), collection.First().ImplementationType, "The service type to target should be equal.");
            Assert.AreEqual(ServiceLifetime.Scoped, collection.First().Lifetime, "The registered service should be registered as Singleton.");
        }

        [Test]
        public override void RightScopedObjectCreatedAndResolved()
        {
            _ = collection.AddScoped((s) => new SimpleTestingObject());

            var provider = collection.BuildServiceProvider();

            var pile1 = provider.GetService<SimpleTestingObject>();

            Assert.NotNull(pile1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), pile1.GetType(), "The types should be the same.");
        }

        [Test]
        public override void ObjectProvidedAreNotSameOnDifferentCallPile()
        {
            _ = collection.AddScoped((s) => new SimpleTestingObject());

            var provider = collection.BuildServiceProvider();

            var pile1 = provider.GetService<SimpleTestingObject>();
            var pile2 = provider.GetService<SimpleTestingObject>();

            Assert.AreNotSame(pile1, pile2, "Both objects should be the same on init.");

            pile1.TestInt = 2;
            pile2.TestInt = 2;
            pile1.TestStr = "test";
            pile2.TestStr = "test";

            Assert.AreNotSame(pile1, pile2, "Both objects should be the same after edit.");
        }

        [Test]
        public override void AddMultipleScopedToCollection()
        {
            Assert.IsFalse(collection.Any(), "The collection should be null on initialization.");

            _ = collection.AddScoped((s) => new SimpleTestingObject());
            _ = collection.AddScoped((s) => new ClassParameterTestingObject(s.GetService<SimpleTestingObject>()));
            _ = collection.AddScoped((s) => new ClassParameterTestingObject2(s.GetService<ClassParameterTestingObject>(), s.GetService<SimpleTestingObject>()));

            Assert.AreEqual(3, collection.Count(), "The collection should have an item after Scoped added.");
        }

        [Test]
        public override void RightScopedObjectCreatedAndResolvedWithParams()
        {
            _ = collection.AddScoped((s) => new SimpleTestingObject());
            _ = collection.AddScoped((s) => new ClassParameterTestingObject(s.GetService<SimpleTestingObject>()));
            _ = collection.AddScoped((s) => new ClassParameterTestingObject2(s.GetService<ClassParameterTestingObject>(), s.GetService<SimpleTestingObject>()));

            var provider = collection.BuildServiceProvider();

            var pile1 = provider.GetService<SimpleTestingObject>();

            Assert.NotNull(pile1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), pile1.GetType(), "The types should be the same.");

            var pile2 = provider.GetService<ClassParameterTestingObject>();

            Assert.NotNull(pile2, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(ClassParameterTestingObject), pile2.GetType(), "The types should be the same.");

            var pile3 = provider.GetService<ClassParameterTestingObject2>();

            Assert.NotNull(pile3, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(ClassParameterTestingObject2), pile3.GetType(), "The types should be the same.");
        }

        [Test]
        public override void ObjectProvidedAreTheSameOnSameCallPileWithParams()
        {
            _ = collection.AddScoped((s) => new SimpleTestingObject());
            _ = collection.AddScoped((s) => new ClassParameterTestingObject(s.GetService<SimpleTestingObject>()));
            _ = collection.AddScoped((s) => new ClassParameterTestingObject2(s.GetService<ClassParameterTestingObject>(), s.GetService<SimpleTestingObject>()));

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<ClassParameterTestingObject2>();
            var object2 = provider.GetService<ClassParameterTestingObject2>();
            var object3 = provider.GetService<SimpleTestingObject>();

            Assert.AreNotSame(object1, object2, "Both base objects shouldn't be the same on init.");
            Assert.AreSame(object1.SimpleTestObj, object1.ParamTestObj.SimpleTestObj, "Both inner object of the same type should be the same on init.");
            Assert.AreSame(object2.SimpleTestObj, object2.ParamTestObj.SimpleTestObj, "Both inner object of the same type should be the same on init.");
            Assert.AreNotSame(object1.SimpleTestObj, object2.SimpleTestObj, "Both the inner objects and external object shouldn't be the same on init.");
            Assert.AreNotSame(object1.SimpleTestObj, object2.ParamTestObj.SimpleTestObj, "Both the inner objects and external object shouldn't be the same on init.");
            Assert.AreNotSame(object1.SimpleTestObj, object3, "Both the inner objects and external object shouldn't be the same on init.");

            object1.SimpleTestObj.TestInt = 2;
            object1.ParamTestObj.SimpleTestObj.TestInt = 2;
            object2.SimpleTestObj.TestInt = 2;
            object2.ParamTestObj.SimpleTestObj.TestInt = 2;
            object3.TestInt = 2;
            object1.SimpleTestObj.TestStr = "nono";
            object1.ParamTestObj.SimpleTestObj.TestStr = "nono";
            object2.SimpleTestObj.TestStr = "nono";
            object2.ParamTestObj.SimpleTestObj.TestStr = "nono";
            object3.TestStr = "nono";

            Assert.AreNotSame(object1, object2, "Both base objects shouldn't be the same after edit.");
            Assert.AreSame(object1.SimpleTestObj, object1.ParamTestObj.SimpleTestObj, "Both inner object of the same type should be the same after edit.");
            Assert.AreSame(object2.SimpleTestObj, object2.ParamTestObj.SimpleTestObj, "Both inner object of the same type should be the same after edit.");
            Assert.AreNotSame(object1.SimpleTestObj, object2.SimpleTestObj, "Both the inner objects and external object shouldn't be the same after edit.");
            Assert.AreNotSame(object1.SimpleTestObj, object2.ParamTestObj.SimpleTestObj, "Both the inner objects and external object shouldn't be the same after edit.");
            Assert.AreNotSame(object1.SimpleTestObj, object3, "Both the inner objects and external object shouldn't be the same after edit.");
        }

        [Test]
        public override void HasMissingReference()
        {
            _ = collection.AddScoped((s) => new ClassParameterTestingObject(s.GetService<SimpleTestingObject>()));

            var provider = collection.BuildServiceProvider();

            _ = Assert.Throws<Exception>(delegate { _ = provider.GetService<ClassParameterTestingObject>(); }, "Should throw an exception if one of the parameter wanted is not referenced in the collection.");
        }
    }
}
