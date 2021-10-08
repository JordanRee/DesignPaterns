
namespace DIContainer.Test.Scoped
{
    using DIContainer.Descriptors;
    using DIContainer.Extentions;
    using NUnit.Framework;
    using System.Linq;

    public class InterfaceImplementationSubscriptionTest : ScopedBase
    {

        [Test]
        public override void AddScopedToCollection()
        {
            Assert.IsFalse(collection.Any(), "The collection should be null on initialization.");

            _ = collection.AddScoped<ISimpleTestingObject, SimpleTestingObject>();

            Assert.AreEqual(1, collection.Count(), "The collection should have an item after Singleton added.");

            Assert.AreEqual(typeof(ISimpleTestingObject), collection.First().ServiceType, "The service type to target should be equal.");
            Assert.AreEqual(typeof(SimpleTestingObject), collection.First().ImplementationType, "The service type to target should be equal.");
            Assert.AreEqual(ServiceLifetime.Scoped, collection.First().Lifetime, "The registered service should be registered as Singleton.");
        }

        [Test]
        public override void RightScopedObjectCreatedAndResolved()
        {
            _ = collection.AddScoped<ISimpleTestingObject, SimpleTestingObject>();

            var provider = collection.BuildServiceProvider();

            var pile1 = provider.GetService<ISimpleTestingObject>();

            Assert.NotNull(pile1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), pile1.GetType(), "The types should be the same.");
            Assert.Contains(typeof(ISimpleTestingObject), pile1.GetType().GetInterfaces(), "The resolved object should contain the wanted interface.");
        }

        [Test]
        public override void ObjectProvidedAreNotSameOnDifferentCallPile()
        {
            _ = collection.AddScoped<ISimpleTestingObject, SimpleTestingObject>();

            var provider = collection.BuildServiceProvider();

            var pile1 = provider.GetService<ISimpleTestingObject>();
            var pile2 = provider.GetService<ISimpleTestingObject>();

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

            _ = collection.AddScoped<ISimpleTestingObject, SimpleTestingObject>();
            _ = collection.AddScoped<IParameterTestingObject, InterfaceParameterTestingObject>();
            _ = collection.AddScoped<IParameterTestingObject2, InterfaceParameterTestingObject2>();

            Assert.AreEqual(3, collection.Count(), "The collection should have an item after Singleton added.");
        }

        [Test]
        public override void RightScopedObjectCreatedAndResolvedWithParams()
        {
            _ = collection.AddScoped<ISimpleTestingObject, SimpleTestingObject>();
            _ = collection.AddScoped<IParameterTestingObject, InterfaceParameterTestingObject>();
            _ = collection.AddScoped<IParameterTestingObject2, InterfaceParameterTestingObject2>();

            var provider = collection.BuildServiceProvider();

            var pile1 = provider.GetService<ISimpleTestingObject>();

            Assert.NotNull(pile1, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(SimpleTestingObject), pile1.GetType(), "The types should be the same.");
            Assert.Contains(typeof(ISimpleTestingObject), pile1.GetType().GetInterfaces(), "The resolved object should contain the wanted interface.");

            var pile2 = provider.GetService<IParameterTestingObject>();

            Assert.NotNull(pile2, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(InterfaceParameterTestingObject), pile2.GetType(), "The types should be the same.");
            Assert.Contains(typeof(IParameterTestingObject), pile2.GetType().GetInterfaces(), "The resolved object should contain the wanted interface.");

            var pile3 = provider.GetService<IParameterTestingObject2>();

            Assert.NotNull(pile3, "The service return by the provider should not be null.");
            Assert.AreEqual(typeof(InterfaceParameterTestingObject2), pile3.GetType(), "The types should be the same.");
            Assert.Contains(typeof(IParameterTestingObject2), pile3.GetType().GetInterfaces(), "The resolved object should contain the wanted interface.");
        }

        [Test]
        public override void ObjectProvidedAreTheSameOnSameCallPileWithParams()
        {
            _ = collection.AddScoped<ISimpleTestingObject, SimpleTestingObject>();
            _ = collection.AddScoped<IParameterTestingObject, InterfaceParameterTestingObject>();
            _ = collection.AddScoped<IParameterTestingObject2, InterfaceParameterTestingObject2>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<IParameterTestingObject2>();
            var object2 = provider.GetService<IParameterTestingObject2>();
            var object3 = provider.GetService<ISimpleTestingObject>();

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
    }
}
