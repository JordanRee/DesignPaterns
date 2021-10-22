
namespace Singleton
{
    /// <summary>
    /// Create singleton class.
    /// </summary>
    public class SingletonExample
    {
        /// <summary>
        /// Store the singleton instance.   
        /// </summary>
        private static SingletonExample instance;

        /// <summary>
        /// Store a lock <see langword="object"/> to handle multithreading calls.
        /// </summary>
        private static readonly object _lock = new();

        /// <summary>
        /// Create a new <see cref="SingletonExample"/> class instance.
        /// </summary>
        /// <param name="testStr">Test <see langword="string"/> content.</param>
        /// <param name="testInt">Test <see langword="int"/> content.</param>
        private SingletonExample(string testStr, int testInt)
        {
            TestStr = testStr;
            TestInt = testInt;
        }

        /// <summary>
        /// Gets or sets the testing <see langword="string"/> value.
        /// </summary>
        public string TestStr { get; set; }
        /// <summary>
        /// Gets or sets the testing <see langword="int"/> value.
        /// </summary>
        public int TestInt { get; set; }

        /// <summary>
        /// Default <see cref="SingletonExample"/> singleton getter/initializer.
        /// </summary>
        /// <returns><see cref="SingletonExample"/> class with a singleton patern instanciation.</returns>
        public static SingletonExample GetSingleton()
            => GetSingleton("default", 0);

        /// <summary>
        /// <see cref="SingletonExample"/> singleton getter/initializer with parameters that are taken on the singleton first initialization.
        /// </summary>
        /// <param name="testStr">Test <see langword="string"/> content.</param>
        /// <param name="testInt">Test <see langword="int"/> content.</param>
        /// <returns><see cref="SingletonExample"/> class with a singleton patern instanciation.</returns>
        public static SingletonExample GetSingleton(string testStr, int testInt)
        {
            lock (_lock)
                return instance ??= new(testStr, testInt);
        }

        /// <summary>
        /// Reset singleton chain.
        /// </summary>
        public static void ResetSingleton()
        {
            lock (_lock)
                instance = null;
        }
    }
}
