
namespace DIContainer.Collections
{
    using DIContainer.Descriptors;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Singleton instance declaration collection.
    /// </summary>
    internal class SingletonCollection : ObservableCollection<SingletonDescriptor>
    {
        /// <summary>
        /// Create a new <see cref="SingletonCollection"/>.
        /// </summary>
        internal static SingletonCollection CreateCollection() => new();
    }
}
