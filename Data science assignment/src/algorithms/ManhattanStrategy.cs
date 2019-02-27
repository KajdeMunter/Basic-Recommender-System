using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class ManhattanStrategy : IStrategy
    {
        Double result = 0;
        public Double Execute(UserPreference pref1, UserPreference pref2)
        {
            foreach (KeyValuePair<int, float> kvprating1 in pref1.ratings)
            {
                foreach(KeyValuePair<int,float> kvprating2 in pref2.ratings)
                {
                    if (kvprating1.Key == kvprating2.Key)
                    {
                        Double output = Math.Abs(kvprating1.Value - kvprating2.Value);
                        result += output;
                    }
                }
            }
            return result;
        }
    }
}
