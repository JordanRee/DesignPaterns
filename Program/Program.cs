
namespace Program
{
    using DIContainer.Collections;
    using DIContainer.Extentions;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();

            collection.AddSingleton<ISingletonTesting, SingletonTesting>();

            var provider = collection.BuildServiceProvider();

            var object1 = provider.GetService<ISingletonTesting>();
            var object2 = provider.GetService<ISingletonTesting>();
            var object3 = provider.GetService<ISingletonTesting>();
            var object4 = provider.GetService<ISingletonTesting>();

            object2.TestStr = "Hello";
            object3.TestInt = 3;

            if ((object1 == object2) == (object3 == object4))
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");
        }
    }

    interface ISingletonTesting
    {
        string TestStr { get; set; }
        int TestInt { get; set; }
    }

    class SingletonTesting : ISingletonTesting
    {
        public string TestStr { get; set; } = "1";
        public int TestInt { get; set; } = 1;
    }
}
