
namespace AbstractFactory.Test
{
    using AbstractFactory.Factories;
    using AbstractFactory.Interfaces;
    using NUnit.Framework;

    public class FactoryTests
    {
        private IAbstractVehicleFactory common;
        private IAbstractVehicleFactory luxurious;

        [SetUp]
        public void SetUp()
        {
            common = CommonFactory.GetFactory();
            luxurious = LuxuriousFactory.GetFactory();
        }

        [TearDown]
        public void TearDown()
        {
            common = null;
            luxurious = null;
        }

        [Test]
        public void FactoriesExists()
        {
            Assert.IsNotNull(common, "Common factory should not be null.");
            Assert.IsNotNull(luxurious, "Luxurious factory should not be null.");
        }

        [Test]
        public void CreateCar()
        {
            var commonCar = common.CreateCar();
            var luxuriousCar = luxurious.CreateCar();

            Assert.IsNotNull(commonCar, "The common car should not be null.");
            Assert.IsNotNull(luxuriousCar, "The luxurious car should not be null.");

            Assert.IsNotEmpty(commonCar.Name, "The common car's name should be filled.");
            Assert.IsNotEmpty(luxuriousCar.Name, "The luxurious car's name should be filled.");
        }

        [Test]
        public void ShipCar()
        {
            var commonShip = common.CreateShip();
            var luxuriousShip = luxurious.CreateShip();

            Assert.IsNotNull(commonShip, "The common ship should not be null.");
            Assert.IsNotNull(luxuriousShip, "The luxurious ship should not be null.");

            Assert.IsNotEmpty(commonShip.Name, "The common ship's name should be filled.");
            Assert.IsNotEmpty(luxuriousShip.Name, "The luxurious ship's name should be filled.");
        }
    }
}
