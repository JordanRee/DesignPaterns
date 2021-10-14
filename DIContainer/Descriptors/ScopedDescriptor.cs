
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
        /// <param name="implementation"><see langword="object"/> to store as Scoped implementation.</param>
        /// 
        private ScopeDescriptor(object implementation) 
            => Implementation = implementation;

        /// <summary>
        /// Get the implemented object.
        /// </summary>
        public object Implementation { get; }

        /// <summary>
        /// Create a new <see cref="ScopeDescriptor"/>.
        /// </summary>
        /// <param name="implementation">Implementation stored by the <see cref="ScopeDescriptor"/>.</param>
        /// 
        /// <returns>The <see cref="ScopeDescriptor"/> initialized.</returns>
        public static ScopeDescriptor CreateScope(object implementation)
            => new(implementation);
    }
}
