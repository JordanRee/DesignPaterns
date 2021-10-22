namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;
    using System;
    using System.Collections.Generic;

    public class CommonCarFactory : IAbstractCarFactory
    {
        private readonly IList<string> availableModels =
            new List<string>() { "Ford F-Series", "Chevrolet Silverado", "Ram Pickup", "Toyota RAV4", "Honda CR-V", "Toyota Camry", "Chevrolet Equinox", "Honda Civic", "GMC Sierra", "Toyota Tacoma", "Nissan Rogue", "Ford Explorer"};

        public CommonCarFactory()
            => Name = availableModels[new Random().Next(availableModels.Count - 1)];

        public string Name { get; }
    }
}
