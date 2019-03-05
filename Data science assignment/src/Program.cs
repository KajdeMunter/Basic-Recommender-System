using Data_science_assignment.src.algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ConsoleTables;
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
                        ConsoleTable table = new ConsoleTable(new ConsoleTableOptions
                        {
                            Columns = new[] {"uid"},
                            EnableCount = false,
                        });

                        IEnumerable<string> itemstrings =
                            uniqueArticles.Select(i => i.ToString());

                        foreach (string item in itemstrings)
                        {
                            table.AddColumn(new[] {"1", "2"});
                        }

                        foreach (UserPreference userpref in preferences)
                        {
                            //ConsoleTable.From(new[] {1, 2, 3, 4, 5, 6, 7}).Write();
                        }

                        table.Write();

                        break;

                    case "manhattan":
                        // todo
                        break;

                    case "cosine":
                        // todo
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
                    Console.WriteLine("That is not a valid userID, please try again.");
                    return;
                }

                foreach (UserPreference preference in preferences)
                {
                    AlgorithmContext context = new AlgorithmContext(strategy, userToRate, preference);
                    Console.WriteLine(
                        $"The {choice} similarity between UID {userToRate.userId} and {preference.userId} is: {context.ExecuteStrategy()} ");
                }
            }
        }

        /// <summary>
        /// Asks a question to the user and returns a lowercase string
        /// </summary>
        /// <param name="question">Question to ask</param>
        /// <returns>Lowercase user input</returns>
        static string AskQuestion(string question)
        {
                Console.WriteLine(question);
                return Console.ReadLine().ToLower();
        }
    }
}
