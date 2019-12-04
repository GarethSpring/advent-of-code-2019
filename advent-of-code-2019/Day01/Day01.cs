using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2019.Day01
{
    public class Day01
    {
        private List<int> input;

        public int Part1()
        {
            LoadInput();

            int sum = 0;

            foreach (int m in input)
            {
                sum += (int)CalcFuel(m);
            }

            return sum;
        }

        public int Part2()
        {
            LoadInput();

            int sum = 0;

            foreach (int m in input)
            {
                var fuelForModule = CalcFuel(m);
                var extraFuel = CalcFuel(fuelForModule);
                while (extraFuel >= 0)
                {
                    sum += (int)extraFuel;
                    extraFuel = CalcFuel(extraFuel);
                }

                sum += (int)fuelForModule;
            }

            return sum;
        }

        public decimal CalcFuel(decimal mass)
        {
            return Math.Floor(mass / 3) - 2;
        }

        private void LoadInput()
        {
            input = new List<int>();

            foreach (var line in File.ReadAllLines("Day01\\Input\\input.txt"))
            {
                input.Add(Convert.ToInt32(line));
            }
        }
    }
}
