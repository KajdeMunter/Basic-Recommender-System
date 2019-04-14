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
            _data = data;
        }

        /// <summary>
        /// Calculates nearest neighbours based on a threshold and amount of neighbours using the given algorithm
        /// </summary>
        /// <param name="userToRate"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public List<Tuple<double, UserPreference>> Calculate(UserPreference userToRate, IStrategy strategy)
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
                return new List<Tuple<double, UserPreference>>();
            }

            int listcnt = 0;

            // Create a result list with <similarity, UserPreference>
            List<Tuple<double, UserPreference>> result = new List<Tuple<double, UserPreference>>();

            // Remove unrated items from target
            Dictionary<int, float> targetwithoutzeroratings = _data.loader.getRatingsWithoutZero(userToRate);

            foreach (UserPreference preference in _data.preferences)
            {
                // Don't include user to rate
                if (preference.userId != userToRate.userId)
                {
                    double sim = new AlgorithmContext(strategy, userToRate.ratings, preference.ratings).ExecuteStrategy();

                    // Remove unrated items from preference
                    Dictionary<int, float> prefwithoutzeroratings = _data.loader.getRatingsWithoutZero(preference);

                    // if similarity > threshold and userId has rated additional items with respect to target
                    if (sim > threshold && prefwithoutzeroratings.Keys.ToList()
                            .Exists(article => !targetwithoutzeroratings.ContainsKey(article)))
                    {

                        // If the list of neighbours is not full yet and the similarity is not already in the list
                        if (listcnt < k)
                        {
                            // insert userId and its similarity
                            result.Add(new Tuple<double, UserPreference>(sim, preference));
                            ++listcnt;
                        }
                        // Else if the list is already full But similarity is greater than the lowest similarity in the list
                        else
                        {
                            result = result.OrderBy(t => t.Item1).ToList();
                            Tuple<double, UserPreference> lowestSimilarityUser = result.First();

                            double lowestSimilarity = lowestSimilarityUser.Item1;

                            if (sim > lowestSimilarity)
                            {
                                // Replace the neighbour associated to such lowest similarity with userId
                                result.Remove(lowestSimilarityUser);
                                result.Add(new Tuple<double, UserPreference>(sim, preference));
                            }
                        }
                        // If the neighbours list is full, update the value of the threshold
                        if (listcnt >= k)
                        {
                            // the new threshold is the lowest neighbour similarity
                            threshold = result.Min(t => t.Item1);
                        }
                    }
                }
            }

            Console.WriteLine($"The nearest neighbours for UID {userToRate.userId} are: ");
            foreach (Tuple<double, UserPreference> neighbour in result.OrderBy(t => t.Item2.userId))
            {
                Console.WriteLine($"UID {neighbour.Item2.userId} with similarity {neighbour.Item1}");
            }

            return result;
        }
    }
}
