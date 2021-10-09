
namespace DIContainer.Test.Singleton
{
    using NUnit.Framework;
    using DIContainer.Collections;

    public abstract class SingletonBase : TestEnvironement
    {
        protected ServiceCollection collection;

        [SetUp]
        protected void SetUp()
        {
            collection = new ServiceCollection();
        }

        [TearDown]
        protected void TearDown()
        {
            collection.Clear();
            collection = null;
        }

        public abstract void AddSingletonToCollection();
        
        public abstract void RightSingletonObjectCreatedAndResolved();

        public abstract void ObjectProvidedAreTheSame();

        public abstract void AddMultipleSingletonToCollection();

        public abstract void RightSingletonObjectCreatedAndResolvedWithParams();

        public abstract void ObjectProvidedAreTheSameWithParams();

        public abstract void HasMissingReference();
    }
}
