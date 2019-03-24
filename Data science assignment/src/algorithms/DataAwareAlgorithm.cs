using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    class DataAwareAlgorithm
    {
        public readonly List<UserPreference> preferences;
        public readonly int[] uniqueUsers;
        public readonly int[] uniqueArticles;
        public readonly PreferenceLoader loader;

        public DataAwareAlgorithm(List<UserPreference> preferences, int[] uniqueUsers, int[] uniqueArticles, PreferenceLoader loader)
        {
            this.preferences = preferences;
            this.uniqueUsers = uniqueUsers;
            this.uniqueArticles = uniqueArticles;
            this.loader = loader;
        }
    }
}
