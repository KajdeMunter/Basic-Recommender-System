using System.Collections.Generic;

namespace Data_science_assignment
{
    class UserPreferences
    {
        // <ArticleID, Rating>
        public Dictionary<int, float> ratings;
        public int userID;

        public UserPreferences(int userID, Dictionary<int, float> ratings)
        {
            this.userID = userID;
            this.ratings = ratings;
        }

        public void addRating(int ArticleID, float Rating)
        {
            ratings.Add(ArticleID, Rating);
        }
    }
}
