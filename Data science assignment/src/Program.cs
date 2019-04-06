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

            // Setup variables
            DataReader reader;

            if (Utils.AskQuestion("Choose dataset size: [S/L]") == "s")
            {
                reader = new DataReader(@"../../assets/userItem.data", new[] {','});
            }
            else
            {
                reader = new DataReader(@"../../assets/movielens.data", new[] {'\t'});
            }

            PreferenceLoader loader = new PreferenceLoader(reader);

            List<UserPreference> preferences = loader.LoadPreferences();
            int[] uniqueUsers = loader.GetUniqueUsers();
            int[] uniqueArticles = loader.GetUniqueArticles();
            DataAwareAlgorithm dataAwareAlgorithm = new DataAwareAlgorithm(preferences, uniqueUsers, uniqueArticles, loader);

            string q = "What do you want to do? [getAllRatings|Sparcity|Manhattan|Cosine|Pearson|Euclidean|AdjCosine|SlopeOne|Exit]";
            string choice;
           
            // Gets user input and executes corresponding user choice
            do
            {
                choice = Utils.AskQuestion(q);

                switch(choice)
                {
                    case "getallratings":
                        Utils.PrintRatings(uniqueArticles, uniqueUsers, preferences);
                        break;
                    case "sparcity":
                        Console.WriteLine($"The sparcity is: {Utils.ComputeSparcity(preferences, uniqueUsers, uniqueArticles, loader)}");
                        break;
                    case "manhattan":
                        HandleStrategyResponse(new ManhattanStrategy());
                        break;

                    case "cosine":
                        HandleStrategyResponse(new CosineStrategy());
                        break;

                    case "pearson":
                        HandleStrategyResponse(new PearsonStrategy());
                        break;

                    case "euclidean":
                        HandleStrategyResponse(new EuclideanStrategy());
                        break;
                    case "adjcosine":
                        HandleAdjCosine(new AdjustedCosine(dataAwareAlgorithm));
                        break;
                    case "slopeone":
                        HandleSlopeOne(new SlopeOne(dataAwareAlgorithm));
                        break;
                }
            }
            while (choice != "exit");

            void HandleSlopeOne(SlopeOne slopeOne)
            {
                slopeOne.PrintDeviations();

                string uidResponse = Utils.AskQuestion(
                    $"Please enter the userID you want to predict the rating for. Should be one of: " +
                    $"{string.Join(", ", uniqueUsers)}");

                UserPreference userToRate;
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

                // For every item the user has not rated yet
                foreach (int pid in loader.getUnratedItems(userToRate).Keys)
                {
                    Console.WriteLine($"Predicted rating for item {pid} is: {slopeOne.PredictRating(userToRate, pid)}");
                }
            }

            void HandleAdjCosine(AdjustedCosine adjCosine)
            {
                string uidResponse = Utils.AskQuestion(
                    $"Please enter the userID you want to predict the rating for. Should be one of: " +
                    $"{string.Join(", ", uniqueUsers)}");

                UserPreference userToRate;
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

                // For every item the user has not rated yet
                foreach (int pid in loader.getUnratedItems(userToRate).Keys)
                {
                    Console.WriteLine($"Predicted rating for item {pid} is: {adjCosine.PredictRating(userToRate, pid)}");
                }
            }

            // Asks for a userId and executes the given strategy on that user and all other preferences
            void HandleStrategyResponse(IStrategy strategy)
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
                    PredictRating predictRating = new PredictRating(dataAwareAlgorithm);

                    predictRating.Calculate(nearestNeighbours.Calculate(userToRate, strategy).Reverse().ToDictionary(kvp => kvp.Key, kvp => kvp.Value), userToRate);
                }
                else
                { 
                    // Show all users with their respective similarity to the target user
                    foreach (UserPreference preference in preferences)
                    {
                        AlgorithmContext context = new AlgorithmContext(strategy, userToRate, preference);

                        Console.WriteLine(
                            $"The {choice} similarity between UID {userToRate.userId} and {preference.userId} is: {context.ExecuteStrategy()} ");
                    }
                }
            }
        }
    }
}
