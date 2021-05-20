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
                    bool alphCheck = false;
                    int alph = 0;
                    Alphabet alphabet = new Alphabet(alph);
                    while (!alphCheck)
                    {
                        Console.WriteLine("What should the size of the alphabet be?");
                        alphCheck = Int32.TryParse(Console.ReadLine(), out alph);
                        if (alphCheck)
                            alphabet = new Alphabet(alph);
                        else
                            Console.WriteLine("Incorrect input - alphabet");
                    }

                    bool lengthCheck = false;
                    int length = 0;
                    while (!lengthCheck)
                    {
                        Console.WriteLine("What should the maximum length of the word be?");
                        lengthCheck = Int32.TryParse(Console.ReadLine(), out length);
                        if (!lengthCheck)
                            Console.WriteLine("Incorrect input - length");
                    }

                    var hand = "n";
                    while (true)
                    {
                        Console.WriteLine("Would you like to create the instance by hand? [y|n]");
                        hand = Console.ReadLine();
                        if (hand == "n" || hand == "y")
                            break;
                        else
                        {
                            Console.WriteLine("Incorrect input - instance by hand");
                            continue;
                        }
                    }

                    if (hand == "y")
                    {
                        bool invalid = false;
                        string seq = String.Empty;
                        while (!invalid)
                        {
                            Console.WriteLine("Please type in the sequence of characters");
                            seq = Console.ReadLine();
                            if (seq.Length < length)
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

                        while (true)
                        {
                            Console.WriteLine("Would you like to repeat this instance? [y|n]");
                            var repeat = Console.ReadLine();
                            if (repeat == "y")
                            {
                                bool timesCheck = false;
                                while (!timesCheck)
                                {
                                    int times = 0;
                                    Console.WriteLine("How many times?");
                                    timesCheck = Int32.TryParse(Console.ReadLine(), out times);
                                    if (timesCheck)
                                    {
                                        for (int i = 0; i < times; i++)
                                        {
                                            result.Add(res);
                                            sw.WriteLine($"{alph}:{length}:{seq}");
                                        }
                                    }
                                    else
                                        Console.WriteLine("Incorrect input - instance repeat number");
                                }
                                break;
                            }
                            else if (repeat == "n")
                                break;
                            else if (repeat != "y" && repeat != "n")
                            {
                                Console.WriteLine("Incorrect input - instance repeat");
                                continue;
                            }

                        }
                    }
                    else
                    {
                        bool sequenceCheck = false;
                        while(!sequenceCheck)
                        {
                            int number = 0;
                            Console.WriteLine("A sequence will be generated randomly. How many sequences should be generated?");
                            sequenceCheck = Int32.TryParse(Console.ReadLine(), out number);
                            if (sequenceCheck)
                            {
                                if (number > 1)
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

                                    while(true)
                                    {
                                        Console.WriteLine("Would you like to repeat this instance? [y|n]");
                                        var repeat = Console.ReadLine();
                                        if (repeat == "y")
                                        {
                                            bool timesCheck = false;
                                            while(!timesCheck)
                                            {
                                                int times = 0;
                                                Console.WriteLine("How many times?");
                                                timesCheck = Int32.TryParse(Console.ReadLine(), out times);
                                                if (timesCheck)
                                                {
                                                    for (int i = 0; i < times; i++)
                                                    {
                                                        result.Add(res);
                                                        sw.WriteLine($"{alph}:{length}:{seq}");
                                                    }
                                                }
                                                else
                                                    Console.WriteLine("Incorrect input - instance repeat number");
                                            }
                                            break;
                                        }
                                        else if (repeat == "n")
                                            break;
                                        else
                                        {
                                            Console.WriteLine("Incorrect input - instance repeat");
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                                Console.WriteLine("Incorrect input - number of sequences");
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Would you like to finish generating? [y|n]");
                        end = Console.ReadLine();
                        if (end == "y" || end == "n")
                            break;
                        else
                        {
                            Console.WriteLine("Incorrect input - generator finish");
                            continue;
                        }
                    }
                }
            }
            return result;
        }
    }
}
