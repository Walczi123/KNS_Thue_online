using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ThueOnline.Game;

namespace GameTest
{
    public static class InputGenerator
    {
        public static List<GameInstance> GenerateInput()
        {
            string filename;
            Console.WriteLine("Please enter the generated input file path (default input.txt)");
            string input = Console.ReadLine();
            if (input == "")
                filename = "../../../../input.txt";
            else
                filename = input;

            var result = new List<GameInstance>();

            using (StreamWriter sw = File.CreateText(filename))
            {
                var end = "n";
                while (end != "y")
                {
                    Console.WriteLine("What should the size of the alphabet be?");
                    var alph = Int32.Parse(Console.ReadLine());
                    var alphabet = new Alphabet(alph);

                    Console.WriteLine("What should the maximum length of the word be?");
                    var length = Int32.Parse(Console.ReadLine());

                    var hand = "n";
                    Console.WriteLine("Would you like to create the instance by hand? [y|n]");
                    hand = Console.ReadLine();

                    if (hand == "y")
                    {
                        bool invalid = false;
                        string seq = String.Empty;
                        while (!invalid)
                        {
                            Console.WriteLine("Please type in the sequence of characters");
                            seq = Console.ReadLine();
                            if(seq.Length < length)
                            {
                                Console.WriteLine("Invalid sequence! Too short");
                                continue;
                            }
                            invalid = seq.CheckAlphabet(alphabet);
                            if (!invalid)
                                Console.WriteLine("Invalid sequence! Wrong characters");
                        }

                        var res = new GameInstance(seq, alph, length);
                        result.Add(res);
                        sw.WriteLine($"{alph}:{length}:{seq}");

                        Console.WriteLine("Would you like to repeat this instance? [y|n]");
                        if(Console.ReadLine() == "y")
                        {
                            Console.WriteLine("How many times?");
                            var times = Int32.Parse(Console.ReadLine());
                            for(int i = 0; i < times; i++)
                            {
                                result.Add(res);
                                sw.WriteLine($"{alph}:{length}:{seq}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("A sequence will be generated randomly. How many sequences should be generated?");
                        var number = Int32.Parse(Console.ReadLine());
                        if(number > 1)
                        {
                            for (int i = 0; i < number; i++)
                            {
                                string seq = string.Empty;
                                for (int j = 0; j < length; j++)
                                    seq += alphabet.GetRandomLetter();

                                var resR = new GameInstance(seq, alph, length);
                                result.Add(resR);
                                sw.WriteLine($"{alph}:{length}:{seq}");
                            }
                        }
                        else if (number == 1)
                        {
                            string seq = string.Empty;
                            for (int j = 0; j < length; j++)
                                seq += alphabet.GetRandomLetter();
                            var res = new GameInstance(seq, alph, length);

                            Console.WriteLine("Would you like to repeat this instance? [y|n]");
                            if (Console.ReadLine() == "y")
                            {
                                Console.WriteLine("How many times?");
                                var times = Int32.Parse(Console.ReadLine());
                                for (int i = 0; i < times; i++)
                                {
                                    result.Add(res);
                                    sw.WriteLine($"{alph}:{length}:{seq}");
                                }
                            }
                        }
                    }

                    Console.WriteLine("Would you like to finish generating? [y|n]");
                    end = Console.ReadLine();
                }
            }
            return result;
        }
    }
}
