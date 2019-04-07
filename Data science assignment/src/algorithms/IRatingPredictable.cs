using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    interface IRatingPredictable
    {
        /// <summary>
        /// Takes a target user and item and predicts a rating using slope one
        /// </summary>
        /// <param name="target"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        double PredictRating(UserPreference target, int i);
    }
}
