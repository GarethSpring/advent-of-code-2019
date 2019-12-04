using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2019.Day02
{
    public class Day02
    {
        private List<int> intCode;

        public int Part2(string input)
        {
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    var result = GetResult(input, noun, verb);

                    if (result == 19690720)
                    {
                        return noun * 100 + verb;
                    }
                }
            }

            return 0;
        }

        public int GetResult(string input, int noun, int verb)
        {
            GetInput(input);

            intCode[1] = noun;
            intCode[2] = verb;

            int opCode = 0;
            int opPointer = 0;

            while (opCode != 99)
            {
                opCode = intCode[opPointer];

                switch (opCode)
                {
                    case 1:
                        OpCode1(++opPointer, ++opPointer, ++opPointer);
                        break;
                    case 2:
                        OpCode2(++opPointer, ++opPointer, ++opPointer);
                        break;
                }

                opPointer += 1;
            }

            return intCode[0];
        }

        private void OpCode1(int p1, int p2, int p3)
        {
            intCode[intCode[p3]] = intCode[intCode[p1]] + intCode[intCode[p2]];
        }

        private void OpCode2(int p1, int p2, int p3)
        {
            intCode[intCode[p3]] = intCode[intCode[p1]] * intCode[intCode[p2]];
        }

        private void GetInput(string input)
        {
            var stringCode = input.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            intCode = new List<int>();

            foreach (var s in stringCode)
            {
                intCode.Add(Convert.ToInt32(s));
            }
        }
    }
}
