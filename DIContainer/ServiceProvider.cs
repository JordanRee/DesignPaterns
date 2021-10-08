
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
            => GetService(serviceType, NewScope());

        private object GetService(Type serviceType, int scope)
        {
            // Find service
            var serviceDescriptor = collection.ToList().First(s => s.ServiceType == serviceType);

            if (serviceDescriptor is null)
                throw new Exception($"{serviceType.FullName} is not registered in the service collection.");
            else
                Console.WriteLine("Resolving {0}", serviceType.FullName);

            // Find constructor
            var implementedType = serviceDescriptor.ImplementationType;
            object instance;

            // Instantiate depending of the lifetime choosen
            switch (serviceDescriptor.Lifetime)
            {
                // Singleton
                case ServiceLifetime.Singleton:
                    if (!singletonCollection.Any(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType))
                    {
                        instance = implementedType.GetConstructors().First().GetParameters().Length == 0
                            ? CreateInstance(implementedType)
                            : CreateInstance(implementedType, scope);

                        singletonCollection.Add(SingletonDescriptor.CreateSingleton(serviceDescriptor.ServiceType, instance));
                            
                        return instance;
                    }
                    else
                    {
                        Console.WriteLine("{0} found as Singleton", serviceType.FullName);
                        return singletonCollection.First(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType).Implementation;
                    }

                // Scoped
                case ServiceLifetime.Scoped:
                    if (!scopedCollection.Any(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType && s.ScopeId == scope))
                    {
                        instance = implementedType.GetConstructors().First().GetParameters().Length == 0
                            ? CreateInstance(implementedType)
                            : CreateInstance(implementedType, scope);

                        scopedCollection.Add(ScopeDescriptor.CreateScope(scope, serviceDescriptor.ServiceType, instance));

                        return instance;
                    }
                    else
                    {
                        Console.WriteLine("{0} found as Scoped", serviceType.FullName);
                        return scopedCollection.First(s => s.Implementation.GetType() == serviceDescriptor.ImplementationType && s.ScopeId == scope).Implementation;
                    }

                // Transient
                case ServiceLifetime.Transient:
                    return implementedType.GetConstructors().First().GetParameters().Length == 0
                        ? CreateInstance(implementedType)
                        : CreateInstance(implementedType, scope);
            }

            throw new Exception("Something went worng.");
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
                parameters.Add(GetService(param.ParameterType, scope));

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
