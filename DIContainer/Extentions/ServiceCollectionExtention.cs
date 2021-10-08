
namespace DIContainer.Extentions
{
    using DIContainer.Collections;
    using DIContainer.Descriptors;

    public static class ServiceCollectionExtention
    {
        public static ServiceCollection AddSingleton<T>(this ServiceCollection collection)
            where T : class
        {
            collection.Add(ServiceDescriptor.CreateSingleton<T, T>());

            return collection;
        }

        public static ServiceCollection AddSingleton<TService, TImplementation>(this ServiceCollection collection)
            where TService : class
            where TImplementation : class, TService
        {
            collection.Add(ServiceDescriptor.CreateSingleton<TService, TImplementation>());

            return collection;
        }

        public static ServiceCollection AddScoped<T>(this ServiceCollection collection)
            where T : class
        {
            collection.Add(ServiceDescriptor.CreateScoped<T, T>());

            return collection;
        }

        public static ServiceCollection AddScoped<TService, TImplementation>(this ServiceCollection collection)
            where TService : class
            where TImplementation : class, TService
        {
            collection.Add(ServiceDescriptor.CreateScoped<TService, TImplementation>());

            return collection;
        }

        public static ServiceCollection AddTransient<T>(this ServiceCollection collection)
            where T : class
        {
            collection.Add(ServiceDescriptor.CreateTransient<T, T>());

            return collection;
        }

        public static ServiceCollection AddTransient<TService, TImplementation>(this ServiceCollection collection)
            where TService : class
            where TImplementation : class, TService
        {
            collection.Add(ServiceDescriptor.CreateTransient<TService, TImplementation>());

            return collection;
        }
    }
}
