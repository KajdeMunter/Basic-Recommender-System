using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src
{
    class PreferenceLoader
    {
        private readonly DataReader _reader;

        public PreferenceLoader(DataReader reader)
        {
            this._reader = reader;
        }

        /// <summary>
        /// Returns all unique users
        /// </summary>
        /// <returns></returns>
        public int[] GetUniqueUsers()
        {
            return _reader.getUnique(0);
        }

        /// <summary>
        /// Returns all unique articles
        /// </summary>
        /// <returns></returns>
        public int[] GetUniqueArticles()
        {
            return _reader.getUnique(1);
        }
        
        /// <summary>
        /// Loads all users and their ratings into a List of UserPreferences.
        /// </summary>
        /// <returns>A list of all users and their preferences</returns>
        public List<UserPreference> LoadPreferences()
        {
            List<UserPreference> preferences = new List<UserPreference>();

            // For every unique userID
            foreach (int userID in GetUniqueUsers())
            {
                // Create Dictionary with <ArticleID, Rating>
                SortedDictionary<int, float> userRatings = new SortedDictionary<int, float>();

                // Add a new userPreference to the preference list
                UserPreference preference = new UserPreference(userID, userRatings);

                // For every instance of the unique UserID in all the lines of the dataset
                foreach (string[] line in _reader.readLines())
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
            foreach (int articleID in _reader.getUnique(1))
            {
                foreach (UserPreference user in preferences)
                {
                    if (!user.ratings.Keys.Contains(articleID))
                    {
                        user.addRating(articleID, 0);
                    }
                }
            }

            return preferences;
        }

        /// <summary>
        /// Takes a userpreference and removes all products that are not rated
        /// </summary>
        /// <param name="preference"></param>
        /// <returns></returns>
        public Dictionary<int, float> getRatingsWithoutZero(UserPreference preference)
        {
            return preference.ratings.Where(kvp => kvp.Value > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
