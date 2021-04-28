using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ThueOnline.Game
{

    public class Move
    {
        public int place;
        public char letter;

        public Move(int place, char letter)
        {
            this.place = place;
            this.letter = letter;
        }
    }
    public class Node
    {
        public int wins;
        public int visits;
        public Node parent;
        public string state;
        public List<Move> untriedMoves = new List<Move>();
        public List<Node> childNodes;

        public Node(string state, Node parent = null) 
        {
            this.wins = 0;
            this.visits = 0;
            this.parent = parent;
            this.state = state;
            this.childNodes = new List<Node>();
        }

        public float GetUtcScore()
        {
            double c = Math.Sqrt(2);
            return (float)(this.wins / this.visits + c * Math.Sqrt(Math.Log(this.parent.visits) / this.visits));
        }


        public void AddChild(string state)
        {
            this.untriedMoves.Skip(1);
            this.childNodes.Add(new Node(this, state));
        }
        
    }
    public class AI
    {
        Random random = new Random();
        public int lettersAmount;
        public string state;
        
        public AI(int lettersAmount, string state, bool root = false) 
        {
            this.lettersAmount = lettersAmount;
            this.state = state;
        }
       
        public Node Selection(Node n)
        {
            List<Node> bestChildren = new List<Node>();
            float bestScore = Single.NegativeInfinity;
            float score;
            
            for(int i = 0; i< n.childNodes.Count(); i++) 
            {
                score = n.GetUtcScore();
                if (score > bestScore)
                {
                    bestScore = score;
                    bestChildren = new List<Node> { n.childNodes[i] };
                }
                else if (score == bestScore)
                {
                    bestChildren.Add(n.childNodes[i]);
                }
            }
            return bestChildren[random.Next(bestChildren.Count)];
        }
       
        string CreateNewState(Move move, string oldState)
        {
            return oldState.Insert(move.place, move.letter.ToString());
        }

        private List<Move> GetAllPosibleMoves()
        {
            List<Move> l = new List<Move>();
            for (int i=0; i<this.state.Count(); i++)
            {
                for (int j = 0; j < this.lettersAmount; j++)
                    l.Append(new Move(i, Convert.ToChar(65 + j)));
            }
            return l;
        }

        public void MakeMove(Move move)
        {
            this.state = CreateNewState(move, this.state);
        }

        public void Expansion(Node n)
        {
            string oldState = n.state;
            if(n.untriedMoves.Any())
            {
                Move move = n.untriedMoves[0];
                string state = CreateNewState(move, oldState);
                n.AddChild(state);
            }
        }

        public void Playout()
        {
            while(true)
            {
                List<Move> allPosibleMoves = this.GetAllPosibleMoves();
                if(allPosibleMoves.Any())
                {
                    this.MakeMove(allPosibleMoves[random.Next(allPosibleMoves.Count)]);
                    continue;
                }
                break;
            }
        }

        public void Backpropagation(int result, Node n)
        {
            n.wins += result;
            n.visits += 1;
            if(n.parent != null)
                    Backpropagation(result, n.parent);

        }

        public GetMove(Node n, string state)
        {
            Node childNode = this.Selection(n);
            this.Expansion(childNode);
            this.Playout();
            ThueGame thueGame = new ThueGame;
            int result = IsRepetiton(string word)
            this.Backpropagation()

        }

}
