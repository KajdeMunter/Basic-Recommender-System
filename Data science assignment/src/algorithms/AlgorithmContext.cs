using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    /// <summary>
    /// The AlgorithmContext uses IStrategy to call the algorithm defined by a concrete strategy.
    /// </summary>
    class AlgorithmContext
    {
        private readonly IStrategy _strategy;
        private readonly SortedDictionary<int, float> _pointP;
        private readonly SortedDictionary<int, float> _pointQ;

        /// <summary>
        /// Context constructor
        /// </summary>
        /// <param name="strategy">IStrategy to execute on the preferences</param>
        /// <param name="pointP">Preference to pass to the strategy</param>
        /// <param name="pointQ">Preference to pass to the strategy</param>
        public AlgorithmContext(IStrategy strategy, SortedDictionary<int, float> pointP, SortedDictionary<int, float> pointQ)
        {
            _strategy = strategy;
            _pointP = pointP;
            _pointQ = pointQ;
        }

        /// <summary>
        /// Calls Execute() with the preferences
        /// </summary>
        /// <returns>Result of the strategy agains the two given preferences</returns>
        public double ExecuteStrategy()
        {
            return _strategy.Execute(_pointP, _pointQ);
        }
    }
}
