using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHourSolver
{
    class State
    {
        public int Cost { get; private set; }
        public RushHourGame Game { get; private set; }
        public State PreviousState { get; private set; }
        public Movement PreviousMove { get; private set; }

        //CONSTRUCTORS
        public State(RushHourGame game)
        {
            this.Game = game;
            this.Cost = 0;
            this.PreviousState = null;
        }

        public State(int cost, RushHourGame game, State previousState, Movement previousMove)
        {
            this.Cost = cost;
            this.Game = game;
            this.PreviousState = previousState;
            this.PreviousMove = previousMove;
        }

        //METHODS
        public void ClearGame()
        {
            this.Game = null;
        }
    }
}
