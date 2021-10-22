
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;
    using System;
    using System.Collections.Generic;

    public class LuxuriousShipFactory : IAbstractShipFactory
    {
        private readonly IList<string> availableModels =
            new List<string>() { "Row V. Wave", "Dirty Oar", "Tumeric", "Life is Good", "Dreadnought", "Ships n' Giggles", "Moor Often Than Knot", "Grace to Glory", "Best of Boat Worlds", "The Court Ship", "Sea Senora", "Big Nauti"};

        public LuxuriousShipFactory()
            => Name = availableModels[new Random().Next(availableModels.Count - 1)];

        public string Name { get; }
    }
}
