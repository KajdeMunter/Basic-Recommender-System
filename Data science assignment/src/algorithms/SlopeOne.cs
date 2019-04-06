using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class SlopeOne
    {
        private readonly DataAwareAlgorithm _data;

        /// <summary>
        /// AdjustedCosine constructor
        /// </summary>
        /// <param name="data"></param>
        public SlopeOne(DataAwareAlgorithm data)
        {
            _data = data;
        }

        /// <summary>
        /// Returns a tuple containing the deviation for 2 products and howManyUsers
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>a tuple containing the deviation for 2 products and howManyUsers</returns>
        public Tuple<float,int> GetDeviation(int i, int j)
        {
            float Currdev = 0;
            int usercnt = 0;

            // Foreach user u who rated both items i and j
            foreach (UserPreference preference in _data.preferences)
            {
                Dictionary<int, float> pref = _data.loader.getRatingsWithoutZero(preference);

                if (pref.ContainsKey(i) && pref.ContainsKey(j))
                {
                    Currdev += pref[i] - pref[j];
                    usercnt++;
                }
            }

            // Currdev / (How many users rated both I and J)
            float dev = Currdev / usercnt;

            return new Tuple<float, int>(dev, usercnt);
        }

        /// <summary>
        /// Prints deviations in a pretty formatted table
        /// </summary>
        public void PrintDeviations()
        {
            Console.Write("\t");

            foreach (int pid in _data.uniqueArticles)
            {
                Console.Write($"{pid}\t");
            }

            Console.WriteLine();

            foreach (int pid1 in _data.uniqueArticles)
            {
                Console.Write($"{pid1} \t");

                foreach (int pid2 in _data.uniqueArticles)
                {
                    Console.Write($"{Math.Round(GetDeviation(pid1, pid2).Item1)} \t");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public double PredictRating(UserPreference target, int i)
        {
            double numerator = 0;
            double denominator = 0;

            // foreach item j that target has already rated
            foreach (KeyValuePair<int, float> kvp in _data.loader.getRatingsWithoutZero(target))
            {
                // Extract info about deviation between i and j (Deviation and howManyUsers)
                Tuple<float, int> deviationInfo = GetDeviation(i, kvp.Key);
                float deviation = deviationInfo.Item1;
                int howManyUsers = deviationInfo.Item2;

                // numerator += (rating of u for j + deviation between i and j) * (howManyUsers)
                numerator += (kvp.Value + deviation) * howManyUsers;

                denominator += howManyUsers;
            }

            return numerator / denominator;
        }
    }
}
