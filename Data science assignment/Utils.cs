using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_science_assignment
{
    public static class Utils
    {
        /// <summary>
        /// Asks a question to the user and returns a lowercase, white-space trimmed string
        /// </summary>
        /// <param name="question">The question to ask</param>
        /// <returns>Lowercase trimmed user input</returns>
        public static string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine().ToLower().Trim();
        }
    }
}
