
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Define a luxurious car.
    /// </summary>
    public class LuxuriousCar : IAbstractCarProduct
    {
        /// <summary>
        /// Sort the available models.
        /// </summary>
        private static IList<string> availableModels = 
            new List<string>() { "Lamborghini Diablo", "Ford Raptor", "Ferrari Testarossa", "Porsche 911 Carrera", "Jensen Interceptor", "Lamborghini Huracán", "Ferrari 812 Superfast", "Jeep Gladiator"};

        /// <summary>
        /// Create a new instance of <see cref="LuxuriousCar"/>.
        /// </summary>
        public LuxuriousCar()
            => Name = availableModels[new Random().Next(availableModels.Count - 1)];

        /// <inheritdoc/>
        public string Name { get; }
    }
}
