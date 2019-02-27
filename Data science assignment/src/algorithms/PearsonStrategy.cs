using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class PearsonStrategy : IStrategy
    {
        Double result, numerator, denominator, productSum, xSum, xSqr, ySqr, ySum, n = 0;
        public Double Execute(UserPreference pref1, UserPreference pref2)
        {
            foreach (KeyValuePair<int, float> kvprating1 in pref1.ratings)
            {
                foreach (KeyValuePair<int, float> kvprating2 in pref2.ratings)
                {
                    if (kvprating1.Key == kvprating2.Key)
                    {
                        //Prepare the data for the formula
                        productSum += kvprating1.Value * kvprating2.Value;
                        xSum += kvprating1.Value;
                        ySum += kvprating2.Value;
                        xSqr += kvprating1.Value * kvprating1.Value;
                        ySqr += kvprating2.Value * kvprating2.Value;
                        n += 1; 
                        
                    }
                }
            }

            //Execute the formula for the Pearson coefficient
            numerator = productSum - ((xSum * ySum) / n);
            denominator = Math.Sqrt(xSqr - (xSum * xSum) / n) * Math.Sqrt(ySqr - (ySum * ySum) / n);
            result = numerator / denominator;
            return result;
        }
    }
}
