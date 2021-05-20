using System;
using System.Collections.Generic;

namespace ThueOnline.Game
{
    public enum Winner
    {
        Player,
        Computer
    }

    public class Alphabet
    {
        public Alphabet(int size)
        {
            this.Letters = new List<char>();
            for (int i = 0; i < size; i++)
            {
                this.Letters.Add((char)('A' + i));
            }
        }

        public List<char> Letters
        {
            get;
        }

        public void Print()
        {
            Console.WriteLine("Alphabet:");
            foreach (var letter in Letters)
            {
                Console.Write($"'{letter}' ");
            }
            Console.WriteLine();
        }

        public char GetRandomLetter()
        {
            var rand = new Random();
            return Letters[rand.Next() % Letters.Count];

        }
    }
}
