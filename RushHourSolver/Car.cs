using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHourSolver
{
    class Car
    {
        public const bool HORIZONTAL = false;
        public const bool VERTICAL = true;

        public bool Direction { get; private set; }
        public int Size { get; private set; }
        public char Identifier { get; private set; }

        // CONSTRUCTORS
        public Car(bool direction, int size, char identifier)
        {
            this.Direction = direction;
            this.Size = size;
            this.Identifier = identifier;
        }

        //METHODS
    }
}
