using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    interface IStrategy
    {
        Double Execute(UserPreference pref1, UserPreference pref2);
    }
}
