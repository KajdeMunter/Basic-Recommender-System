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
        public static void printRatings(int[] uniqueArticles, int[] uniqueUsers, List<UserPreference> preferences)
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
    }
}
