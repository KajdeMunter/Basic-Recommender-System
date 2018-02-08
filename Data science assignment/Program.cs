using System;
using System.Collections.Generic;

namespace Data_science_assignment
{
    class Program
    {
        static void Main()
        {
            // Create Dictionary with <UserID, UserPreferences> which is not really needed but the slide told me to anyway
            Dictionary<int, UserPreferences> userpreferences = new Dictionary<int, UserPreferences>();

            // Create Dictionary with <ArticleID, Rating>
            Dictionary<int, float> userRatings = new Dictionary<int, float>();

            DataReader reader = new DataReader();

            int cnt = 0;

            // For every unique userID
            foreach(int userID in reader.getUsers())
            {
                // Create a new UserPreferences
                UserPreferences preferences = new UserPreferences(userID, userRatings);
                
                // Add userID and UserPreferences to userpreference dictionary
                userpreferences.Add(userID, preferences);

                // Read all the lines in the dataset
                List<string[]> data = reader.readLines();
                
                // For every instance of the unique UserID in all the lines of the dataset
                if(Convert.ToInt32(data[cnt][0]) == userID)
                {
                    preferences.addRating(Convert.ToInt32(data[cnt][1]), Convert.ToSingle(data[cnt][2]));
                }
                cnt++;
            }           
        }
    }
}
