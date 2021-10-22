
namespace DIContainer.Descriptors
{
    /// <summary>
    /// Different 
    /// </summary>
    public enum ServiceLifetime
    {
        /// <summary>
        /// Creates a new Service only once during the application lifetime, and uses it everywhere.
        /// </summary>
        Singleton,
        /// <summary>
        /// Creates a new instance for every scope. (Each "request" is a Scope). Within the scope, it reuses the existing service.
        /// </summary>
        Scoped,
        /// <summary>
        /// Creates a new instance of the service every time you request it.
        /// </summary>
        Transient
    }
}