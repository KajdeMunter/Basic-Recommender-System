using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class PredictRating
    {
        private readonly DataAwareAlgorithm _data;

        public PredictRating(DataAwareAlgorithm data)
        {
            _data = data;
        }

        /// <summary>
        /// Predict a rating based on the nearest neighbours
        /// </summary>
        /// <param name="neighbours"></param>
        /// <param name="targetUser"></param>
        public void Calculate(List<Tuple<double, UserPreference>> neighbours, UserPreference targetUser)
        { 
            List<int> productsToRate = new List<int>();

            // Add products that neighbours have rated
            foreach (Tuple<double, UserPreference> tuple in neighbours)
            {
                foreach (int pid in tuple.Item2.ratings.Keys)
                {
                    if (!productsToRate.Contains(pid)) productsToRate.Add(pid);
                }
            }

            // Remove the products that the user has already rated so we have a list of products that we can predict
            foreach (int pid in _data.loader.getRatingsWithoutZero(targetUser).Keys)
            {
                productsToRate.Remove(pid);
            }

            // Predict rating
            foreach (int pid in productsToRate)
            {
                double numerator = 0.0;
                double denominator = 0.0;

                foreach (Tuple<double, UserPreference> kvp in neighbours)
                {
                    // Use only the users that rated the product
                    if (_data.loader.getRatingsWithoutZero(kvp.Item2).ContainsKey(pid))
                    {
                        numerator += kvp.Item1 * _data.loader.getRatingsWithoutZero(kvp.Item2)[pid];
                        denominator += kvp.Item1;
                    }
                }

                Console.WriteLine($"Predicted rating for {pid} is {numerator / denominator}");
            }
        }

    }
}
