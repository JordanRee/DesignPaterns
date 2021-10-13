
namespace DIContainer.Descriptors
{
    /// <summary>
    /// Class to describe a Singleton implementation.
    /// </summary>
    internal class SingletonDescriptor
    {
        /// <summary>
        /// Create an instance of the <see cref="SingletonDescriptor"/> class.
        /// </summary>
        /// <param name="implementation"><see langword="object"/> to store as Singleton.</param>
        private SingletonDescriptor(object implementation) 
            => Implementation = implementation;

        /// <summary>
        /// Get the implemented object.
        /// </summary>
        public object Implementation { get; }

        /// <summary>
        /// Create a new <see cref="SingletonDescriptor"/>.
        /// </summary>
        /// <param name="implementation">Implementation stored by the <see cref="SingletonDescriptor"/>.</param>
        /// <returns>The <see cref="SingletonDescriptor"/> initialized.</returns>
        public static SingletonDescriptor CreateSingleton(object implementation)
            => new(implementation);
    }
}
