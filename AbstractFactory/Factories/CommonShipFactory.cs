
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;
    using System;
    using System.Collections.Generic;

    public class CommonShipFactory : IAbstractShipFactory
    {
        private readonly IList<string> availableModels =
            new List<string>() { "Serenity", "Freedom", "Liberty", "Osprey", "Second Wind", "Destiny", " Andiamo", "Dream Catcher", "Spirit", "Odyssey", "Carpe Diem", "Island Time"};

        public CommonShipFactory()
            => Name = availableModels[new Random().Next(availableModels.Count - 1)];

        public string Name { get; }
    }
}
