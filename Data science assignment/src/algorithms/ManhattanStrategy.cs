using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class ManhattanStrategy : IStrategy
    {
        public Double Execute(UserPreference pref1, UserPreference pref2)
        {
            Double result = 0;

            foreach (KeyValuePair<int, float> kvprating1 in pref1.ratings)
            {
                foreach(KeyValuePair<int,float> kvprating2 in pref2.ratings)
                {
                    if (kvprating1.Key == kvprating2.Key && kvprating1.Value > 0 && kvprating2.Value > 0)
                    {
                        Double output = Math.Abs(kvprating1.Value - kvprating2.Value);
                        result += output;
                    }
                }
            }
            return 1/(1+result);
        }
    }
}
