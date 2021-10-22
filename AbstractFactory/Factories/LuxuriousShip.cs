
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Define a luxurious ship.
    /// </summary>
    public class LuxuriousShip : IAbstractShipProduct
    {
        /// <summary>
        /// Sort the available models.
        /// </summary>
        private static IList<string> availableModels =
            new List<string>() { "Row V. Wave", "Dirty Oar", "Tumeric", "Life is Good", "Dreadnought", "Ships n' Giggles", "Moor Often Than Knot", "Grace to Glory", "Best of Boat Worlds", "The Court Ship", "Sea Senora", "Big Nauti"};

        /// <summary>
        /// Create a new instance of <see cref="LuxuriousShip"/>.
        /// </summary>
        public LuxuriousShip()
            => Name = availableModels[new Random().Next(availableModels.Count - 1)];

        /// <inheritdoc/>
        public string Name { get; }
    }
}
