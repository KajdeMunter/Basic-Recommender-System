using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class CosineStrategy : IStrategy
    {
        public Double Execute(UserPreference rating1, UserPreference rating2)
        {
            float sumOfXmultipliedByY = 0F;
            float sumOfSquaredX = 0F;
            float sumOfSquaredY = 0F;

            foreach (KeyValuePair<int, float> kvprating1 in rating1.ratings)
            {
                foreach (KeyValuePair<int, float> kvprating2 in rating2.ratings)
                {
                    sumOfXmultipliedByY += kvprating1.Value * kvprating2.Value;
                    sumOfSquaredX += kvprating1.Value * kvprating1.Value;
                    sumOfSquaredY += kvprating2.Value * kvprating2.Value;

                }
            }

            return sumOfXmultipliedByY / (Math.Sqrt(sumOfSquaredX) * Math.Sqrt(sumOfSquaredY));
        }
    }
}
