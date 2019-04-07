using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class AdjustedCosine : IRatingPredictable
    {
        private readonly DataAwareAlgorithm _data;

        /// <summary>
        /// AdjustedCosine constructor
        /// </summary>
        /// <param name="data"></param>
        public AdjustedCosine(DataAwareAlgorithm data)
        {
            _data = data;
        }

        /// <summary>
        /// Gets the item-item Adjusted Cosine Similarity between 2 items i and j
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public Double GetItemSimilarity(int i, int j)
        {
            double numerator = 0.0;
            double denumerator = 0.0;
            double x1 = 0.0;
            double x2 = 0.0;

            // For every user that rated both items
            foreach (UserPreference preference in _data.preferences)
            {
                Dictionary<int, float> pref = _data.loader.getRatingsWithoutZero(preference);

                if (pref.ContainsKey(i) && pref.ContainsKey(j))
                {
                    // Numerator:
                    // multiply the adjusted rating of those two items and sum the results
                    double avgRating = pref.Values.Average();
                    numerator += (pref[i] - avgRating) * (pref[j] - avgRating);

                    // Denumerator:
                    // Sum the squares of all the adjusted ratings for item i and j and take the square root of that result
                    x1 += Math.Pow(pref[i] - avgRating, 2);
                    x2 += Math.Pow(pref[j] - avgRating, 2);
                }
            }

            denumerator = Math.Sqrt(x1) * Math.Sqrt(x2);

            return numerator / denumerator;
        }

        /// <summary>
        /// Predict the rating using the item-item Adjusted Cosine Similarity
        /// </summary>
        /// <param name="target">User to predict rating for</param>
        /// <param name="i">Product id that target has not yet rated</param>
        /// <returns></returns>
        public double PredictRating(UserPreference target, int i)
        {
            double numerator = 0.0;
            double denumerator = 0.0;

            // foreach item j that user target already rated
            foreach (KeyValuePair<int, float> kvp in _data.loader.getRatingsWithoutZero(target))
            {
                double sim = GetItemSimilarity(i, kvp.Key);

                // numerator += (similarity between i and j) * (rating of target for j)
                numerator += sim * Utils.Normalize(kvp.Value, 1, 5);

                // denumerator += absolute value of similarity between i and j
                denumerator += Math.Abs(sim);
            }
            
            return Utils.Denormalize(numerator / denumerator, 1, 5);
        }
    }
}
