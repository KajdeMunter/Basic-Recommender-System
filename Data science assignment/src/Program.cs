using System;
using System.Collections.Generic;

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
                        preferences.addRating(Convert.ToInt32(line[1]), Convert.ToSingle(line[2]));
                    }
                }

                cnt++;
            }

            // For validation
            foreach (KeyValuePair<int, UserPreferences> kvp in userpreferences)
            {
                foreach (KeyValuePair<int, float> ratings in kvp.Value.ratings)
                {
                    Console.WriteLine(string.Format("UserID {0} rated {1} a {2}", kvp.Key, ratings.Key, ratings.Value));
                }
            }
        }
    }
}
