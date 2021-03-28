using System;
using ThueOnline.Game;

namespace ThueOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            Alphabet alphabet = new Alphabet(3);
            int limit = 7;
            var game = new ThueGame(alphabet, limit);
            game.Start();
            Console.WriteLine("Bye");
            Console.ReadKey();
        }
    }
}

