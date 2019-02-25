using Data_science_assignment.src.algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ConsoleTables;

namespace Data_science_assignment
{
    class Program
    {
        static void Main()
        {
            DataReader reader = new DataReader(@"../../assets/userItem.data", new[] {',', '\t'});

            List<UserPreference> preferences = new List<UserPreference>();
            int[] uniqueUsers = reader.getUnique(0);

            // For every unique userID
            foreach (int userID in uniqueUsers)
            {

                // Create Dictionary with <ArticleID, Rating>
                SortedDictionary<int, float> userRatings = new SortedDictionary<int, float>();

                // Add a new userPreference to the preference list
                UserPreference preference = new UserPreference(userID, userRatings);

                // For every instance of the unique UserID in all the lines of the dataset
                foreach (string[] line in reader.readLines())
                {
                    if (Convert.ToInt32(line[0]) == userID)
                    {
                        preference.addRating(Convert.ToInt32(line[1]),
                            Single.Parse(line[2], CultureInfo.InvariantCulture));
                    }
                }

                // Add the users preference to the list of all preferences and users
                preferences.Add(preference);
            }

            // Add rating 0 to every article a user has not rated yet
            foreach (int articleID in reader.getUnique(1))
            {
                foreach (UserPreference user in preferences)
                {
                    if (!user.ratings.Keys.Contains(articleID))
                    {
                        user.addRating(articleID, 0);
                    }
                }
            }

            string q = "What do you want to do? [getAllRatings, Manhattan, Cosine, Pearson, Euclidean, Exit]";
            string choice;
           
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
                            reader.getUnique(1).Select(i => i.ToString());

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
                                $"The Euclidiean distance between UID {userToRate.userId} and {preference.userId} is: ");
                            context.ContextInterface();
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
