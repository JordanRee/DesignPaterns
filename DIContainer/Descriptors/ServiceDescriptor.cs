
namespace DIContainer.Descriptors
{
    using System;

    public class ServiceDescriptor
    {
        private ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }

        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public ServiceLifetime Lifetime { get; }

        public static ServiceDescriptor CreateSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => new (typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton);

        public static ServiceDescriptor CreateScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => new(typeof(TService), typeof(TImplementation), ServiceLifetime.Scoped);

        public static ServiceDescriptor CreateTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => new(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);
    }

    public enum ServiceLifetime
    {
        Singleton,
        Scoped,
        Transient
    }
}