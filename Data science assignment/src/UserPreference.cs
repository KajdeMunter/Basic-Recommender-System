using System;
using System.Collections.Generic;

namespace Data_science_assignment
{
    class UserPreference
    {
        public int userId { get; }

        // <ArticleID, Rating>
        public SortedDictionary<int, float> ratings { get; }

        public UserPreference(int userId, SortedDictionary<int, float> Ratings)
        {
            this.userId = userId;
            this.ratings = Ratings;
        }

        public void addRating(int ArticleID, float Rating)
        {
            try
            {
                ratings.Add(ArticleID, Rating);
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"An element with Key \"{ArticleID}\" already exists.");
            }
        }
    }
}
