using advent_of_code_2019.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace advent_of_code_2019.Day13
{
    public class Day13
    {
        private readonly List<Tile> tiles = new List<Tile>();

        public long Part1(string program)
        {
            var CPU = new Cpu(program, new List<long>());

            while (!CPU.IsFinished)
            {
                var tile = new Tile();
                tile.X = CPU.Run();
                tile.Y = CPU.Run();
                tile.Id = CPU.Run();
                tiles.Add(tile);
            }

            return tiles.Count(t => t.Id == 2);
        }

        public long Part2(string program)
        {
            var CPU = new Cpu(program, new List<long>());

            // Play for Free!
            CPU.Poke(0, 2);

            Render(program);

            while (tiles.Any(t => t.Id == 2))
            {
                Render(program);

                Play(program);

            }

            return 0;
        }

        private void Play(string program)
        {

        }

        private void Render(string program)
        {
            var CPU = new Cpu(program, new List<long>());

            while (!CPU.IsFinished)
            {
                var tile = new Tile();
                tile.X = CPU.Run();
                tile.Y = CPU.Run();
                tile.Id = CPU.Run();
                tiles.Add(tile);
            }

            var maxX = tiles.Max(t => t.X);
            var maxY = tiles.Max(t => t.Y);
            var minX = tiles.Min(t => t.X);
            var minY = tiles.Min(t => t.Y);

            for (long y = minY; y <= maxY; y++)
            {
                for (long x = minX; x <= maxX; x++)
                {
                    Tile tile = tiles.FirstOrDefault(t => t.X == x & t.Y == y);
                    if (tile != null)
                    {
                        switch(tile.Id)
                        {
                            case 0:
                                Debug.Write(" ");
                                break;
                            case 1:
                                Debug.Write("|");
                                break;
                            case 2:
                                Debug.Write("#");
                                break;
                            case 3:
                                Debug.Write("=");
                                break;
                            case 4:
                                Debug.Write("o");
                                break;
                        }

                    }
                }
                Debug.WriteLine("");
            }

            Debug.WriteLine("");
        }
    }
}
