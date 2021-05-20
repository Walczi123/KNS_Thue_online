using System;
using System.Collections.Generic;
using System.IO;

namespace GameTest
{
    public static class FileHandler
    {
        public static List<GameInstance> ReadFile()
        {
            string filename;
            Console.WriteLine("Please enter the input file path (default input.txt)");
            string input = Console.ReadLine();
            if (input == "")
                filename = "../../../../input.txt";
            else
                filename = input;

            List<GameInstance> results = new List<GameInstance>();

            using (StreamReader sr = File.OpenText(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split(':');
                    //alphabetLength:wordLength:word
                    results.Add(new GameInstance(split[2], Int32.Parse(split[0]), Int32.Parse(split[1])));
                }
            }

            return results;
        }

        public static void WriteFile(List<GameResult> results)
        {
            string filename;
            Console.WriteLine("Please enter the output file path (default output.txt)");
            string output = Console.ReadLine();
            if (output == "")
                filename = "../../../../output.txt";
            else
                filename = output;

            using (StreamWriter sw = File.CreateText(filename))
            {
                int count = 0;
                foreach (var instance in results)
                {
                    
                    sw.Write($"{++count}. ");
                    if (!instance.invalid)
                    {
                        sw.Write($"{instance.winner} \n {instance.gameplay} \n {instance.order}");
                    }
                    else
                        sw.Write("Invalid game attributes");
                    sw.WriteLine();
                }
            }

        }
    }

    public class GameInstance
    {
        public string inputs;
        public int alphabetLength;
        public int wordLength;

        public GameInstance()
        {

        }

        public GameInstance(string inp, int aLength, int wLength)
        {
            inputs = inp;
            alphabetLength = aLength;
            wordLength = wLength;
        }
    }

    public class GameResult
    {
        public int winner;
        public string gameplay;
        public string order;
        public bool invalid;

        public GameResult()
        {

        }

        public GameResult(int w, string g, string o)
        {
            winner = w;
            gameplay = g;
            order = o;
            invalid = false;
        }
    }
}
