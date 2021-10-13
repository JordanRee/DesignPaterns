
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
        /// Gets an instance of the implementation choosen for <typeparamref name="T"/> service type given.
        /// </summary>
        /// <typeparam name="T">Service type wanted from the collection.</typeparam>
        /// <returns>An instance of the wanted service.</returns>
        public T GetService<T>() 
            => (T)GetService(typeof(T));

        /// <summary>
        /// Gets an instance of the implementation choosen for <paramref name="serviceType"/> service type given.
        /// </summary>
        /// <param name="serviceType">Service type wanted from the collection.</param>
        /// <returns>An instance of the wanted service.</returns>
        public object GetService(Type serviceType) 
            => GetService(serviceType, NewScope(), true);

        /// <summary>
        /// Gets an instance of the implementation choosen for <paramref name="serviceType"/> service type given.
        /// </summary>
        /// <param name="serviceType">Service type wanted from the collection.</param>
        /// <param name="scopeId">Indicate the scope id to handle.</param>
        /// <param name="isBaseScope">Indicate if the current service wanted is the first of his scope.</param>
        /// <returns>An instance of the wanted service.</returns>
        private object GetService(Type serviceType, int scopeId, bool isBaseScope)
        {
            // Find service
            var serviceDescriptor = collection.ToList().FirstOrDefault(s => s.ServiceType == serviceType);

            if (serviceDescriptor is null)
                throw new Exception($"{serviceType.FullName} is not registered in the service collection.");
            else
                Console.WriteLine("Resolving {0}", serviceType.FullName);

            // Find constructor
            var implementedType = serviceDescriptor.ImplementationType;
            object instance = null;

            // Instantiate depending of the lifetime choosen
            switch (serviceDescriptor.Lifetime)
            {
                // Singleton
                case ServiceLifetime.Singleton:
                    if (!singletonCollection.Any(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType))
                    {
                        instance = implementedType.GetConstructors().First().GetParameters().Length == 0
                            ? CreateInstance(implementedType)
                            : CreateInstance(implementedType, scopeId);

                        singletonCollection.Add(SingletonDescriptor.CreateSingleton(instance));
                    }
                    else
                    {
                        Console.WriteLine("{0} found as Singleton", serviceType.FullName);
                        return singletonCollection.First(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType).Implementation;
                    }

                    break;

                // Scoped
                case ServiceLifetime.Scoped:
                    if (!scopedCollection.Any(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType && s.ScopeId == scopeId))
                    {
                        instance = implementedType.GetConstructors().First().GetParameters().Length == 0
                            ? CreateInstance(implementedType)
                            : CreateInstance(implementedType, scopeId);

                        scopedCollection.Add(ScopeDescriptor.CreateScope(scopeId, instance));
                    }
                    else
                    {
                        Console.WriteLine("{0} found as Scoped", serviceType.FullName);
                        instance = scopedCollection.First(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType && s.ScopeId == scopeId).Implementation;
                    }

                    break;

                // Transient
                case ServiceLifetime.Transient:
                    Console.WriteLine("{0} found as Transient");
                    instance = implementedType.GetConstructors().First().GetParameters().Length == 0
                        ? CreateInstance(implementedType)
                        : CreateInstance(implementedType, scopeId);

                    break;
            }

            if(isBaseScope)
                _ = CleanupScope(scopeId) ? true : throw new Exception("Scope failed to be fully cleaned up.");

            return instance;
        }

        /// <summary>
        /// Cleanup the given <paramref name="scopeId"/> reference and their instance associations from the list.
        /// </summary>
        /// <param name="scopeId">Indicate the scope id to handle.</param>
        /// <returns>Return <see langword="true"/> if the scope is totally removed from the collection. Else, return <see langword="false"/>.</returns>
        private bool CleanupScope(int scopeId)
        {
            ScopeDescriptor service = null;

            while ((service = scopedCollection.FirstOrDefault(s => s.ScopeId == scopeId)) is not null)
                _ = scopedCollection.Remove(service);

            return !scopedCollection.Any(s => s.ScopeId == scopeId);
        }

        /// <summary>
        /// Create an instance of the given <paramref name="type"/> with its default constructor.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to generate instance from.</param>
        /// <returns>Default <see cref="Type"/> instance generated.</returns>
        private static object CreateInstance(Type type)
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
        /// <param name="scopeId">Indicate the scope id to handle.</param>
        /// <returns>Default <see cref="Type"/> instance generated.</returns>
        private object CreateInstance(Type type, int scopeId)
        {
            Console.WriteLine("Create {0} instance.", type.FullName);
            var implementedConstructor = type.GetConstructors().First();
            var implementedParams = implementedConstructor.GetParameters();

            // Constructor with parameters
            var parameters = new List<object>();
            Console.WriteLine("Use {0} constructor.", implementedConstructor.ToString());

            foreach (var param in implementedParams)
                parameters.Add(GetService(param.ParameterType, scopeId, false));

            var instance =  implementedConstructor.Invoke(parameters.ToArray());
            Console.WriteLine("Instance {0} successfully.", type.FullName);

            return instance;
        }

        /// <summary>
        /// Create a new and unused scope identifier.
        /// </summary>
        /// <returns>Return a new and unsused scope identifier.</returns>
        private int NewScope()
        {
            if (scopedCollection.Count >= int.MaxValue)
                throw new OutOfMemoryException("Maximum scope relation reached.");

            var rand = new Random();
            int newId;

            var scopedCollectionQueryable = scopedCollection.AsQueryable();

            do
            {
                newId = rand.Next();
            }
            while (scopedCollectionQueryable.Any(s => s.ScopeId == newId));

            Console.WriteLine("Create new scope with Id {0}.", newId);

            return newId;
        }
    }
}
