
namespace Singleton
{
    public sealed class Singleton
    {
        private static Singleton instance;

        private Singleton(string testStr, int testInt)
        {
            TestStr = testStr;
            TestInt = testInt;
        }

        public string TestStr { get; }
        public int TestInt { get; }

        public static Singleton GetSingleton(string testStr, int testInt) 
            => instance ??= new(testStr, testInt);

        public static Singleton GetSingleton()
            => instance ??= new("default", 0);
    }
}
