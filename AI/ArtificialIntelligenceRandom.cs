using System;
using ThueOnline.Game;

namespace Thue_online.AI
{
    public class ArtificialIntelligenceRandnom : IArtificialIntelligence
    {
        Random random = new Random();
        public ArtificialIntelligenceRandnom()
        { }
        public int MakeMove(string initialState, Alphabet alphabet, int endCondition, int numberOfIteration)
        {
            return this.random.Next(0, initialState.Length);
        }

    }
}
