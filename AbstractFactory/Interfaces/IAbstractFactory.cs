
namespace AbstractFactory.Interfaces
{
    public interface IAbstractFactory
    {
        IAbstractCarFactory CreateCar();
        IAbstractShipFactory CreateShip();
    }
}
