
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
        /// Get the service type targeted.
        /// </summary>
        public Type ServiceType { get; }
        /// <summary>
        /// Get the implementation type.
        /// </summary>
        public Type ImplementationType { get; }
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
            => new (typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton);

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
        /// Create a Transient <see cref="ServiceDescriptor"/>.
        /// </summary>
        /// <typeparam name="TService">Service targeted.</typeparam>
        /// <typeparam name="TImplementation">Implementation type wanted.</typeparam>
        /// <returns><see cref="ServiceDescriptor"/> of the implementation.</returns>
        public static ServiceDescriptor CreateTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => new(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);
    }

    /// <summary>
    /// Different 
    /// </summary>
    public enum ServiceLifetime
    {
        /// <summary>
        /// Creates a new Service only once during the application lifetime, and uses it everywhere.
        /// </summary>
        Singleton,
        /// <summary>
        /// Creates a new instance for every scope. (Each "request" is a Scope). Within the scope, it reuses the existing service.
        /// </summary>
        Scoped,
        /// <summary>
        /// Creates a new instance of the service every time you request it.
        /// </summary>
        Transient
    }
}