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
        public void Calculate(Dictionary<double, UserPreference> neighbours, UserPreference targetUser)
        {
            double numerator = 0.0;
            double denominator = 0.0;

            List<UserPreference> neighbourPreferences = new List<UserPreference>();

            foreach (UserPreference pref in neighbours.Values)
            {
                neighbourPreferences.Add(pref);
            }

            // Get the products all neighbours have rated
            HashSet<int> targetHashSet = new HashSet<int>(_data.loader.getRatingsWithoutZero(neighbourPreferences.First()).Keys);

            foreach (UserPreference neighbourPreference in neighbours.Values.Skip(1))
            {
                targetHashSet.IntersectWith(_data.loader.getRatingsWithoutZero(neighbourPreference).Keys);
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
                double res = 0.0;

                foreach (KeyValuePair<double, UserPreference> kvp in neighbours)
                {
                    numerator += (kvp.Key * kvp.Value.ratings[pid]);
                    denominator += kvp.Key;
                }

                Console.WriteLine($"Predicted rating for {pid} is {numerator / denominator}");
            }
        }

    }
}
