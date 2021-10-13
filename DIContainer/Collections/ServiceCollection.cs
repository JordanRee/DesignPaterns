
namespace DIContainer.Collections
{
    using DIContainer.Descriptors;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Service instance declaration collection.
    /// </summary>
    public class ServiceCollection : ObservableCollection<ServiceDescriptor>
    {
        /// <summary>
        /// Create a new <see cref="ServiceCollection"/>.
        /// </summary>
        public static ServiceCollection CreateCollection() => new();
    }
}
