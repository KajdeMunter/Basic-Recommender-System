using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class NearestNeighbours
    {
        private readonly DataAwareAlgorithm _data;

        public NearestNeighbours(DataAwareAlgorithm data)
        {
            this._data = data;
        }

        /// <summary>
        /// Calculates nearest neighbours based on a threshold and amount of neighbours using the given algorithm
        /// </summary>
        /// <param name="userToRate"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public SortedDictionary<double, UserPreference> Calculate(UserPreference userToRate, IStrategy strategy)
        {
            string thresholdResponse = Utils.AskQuestion("Please enter a threshold: ");

            string amountOfNeighboursResponse = Utils.AskQuestion("Please enter the amount of neighbours: ");

            double threshold;
            int k;

            try
            {
                threshold = double.Parse(thresholdResponse, CultureInfo.InvariantCulture.NumberFormat);
                k = Convert.ToInt32(amountOfNeighboursResponse);
            }
            catch (FormatException)
            {
                Console.WriteLine("That is not a valid response. please try again.");
                return new SortedDictionary<double, UserPreference>();
            }

            int listcnt = 0;

            // Create a result list with <similarity, UserPreference>
            SortedDictionary<double, UserPreference> result = new SortedDictionary<double, UserPreference>();

            // Remove unrated items from target
            Dictionary<int, float> targetwithoutzeroratings = _data.loader.getRatingsWithoutZero(userToRate);

            foreach (UserPreference preference in _data.preferences)
            {
                // Don't include user to rate
                if (preference.userId != userToRate.userId)
                {
                    double sim = new AlgorithmContext(strategy, userToRate, preference).ExecuteStrategy();

                    // Remove unrated items from preference
                    Dictionary<int, float> prefwithoutzeroratings = _data.loader.getRatingsWithoutZero(preference);

                    // if similarity > threshold and userId has rated additional items with respect to target
                    if (sim > threshold && prefwithoutzeroratings.Keys.ToList()
                            .Exists(article => !targetwithoutzeroratings.ContainsKey(article)))
                    {

                        // If the list of neighbours is not full yet
                        if (listcnt < k)
                        {
                            // insert userId and its similarity
                            result.Add(sim, preference);
                            ++listcnt;
                        }
                        // Else if the list is already full But similarity is greater than the lowest similarity in the list
                        else
                        {
                            KeyValuePair<double, UserPreference> lowestSimilarityUser = result.First();

                            double lowestSimilarity = lowestSimilarityUser.Key;

                            if (sim > lowestSimilarity)
                            {
                                // Replace the neighbour associated to such lowest similarity with userId
                                result.Remove(lowestSimilarityUser.Key);
                                result.Add(sim, preference);
                            }
                        }
                        // If the neighbours list is full, update the value of the threshold
                        if (listcnt >= k)
                        {
                            // the new threshold is the lowest neighbour similarity
                            threshold = result.Min(t => t.Key);
                        }
                    }
                }
            }

            Console.WriteLine($"The nearest neighbours for UID {userToRate.userId} are: ");
            foreach (KeyValuePair<double, UserPreference> neighbour in result.Reverse())
            {
                Console.WriteLine($"UID {neighbour.Value.userId} with similarity {neighbour.Key}");
            }

            return result;
        }
    }
}
