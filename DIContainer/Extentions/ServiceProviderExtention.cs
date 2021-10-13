
namespace DIContainer.Extentions
{
    using DIContainer.Collections;

    /// <summary>
    /// Extention to build <see cref="ServiceCollection"/> into <see cref="ServiceProvider"/>.
    /// </summary>
    public static class ServiceProviderExtention
    {
        /// <summary>
        /// Build <see cref="ServiceProvider"/> from <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection"><see cref="ServiceCollection"/> to build into <see cref="ServiceProvider"/>.</param>
        /// <returns>Builded <see cref="ServiceProvider"/>.</returns>
        public static ServiceProvider BuildServiceProvider(this ServiceCollection collection) 
            => new(collection);
    }
}
