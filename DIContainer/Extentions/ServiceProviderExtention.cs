
namespace DIContainer.Extentions
{
    using DIContainer.Collections;

    public static class ServiceProviderExtention
    {
        public static ServiceProvider BuildServiceProvider(this ServiceCollection collection)
        {
            return new ServiceProvider(collection);
        }
    }
}
