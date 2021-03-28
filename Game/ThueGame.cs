using System;

namespace ThueOnline.Game
{
    public class ThueGame
    {
        #region Attributes

        Winner winner = Winner.Player;
        string word = "";
        Alphabet alphabet;
        int limit;
        int currentPosiotion = 0;
        string inputSymbol = "_";
        Random random = new Random();

        #endregion

        #region Public Methods
        public ThueGame(Alphabet alphabet, int limit)
        {
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
                this.word = InsertSymbol(this.currentPosiotion, this.inputSymbol);
                PrintWord();
                PlayerMove();
                PrintWord();
            }

            if (this.winner.Equals(Winner.Player))
            {
                Console.WriteLine("You win, Congratulation");
            }
            else
            {
                Console.WriteLine("Computer win, try again");
            }
        }

        #endregion

        #region Private Methods

        private bool IsRepetiton(string word)
        {
            for(int i=1; i<=word.Length/2; i++)
            {   
                for(int j = 0; j <= word.Length - (2*i); j++)
                {
                    var firstPart = word.Substring(j, i);
                    var secondPart = word.Substring(j+i, i);
                    if (String.Equals(firstPart, secondPart))
                    {
                        Console.WriteLine("Repetition found");
                        Console.WriteLine($"Frist: {firstPart}");
                        Console.WriteLine($"Second: {secondPart}");
                        this.winner = Winner.Computer;
                        return true;
                    }
                }
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
            this.word = ReplaceSymbol(this.currentPosiotion, letter);
        }

        private void ComputerMove()
        {
            int length = this.word.Length;
            this.currentPosiotion = this.random.Next(0, length);
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
