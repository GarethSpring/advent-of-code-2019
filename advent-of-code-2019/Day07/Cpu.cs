using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2019.Day07
{
    public class Cpu
    {
        private int opPointer = 0;
        private int inputPointer = 0;
        private List<int> intCode;

        public List<int> Inputs { get; set; }

        public Stack<int> Outputs { get; set; }

        public bool IsHalted { get; set; }

        public bool IsFinished { get; set; }

        private int opCode;

        public Cpu(string program, List<int> inputs)
        {
            GetInput(program);

            Inputs = inputs;
        }

        public Cpu()
        {
            opCode = 0;
            IsFinished = false;
        }

        public int Run()
        {
            IsHalted = false;
            Outputs = new Stack<int>();

            int output = 0;

            while (opCode != 99 && !IsHalted)
            {
                int mode1 = 0;
                int mode2 = 0;
                int mode3 = 0;

                if (intCode[opPointer] > 99)
                {
                    // Parameter Modes
                    string stringCode = Convert.ToString(intCode[opPointer]).PadLeft(5, '0');

                    opCode = Convert.ToInt32(stringCode.Substring(3, 2));

                    mode1 = Convert.ToInt32(stringCode.Substring(2, 1));
                    mode2 = Convert.ToInt32(stringCode.Substring(1, 1));
                    mode3 = Convert.ToInt32(stringCode.Substring(0, 1));
                }
                else
                {
                    // Position Mode
                    opCode = intCode[opPointer];
                }

                switch (opCode)
                {
                    case 1:
                        Add(opPointer + 1, opPointer + 2, opPointer + 3, mode1, mode2, mode3);
                        break;
                    case 2:
                        Multiply(opPointer + 1, opPointer + 2, opPointer + 3, mode1, mode2, mode3);
                        break;
                    case 3:
                        Input(opPointer + 1, Inputs[inputPointer >= Inputs.Count ? Inputs.Count - 1 : inputPointer]);
                        break;
                    case 4:
                        output = Output(opPointer + 1, mode1);
                        break;
                    case 5:
                        JumpNZ(++opPointer, ++opPointer, mode1, mode2);
                        break;
                    case 6:
                        JumpZ(++opPointer, ++opPointer, mode1, mode2);
                        break;
                    case 7:
                        LessThan(++opPointer, ++opPointer, ++opPointer, mode1, mode2);
                        break;
                    case 8:
                        Equals(++opPointer, ++opPointer, ++opPointer, mode1, mode2);
                        break;
                }
            }

            IsHalted = true;
            if (opCode == 99)
            {
                IsFinished = true;
            }

            return output;
        }

        private void Add(int p1, int p2, int p3, int i1, int i2, int i3)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];
            var v2 = i2 == 0 ? intCode[intCode[p2]] : intCode[p2];

            intCode[intCode[p3]] = v1 + v2;
            opPointer += 4;

            //Debug.WriteLine($"ADD {v1} , {v2} INTO {intCode[p3]} : {i1}, {i2}, {i3}");
        }

        private void Multiply(int p1, int p2, int p3, int i1, int i2, int i3)
        {
            intCode[intCode[p3]] = (i1 == 0 ? intCode[intCode[p1]] : intCode[p1]) * (i2 == 0 ? intCode[intCode[p2]] : intCode[p2]);
            opPointer += 4;

            //Debug.WriteLine($"MULT {intCode[p1]} , {intCode[p2]} INTO {intCode[p3]}  : {i1}, {i2}, {i3}");
        }

        private void Input(int p1, int input)
        {
            intCode[intCode[p1]] = input;
            inputPointer++;
            opPointer += 2;

            //Debug.WriteLine($"WRITE {input} to {intCode[p1]}");
        }

        private int Output(int p1, int i1)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];
            Outputs.Push(v1);
            //Debug.WriteLine($"OUTPUT {v1}");
            IsHalted = true;

            opPointer += 2;
            return v1;
        }

        private void JumpNZ(int p1, int p2, int i1, int i2)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];

            if (v1 != 0)
            {
                opPointer = i2 == 0 ? intCode[intCode[p2]] : intCode[p2];
                //Debug.WriteLine($"JUMPNZ to {pointer}");
            }
            else
            {
                //Debug.WriteLine("NOP JUMPNZ");
                opPointer++;
            }
        }

        private void JumpZ(int p1, int p2, int i1, int i2)
        {
            var v1 = i1 == 0 ? intCode[intCode[p1]] : intCode[p1];

            if (v1 == 0)
            {
                opPointer = i2 == 0 ? intCode[intCode[p2]] : intCode[p2];
                // Debug.WriteLine($"JUMPZ to {pointer}");
            }
            else
            {
                //Debug.WriteLine("NOP JUMPZ");
                opPointer++;
            }
        }

        private void LessThan(int p1, int p2, int p3, int i1, int i2)
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

            opPointer += 1;
        }

        private void Equals(int p1, int p2, int p3, int i1, int i2)
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

            opPointer += 1;
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
