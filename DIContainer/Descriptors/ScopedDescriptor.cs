
namespace DIContainer.Descriptors
{
    using System;

    internal class ScopeDescriptor
    {
        private ScopeDescriptor(int scopeId, Type serviceType, object implementation)
        {
            ScopeId = scopeId;
            ServiceType = serviceType;
            Implementation = implementation;
        }

        public int ScopeId { get; }
        public Type ServiceType { get; }
        public object Implementation { get; }

        public static ScopeDescriptor CreateScope(int scopeId, Type serviceType, object implementation)
            => new(scopeId, serviceType, implementation);
    }
}
