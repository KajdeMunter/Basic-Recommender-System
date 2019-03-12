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
        private readonly UserPreference _pref1;
        private readonly UserPreference _pref2;

        /// <summary>
        /// Context constructor
        /// </summary>
        /// <param name="strategy">IStrategy to execute on the preferences</param>
        /// <param name="preference1">Preference to pass to the strategy</param>
        /// <param name="preference2">Preference to pass to the strategy</param>
        public AlgorithmContext(IStrategy strategy, UserPreference preference1, UserPreference preference2)
        {
            _strategy = strategy;
            _pref1 = preference1;
            _pref2 = preference2;
        }

        /// <summary>
        /// Calls Execute() with the preferences
        /// </summary>
        /// <returns>Result of the strategy agains the two given preferences</returns>
        public double ExecuteStrategy()
        {
            return _strategy.Execute(_pref1, _pref2);
        }
    }
}
