
namespace DIContainer.Descriptors
{
    using System;

    /// <summary>
    /// Class to describe a Services implementation.
    /// </summary>
    public class ServiceDescriptor
    {
        /// <summary>
        /// Create an instance of the <see cref="ScopeDescriptor"/> class.
        /// </summary>
        /// <param name="serviceType">Targeted service type.</param>
        /// <param name="implementationType"><see cref="Type"/> to implement.</param>
        /// <param name="lifetime">Lifetime associated to the implementatino.</param>
        private ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }

        /// <summary>
        /// Create an instance of the <see cref="ScopeDescriptor"/> class.
        /// </summary>
        /// <param name="serviceType">Targeted service type.</param>
        /// <param name="implementation">Implementation to use.</param>
        private ServiceDescriptor(Type serviceType, object implementation)
        {
            ServiceType = serviceType;
            ImplementationType = implementation.GetType();
            Implementation = implementation;
            Lifetime = ServiceLifetime.Singleton;
        }

        /// <summary>
        /// Create an instance of the <see cref="ScopeDescriptor"/> class.
        /// </summary>
        /// <param name="serviceType">Targeted service type.</param>
        /// <param name="factory">Factory to use for the implementations.</param>
        /// <param name="lifetime">Lifetime associated to the implementatino.</param>
        private ServiceDescriptor(Type serviceType, Func<ServiceProvider, object> factory, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = serviceType;
            ImplementationFactory = factory;
            Lifetime = lifetime;
        }

        /// <summary>
        /// Get the service type targeted.
        /// </summary>
        public Type ServiceType { get; }
        /// <summary>
        /// Get the implementation type.
        /// </summary>
        public Type ImplementationType { get; }
        /// <summary>
        /// Get the implementation factory.
        /// </summary>
        public Func<ServiceProvider, object> ImplementationFactory { get; }
        /// <summary>
        /// Get the implementation registered.
        /// </summary>
        public object Implementation { get; }
        /// <summary>
        /// Get the lifetime value of the implementation.
        /// </summary>
        public ServiceLifetime Lifetime { get; }

        /// <summary>
        /// Create a Singleton <see cref="ServiceDescriptor"/>.
        /// </summary>
        /// <typeparam name="TService">Service targeted.</typeparam>
        /// <typeparam name="TImplementation">Implementation type wanted.</typeparam>
        /// <returns><see cref="ServiceDescriptor"/> of the implementation.</returns>
        public static ServiceDescriptor CreateSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => new(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton);

        /// <summary>
        /// Create a Singleton <see cref="ServiceDescriptor"/>.
        /// </summary>
        /// <typeparam name="T">Service targeted.</typeparam>
        /// <param name="implementation"><typeparamref name="T"/> instance to use during the implementation.</param>
        /// <returns><see cref="ServiceDescriptor"/> of the implementation.</returns>
        public static ServiceDescriptor CreateSingleton<T>(T implementation)
            where T : class
            => new(typeof(T), implementation);

        /// <summary>
        /// Create a Singleton <see cref="ServiceDescriptor"/>.
        /// </summary>
        /// <typeparam name="T">Service targeted.</typeparam>
        /// <param name="factory"><typeparamref name="T"/> factory to use during the implementation.</param>
        /// <returns><see cref="ServiceDescriptor"/> of the implementation.</returns>
        public static ServiceDescriptor CreateSingleton<T>(Func<ServiceProvider, T> factory)
            where T : class
            => new(typeof(T), factory, ServiceLifetime.Singleton);

        /// <summary>
        /// Create a Scoped <see cref="ServiceDescriptor"/>.
        /// </summary>
        /// <typeparam name="TService">Service targeted.</typeparam>
        /// <typeparam name="TImplementation">Implementation type wanted.</typeparam>
        /// <returns><see cref="ServiceDescriptor"/> of the implementation.</returns>
        public static ServiceDescriptor CreateScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => new(typeof(TService), typeof(TImplementation), ServiceLifetime.Scoped);

        /// <summary>
        /// Create a Scoped <see cref="ServiceDescriptor"/>.
        /// </summary>
        /// <typeparam name="T">Service targeted.</typeparam>
        /// <param name="factory"><typeparamref name="T"/> factory to use during the implementation.</param>
        /// <returns><see cref="ServiceDescriptor"/> of the implementation.</returns>
        public static ServiceDescriptor CreateScoped<T>(Func<ServiceProvider, T> factory)
            where T : class
            => new(typeof(T), factory, ServiceLifetime.Scoped);

        /// <summary>
        /// Create a Transient <see cref="ServiceDescriptor"/>.
        /// </summary>
        /// <typeparam name="TService">Service targeted.</typeparam>
        /// <typeparam name="TImplementation">Implementation type wanted.</typeparam>
        /// <returns><see cref="ServiceDescriptor"/> of the implementation.</returns>
        public static ServiceDescriptor CreateTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => new(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);

        /// <summary>
        /// Create a Transient <see cref="ServiceDescriptor"/>.
        /// </summary>
        /// <typeparam name="T">Service targeted.</typeparam>
        /// <param name="factory"><typeparamref name="T"/> factory to use during the implementation.</param>
        /// <returns><see cref="ServiceDescriptor"/> of the implementation.</returns>
        public static ServiceDescriptor CreateTransient<T>(Func<ServiceProvider, T> factory)
            where T : class
            => new(typeof(T), factory, ServiceLifetime.Transient);
    }
}