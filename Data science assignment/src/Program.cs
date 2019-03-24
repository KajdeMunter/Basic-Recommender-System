using Data_science_assignment.src.algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
            DataAwareAlgorithm dataAwareAlgorithm = new DataAwareAlgorithm(preferences, uniqueUsers, uniqueArticles, loader);


            string q = "What do you want to do? [getAllRatings, Manhattan, Cosine, Pearson, Euclidean, Exit]";
            string choice;
           
            // Gets user input and executes corresponding user choice
            do
            {
                choice = Utils.AskQuestion(q);

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

                string uidResponse = Utils.AskQuestion($"Please enter the userID you want to compare other users to. Should be one of: " +
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

                if (Utils.AskQuestion("Calculate nearest neighbours and predict rating? [y\\N] ") == "y")
                {
                    NearestNeighbours nearestNeighbours = new NearestNeighbours(dataAwareAlgorithm);
                    predictRating(nearestNeighbours.Calculate(userToRate, strategy).Reverse().ToDictionary(kvp => kvp.Key, kvp => kvp.Value), userToRate);
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

            void predictRating(Dictionary<double, UserPreference> neighbours, UserPreference targetUser)
            {
                double numerator = 0.0;
                double denominator = 0.0;

                List<UserPreference> neighbourPreferences = new List<UserPreference>();

                foreach (UserPreference pref in neighbours.Values)
                {
                    neighbourPreferences.Add(pref);
                }

                // Get the products all neighbours have rated
                HashSet<int> targetHashSet = new HashSet<int>(loader.getRatingsWithoutZero(neighbourPreferences.First()).Keys);

                foreach (UserPreference neighbourPreference in neighbours.Values.Skip(1))
                {
                    targetHashSet.IntersectWith(loader.getRatingsWithoutZero(neighbourPreference).Keys);
                }

                List<int> productsInCommon = targetHashSet.ToList();

                // Remove the products that the user has already rated so we have a list of products that we can predict
                foreach (int pid in loader.getRatingsWithoutZero(targetUser).Keys)
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

                    Console.WriteLine($"Predicted rating for {pid} is {numerator/denominator}");
                }
            }
        }
    }
}
