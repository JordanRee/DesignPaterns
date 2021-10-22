
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;

    public class LuxuriousFactory : IAbstractFactory
    {
        private LuxuriousFactory()
        { }

        public IAbstractCarFactory CreateCar() 
            => new LuxuriousCarFactory();

        public IAbstractShipFactory CreateShip() 
            => new LuxuriousShipFactory();

        public static IAbstractFactory GetFactory() 
            => new LuxuriousFactory();
    }
}
