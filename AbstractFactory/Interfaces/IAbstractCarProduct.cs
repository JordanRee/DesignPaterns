
namespace AbstractFactory.Interfaces
{
    /// <summary>
    /// Define the base components for a car.
    /// </summary>
    public interface IAbstractCarProduct
    {
        /// <summary>
        /// Gets the name of the car.
        /// </summary>
        string Name { get; }
    }
}