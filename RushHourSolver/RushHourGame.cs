using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHourSolver
{
    class RushHourGame
    {
        private Dictionary<IntPair, Car> cars;
        private int width;
        private int height;
        private IntPair goal;

        public List<IntPair> Cars
        {
            get
            {
                return this.cars.Keys.ToList();
            }
        }

        //CONSTRUCTORS
        public RushHourGame(string[] board, int width, int height, int x, int y)
        {
            this.cars = new Dictionary<IntPair, Car>();
            this.width = width;
            this.height = height;
            this.buildDictionary(board);
            this.goal = new IntPair(x, y);
        }

        public RushHourGame(RushHourGame game)
        {
            this.cars = new Dictionary<IntPair, Car>(game.cars);
            this.height = game.height;
            this.width = game.width;
            this.goal = game.goal;
        }

        //METHODS
        private void buildDictionary(string[] board)
        {
            HashSet<char> identifiers = new HashSet<char>();

            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    char identifier = board[y][x];

                    if (identifier != '.' && !identifiers.Contains(identifier))
                    {
                        bool direction;
                        int size = 2;

                        identifiers.Add(identifier);

                        if (y + 1 < this.height && board[y + 1][x] == identifier)
                        {
                            int y2 = y + 2;

                            while (y2 < this.height && board[y2++][x] == identifier)
                                size++;

                            direction = Car.VERTICAL;
                        }
                        else
                        {
                            int x2 = x + 2;

                            while (x2 < this.width && board[y][x2++] == identifier)
                                size++;

                            direction = Car.HORIZONTAL;
                        }

                        this.cars.Add(new IntPair(x, y), new Car(direction, size, identifier));
                    }
                }
            }
        }

        public bool IsOccupied(IntPair pos)
        {
            return this._carAt(pos).X >= 0;
        }

        private IntPair _carAt(IntPair pos)
        {
            Car car;
            IntPair p = pos;

            if (this.cars.TryGetValue(p, out car))
                return p;

            
            for (int x2 = pos.X - 1; x2 >= 0; x2--)
            {
                p.X = x2;
                if (this.cars.TryGetValue(p, out car))
                    if (car.Direction == Car.HORIZONTAL && car.Size >= pos.X - x2 + 1)
                        return p;
                    else
                        break;
            }

            p.X = pos.X;

            for (int y2 = pos.Y - 1; y2 >= 0; y2--)
            {
                p.Y = y2;
                if (this.cars.TryGetValue(p, out car))
                    if (car.Direction == Car.VERTICAL && car.Size >= pos.Y - y2 + 1)
                        return p;
                    else
                        break;
            }

            return new IntPair(-1, -1);
        }

        public char CarAt(IntPair pos)
        {
            IntPair p = this._carAt(pos);

            if (p.X < 0)
                return '.';

            return this.cars[p].Identifier;
        }

        public int MovesAvailable(IntPair pos, int direction)
        {
            switch (direction)
            {
                case Movement.UP:
                    return upMoves(pos);
                case Movement.DOWN:
                    return downMoves(pos);
                case Movement.LEFT:
                    return leftMoves(pos);
                case Movement.RIGHT:
                    return rightMoves(pos);
            }

            return 0;
        }

        private int upMoves(IntPair pos)
        {
            int i = 0;

            if (this.cars[this._carAt(pos)].Direction != Car.VERTICAL)
                return 0;

            while (pos.Y-- > 0)
            {
                if (!this.IsOccupied(pos))
                    i++;
                else
                    break;
            }

            return i;
        }

        private int leftMoves(IntPair pos)
        {
            int i = 0;

            if (this.cars[this._carAt(pos)].Direction != Car.HORIZONTAL)
                return 0;

            while (pos.X-- > 0)
            {
                if (!this.IsOccupied(pos))
                    i++;
                else
                    break;
            }

            return i;
        }

        private int rightMoves(IntPair pos)
        {
            int i = 0;

            if (this.cars[this._carAt(pos)].Direction != Car.HORIZONTAL)
                return 0;

            pos.X += this.cars[pos].Size - 1;
            
            while (pos.X++ < this.width - 1)
            {
                if (!this.IsOccupied(pos))
                    i++;
                else
                    break;
            }

            return i;
        }

        private int downMoves(IntPair pos)
        {
            int i = 0;

            if (this.cars[this._carAt(pos)].Direction != Car.VERTICAL)
                return 0;

            pos.Y += this.cars[pos].Size - 1;
            
            while (pos.Y++ < this.height - 1)
            {
                if (!this.IsOccupied(pos))
                    i++;
                else
                    break;
            }

            return i;
        }

        public bool Move(IntPair pos, int n, int direction)
        {
            pos = _carAt(pos);
            IntPair newPos = pos;

            if (pos.X < 0 || this.MovesAvailable(pos, direction) < n)
                return false;

            Car car = this.cars[pos];

            switch (direction)
            {
                case Movement.UP:
                    if (car.Direction != Car.VERTICAL)
                        return false;
                    newPos.Y -= n;
                    break;
                case Movement.DOWN:
                    if (car.Direction != Car.VERTICAL)
                        return false;
                    newPos.Y += n;
                    break;
                case Movement.RIGHT:
                    if (car.Direction != Car.HORIZONTAL)
                        return false;
                    newPos.X += n;
                    break;
                case Movement.LEFT:
                    if (car.Direction != Car.HORIZONTAL)
                        return false;
                    newPos.X -= n;
                    break;
                default:    
                    return false;
            }

            this.cars.Remove(pos);
            this.cars.Add(newPos, car);

            return true;
        }

        public string GetPositionsString()
        {
            SortedSet<int> v = new SortedSet<int>();
            SortedSet<int> h = new SortedSet<int>();
            StringBuilder sbv = new StringBuilder();
            StringBuilder sbh = new StringBuilder();

            foreach (IntPair n in this.cars.Keys)
                if (cars[n].Direction == Car.VERTICAL)
                    //sbv.Append(n.X * this.height + n.Y);
                    v.Add(n.X * this.height + n.Y);
                else
                    //sbh.Append(n.Y * this.width + n.X);
                    h.Add(n.Y * this.width + n.X);

            //return sbv.Append(":").Append(sbh).ToString();
            foreach (int n in v)
            {
                sbv.Append(n);
            }

            sbv.Append(":");

            foreach (int n in h)
            {
                sbv.Append(n);
            }

            return sbv.ToString();
        }

        private bool isRedCar(IntPair pos)
        {
            char c = this.cars[pos].Identifier;

            return c == 'x' || c == 'X'; 
        }

        public int CarsBlockingGoal()
        {
            IntPair p = this.goal;
            int n = 0;

            for (; ;)
            {
                IntPair car = this._carAt(p);

                if (car.X >= 0)
                {
                    if (this.isRedCar(car))
                        break;
                    else
                        n++;
                }

                p.X--;
            }
            
            return n;
        }

        public bool Finished()
        {
            if (this.cars.ContainsKey(this.goal))
            {
                IntPair n = this._carAt(this.goal);

                if (n.X >= 0)
                {
                    if (this.isRedCar(n))
                        return true;
                }

                
            }

            return false;
        }

        public bool CarIsVertical(IntPair pos)
        {
            return this.cars[pos].Direction == Car.VERTICAL;
        }

        public bool CarIsHorizontal(IntPair pos)
        {
            return this.cars[pos].Direction == Car.HORIZONTAL;
        }

        public void Clear()
        {
            this.cars.Clear();
            this.cars = null;
        }

        public override String ToString()
        {
            char[][] board = new char[this.height][];

            for (int i = 0; i < this.height; i++ )
            {
                board[i] = new char[this.width];
            }

            foreach (IntPair k in this.cars.Keys)
            {
                Car car = this.cars[k];

                if (car.Direction == Car.HORIZONTAL)
                {
                    for (int i = 0; i < car.Size; i++)
                        board[k.Y][k.X + i] = car.Identifier;
                }
                else
                {
                    for (int i = 0; i < car.Size; i++)
                        board[k.Y + i][k.X] = car.Identifier;
                }
            }

            StringBuilder sb = new StringBuilder();

            foreach (char[] ar in board)
            {
                sb.AppendLine((new string(ar)).Replace('\0', '.'));
            }

            return sb.ToString();
        }
    }
}
