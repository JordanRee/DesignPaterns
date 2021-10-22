
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Define a common ship.
    /// </summary>
    public class CommonShip : IAbstractShipProduct
    {
        /// <summary>
        /// Sort the available models.
        /// </summary>
        private static IList<string> availableModels =
            new List<string>() { "Serenity", "Freedom", "Liberty", "Osprey", "Second Wind", "Destiny", " Andiamo", "Dream Catcher", "Spirit", "Odyssey", "Carpe Diem", "Island Time"};

        /// <summary>
        /// Create a new instance of <see cref="CommonShip"/>.
        /// </summary>
        public CommonShip()
            => Name = availableModels[new Random().Next(availableModels.Count - 1)];

        /// <inheritdoc/>
        public string Name { get; }
    }
}
