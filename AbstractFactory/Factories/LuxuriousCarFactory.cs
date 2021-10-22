
namespace AbstractFactory.Factories
{
    using AbstractFactory.Interfaces;
    using System;
    using System.Collections.Generic;

    public class LuxuriousCarFactory : IAbstractCarFactory
    {
        private readonly IList<string> availableModels = 
            new List<string>() { "Lamborghini Diablo", "Ford Raptor", "Ferrari Testarossa", "Porsche 911 Carrera", "Jensen Interceptor", "Lamborghini Huracán", "Ferrari 812 Superfast", "Jeep Gladiator"};

        public LuxuriousCarFactory()
            => Name = availableModels[new Random().Next(availableModels.Count - 1)]; 

        public string Name { get; }
    }
}
