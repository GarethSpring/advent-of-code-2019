using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

namespace advent_of_code_2019.Day16
{
    public class Day16
    {
        private List<long> intList = new List<long>();
        private List<long> pattern = new List<long> { 0, 1, 0, -1 };

        public long Part1(string input, int phases)
        {
            ParseInput(input);
            string resultString = string.Empty;

            for (int x = 1; x <= phases; x++)
            {
                resultString = string.Empty;
                intList = DoPhase(intList);

                Debug.Write($"After {x} phase: ");

                foreach(var i in intList.Take(8))
                {
                    Debug.Write(i);
                    resultString += i.ToString();
                }

                Debug.WriteLine("");
            }

            return Convert.ToInt64(resultString);
        }

        public long Part2(string input, int phases)
        {
            int offset = Convert.ToInt32(input.Substring(0, 7));
            input = Duplicate(input, 10000);
            ParseInput(input);
            string resultString = string.Empty;

            for (int x = 1; x <= phases; x++)
            {
                resultString = string.Empty;
                intList = DoPhase2Voodoo(intList);

                Debug.WriteLine($"Phase {x}");
            }

            var result = intList.Skip(offset).Take(8);

            foreach (var x in result)
            {
                resultString += x;
            }

            Debug.WriteLine(resultString);

            return Convert.ToInt32(resultString);
        }

        public string Duplicate(string input, int times)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < times; i++)
            {
                sb.Append(input);
            }

            return sb.ToString();
        }

        /// <summary>
         /// Uses pattern x = element[x] = element[x] + [element[x + 1] to solve
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Output</returns>
        private List<long> DoPhase2Voodoo(List<long> input)
        {
            var phaseResult = input;
            var temp = new List<long>();

            phaseResult[input.Count - 1] = input[input.Count - 1];

            for (int i = input.Count - 2; i >= 0; i--)
            {
                phaseResult[i] = GetSingleDigitResult(phaseResult[i + 1] + phaseResult[i]);
            }

            return phaseResult;
        }

        private List<long> DoPhase(List<long> input)
        {
            var phaseResult = new List<long>();


            long digit;

            int patternIndex = 0;

            int curRepetitions = 0;

            for (int i = 0; i < input.Count; i++)
            {
                patternIndex = 0;
                curRepetitions = 0;

                long result = 0;
                digit = 0;
                for (int j = 0; j < input.Count; j++)
                {
                    if (j == 0)
                    {
                        curRepetitions = 1;
                    }

                    long pat = pattern[CalcPatternIndex(ref patternIndex, i + 1, ref curRepetitions)];

                    digit = input[j] * pat;
                    result += digit;
                }

                result = GetSingleDigitResult(result);

                Debug.WriteLine($"Result = {result}");

                phaseResult.Add(result);

            }

            return phaseResult;
        }

        private long GetSingleDigitResult(long result)
        {
            string s = result.ToString();
            return Convert.ToInt64(s.Substring(s.Length - 1, 1));
        }

        private int CalcPatternIndex(ref int curPatternPos, int curSignalPos, ref int curRepetitions)
        {
            if (curRepetitions == curSignalPos)
            {
                // Move to next signal element
                curRepetitions = 1;
                if (curPatternPos == pattern.Count -1 )
                {
                    curPatternPos = 0;
                }
                else
                {
                    curPatternPos++;
                }

                return curPatternPos;
            }
            else
            {
                curRepetitions++;
                return curPatternPos;
            }
        }

        private void ParseInput(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                intList.Add(Convert.ToInt32(input.Substring(i, 1)));
            }
        }
    }
}
