using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace advent_of_code_2019.Day06
{
    public class Day06
    {
        public List<Sat> Sats = new List<Sat>();

        public int Part1()
        {
            LoadInput();

            SetDirectOrbits();

            var blackHole = Sats.Single(s => s.Orbits == null);

            int orbits = 0;

            foreach (Sat sat in Sats) 
            {
                var result = Traverse(sat, 0, sat.Name);
                orbits += result;
                Debug.WriteLine(result);
            }

            return orbits;
        }

        public int Part2()
        {
            LoadInput();

            SetDirectOrbits();

            var l1 = GetChildren(Sats.Single(s => s.Name == "SAN"));
            var l2 = GetChildren(Sats.Single(s => s.Name == "YOU"));

            var commSat = l1.Intersect(l2).ToList().First();


            int count = 0;
            foreach(var s in l1)
            {
                if (s.Name != commSat.Name)
                {
                    Debug.WriteLine(s.Name);
                    count++;
                }
                else
                {
                    break;
                }
            }

            foreach (var s in l2)
            {
                if (s.Name != commSat.Name)
                {
                    Debug.WriteLine(s.Name);
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count;
        }

        private List<Sat> GetChildren(Sat sat)
        {
            List<Sat> sats = new List<Sat>();

            while (sat.Orbits != null)
            {
                sats.Add(sat.Orbits);
                sat = sat.Orbits;
            }

            return sats;
        }

        private int GetOrbits(Sat sat)
        {
            var res = Traverse(sat, 0, sat.Name);
            Debug.WriteLine(res);
            return res;
            
        }

        private int Traverse(Sat sat, int count, string orbit)
        {
            //Debug.WriteLine(orbit);

            if (sat.Name == "COM")
                return count;

            if (sat.Orbits != null)
            {
                orbit += $" >{sat.Orbits.Name}";
                count++;
                count = Traverse(sat.Orbits, count, orbit);
            }

            return count;
        }

        private void SetDirectOrbits()
        {
            foreach (Sat sat in Sats)
            {
                foreach(string orbitterName in sat.OrbiterNames)
                {
                    // D -> E & I
                    Sat orbitter = Sats.FirstOrDefault(s => s.Name == orbitterName);
                    if (orbitter == null)
                    {
                        Sat sat2 = new Sat
                        {
                            Name = orbitterName,
                            OrbiterNames = new List<string>()
                        };

                        sat2.Orbits = sat;
                    }
                    else
                    {
                        orbitter.Orbits = sat;
                    }
                }
            }
        }

        private void LoadInput()
        {
            foreach (var line in File.ReadAllLines("Day06\\Input\\input.txt"))
            {
                //91M)VLK
                string Name = line.Substring(0, line.IndexOf(')'));
                string orbiter = line.Substring(line.IndexOf(')') + 1);

                if (!Sats.Any(s => s.Name == Name))
                {
                    Sat sat = new Sat
                    {
                        Name = Name,
                        OrbiterNames = new List<string> { orbiter }
                    };

                    Sats.Add(sat);
                }
                else
                {
                    Sat sat = Sats.Single(s => s.Name == Name);
                    sat.OrbiterNames.Add(orbiter);
                }

                // Catch edge cases
                if (!Sats.Any(s => s.Name == orbiter))
                {
                    Sat sat = new Sat
                    {
                        Name = orbiter,
                        OrbiterNames = new List<string>()
                    };

                    Sats.Add(sat);
                }
            }
        }
    }

    public class Sat
    {
        public string Name { get; set; }

        public List<string> OrbiterNames { get; set; }

        public Sat Orbits { get; set; }
    }
}
