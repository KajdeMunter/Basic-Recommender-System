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
        public Double Execute(UserPreferences rating1, UserPreferences rating2)
        {
            foreach (KeyValuePair<int, float> kvprating1 in rating1.ratings)
            {
                foreach(KeyValuePair<int,float> kvprating2 in rating2.ratings)
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
