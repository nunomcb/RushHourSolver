using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace RushHourSolver
{
    class RushHourSolver
    {
        private bool heuristic;

        //CONSTRUCTORS
        public RushHourSolver(bool heuristic)
        {
            this.heuristic = heuristic;
        }

        //METHODS
        private int getPriority(State s)
        {
            if (this.heuristic) {
                int blocking = s.Game.CarsBlockingGoal();

                return s.Cost + blocking;
            }
            else {
                return s.Cost;
            }
        }

        private void printMovements(State s)
        {
            while (s.PreviousState != null)
            {
                Console.WriteLine(s.PreviousMove.ToString());
                s = s.PreviousState;
            }
        }

        private string[] movementsList(State s)
        {
            if (s == null)
                return new string[0];

            List<string> l = new List<string>();

            while (s.PreviousState != null)
            {
                l.Insert(0, s.PreviousMove.ToString());
                s = s.PreviousState;
            }

            return l.ToArray();
        }

        public string[] Solve(RushHourGame game)
        {
            State finalState = null;
            State state = new State(game);
            PriorityQueue<State, int> queue = new PriorityQueue<State,int>(true);
            ConcurrentTabooList<string> visited = new ConcurrentTabooList<string>();
            state = new State(game);
            
            queue.Enqueue(state, 0);

            while (!queue.IsEmpty() && finalState == null)
            {
                state = queue.Dequeue();

                RushHourGame g = state.Game;
                
                Parallel.ForEach(g.Cars, p =>
                {

                    Dictionary<State, int> next = new Dictionary<State, int>();

                    foreach (char direction in Movement.DIRECTIONS)
                    {
                        for (int i = 1; i <= g.MovesAvailable(p, direction); i++)
                        {
                            RushHourGame newGame = new RushHourGame(g);
                            newGame.Move(p, i, direction);

                            if (newGame.Finished())
                            {
                                finalState = new State(state.Cost + 1, newGame, state, new Movement(g.CarAt(p), direction, i));
                                return;
                            }

                            string h = newGame.GetPositionsString();

                            if (visited.TryAdd(h))
                            {
                                State newState = new State(state.Cost + 1, newGame, state, new Movement(g.CarAt(p), direction, i));
                                next.Add(newState, this.getPriority(newState));
                            }
                        }
                    }

                    lock (queue)
                    {
                        foreach (State s in next.Keys)
                        {
                            queue.Enqueue(s, next[s]);
                        }
                    }
                });
            }

            state.ClearGame();

            return movementsList(finalState);
        }
    }
}
