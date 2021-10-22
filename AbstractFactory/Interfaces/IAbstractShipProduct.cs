
namespace AbstractFactory.Interfaces
{
    /// <summary>
    /// Define the base components for a ship.
    /// </summary>
    public interface IAbstractShipProduct
    {
        /// <summary>
        /// Gets the name of the ship.
        /// </summary>
        string Name { get; }
    }
}