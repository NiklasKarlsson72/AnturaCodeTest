using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
// Antura programming test. Trying to optimize the code and make it safe to execute
namespace AnturaTest
{
    class Program
    {
        public static string[] args;
        Program(string[] args)
        {
            Program.args = args;
        }
        private void Run()
        {
            if (args.Length == 1)
            {
                string filename = args[0];
                if (File.Exists(filename))
                {
                    var f = File.Open(filename, FileMode.Open);
                    StreamReader file = new StreamReader(f);

                    string name = GetName(); // Get the filename excluding path and file extension

                    int counter = CountOccurances(file, name); // Count the occurances of the filename within the file 

                    f.Close();

                    Console.WriteLine($"Found {counter} occurances of the word '{name}' in the file '{filename}'.");              
                }
                else
                {
                    Console.WriteLine($"File '{filename}' not found.");
                }
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("The application only supports one filename at the time.");
            }
            else
            {
                Console.WriteLine("Please write a filename to search.");
            }
        }

        private static int CountOccurances(StreamReader file, string name)
        {
            string line;
            int counter = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (line != null && line.Contains(name))
                {
                    foreach (Match m in Regex.Matches(line, name))
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        private static string GetName()
        {
            string name;
            int pos2 = args[0].LastIndexOf('.');
            int pos1 = args[0].LastIndexOf('\\');
            if (pos1 > 0)
                pos1 = pos1 + 1;

            if (pos2 > 0 && pos1 > 0)
                name = args[0].Substring(pos1, pos2 - pos1);
            else if (pos2 > 0)
                name = args[0].Substring(0, pos2);
            else
                name = args[0];
            return name;
        }

        static void Main(string[] args)
        {
            Program program = new Program(args);
            program.Run();
        }
    }
}