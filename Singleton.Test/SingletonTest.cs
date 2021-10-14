
namespace Singleton.Test
{
    using NUnit.Framework;

    public class SingletonTest
    {
        [Test]
        public void GetDefaultObjectOnInit()
        {
            var obj = Singleton.GetSingleton();

            Assert.NotNull(obj, "The object isn't supposed to be null on init.");
        }
    }
}
