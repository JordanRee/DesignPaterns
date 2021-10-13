
namespace DIContainer.Descriptors
{
    /// <summary>
    /// Class to describe a Scoped implementation.
    /// </summary>
    internal class ScopeDescriptor
    {
        /// <summary>
        /// Create an instance of the <see cref="ScopeDescriptor"/> class.
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="implementation"><see langword="object"/> to store as Scoped implementation.</param>
        private ScopeDescriptor(int scopeId, object implementation)
        {
            ScopeId = scopeId;
            Implementation = implementation;
        }

        /// <summary>
        /// Get the scope id of the object.
        /// </summary>
        public int ScopeId { get; }
        /// <summary>
        /// Get the implemented object.
        /// </summary>
        public object Implementation { get; }

        /// <summary>
        /// Create a new <see cref="ScopeDescriptor"/>.
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="implementation">Implementation stored by the <see cref="ScopeDescriptor"/>.</param>
        /// <returns>The <see cref="ScopeDescriptor"/> initialized.</returns>
        public static ScopeDescriptor CreateScope(int scopeId, object implementation)
            => new(scopeId, implementation);
    }
}
