using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace advent_of_code_2019.Day12
{
    public class Day12
    {
        private readonly List<Moon> moons = new List<Moon>();

        public int Part1()
        {
            ParseInput();

            for (int i = 0; i < 1000; i++)
            {
                ApplyGravity();
                ApplyVelocity();
            }

            return moons.Sum(m => m.TotalEnergy);
        }

        public long Part2()
        {
            ParseInput();

            var hashes = new HashSet<(int, int, int, int, int, int, int, int)>();
            var xCount = 0;

            while (true)
            {
                if (FoundRepetition((
                    moons[0].Position.X,
                    moons[0].Velocity.X,
                    moons[1].Position.X,
                    moons[1].Velocity.X,
                    moons[2].Position.X,
                    moons[2].Velocity.X,
                    moons[3].Position.X,
                    moons[3].Velocity.X), hashes, true, false, false))
                {
                    break;
                }

                xCount++;
            }

            var yCount = 0;

            while (true)
            {
                if (FoundRepetition((
                    moons[0].Position.Y,
                    moons[0].Velocity.Y,
                    moons[1].Position.Y,
                    moons[1].Velocity.Y,
                    moons[2].Position.Y,
                    moons[2].Velocity.Y,
                    moons[3].Position.Y,
                    moons[3].Velocity.Y), hashes, false, true, false))
                {
                    break;
                }

                yCount++;
            }

            var zCount = 0;

            while (true)
            {
                if (FoundRepetition((
                    moons[0].Position.Z,
                    moons[0].Velocity.Z,
                    moons[1].Position.Z,
                    moons[1].Velocity.Z,
                    moons[2].Position.Z,
                    moons[2].Velocity.Z,
                    moons[3].Position.Z,
                    moons[3].Velocity.Z), hashes, false, false, true))
                {
                    break;
                }

                zCount++;
            }

            var result= LeastCommonMultiple(xCount, LeastCommonMultiple(yCount, zCount));

            return result;
        }

        private bool FoundRepetition(
            (int, int, int, int, int, int, int, int) inputs,
            HashSet<(int, int, int, int, int, int, int, int)> hashes,
            bool calcX,
            bool calcY,
            bool calcZ)
        {
            if (hashes.Contains(inputs))
            {
                hashes.Clear();
                return true;
            }

            hashes.Add(inputs);
            ApplyGravity(calcX, calcY, calcZ);
            ApplyVelocity();

            return false;
        }

        private void ApplyVelocity()
        {
            foreach (var moon in moons)
            {
                moon.ApplyVelocity();
            }
        }

        private void ApplyGravity(bool applyX = true, bool applyY = true, bool applyZ = true)
        { 
            for (int m = 0; m < moons.Count - 1; m++)
            {
                for (int m1 = m + 1; m1 < moons.Count; m1++)
                {
                    var moon1 = moons[m];
                    var moon2 = moons[m1];

                    moon1.ApplyGravity(moon2, applyX, applyY, applyZ);
                }
            }
        }

        private void ParseInput()
        {
            Regex regex = new Regex(@"[<][x][=](-?[01234567890]{1,2})[,]\s[y][=](-?[01234567890]{1,2})[,]\s[z][=](-?[01234567890]{1,2})", RegexOptions.Compiled);

            foreach (var line in File.ReadAllLines("Day12\\Input\\input.txt"))
            {
                Match match = regex.Match(line);
                var moon = new Moon
                {
                    Position = new Vector3
                    {
                        X = Convert.ToInt32(match.Groups[1].Captures[0].Value),
                        Y = Convert.ToInt32(match.Groups[2].Captures[0].Value),
                        Z = Convert.ToInt32(match.Groups[3].Captures[0].Value)
                    },
                    Velocity = new Vector3()
                };
                moons.Add(moon);
            }
        }

        private static long LeastCommonMultiple(long a, long b)
        {
            return a * b / GreatestCommonDivisor(a, b);
        }

        private static long GreatestCommonDivisor(long a, long b)
        {
            while (a != b)
            {
                if (a < b)
                {
                    b -= a;
                }
                else
                {
                    a -= b;
                }
            }

            return a;
        }
    }
}
