using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    interface IStrategy
    {
        void Execute(Dictionary<int, UserPreferences> rating1, Dictionary<int, UserPreferences> rating2);
    }
}
