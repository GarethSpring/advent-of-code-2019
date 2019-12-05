using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace advent_of_code_2019.Day05
{
    public class Day05
    {
        private List<int> intCode;

        public int GetResult(string program, int input)
        {
            GetInput(program);

            int opCode = 0;
            int opPointer = 0;

            int output = 0;

            while (opCode != 99)
            {
                if (intCode[opPointer] > 99)
                {
                    // Parameter Modes

                    string stringCode = Convert.ToString(intCode[opPointer]).PadLeft(5, '0');

                    opCode = Convert.ToInt32(stringCode.Substring(3,2));
                    Debug.WriteLine($"{opPointer} OPP {opCode}");

                    var mode1 = Convert.ToInt32(stringCode.Substring(2, 1));
                    var mode2 = Convert.ToInt32(stringCode.Substring(1, 1));
                    var mode3 = Convert.ToInt32(stringCode.Substring(0, 1));

                    switch (opCode)
                    {
                        case 1:
                            OpCode1(opPointer + 1, opPointer + 2, opPointer + 3, mode1, mode2, mode3);
                            opPointer += 4;
                            break;
                        case 2:
                            OpCode2(opPointer + 1, opPointer + 2, opPointer + 3, mode1, mode2, mode3);
                            opPointer += 4;
                            break;
                        case 3:
                            OpCode3(opPointer + 1, input);
                            opPointer+=2;
                            break;
                        case 4:
                            output = OpCode4(opPointer + 1, mode1);
                            opPointer+=2;
                            break;
                        case 5:
                            OpCode5(++opPointer, ++opPointer, mode1, mode2, ref opPointer);
                            break;
                        case 6:
                            OpCode6(++opPointer, ++opPointer, mode1, mode2, ref opPointer);
                            break;
                        case 7:
                            OpCode7(++opPointer, ++opPointer, ++opPointer, mode1, mode2);
                            opPointer += 1;
                            break;
                        case 8:
                            OpCode8(++opPointer, ++opPointer, ++opPointer, mode1, mode2);
                            opPointer += 1;
                            break;
                    }  
                }
                else
                {
                    // Position Mode only
                    opCode = intCode[opPointer];

                    Debug.WriteLine($"{opPointer} OPI {opCode}");

                    switch (opCode)
                    {
                        case 1:
                            OpCode1(opPointer+1, opPointer+2, opPointer+3, 0, 0, 0);
                            opPointer += 4;
                            break;
                        case 2:
                            OpCode2(opPointer + 1, opPointer + 2, opPointer + 3, 0, 0, 0);
                            opPointer += 4;
                            break;
                        case 3:
                            OpCode3(opPointer + 1, input);
                            opPointer+=2;
                            break;
                        case 4:
                            output = OpCode4(opPointer+1, 0);
                            opPointer+=2;
                            break;
                        case 5:
                            OpCode5(++opPointer, ++opPointer, 0, 0, ref opPointer);
                            break;
                        case 6:
                            OpCode6(++opPointer, ++opPointer, 0, 0, ref opPointer);
                            break;
                        case 7:
                            OpCode7(++opPointer, ++opPointer, ++opPointer, 0, 0);
                            opPointer += 1;
                            break;
                        case 8:
                            OpCode8(++opPointer, ++opPointer, ++opPointer, 0, 0);
                            opPointer += 1;
                            break;
                    }
                }
            }

            return output;
        }

        private void OpCode1(int p1, int p2, int p3, int i1, int i2, int i3)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];
            var v2 = i2 == 0 ? intCode[intCode[p2]] : intCode[p2];

            Debug.WriteLine($"ADD {v1} , {v2} INTO {intCode[p3]} : {i1}, {i2}, {i3}");
            intCode[intCode[p3]] = v1 + v2;
        }

        private void OpCode2(int p1, int p2, int p3, int i1, int i2, int i3)
        {
            Debug.WriteLine($"MULT {intCode[p1]} , {intCode[p2]} INTO {intCode[p3]}  : {i1}, {i2}, {i3}");
            intCode[intCode[p3]] = (i1 == 0 ? intCode[intCode[p1]] : intCode[p1]) * (i2 == 0 ? intCode[intCode[p2]] : intCode[p2]);
        }

        private void OpCode3(int p1, int input)
        {
            Debug.WriteLine($"WRITE {input} to {intCode[p1]}");
            intCode[intCode[p1]] = input;
        }

        private int OpCode4(int p1, int i1)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];

            Debug.WriteLine($"OUTPUT {v1}");
            return v1;
        }

        private void OpCode5(int p1, int p2, int i1, int i2, ref int pointer)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];

            if (v1 != 0)
            {
                pointer = i2 == 0 ? intCode[intCode[p2]] : intCode[p2];
                Debug.WriteLine($"JUMPNZ to {pointer}");
            }
            else
            {
                Debug.WriteLine("NOP JUMPNZ");
                pointer++;
            }
        }

        private void OpCode6(int p1, int p2, int i1, int i2, ref int pointer)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];

            if (v1 == 0)
            {
                pointer = i2 == 0 ? intCode[intCode[p2]] : intCode[p2];
                Debug.WriteLine($"JUMPZ to {pointer}");
            }
            else
            {
                Debug.WriteLine("NOP JUMPZ");
                pointer++;
            }
        }

        private void OpCode7(int p1, int p2, int p3, int i1, int i2)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];
            var v2 = i2 == 0 ? intCode[intCode[p2]] : intCode[p2];

            if (v1 < v2)
            {
                intCode[intCode[p3]] = 1;
            }
            else
            {
                intCode[intCode[p3]] = 0;
            }
        }

        private void OpCode8(int p1, int p2, int p3, int i1, int i2)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];
            var v2 = i2 == 0 ? intCode[intCode[p2]] : intCode[p2];

            if (v1 == v2)
            {
                intCode[intCode[p3]] = 1;
            }
            else
            {
                intCode[intCode[p3]] = 0;
            }
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