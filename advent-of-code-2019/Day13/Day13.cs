using advent_of_code_2019.Common;
using System.Collections.Generic;
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
    }
}
