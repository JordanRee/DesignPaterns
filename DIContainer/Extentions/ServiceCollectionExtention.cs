
namespace DIContainer.Extentions
{
    using DIContainer.Collections;
    using DIContainer.Descriptors;

    /// <summary>
    /// Extention to ease the Service listing.
    /// </summary>
    public static class ServiceCollectionExtention
    {
        /// <summary>
        /// Add new <typeparamref name="T"/> implementation type to the <paramref name="collection"/> as Singleton.
        /// </summary>
        /// <typeparam name="T">Implementation type wanted.</typeparam>
        /// <param name="collection"><see cref="ServiceCollection"/> to add the implementation type in.</param>
        /// <returns>Return the service collection given.</returns>
        public static ServiceCollection AddSingleton<T>(this ServiceCollection collection)
            where T : class
        {
            collection.Add(ServiceDescriptor.CreateSingleton<T, T>());

            return collection;
        }

        /// <summary>
        /// Add new <typeparamref name="TImplementation"/> implementation type for <typeparamref name="TService"/> to the <paramref name="collection"/> as Singleton.
        /// </summary>
        /// <typeparam name="TImplementation">Implementation type wanted.</typeparam>
        /// <typeparam name="TService">Service targeted type.</typeparam>
        /// <param name="collection"><see cref="ServiceCollection"/> to add the implementation type in.</param>
        /// <returns>Return the service collection given.</returns>
        public static ServiceCollection AddSingleton<TService, TImplementation>(this ServiceCollection collection)
            where TService : class
            where TImplementation : class, TService
        {
            collection.Add(ServiceDescriptor.CreateSingleton<TService, TImplementation>());

            return collection;
        }

        /// <summary>
        /// Add new <typeparamref name="T"/> implementation type to the <paramref name="collection"/> as Singleton with the <paramref name="implementation"/> provided.
        /// </summary>
        /// <typeparam name="T">Implementation type wanted.</typeparam>
        /// <param name="collection"><see cref="ServiceCollection"/> to add the implementation type in.</param>
        /// <param name="implementation"><typeparamref name="T"/> implementation to use.</param>
        /// <returns></returns>
        public static ServiceCollection AddSingleton<T>(this ServiceCollection collection, T implementation)
            where T : class
        {
            collection.Add(ServiceDescriptor.CreateSingleton(implementation));

            return collection;
        }

        /// <summary>
        /// Add new <typeparamref name="T"/> implementation type to the <paramref name="collection"/> as Scoped.
        /// </summary>
        /// <typeparam name="T">Implementation type wanted.</typeparam>
        /// <param name="collection"><see cref="ServiceCollection"/> to add the implementation type in.</param>
        /// <returns>Return the service collection given.</returns>
        public static ServiceCollection AddScoped<T>(this ServiceCollection collection)
            where T : class
        {
            collection.Add(ServiceDescriptor.CreateScoped<T, T>());

            return collection;
        }

        /// <summary>
        /// Add new <typeparamref name="TImplementation"/> implementation type for <typeparamref name="TService"/> to the <paramref name="collection"/> as Scoped.
        /// </summary>
        /// <typeparam name="TImplementation">Implementation type wanted.</typeparam>
        /// <typeparam name="TService">Service targeted type.</typeparam>
        /// <param name="collection"><see cref="ServiceCollection"/> to add the implementation type in.</param>
        /// <returns>Return the service collection given.</returns>
        public static ServiceCollection AddScoped<TService, TImplementation>(this ServiceCollection collection)
            where TService : class
            where TImplementation : class, TService
        {
            collection.Add(ServiceDescriptor.CreateScoped<TService, TImplementation>());

            return collection;
        }

        /// <summary>
        /// Add new <typeparamref name="T"/> implementation type to the <paramref name="collection"/> as Transient.
        /// </summary>
        /// <typeparam name="T">Implementation type wanted.</typeparam>
        /// <param name="collection"><see cref="ServiceCollection"/> to add the implementation type in.</param>
        /// <returns>Return the service collection given.</returns>
        public static ServiceCollection AddTransient<T>(this ServiceCollection collection)
            where T : class
        {
            collection.Add(ServiceDescriptor.CreateTransient<T, T>());

            return collection;
        }

        /// <summary>
        /// Add new <typeparamref name="TImplementation"/> implementation type for <typeparamref name="TService"/> to the <paramref name="collection"/> as Transient.
        /// </summary>
        /// <typeparam name="TImplementation">Implementation type wanted.</typeparam>
        /// <typeparam name="TService">Service targeted type.</typeparam>
        /// <param name="collection"><see cref="ServiceCollection"/> to add the implementation type in.</param>
        /// <returns>Return the service collection given.</returns>
        public static ServiceCollection AddTransient<TService, TImplementation>(this ServiceCollection collection)
            where TService : class
            where TImplementation : class, TService
        {
            collection.Add(ServiceDescriptor.CreateTransient<TService, TImplementation>());

            return collection;
        }
    }
}
