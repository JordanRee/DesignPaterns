
namespace DIContainer.Descriptors
{
    using System;

    internal class SingletonDescriptor
    {
        private SingletonDescriptor(Type serviceType, object implementation)
        {
            ServiceType = serviceType;
            Implementation = implementation;
        }

        public Type ServiceType { get; }
        public object Implementation { get; }

        public static SingletonDescriptor CreateSingleton(Type serviceType, object implementation)
            => new SingletonDescriptor(serviceType, implementation);
    }
}
