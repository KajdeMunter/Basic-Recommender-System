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
    class Program
    {
        static void Main()
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
                choice = askQuestion(q);

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
                        // todo
                        break;

                    case "euclidean":
                        int uid = Convert.ToInt32(askQuestion($"Please enter the userID you want to compare other users to. Should be one of: " +
                                                              $"{string.Join(", ", uniqueUsers)}"));
                        UserPreference userToRate;

                        try
                        {
                            userToRate = preferences[uid - 1];
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("That is not a valid userID, please try again.");
                            break;
                        }

                        foreach (UserPreference preference in preferences)
                        {
                            Context context = new Context(new EuclideanStrategy(), userToRate, preference);
                            Console.WriteLine(
                                $"The Euclidiean similarity between UID {userToRate.userId} and {preference.userId} is: {1/(1+context.ExecuteStrategy())} ");
                        }

                        break;
                }
            }
            while (choice != "exit");
        }

        static string askQuestion(string question)
        {
                Console.WriteLine(question);
                return Console.ReadLine().ToLower();
        }
    }
}
