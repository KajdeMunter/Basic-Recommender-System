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
        private readonly UserPreference rating1;
        private readonly UserPreference rating2;

        public Context(IStrategy strategy, UserPreference rating1, UserPreference rating2)
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
