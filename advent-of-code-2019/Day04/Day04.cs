using System;
using System.Text.RegularExpressions;

namespace advent_of_code_2019.Day04
{
    public class Day04
    {
        Regex reg1 = new Regex("([023456789]|^)[1]{2}([023456789]|$)", RegexOptions.Compiled);
        Regex reg2 = new Regex("([013456789]|^)[2]{2}([013456789]|$)", RegexOptions.Compiled);
        Regex reg3 = new Regex("([012456789]|^)[3]{2}([012456789]|$)", RegexOptions.Compiled);
        Regex reg4 = new Regex("([012356789]|^)[4]{2}([012356789]|$)", RegexOptions.Compiled);
        Regex reg5 = new Regex("([012346789]|^)[5]{2}([012346789]|$)", RegexOptions.Compiled);
        Regex reg6 = new Regex("([012345789]|^)[6]{2}([012345789]|$)", RegexOptions.Compiled);
        Regex reg7 = new Regex("([012345689]|^)[7]{2}([012345689]|$)", RegexOptions.Compiled);
        Regex reg8 = new Regex("([012345679]|^)[8]{2}([012345679]|$)", RegexOptions.Compiled);
        Regex reg9 = new Regex("([012345678]|^)[9]{2}([012345678]|$)", RegexOptions.Compiled);
        Regex reg0 = new Regex("([123456789]|^)[0]{2}([123456789]|$)", RegexOptions.Compiled);

        public int Part1(string input)
        {
            int from = Convert.ToInt32(input.Substring(0, 6));
            int to = Convert.ToInt32(input.Substring(7, 6));

            int matchCount = 0;

            for (int i = from; i <= to; i++)
            {
                string s = i.ToString();

                if (s.Contains("11") || s.Contains("22") || s.Contains("33") || s.Contains("44") || s.Contains("55")
                    || s.Contains("66") || s.Contains("77") || s.Contains("88") || s.Contains("99") || s.Contains("00"))
                {
                    int d0 = Convert.ToInt32(s[0]);
                    int d1 = Convert.ToInt32(s[1]);
                    int d2 = Convert.ToInt32(s[2]);
                    int d3 = Convert.ToInt32(s[3]);
                    int d4 = Convert.ToInt32(s[4]);
                    int d5 = Convert.ToInt32(s[5]);

                    if (d1 >= d0 && d2 >= d1 && d3 >= d2 && d4 >= d3 && d5 >= d4)
                    {
                        matchCount++;
                    }
                }
            }

            return matchCount;
        }

        public int Part2(string input)
        {
            int from = Convert.ToInt32(input.Substring(0, 6));
            int to = Convert.ToInt32(input.Substring(7, 6));

            int matchCount = 0;

            for (int i = from; i <= to; i++)
            {
                string s = i.ToString();

                if (s.Contains("11") || s.Contains("22") || s.Contains("33") || s.Contains("44") || s.Contains("55")
                    || s.Contains("66") || s.Contains("77") || s.Contains("88") || s.Contains("99") || s.Contains("00"))
                {
                    int d0 = Convert.ToInt32(s[0]);
                    int d1 = Convert.ToInt32(s[1]);
                    int d2 = Convert.ToInt32(s[2]);
                    int d3 = Convert.ToInt32(s[3]);
                    int d4 = Convert.ToInt32(s[4]);
                    int d5 = Convert.ToInt32(s[5]);

                    if (d1 >= d0 && d2 >= d1 && d3 >= d2 && d4 >= d3 && d5 >= d4)
                    {
                        if (DigitMatch(s))
                        {
                            matchCount++;
                        }
                    }
                }
            }

            return matchCount;
        }

        private bool DigitMatch(string input)
        {
           if (
                reg0.IsMatch(input) ||
                reg1.IsMatch(input) ||
                reg2.IsMatch(input) ||
                reg3.IsMatch(input) ||
                reg4.IsMatch(input) ||
                reg5.IsMatch(input) ||
                reg6.IsMatch(input) ||
                reg7.IsMatch(input) ||
                reg8.IsMatch(input) ||
                reg9.IsMatch(input))
            {
                return true;
            }

            return false;
        }
    }
}
