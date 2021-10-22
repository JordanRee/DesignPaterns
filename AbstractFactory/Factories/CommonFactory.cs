
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;

    /// <summary>
    /// Define a common vehicle factory.
    /// </summary>
    public class CommonFactory : IAbstractVehicleFactory
    {
        private CommonFactory()
        { }

        /// <inheritdoc/>
        public IAbstractCarProduct CreateCar()
            => new CommonCar();

        /// <inheritdoc/>
        public IAbstractShipProduct CreateShip()
            => new CommonShip();


        /// <summary>
        /// Create a common vehicle factory.
        /// </summary>
        /// <returns>Return a <see cref="IAbstractVehicleFactory"/> factory.</returns>
        public static IAbstractVehicleFactory GetFactory()
            => new CommonFactory();
    }
}
