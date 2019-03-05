using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    /// <summary>
    /// Interface for the strategy pattern. A behavioral design pattern that enables selecting an algorithm at runtime.
    /// </summary>
    interface IStrategy
    {
        Double Execute(UserPreference pref1, UserPreference pref2);
    }
}
