using advent_of_code_2019.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2019.Day19
{
    public class Day19
    {
        private Dictionary<(int, int), int> space = new Dictionary<(int, int), int>();
        private int MaxDimension = 1200;
        private int MinDimension = 600;
        private bool[] FoundBeam; 

        public long Part1(string program)
        {
            long affectedDroneCount = 0;

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    var Cpu = new Cpu(program, new Queue<long>());
                    Cpu.Inputs.Enqueue(x);
                    Cpu.Inputs.Enqueue(y);
                    var result = Cpu.Run();
                    if (result == 1)
                    {
                        affectedDroneCount++;
                    }
                }
            }

            return affectedDroneCount;
        }

        public long Part2(string program)
        {
            FoundBeam = new bool[MaxDimension];

            for (int x = MinDimension; x < MaxDimension; x++)
            {
                for (int y = MinDimension; y < MaxDimension; y++)
                {
                    var Cpu = new Cpu(program, new Queue<long>());
                    Cpu.Inputs.Enqueue(x);
                    Cpu.Inputs.Enqueue(y);
                    var result = Cpu.Run();

                    space[(x, y)] = (int)result;
                    if (result == 1)
                    {
                        FoundBeam[y] = true;
                    }
                }
            }

            var coords = FindSquare();

            if (false)
            {
                File.WriteAllText(@"out2.txt", string.Empty);

                for (int y1 = MinDimension; y1 < MaxDimension; y1++)
                {
                    for (int x1 = MinDimension; x1 < MaxDimension; x1++)
                    {
                        File.AppendAllText(@"out2.txt", space[(x1, y1)].ToString());
                    }

                    File.AppendAllText(@"out2.txt", Environment.NewLine);
                }
            }

            var answer = (coords.Item1 * 10000) + coords.Item2;

            return answer;
        }

        private (int, int) FindSquare()
        {
            for (int x = MinDimension; x < MaxDimension; x++)
            {
                for (int y = MinDimension; y < MaxDimension; y++)
                {
                    if (CheckSquare(x, y))
                    {
                        return (x, y);
                    }
                }
            }

            return (0, 0);
        }

        private bool CheckSquare(int x, int y)
        {
            if (x > MaxDimension - 101 || y > MaxDimension - 101)
            {
                return false;
            }

            for (int a = 0; a < 100; a++)
            {
                if (space[(x + a, y)] != 1)
                {
                    return false;
                }
            }


            for (int b = 0; b < 100; b++)
            {
                if (space[(x, y + b)] != 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
