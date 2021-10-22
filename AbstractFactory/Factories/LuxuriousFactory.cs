
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;

    /// <summary>
    /// Define a kuxurious vehicle factory.
    /// </summary>
    public class LuxuriousFactory : IAbstractVehicleFactory
    {
        private LuxuriousFactory()
        { }

        /// <inheritdoc/>
        public IAbstractCarProduct CreateCar() 
            => new LuxuriousCar();

        /// <inheritdoc/>
        public IAbstractShipProduct CreateShip() 
            => new LuxuriousShip();

        /// <summary>
        /// Create a luxurious vehicle factory.
        /// </summary>
        /// <returns>Return a <see cref="IAbstractVehicleFactory"/> factory.</returns>
        public static IAbstractVehicleFactory GetFactory() 
            => new LuxuriousFactory();
    }
}
