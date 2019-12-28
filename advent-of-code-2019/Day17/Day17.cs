using advent_of_code_2019.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace advent_of_code_2019.Day17
{
    public class Day17
    {
        private List<Coord> coords = new List<Coord>();

        private const int Scaffold = 35;
        private const int NewLine = 10;

        public long Part1(string program, long initialInput)
        {
            var CPU = new Cpu(program, new Queue<long>());
            CPU.Inputs.Enqueue(initialInput);

            int x = 0;
            int y = 1;

            while (!CPU.IsFinished)
            {
                var output = CPU.Run();

                if (output == NewLine)
                {
                    x = 0;
                    y++;
                    continue;
                }
                
                x++;

                var coord = new Coord
                {
                    X = x,
                    Y = y,
                    Content = (int)output
                };

                coords.Add(coord);
            }

            //Render();

            var result = GetAlignmentParameters();

            return result;
        }

        private void Render()
        {
            for (int y = 1; y <= coords.Max(c => c.Y); y++)
            {
                for (int x = 1; x <= coords.Max(c => c.X); x++)
                {
                    var coord = coords.FirstOrDefault(c => c.X == x && c.Y == y);

                    if (coord == null)
                    {
                        Debug.Write("?");
                    }
                    else
                    {
                        char character = (char)coord.Content;
                        string text = character.ToString();

                        Debug.Write(text);
                    }
                }

                Debug.WriteLine("");
            }
           
        }

        private int GetAlignmentParameters()
        {
            var parameters = new List<int>();

            int maxX = 37;
            int maxY = 43;

            for (int y = 2; y < maxY; y++)
            {
                for (int x = 2; x < coords.Max(c => c.X); x++)
                {
                    if (
                        GetCoord(x, y).Content == Scaffold
                        && GetCoord(x - 1, y).Content == Scaffold
                        && GetCoord(x + 1, y).Content == Scaffold
                        && GetCoord(x, y + 1).Content == Scaffold
                        && GetCoord(x, y - 1).Content == Scaffold
                        )
                    {
                        parameters.Add((x - 1) * (y - 1));
                    }
                }
            }

            return parameters.Sum();
        }

        private Coord GetCoord(int x, int y)
        {
            return coords.FirstOrDefault(c => c.X == x && c.Y == y);
        }
    }
}
