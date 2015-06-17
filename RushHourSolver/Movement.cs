using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHourSolver
{
    struct Movement
    {
        public const char UP = 'u';
        public const char DOWN = 'd';
        public const char LEFT = 'l';
        public const char RIGHT = 'r';
        public static readonly char[] DIRECTIONS = new char[]{ UP, DOWN, LEFT, RIGHT };
        public char Car { get; private set; }
        public char Direction { get; private set; }
        public int Amount { get; private set; }

        public Movement(char car, char direction, int amount) : this()
        {
            this.Car = car;
            this.Direction = direction;
            this.Amount = amount;
        }

        public override string ToString()
        {
            return "" + this.Car + this.Direction + this.Amount;
        }
    }
}
