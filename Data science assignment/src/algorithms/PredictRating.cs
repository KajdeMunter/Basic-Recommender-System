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
            this._data = data;
        }

        /// <summary>
        /// Predict a rating based on the nearest neighbours
        /// </summary>
        /// <param name="neighbours"></param>
        /// <param name="targetUser"></param>
        public void Calculate(List<Tuple<double, UserPreference>> neighbours, UserPreference targetUser)
        {
            double numerator = 0.0;
            double denominator = 0.0;

            List<UserPreference> neighbourPreferences = new List<UserPreference>();

            foreach (Tuple<double, UserPreference> tuple in neighbours)
            {
                neighbourPreferences.Add(tuple.Item2);
            }

            // Get the products all neighbours have rated
            HashSet<int> targetHashSet = new HashSet<int>(_data.loader.getRatingsWithoutZero(neighbourPreferences.First()).Keys);

            foreach (Tuple<double, UserPreference> neighbourPreference in neighbours.Skip(1))
            {
                targetHashSet.IntersectWith(_data.loader.getRatingsWithoutZero(neighbourPreference.Item2).Keys);
            }

            List<int> productsInCommon = targetHashSet.ToList();
            
            
            // Remove the products that the user has already rated so we have a list of products that we can predict
            foreach (int pid in _data.loader.getRatingsWithoutZero(targetUser).Keys)
            {
                productsInCommon.Remove(pid);
            }

            // Predict rating
            foreach (int pid in productsInCommon)
            {
                foreach (Tuple<double, UserPreference> kvp in neighbours)
                {
                    numerator += (kvp.Item1 * kvp.Item2.ratings[pid]);
                    denominator += kvp.Item1;
                }

                Console.WriteLine($"Predicted rating for {pid} is {numerator / denominator}");
            }
        }

    }
}
