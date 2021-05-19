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
                alphabetSize = Convert.ToInt32(Console.ReadLine());
                if (alphabetSize <1) Console.WriteLine("It's too little. Choose again.");
                else break;
            }

            while(true){
                Console.WriteLine("How long the word has to be?");
                wordSize = Convert.ToInt32(Console.ReadLine());
                if (wordSize <2) Console.WriteLine("It's too little. Choose again.");
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

