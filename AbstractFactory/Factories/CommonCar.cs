
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Define a common car.
    /// </summary>
    public class CommonCar : IAbstractCarProduct
    {
        /// <summary>
        /// Sort the available models.
        /// </summary>
        private static IList<string> availableModels =
            new List<string>() { "Ford F-Series", "Chevrolet Silverado", "Ram Pickup", "Toyota RAV4", "Honda CR-V", "Toyota Camry", "Chevrolet Equinox", "Honda Civic", "GMC Sierra", "Toyota Tacoma", "Nissan Rogue", "Ford Explorer"};

        /// <summary>
        /// Create a new instance of <see cref="CommonCar"/>.
        /// </summary>
        public CommonCar()
            => Name = availableModels[new Random().Next(availableModels.Count - 1)];

        /// <inheritdoc/>
        public string Name { get; }
    }
}
