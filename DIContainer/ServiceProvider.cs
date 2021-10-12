
namespace DIContainer
{
    using DIContainer.Collections;
    using DIContainer.Descriptors;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ServiceProvider
    {
        private ServiceCollection collection;

        private ScopedCollection scopedCollection;

        private SingletonCollection singletonCollection;

        internal ServiceProvider(ServiceCollection collection)
        {
            this.collection = collection;
            scopedCollection = new();
            singletonCollection = new();
        }

        public T GetService<T>() 
            => (T)GetService(typeof(T));

        public object GetService(Type serviceType)
        {
            var scopeId = NewScope();

            var service = GetService(serviceType, scopeId, true);

            return service;
        }

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

                        singletonCollection.Add(SingletonDescriptor.CreateSingleton(serviceDescriptor.ServiceType, instance));
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

                        scopedCollection.Add(ScopeDescriptor.CreateScope(scopeId, serviceDescriptor.ServiceType, instance));
                    }
                    else
                    {
                        Console.WriteLine("{0} found as Scoped", serviceType.FullName);
                        instance = scopedCollection.First(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType && s.ScopeId == scopeId).Implementation;
                    }

                    break;

                // Transient
                case ServiceLifetime.Transient:
                    instance = implementedType.GetConstructors().First().GetParameters().Length == 0
                        ? CreateInstance(implementedType)
                        : CreateInstance(implementedType, scopeId);

                    break;
            }

            // Scope cleanup
            for (var index = 0; isBaseScope && (index <= scopedCollection.Count(s => s.ScopeId == scopeId) +1); index++)
            {
                var service = scopedCollection.FirstOrDefault(s => s.ScopeId == scopeId);

                _ = scopedCollection.Remove(service);
            }

            return instance;
        }

        private object CreateInstance(Type type)
        {
            Console.WriteLine("Create {0} instance.", type.FullName);
            var instance = Activator.CreateInstance(type);
            Console.WriteLine("Instance {0} successfully.", type.FullName);

            return instance;
        }

        private object CreateInstance(Type type, int scope)
        {
            var implementedConstructor = type.GetConstructors().First();
            var implementedParams = implementedConstructor.GetParameters();

            // Constructor with parameters
            var parameters = new List<object>();
            Console.WriteLine("Use {0} constructor.", implementedConstructor.ToString());

            foreach (var param in implementedParams)
                parameters.Add(GetService(param.ParameterType, scope, false));

            return implementedConstructor.Invoke(parameters.ToArray());
        }

        private int NewScope()
        {
            var rand = new Random();
            int newId;

            if (scopedCollection.Count >= int.MaxValue)
                throw new OutOfMemoryException("Maximum scope relation reached.");

            do
            {
                newId = rand.Next();
            }
            while (scopedCollection.AsQueryable().Any(s => s.ScopeId == newId));

            return newId;
        }
    }
}
