using System;
using Thue_online.AI;
using ThueOnline.Game;

namespace ThueOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            Alphabet alphabet = new Alphabet(3);
            int limit = 7;
            //IArtificialIntelligence ai = new ArtificialIntelligenceRandnom();
            IArtificialIntelligence ai = new ArtificialIntelligenceMCTS();
            var game = new ThueGame(alphabet, limit, ai);
            game.Start();
            Console.WriteLine("Bye");
            Console.ReadKey();
        }
    }
}

