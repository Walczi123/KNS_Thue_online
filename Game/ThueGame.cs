using System;
using Thue_online.AI;
using Thue_online.Extensions;

namespace ThueOnline.Game
{
    public class ThueGame
    {
        #region Attributes

        Winner winner = Winner.Player;
        string word = "";
        Alphabet alphabet;
        int limit;
        int currentPosition = 0;
        string inputSymbol = "_";

        IArtificialIntelligence AI;

        #endregion

        #region Public Methods
        public ThueGame(Alphabet alphabet, int limit, IArtificialIntelligence artificialIntelligence)
        {
            this.AI = artificialIntelligence;
            this.alphabet = alphabet;
            this.limit = limit;
            this.alphabet.Print();
            Console.WriteLine($"The limit is set to {this.limit}");
        }

        public void Start()
        {
            while (!this.IsRepetiton(this.word) &&
                this.word.Length < this.limit)
            {
                ComputerMove();
                this.word = InsertSymbol(this.currentPosition, this.inputSymbol);
                PrintWord();
                PlayerMove();
                PrintWord();
            }

            if (this.winner.Equals(Winner.Player))
            {
                Console.WriteLine("You win, Congratulations!");
            }
            else
            {
                Console.WriteLine("Computer win, try again");
            }
        }

        public string StartTest(string input)
        {
            string order = "";
            int moveCounter = 0;
            int inputCounter = 0;
            while (!this.IsRepetiton(this.word) &&
                this.word.Length < this.limit)
            {
                moveCounter++;
                ComputerMove();
                this.word = InsertSymbol(this.currentPosition, this.inputSymbol);
                order = order.Insert((1 + (int)Math.Floor(Math.Log10(this.currentPosition) + 1)) * this.currentPosition, $"{moveCounter},");
                var letter = input[inputCounter++];
                letter = Char.ToUpper(letter);
                this.word = ReplaceSymbol(this.currentPosition, letter);
            }
            return order;
        }

        public Winner GetWinner()
        {
            return this.winner;
        }

        public string GetWord()
        {
            return this.word;
        }

        #endregion

        #region Private Methods

        public bool IsRepetiton(string word)
        {
            if (word.HasRepetition())
            {
                var (firstPart, secondPart) = word.FindRepetition();
                Console.WriteLine("Repetition found");
                Console.WriteLine($"First: {firstPart}");
                Console.WriteLine($"Second: {secondPart}");
                this.winner = Winner.Computer;
                return true;
            }
            return false;
        }

        private void PlayerMove()
        {
            char letter = '~';
            Console.WriteLine("Enter letter:");
            string line = Console.ReadLine();
            if (line.Length > 0)
            {
                line = line.ToUpper();
                letter = line.ToCharArray()[0];
            }
            while (!this.alphabet.Letters.Contains(letter))
            {
                Console.WriteLine("Your letter is not in alphabet.");
                this.alphabet.Print();
                Console.WriteLine("Enter new letter:");
                line = Console.ReadLine();
                if (line.Length > 0)
                {
                    line = line.ToUpper();
                    letter = line.ToCharArray()[0];
                }
            }
            this.word = ReplaceSymbol(this.currentPosition, letter);
        }

        private void ComputerMove()
        {
            this.currentPosition = this.AI.MakeMove(word, alphabet, this.limit);
        }

        private string InsertSymbol(int position, string symbol)
        {
            return this.word.Insert(position, symbol);
        }

        private string ReplaceSymbol(int position, char symbol)
        {
            char[] chars = this.word.ToCharArray();
            chars[position] = symbol;
            return new string(chars);
        }

        private void PrintWord()
        {
            Console.WriteLine(this.word);
        }

        #endregion
    }
}
