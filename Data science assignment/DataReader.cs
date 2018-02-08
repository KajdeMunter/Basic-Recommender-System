using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Data_science_assignment
{
    class DataReader
    {
        // Reads the userItem dataset and returns all unique users.
        public int[] getUsers()
        {
            var users = (from lines in File.ReadAllLines(@"../../userItem.data")
                       let part = lines.Split(',')
                       select part[0]).Distinct().ToArray();
            return Array.ConvertAll(users, int.Parse);                      
        }
        
        // Reads the userItem dataset and returns Object[]
        public List<string[]> readLines()
        {
            var line = (from lines in File.ReadAllLines(@"../../userItem.data")
                        let parts = lines.Split(',')
                        select parts).ToList();
            return line;
        }

    }
}
