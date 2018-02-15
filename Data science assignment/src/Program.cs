using Data_science_assignment.src.algorithms;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Data_science_assignment
{
    class Program
    {
        static void Main()
        {
            // Create Dictionary with <UserID, UserPreferences>
            Dictionary<int, UserPreferences> userpreferences = new Dictionary<int, UserPreferences>();

            DataReader reader = new DataReader();

            int cnt = 0;

            // For every unique userID
            foreach(int userID in reader.getUsers())
            {
                // Create Dictionary with <ArticleID, Rating>
                Dictionary<int, float> userRatings = new Dictionary<int, float>();

                // Create a new UserPreferences
                UserPreferences preferences = new UserPreferences(userRatings);
                
                // Add userID and UserPreferences to userpreference dictionary
                userpreferences.Add(userID, preferences);

                // For every instance of the unique UserID in all the lines of the dataset
                foreach (string[] line in reader.readLines()) {
                    if (Convert.ToInt32(line[0]) == userID)
                    {
                        preferences.addRating(Convert.ToInt32(line[1]), Single.Parse(line[2], CultureInfo.InvariantCulture));
                    }
                }

                cnt++;
            }

            // For validation
            foreach (KeyValuePair<int, UserPreferences> kvp in userpreferences)
            {

                // Uncomment to print all user ratings
                /*
                foreach (KeyValuePair<int, float> ratings in kvp.Value.ratings)
                {
                    Console.WriteLine(string.Format("UserID {0} rated {1} a {2}", kvp.Key, ratings.Key, ratings.Value));
                }
                */


                // Uncomment to print the MANHATTAN distance between every user
                /*
                foreach (KeyValuePair<int, UserPreferences> kvp2 in userpreferences)
                {
                    Context context = new Context(new ManhattanStrategy(), kvp.Value, kvp2.Value);
                    Console.WriteLine(string.Format("The Manhattan distance between UID {0} and {1} is: ", kvp.Key, kvp2.Key));
                    context.ContextInterface();
                }
                */

                
                // Uncomment to print the COSINE distance between every user
                /*
                foreach (KeyValuePair<int, UserPreferences> kvp2 in userpreferences)
                {
                    Context context = new Context(new CosineStrategy(), kvp.Value, kvp2.Value);
                    Console.WriteLine(string.Format("The Cosine distance between UID {0} and {1} is: ", kvp.Key, kvp2.Key));
                    context.ContextInterface();
                }
                */

                
                // Uncomment to print the MANHATTAN distance between every user
                /*
                foreach (KeyValuePair<int, UserPreferences> kvp2 in userpreferences)
                {
                    Context context = new Context(new PearsonStrategy(), kvp.Value, kvp2.Value);
                    Console.WriteLine(string.Format("The Pearson distance between UID {0} and {1} is: ", kvp.Key, kvp2.Key));
                    context.ContextInterface();
                }
                */

                
                // Uncomment to print the EUCLIDEAN distance between every user
                /*
                foreach (KeyValuePair<int, UserPreferences> kvp2 in userpreferences)
                {
                    Context context = new Context(new EuclideanStrategy(), kvp.Value, kvp2.Value);
                    Console.WriteLine(string.Format("The Euclidiean distance between UID {0} and {1} is: ", kvp.Key, kvp2.Key));
                    context.ContextInterface();
                }
                */
            }
        }
    }
}
