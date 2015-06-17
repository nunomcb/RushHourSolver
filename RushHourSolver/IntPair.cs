using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHourSolver
{
    struct IntPair : IComparable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public IntPair(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(IntPair pair)
        {
            return this.X == pair.X && this.Y == pair.Y;
        }

        public override bool Equals(object o)
        {
            if (!(o is IntPair))
                return false;

            IntPair pair = (IntPair)o;

            return pair.X == this.X && pair.Y == this.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + this.X.GetHashCode();
                hash = hash * 23 + this.Y.GetHashCode();
                return hash;
            }
        }

        public int CompareTo(object ip)
        {
            IntPair p = (IntPair)ip;

            if (this.X > p.X)
                return 1;
            else if (this.X < p.X)
                return -1;
            else if (this.Y > p.Y)
                return 1;
            else if (this.Y < p.Y)
                return -1;
            else
                return 0;
        }
    }
}
