using System;
using Thue_online.AI;
using ThueOnline.Game;

namespace ThueOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            int alphabetSize = 0;
            int wordSize = 0;
            while(true){
                Console.WriteLine("How many letters in the alphabet?");
                try { 
                    alphabetSize = Convert.ToInt32(Console.ReadLine()); 
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (alphabetSize > 26) Console.WriteLine("It's too much. Choose again.\n");
                else if (alphabetSize < 1) Console.WriteLine("It's too little. Choose again.\n");
                else break;
            }

            while(true){
                Console.WriteLine("How long should the word be?");
                try { 
                    wordSize = Convert.ToInt32(Console.ReadLine()); 
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (wordSize < 2) Console.WriteLine("It's too short. Choose again.\n");
                else break;
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("Your game begins!\n");
            Alphabet alphabet = new Alphabet(alphabetSize);
            //IArtificialIntelligence ai = new ArtificialIntelligenceRandnom();
            IArtificialIntelligence ai = new ArtificialIntelligenceMCTS();
            var game = new ThueGame(alphabet, wordSize, ai);
            game.Start();
            Console.WriteLine("Bye");
            Console.ReadKey();
        }
    }
}

