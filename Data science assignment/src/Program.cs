using Data_science_assignment.src.algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Data_science_assignment.src;

namespace Data_science_assignment
{
    /// <summary>
    /// Startup class for the program
    /// </summary>
    class Program
    {
        /// <summary>
        /// This is the entrypoint into the console application
        /// </summary>
        private static void Main()
        {
            // Setup vars
            DataReader reader = new DataReader(@"../../assets/userItem.data", new[] {',', '\t'});
            PreferenceLoader loader = new PreferenceLoader(reader);
            List<UserPreference> preferences = loader.LoadPreferences();
            int[] uniqueUsers = loader.GetUniqueUsers();
            int[] uniqueArticles = loader.GetUniqueArticles();

            string q = "What do you want to do? [getAllRatings, Manhattan, Cosine, Pearson, Euclidean, Exit]";
            string choice;
           
            // Gets user input and executes corresponding user choice
            do
            {
                choice = AskQuestion(q);

                switch(choice)
                {
                    case "getallratings":
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

                        break;

                    case "manhattan":
                        HandleResponse(new ManhattanStrategy());
                        break;

                    case "cosine":
                        HandleResponse(new CosineStrategy());
                        break;

                    case "pearson":
                        HandleResponse(new PearsonStrategy());
                        break;

                    case "euclidean":
                        HandleResponse(new EuclideanStrategy());
                        break;
                }
            }
            while (choice != "exit");

            // Asks for a userId and executes the given strategy on that user and all other preferences
            void HandleResponse(IStrategy strategy)
            {
                UserPreference userToRate;

                string uidResponse = AskQuestion($"Please enter the userID you want to compare other users to. Should be one of: " +
                                            $"{string.Join(", ", uniqueUsers)}");

                try
                {
                    int uid = Convert.ToInt32(uidResponse);
                    userToRate = preferences[uid - 1];
                }
                catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is FormatException)
                {
                    Console.WriteLine("That is not a valid response, please try again.");
                    return;
                }

                if (AskQuestion("Calculate nearest neighbours? [y\\N] ") == "y")
                {
                    NearestNeighbours(userToRate, strategy);
                }
                else
                {
                    foreach (UserPreference preference in preferences)
                    {
                        AlgorithmContext context = new AlgorithmContext(strategy, userToRate, preference);

                        Console.WriteLine(
                            $"The {choice} similarity between UID {userToRate.userId} and {preference.userId} is: {context.ExecuteStrategy()} ");
                    }
                }
            }

            // Calculates nearest neighbours based on a threshold and amount of neighbours using the given algorithm
            void NearestNeighbours(UserPreference userToRate, IStrategy strategy)
            {
                string thresholdResponse = AskQuestion("Please enter a threshold: ");

                string amountOfNeighboursResponse = AskQuestion("Please enter the amount of neighbours: ");

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
                    return;
                }

                int listcnt = 0;

                // Create a result list with <similarity, UserPreference>
                SortedDictionary<double, UserPreference> result = new SortedDictionary<double, UserPreference>();

                foreach (UserPreference preference in preferences)
                {
                    // Don't include user to rate
                    if (preference.userId != userToRate.userId)
                    {
                        double sim = new AlgorithmContext(strategy, userToRate, preference).ExecuteStrategy();

                        Dictionary<int, float> prefwithoutzeroratings = preference.ratings.Where(kvp => kvp.Value > 0)
                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                        Dictionary<int, float> targetwithoutzeroratings = userToRate.ratings.Where(kvp => kvp.Value > 0)
                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

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
            }
        }

        /// <summary>
        /// Asks a question to the user and returns a lowercase, white-space trimmed string
        /// </summary>
        /// <param name="question">The question to ask</param>
        /// <returns>Lowercase trimmed user input</returns>
        static string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine().ToLower().Trim();
        }
    }
}
