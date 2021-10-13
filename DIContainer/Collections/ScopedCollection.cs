
namespace DIContainer.Collections
{
    using DIContainer.Descriptors;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Scoped instance declaration collection.
    /// </summary>
    internal class ScopedCollection : ObservableCollection<ScopeDescriptor>
    {
        /// <summary>
        /// Create a new <see cref="ScopedCollection"/>.
        /// </summary>
        internal static ScopedCollection CreateCollection() => new();
    }
}
