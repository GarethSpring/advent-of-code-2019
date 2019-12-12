using System;

namespace advent_of_code_2019.Day12
{
    public class Vector3
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int AbsSum()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }
    }
}
