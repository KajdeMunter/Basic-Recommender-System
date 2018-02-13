using System;
using System.Collections.Generic;

namespace Data_science_assignment
{
    class UserPreferences
    {
        // <ArticleID, Rating>
        public Dictionary<int, float> ratings;

        public UserPreferences(Dictionary<int, float> ratings)
        {
            this.ratings = ratings;
        }

        public void addRating(int ArticleID, float Rating)
        {
            ratings.Add(ArticleID, Rating);
        }
    }
}
