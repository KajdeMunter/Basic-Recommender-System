using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Data_science_assignment
{
    class DataReader
    {
        // Reads the dataset and returns all unique users.
        public int[] getUsers(string dataset)
        {
            var users = (from lines in File.ReadAllLines(dataset)
                       let part = lines.Split(new char[] { ',', '\t' })
                       select part[0]).Distinct().ToArray();
            return Array.ConvertAll(users, int.Parse);                      
        }
        
        // Reads the  dataset
        public List<string[]> readLines(string dataset)
        {
            var line = (from lines in File.ReadAllLines(dataset)
                        let parts = lines.Split(new char[] { ',', '\t' })
                        select parts).ToList();
            return line;
        }
    }
}
