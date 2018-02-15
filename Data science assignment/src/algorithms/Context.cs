using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class Context
    {
        private IStrategy _strategy;
        private readonly UserPreferences rating1;
        private readonly UserPreferences rating2;

        public Context(IStrategy strategy, UserPreferences rating1, UserPreferences rating2)
        {
            _strategy = strategy;
            this.rating1 = rating1;
            this.rating2 = rating2;
        }

        public void ContextInterface()
        {
            // Print Double output
            Console.WriteLine(_strategy.Execute(rating1, rating2));
        }
    }
}
