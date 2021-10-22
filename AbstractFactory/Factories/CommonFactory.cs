
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;

    public class CommonFactory : IAbstractFactory
    {
        private CommonFactory()
        { }

        public IAbstractCarFactory CreateCar()
            => new CommonCarFactory();

        public IAbstractShipFactory CreateShip()
            => new CommonShipFactory();

        public static IAbstractFactory GetFactory()
            => new CommonFactory();
    }
}
