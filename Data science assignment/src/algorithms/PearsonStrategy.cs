using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class PearsonStrategy : IStrategy
    {
        public Double Execute(SortedDictionary<int, float> pointP, SortedDictionary<int, float> pointQ)
        {
            Double productSum = 0.0;
            Double xSum = 0.0;
            Double ySum = 0.0;
            Double xSqr = 0.0;
            Double ySqr = 0.0;
            int n = 0;

            foreach (KeyValuePair<int, float> kvprating1 in pointP)
            {
                foreach (KeyValuePair<int, float> kvprating2 in pointQ)
                {
                    if (kvprating1.Key == kvprating2.Key && kvprating1.Value > 0 && kvprating2.Value > 0)
                    {
                        //Prepare the data for the formula
                        productSum += kvprating1.Value * kvprating2.Value;
                        xSum += kvprating1.Value;
                        ySum += kvprating2.Value;
                        xSqr += Math.Pow(kvprating1.Value, 2);
                        ySqr += Math.Pow(kvprating2.Value, 2);
                        n += 1; 
                    }
                }
            }

            //Execute the formula for the Pearson coefficient
            Double numerator = productSum - ((xSum * ySum) / n);
            var denominator = Math.Sqrt(xSqr - (xSum * xSum) / n) * Math.Sqrt(ySqr - (ySum * ySum) / n);
            var result = numerator / denominator;
            return result;
        }
    }
}
