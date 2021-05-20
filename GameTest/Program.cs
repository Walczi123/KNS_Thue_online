using System;
using System.Collections.Generic;
using Thue_online.AI;
using ThueOnline.Game;

namespace GameTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var instances = FileHandler.ReadFile();
            var results = new List<GameResult>();

            int count = 0;
            foreach(var game in instances)
            {
                count++;
                Alphabet alphabet = new Alphabet(game.alphabetLength);
                if (!game.inputs.CheckAlphabet(alphabet))
                {
                    results.Add(new GameResult() { invalid = true });
                    Console.WriteLine($"{count}. Invalid game attributes");
                    continue;
                }
                if(game.inputs.Length < game.wordLength)
                {
                    results.Add(new GameResult() { invalid = true });
                    Console.WriteLine($"{count}. Invalid game attributes");
                    continue;
                }
                IArtificialIntelligence ai = new ArtificialIntelligenceMCTS();
                var thueGame = new ThueGame(alphabet, game.wordLength, ai);
                var order = thueGame.StartTest(game.inputs);
                Console.WriteLine($"{count}. Player {thueGame.GetWinner()} won");
                results.Add(new GameResult((int)thueGame.GetWinner(), thueGame.GetWord(), order));
            }

            FileHandler.WriteFile(results);
        }
    }
}
