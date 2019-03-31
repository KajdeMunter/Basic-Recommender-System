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
        /// Returns the deviation for two products
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public float GetDeviation(int i, int j)
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
            return Currdev / usercnt;
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
                    Console.Write($"{Math.Round(GetDeviation(pid1, pid2))} \t");
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
            return 0;
        }
    }
}
