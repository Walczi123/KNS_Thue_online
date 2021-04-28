using ThueOnline.Game;

namespace Thue_online.AI
{
    public interface IArtificialIntelligence
    {
        int MakeMove(string initialState, Alphabet alphabet, int endCondition, int numberOfIteration = 1000);
    }
}
