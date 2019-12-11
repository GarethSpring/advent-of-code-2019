using advent_of_code_2019.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace advent_of_code_2019.Day11
{
    public class Day11
    {
        private Dictionary<(int, int), Panel> surface;

        private Robot Ed209;

        private Dictionary<Direction, Direction> RightTurns = new Dictionary<Direction, Direction>()
        {
            { Direction.Up, Direction.Right },
            { Direction.Right, Direction.Down },
            { Direction.Down, Direction.Left },
            { Direction.Left, Direction.Up }
        };

        private Dictionary<Direction, Direction> LeftTurns = new Dictionary<Direction, Direction>()
        {
            { Direction.Up, Direction.Left },
            { Direction.Left, Direction.Down },
            { Direction.Down, Direction.Right },
            { Direction.Right, Direction.Up }
        };

        public long Paint(string program, long startColour, bool render = false)
        {
            Ed209 = new Robot()
            {
                Brain = new Cpu(program, new List<long>() { startColour })
            };

            surface = new Dictionary<(int, int), Panel>();

            long outColour = 0;
            long outDirection = 0;
            int paintCounter = 0;

            while (!Ed209.Brain.IsFinished)
            {
                outColour = Ed209.Brain.Run();

                outDirection = Ed209.Brain.Run();

                // Paint
                if (surface.ContainsKey((Ed209.XPos, Ed209.Ypos)))
                {
                    surface[(Ed209.XPos, Ed209.Ypos)].Colour = (int)outColour;
                }
                else
                {
                    surface[(Ed209.XPos, Ed209.Ypos)] = new Panel() { Colour = (int)outColour };
                    paintCounter++;
                }

                // Change Direction
                Ed209.FacingDirection = outDirection == 1 ? RightTurns[Ed209.FacingDirection] : LeftTurns[Ed209.FacingDirection];

                // Move
                switch(Ed209.FacingDirection)
                {
                    case Direction.Down:
                        Ed209.Ypos--;
                        break;
                    case Direction.Up:
                        Ed209.Ypos++;
                        break;
                    case Direction.Left:
                        Ed209.XPos--;
                        break;
                    case Direction.Right:
                        Ed209.XPos++;
                        break;
                }

                // Add Input
                if (surface.ContainsKey((Ed209.XPos, Ed209.Ypos)))
                {
                    Ed209.Brain.Inputs.Add(surface[(Ed209.XPos, Ed209.Ypos)].Colour);
                }
                else
                {
                    Ed209.Brain.Inputs.Add(0);
                }
            }

            if (render)
            {
                Render();
            }

            return paintCounter;
        }

        private void Render()
        {
            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;
            int maxX = Int32.MinValue;
            int maxY = Int32.MinValue;

            foreach (var key in surface.Keys)
            {
                if (key.Item1 > maxX)
                    maxX = key.Item1;

                if (key.Item2 > maxY)
                    maxY = key.Item2;

                if (key.Item1 < minX)
                    minX = key.Item1;

                if (key.Item2 < minY)
                    minY = key.Item2;
            }

            for (int y = maxY; y >= minY; y--)
            {
                for (int x = 0; x < maxX; x++)
                {
                    Debug.Write(GetPixel(x, y));
                }

                Debug.WriteLine("");
            }
        }

        private string GetPixel(int x, int y)
        {
            if (surface.ContainsKey((x, y)))
            {
                return surface[(x, y)].Colour == 1 ? "#" : " ";
            }
            else
            {
                return " ";
            }
        }
    }

    public enum Direction
    {
        Up = 0, 
        Right = 1,
        Down = 2,
        Left = 3
    }

    public class Panel
    {
        public int Colour { get; set; }
    }

    public class Robot
    {
        public int XPos { get; set; }

        public int Ypos { get; set; }

        public Direction FacingDirection { get; set; }

        public Cpu Brain { get; set; }
    }
}
