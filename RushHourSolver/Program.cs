using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHourSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            RushHourGame game;
            String input;

            input = Console.ReadLine();

            char mode = input[0];

            input = Console.ReadLine();

            string[] l = input.Split(new char[] { ' ' });

            int width = int.Parse(l[0]);
            int height = int.Parse(l[1]);

            input = Console.ReadLine();

            l = input.Split(new char[] { ' ' });

            int x = int.Parse(l[0]);
            int y = int.Parse(l[1]);

            bool heuristic = Console.ReadLine() == "1";

            string[] board = new string[height];
            for (int i = 0; i < height; i++)
            {
                board[i] = Console.ReadLine();
            }

            game = new RushHourGame(board, width, height, x, y);

            RushHourSolver solver = new RushHourSolver(heuristic);

            string[] moves = solver.Solve(game);

            if (mode == '1')
            {
                if (moves.Count() > 0)
                    Console.WriteLine(String.Join(", ", solver.Solve(game)));
                else
                    Console.WriteLine("Geen oplossing gevonden");

                Console.ReadLine();
            }
            else
            {
                if (moves.Count() > 0)
                    Console.WriteLine(moves.Count());
                else
                    Console.WriteLine("-1");
            }
        }
    }
}
