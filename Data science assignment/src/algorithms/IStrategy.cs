using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment.src.algorithms
{
    /// <summary>
    /// Interface for the strategy pattern. A behavioral design pattern that enables selecting an algorithm at runtime.
    /// </summary>
    interface IStrategy
    {
        /// <summary>
        /// Takes 2 dicts containing a product id and rating and executes the strategy
        /// </summary>
        /// <param name="pointP"></param>
        /// <param name="pointQ"></param>
        /// <returns></returns>
        Double Execute(SortedDictionary<int, float> pointP, SortedDictionary<int, float> pointQ);
    }
}
