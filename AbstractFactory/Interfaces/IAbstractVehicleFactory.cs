
namespace AbstractFactory.Interfaces
{
    /// <summary>
    /// Define the builders needed for the vehicle factory.
    /// </summary>
    public interface IAbstractVehicleFactory
    {
        /// <summary>
        /// Should handle car creation.
        /// </summary>
        /// <returns>Return <see cref="IAbstractCarProduct"/> car.</returns>
        IAbstractCarProduct CreateCar();

        /// <summary>
        /// Should handle ship creation.
        /// </summary>
        /// <returns>Return <see cref="IAbstractShipProduct"/> ship.</returns>
        IAbstractShipProduct CreateShip();
    }
}
