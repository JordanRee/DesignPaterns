
using DIContainer.Collections;
using NUnit.Framework;

namespace DIContainer.Test.Scoped
{
    public abstract class ScopedBase : TestEnvironement
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

        public abstract void AddScopedToCollection();

        public abstract void RightScopedObjectCreatedAndResolved();

        public abstract void ObjectProvidedAreNotSameOnDifferentCallPile();

        public abstract void AddMultipleScopedToCollection();

        public abstract void RightScopedObjectCreatedAndResolvedWithParams();

        public abstract void ObjectProvidedAreTheSameOnSameCallPileWithParams();

        public abstract void HasMissingReference();
    }
}
