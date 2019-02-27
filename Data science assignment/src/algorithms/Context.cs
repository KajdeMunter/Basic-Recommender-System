using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class Context
    {
        private readonly IStrategy _strategy;
        private readonly UserPreference _pref1;
        private readonly UserPreference _pref2;

        public Context(IStrategy strategy, UserPreference preference1, UserPreference preference2)
        {
            _strategy = strategy;
            this._pref1 = preference1;
            this._pref2 = preference2;
        }

        public double ExecuteStrategy()
        {
            return _strategy.Execute(_pref1, _pref2);
        }
    }
}
