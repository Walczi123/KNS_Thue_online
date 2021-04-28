using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thue_online.Extensions;
using ThueOnline.Game;

namespace Thue_online.AI
{
    public struct Move
    {
        public int position;
        public char symbol;

        public Move(int position, char symbol)
        {
            this.position = position;
            this.symbol = symbol;
        }
    }

    public class Node
    {
        public int wins;
        public int visits;
        public Node parent;
        public string state;
        public List<Move> untriedMoves;
        public List<Node> childNodes;
        Move move;
        char inputSymbol = '_';

        public Node(string state, Move move, Alphabet alphabet, int endCondition, Node parent = null)
        {
            this.wins = 0;
            this.visits = 0;
            this.parent = parent;
            this.state = state;
            this.move = move;
            this.untriedMoves = this.GetAllPosibleMoves(alphabet, endCondition);
            this.childNodes = new List<Node>();      
        }

        private float GetUctScore()
        {
            double c = Math.Sqrt(2);
            return (float)(this.wins / this.visits + c * Math.Sqrt(Math.Log(this.parent.visits) / this.visits));
        }

        public Node SelectUctChild()
        {
            List<Node> bestChildren = new List<Node>();
            Random random = new Random();
            float bestScore = Single.NegativeInfinity;
            float score;
            Node node;

            for (int i = 0; i < this.childNodes.Count(); i++)
            {
                node = this.childNodes[i];
                score = node.GetUctScore();
                if (score > bestScore)
                {
                    bestScore = score;
                    bestChildren = new List<Node> { node };
                }
                else if (score == bestScore)
                {
                    bestChildren.Add(node);
                }
            }
            return bestChildren[random.Next(bestChildren.Count)];
        }

        public Node AddChild(string state, Move move, Alphabet alphabet, int endCondition)
        {
            this.untriedMoves.Skip(1);
            var newNode = new Node(state, move, alphabet, endCondition, this);
            this.childNodes.Add(newNode);
            return newNode;
        }

        public List<Move> GetAllPosibleMoves(Alphabet alphabet, int endCondition)
        {
            List<Move> l = new List<Move>();
            if (this.state.Length >= endCondition || this.state.HasRepetition())
                return l;

            if (this.IsComputerMove())
            {
                for (int i = 0; i < this.state.Count() + 1; i++)
                {
                    l.Add(new Move(i, inputSymbol));
                }
            }
            else
            {
                int i = this.state.IndexOf(inputSymbol);
                foreach (var letter in alphabet.Letters)
                {
                    l.Add(new Move(i, letter));
                }
            }
            return l;
        }

        public Node Clone(Alphabet alphabet, int endCondition)
        {
            return new Node(this.state, this.move, alphabet, endCondition, parent);
        }

        public bool IsComputerMove()
        {
            if (this.move.symbol != this.inputSymbol)
                return true;
            return false;
        }

        public void Backpropagation(int result)
        {
            this.wins += result;
            this.visits += 1;
            if (this.parent != null)
                this.parent.Backpropagation(-result);
        }

        public Move GetMove()
        {
            return this.move;
        }
    }

    public class ArtificialIntelligenceMCTS : IArtificialIntelligence
    {
        char inputSymbol = '_';
        public ArtificialIntelligenceMCTS() { }
        public string StateAfterMove(string state, Move move)
        {
            if(move.symbol == inputSymbol)            
                return state.Insert(move.position, move.symbol.ToString());
            return state.Replace(inputSymbol, move.symbol);
        }

        int GetResult(bool hasRepetition, bool isComputerMove)
        {
            if (hasRepetition && isComputerMove)
                return 1;
            return 0;
        }

        public int MakeMove(string initialState, Alphabet alphabet, int endCondition, int numberOfIteration)
        {
            var rootnode = new Node(initialState, new Move(-1, '@'), alphabet, endCondition);
            Random random = new Random();
            string iterationState;
            Node node;
            for (int i = 0; i < numberOfIteration; i++)
            {
                node = rootnode;
                iterationState = node.state;
                #region Selection
                while (node.untriedMoves.Count == 0 && node.childNodes.Count != 0)
                {
                    node = node.SelectUctChild();
                }
                #endregion
                #region Expansion         
                if (node.untriedMoves.Any())
                {
                    Move move = node.untriedMoves[random.Next(node.untriedMoves.Count)];
                    iterationState = this.StateAfterMove(iterationState, move);
                    node = node.AddChild(iterationState, move, alphabet, endCondition);
                }
                #endregion
                #region Playout
                var playoutNode = node.Clone(alphabet, endCondition);
                while (true)
                {
                    List<Move> allPosibleMoves = playoutNode.GetAllPosibleMoves(alphabet, endCondition);
                    if (allPosibleMoves.Any())
                    {
                        var move = allPosibleMoves[random.Next(allPosibleMoves.Count)];
                        iterationState = this.StateAfterMove(iterationState, move);
                        playoutNode = new Node(iterationState, move, alphabet, endCondition);
                        continue;
                    }
                    break;
                }
                #endregion
                #region Backpropagation
                var result = this.GetResult(iterationState.HasRepetition(), node.IsComputerMove());
                node.Backpropagation(result);
                #endregion 
            }
            return rootnode.childNodes.OrderByDescending(child => child.visits).First().GetMove().position;
        }
    }
}
