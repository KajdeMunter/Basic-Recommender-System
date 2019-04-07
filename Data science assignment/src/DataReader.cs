using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Data_science_assignment
{
    /// <summary>
    /// Class used for reading datasets in different ways
    /// </summary>
    public class DataReader
    {
        private readonly string _dataset;
        private readonly char[] _delimiters;

        /// <summary>
        /// DataReader Constructor
        /// </summary>
        /// <param name="Dataset">Relative path to the dataset file</param>
        /// <param name="delimiters">char[] with column and row delimiter</param>
        public DataReader(string Dataset, char[] delimiters)
        {
            _dataset = Dataset;
            _delimiters = delimiters;
        }

        /// <summary>
        /// Reads the dataset and returns distinct values corresponding to the given index
        /// </summary>
        /// <param name="index">Index to return distinct values for</param>
        /// <returns>Array containing unique values</returns>
        public int[] getUnique(int index)
        {
            var users = (from lines in File.ReadAllLines(_dataset)
                         let part = lines.Split(_delimiters)
                         orderby Int32.Parse(part[index])
                         select part[index]).Distinct().ToArray();
            return Array.ConvertAll(users, int.Parse);                      
        }
        
        /// <summary>
        /// Reads the dataset according to row and column delimiters
        /// </summary>
        /// <returns></returns>
        public List<string[]> readLines()
        {
            var line = (from lines in File.ReadAllLines(_dataset)
                        let parts = lines.Split(_delimiters)
                        select parts).ToList();
            return line;
        }
    }
}
