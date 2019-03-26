using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src
{
    public static class Utils
    {
        /// <summary>
        /// Asks a question to the user and returns a lowercase, white-space trimmed string
        /// </summary>
        /// <param name="question">The question to ask</param>
        /// <returns>Lowercase trimmed user input</returns>
        public static string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine().ToLower().Trim();
        }

        /// <summary>
        /// Prints all ratings in a pretty formatted table
        /// </summary>
        /// <param name="uniqueArticles"></param>
        /// <param name="uniqueUsers"></param>
        /// <param name="preferences"></param>
        public static void PrintRatings(int[] uniqueArticles, int[] uniqueUsers, List<UserPreference> preferences)
        {
            Console.Write("\t");

            foreach (int pid in uniqueArticles)
            {
                Console.Write($"{pid}\t");
            }

            Console.WriteLine();

            foreach (int uid in uniqueUsers)
            {
                Console.Write($"ID {uid}\t");

                foreach (UserPreference pref in preferences)
                {
                    if (pref.userId == uid)
                    {
                        foreach (KeyValuePair<int, float> ratings in pref.ratings)
                        {
                            Console.Write($"{ratings.Value}\t");
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Computes sparcity from data
        /// </summary>
        /// <param name="preferences"></param>
        /// <param name="uniqueUsers"></param>
        /// <param name="uniqueArticles"></param>
        /// <param name="loader"></param>
        /// <returns></returns>
        public static double ComputeSparcity(List<UserPreference> preferences, int[] uniqueUsers, int[] uniqueArticles, PreferenceLoader loader)
        {
            double amountOfRatings = 0;
            foreach (UserPreference pref in preferences)
            {
                amountOfRatings += loader.getRatingsWithoutZero(pref).Count;
            }

            return 1 - amountOfRatings / (preferences.Count * uniqueArticles.Length);
        }

        /// <summary>
        /// Normalize a rating from rmin, rmax into -1, 1
        /// </summary>
        /// <param name="r">Denoramlized Rating</param>
        /// <param name="rmin">Minimum rating to normalize to</param>
        /// <param name="rmax">Maximum rating to normalize to</param>
        /// <returns></returns>
        public static double Normalize(double r, int rmin, int rmax)
        {
            return 2 * ((r - rmin) / (rmax - rmin)) - 1;
        }

        /// <summary>
        /// Denormalize a rating from -1, 1 to rmin, rmax
        /// </summary>
        /// <param name="r">Normalized rating</param>
        /// <param name="rmin"></param>
        /// <param name="rmax"></param>
        /// <returns></returns>
        public static double Denormalize(double r, int rmin, int rmax)
        {
            return ((r + 1) / 2) * (rmax - rmin) + rmin;
        }
    }
}
