using advent_of_code_2019.Common;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2019.Day13
{
    public class Day13
    {
        private readonly List<Tile> tiles = new List<Tile>();

        Tile ball = new Tile();
        Tile paddle = new Tile();

        private const int Block = 2;
        private const int Paddle = 3;
        private const int Ball = 4;

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

            return tiles.Count(t => t.Id == Block);
        }

        public long Initialise(string program)
        {
            var CPU = new Cpu(program, new List<long>());

            while (!CPU.IsFinished)
            {
                var tile = new Tile();
                tile.X = CPU.Run();
                tile.Y = CPU.Run();
                tile.Id = CPU.Run();
                if (tile.Id == Ball)
                {
                    ball = tile;
                }
                else if (tile.Id == Paddle)
                {
                    paddle = tile;
                }
                else
                {
                    tiles.Add(tile);
                }
            }

            return tiles.Count();
        }

        public long Part2(string program)
        {
            long score = 0;

            Initialise(program);

            var CPU = new Cpu(program, new List<long>());

            // Play for Free!
            CPU.Poke(2, 0);

            while (!CPU.IsFinished)
            {
                var tile = new Tile();
                tile.X = CPU.Run();
                tile.Y = CPU.Run();

                tile.Id = CPU.Run();

                if (tile.X == -1 && tile.Y == 0)
                {
                    score = tile.Id;
                }
                else
                {
                    if (tile.Id != Ball && tile.Id != Paddle)
                    {
                        Tile target = tiles.FirstOrDefault(t => t.X == tile.X && t.Y == tile.Y);
                        tiles.Add(tile);
                    }
                    else
                    {
                        if (tile.Id == Ball)
                        {
                            if (paddle.X > tile.X)
                            {
                                // Move Left
                                CPU.Inputs.Add(-1);
                            }
                            else if (paddle.X < tile.X)
                            {
                                // Move right
                                CPU.Inputs.Add(1);
                            }
                            else
                            {
                                CPU.Inputs.Add(0);
                            }
                        }

                        if (tile.Id == Paddle)
                        {
                            paddle.X = tile.X;
                        }
                    }
                }

                if (!tiles.Any(t => t.Id == Block))
                {
                    break;
                }
            }

            return score;
        }
    }
}
