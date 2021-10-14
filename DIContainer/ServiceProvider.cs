
namespace DIContainer
{
    using DIContainer.Collections;
    using DIContainer.Descriptors;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Handle service instanciation.
    /// </summary>
    public class ServiceProvider
    {
        /// <summary>
        /// Store the <see cref="ServiceCollection"/> to handle.
        /// </summary>
        private readonly ServiceCollection collection;

        /// <summary>
        /// Store instances with scopes declatrations references to handle scoped implementations.
        /// </summary>
        private readonly ScopedCollection scopedCollection;
        private int scopeDepth = 0;

        /// <summary>
        /// Store the Singleton instances created.
        /// </summary>
        private readonly SingletonCollection singletonCollection;

        /// <summary>
        /// Initialize a new instance of a <see cref="ServiceProvider"/>.
        /// </summary>
        /// <param name="collection"><see cref="ServiceCollection"/> that gona be used by the <see cref="ServiceProvider"/> to resolve the wanted implementations.</param>
        internal ServiceProvider(ServiceCollection collection)
        {
            this.collection = collection;
            scopedCollection = ScopedCollection.CreateCollection();
            singletonCollection = SingletonCollection.CreateCollection();
        }

        /// <summary>
        /// Gets an instance of the implementation choosen for <paramref name="serviceType"/> service type given.
        /// </summary>
        /// <typeparam name="T">Service type wanted.</typeparam>
        /// <returns>An instance of the wanted service.</returns>
        public T GetService<T>()
            => (T) GetService(typeof(T));

        /// <summary>
        /// Gets an instance of the implementation choosen for <paramref name="serviceType"/> service type given.
        /// </summary>
        /// <param name="serviceType">Service type wanted from the collection.</param>
        /// <returns>An instance of the wanted service.</returns>
        public object GetService(Type serviceType)
            => BuildServiceInstance(serviceType);

        /// <summary>
        /// Gets an instance of the implementation choosen for <paramref name="serviceType"/> service type given.
        /// </summary>
        /// <param name="serviceType">Service type wanted from the collection.</param>
        /// <returns>An instance of the wanted service.</returns>
        private object BuildServiceInstance(Type serviceType)
        {
            // Find service
            var serviceDescriptor = GetServiceDescriptor(serviceType);

            // Update scope depth for clean up scope detection
            scopeDepth += 1;

            // Find constructor
            var implementedType = serviceDescriptor.ImplementationType;
            object instance = null;

            // Instantiate depending of the lifetime choosen
            switch (serviceDescriptor.Lifetime)
            {
                // Singleton
                case ServiceLifetime.Singleton:
                    if (serviceDescriptor.Implementation is not null)
                    {
                        instance = serviceDescriptor.Implementation;
                    }
                    else
                    {
                        if (!singletonCollection.Any(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType))
                        {
                            instance = serviceDescriptor.ImplementationFactory is not null
                                ? serviceDescriptor.ImplementationFactory.Invoke(this)
                                : implementedType.GetConstructors().First().GetParameters().Length == 0
                                    ? CreateInstanceNoParams(implementedType)
                                    : CreateInstanceWithParams(implementedType);

                            singletonCollection.Add(SingletonDescriptor.CreateSingleton(instance));
                        }
                        else
                        {
                            Console.WriteLine("{0} found as Singleton", serviceType.FullName);
                            return singletonCollection.First(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType).Implementation;
                        }
                    }

                    break;

                // Scoped
                case ServiceLifetime.Scoped:
                    if (!scopedCollection.Any(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType))
                    {
                        instance = implementedType.GetConstructors().First().GetParameters().Length == 0
                            ? CreateInstanceNoParams(implementedType)
                            : CreateInstanceWithParams(implementedType);

                        scopedCollection.Add(ScopeDescriptor.CreateScope(instance));
                    }
                    else
                    {
                        Console.WriteLine("{0} found as Scoped", serviceType.FullName);
                        instance = scopedCollection.First(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType).Implementation;
                    }

                    break;

                // Transient
                case ServiceLifetime.Transient:
                    Console.WriteLine("{0} found as Transient");
                    instance = implementedType.GetConstructors().First().GetParameters().Length == 0
                        ? CreateInstanceNoParams(implementedType)
                        : CreateInstanceWithParams(implementedType);

                    break;
            }

            // Update scope depth for clean up scope detection
            scopeDepth -= 1;

            if (scopeDepth == 0)
                _ = CleanupScope() ? true : throw new Exception("Scope failed to be fully cleaned up.");

            return instance;
        }

        /// <summary>
        /// Get <see cref="ServiceDescriptor"/> of the wanted <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType"><see cref="Type"/> to recover the description.</param>
        /// <returns>Return the <see cref="ServiceDescriptor"/> found in the provider collection.</returns>
        private ServiceDescriptor GetServiceDescriptor(Type serviceType)
        {
            var serviceDescriptor = collection.ToList().FirstOrDefault(s => s.ServiceType == serviceType);

            if (serviceDescriptor is null)
                throw new Exception($"{serviceType.FullName} is not registered in the service collection.");
            else
                Console.WriteLine("Resolving {0}", serviceType.FullName);
            return serviceDescriptor;
        }

        /// <summary>
        /// Cleanup the given <paramref name="scopeId"/> reference and their instance associations from the list.
        /// </summary>
        /// <param name="scopeId">Indicate the scope id to handle.</param>
        /// <returns>Return <see langword="true"/> if the scope is totally removed from the collection. Else, return <see langword="false"/>.</returns>
        private bool CleanupScope()
        {
            ScopeDescriptor service;
            while ((service = scopedCollection.FirstOrDefault()) is not null)
                _ = scopedCollection.Remove(service);

            return !scopedCollection.Any();
        }

        /// <summary>
        /// Create an instance of the given <paramref name="type"/> with its default constructor.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to generate instance from.</param>
        /// <returns>Default <see cref="Type"/> instance generated.</returns>
        private static object CreateInstanceNoParams(Type type)
        {
            Console.WriteLine("Create {0} instance.", type.FullName);
            var instance = Activator.CreateInstance(type);
            Console.WriteLine("Instance {0} successfully.", type.FullName);

            return instance;
        }

        /// <summary>
        /// Create an instance of the given <paramref name="type"/> with its first constructor.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to generate instance from.</param>
        /// 
        /// <returns>Default <see cref="Type"/> instance generated.</returns>
        private object CreateInstanceWithParams(Type type)
        {
            Console.WriteLine("Create {0} instance.", type.FullName);
            var implementedConstructor = type.GetConstructors().First();
            var implementedParams = implementedConstructor.GetParameters();

            // Constructor with parameters
            var parameters = new List<object>();
            Console.WriteLine("Use {0} constructor.", implementedConstructor.ToString());

            foreach (var param in implementedParams)
                parameters.Add(GetService(param.ParameterType));

            var instance =  implementedConstructor.Invoke(parameters.ToArray());
            Console.WriteLine("Instance {0} successfully.", type.FullName);

            return instance;
        }
    }
}
