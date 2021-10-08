
namespace DIContainer.Test.Transient
{
    using DIContainer.Collections;
    using NUnit.Framework;

    public abstract class TransientBase : TestEnvironement
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

        public abstract void AddTransientToCollection();

        public abstract void RightTransientObjectCreatedAndResolvedByProvider();

        public abstract void ObjectProvidedAreNotTheSame();

        public abstract void AddMultipleTransientToCollection();

        public abstract void RightTransientObjectCreatedAndResolvedWithParams();

        public abstract void ObjectProvidedAreNotTheSameWithParams();
    }
}
