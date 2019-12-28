using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace advent_of_code_2019.Common
{
    public class Cpu
    {
        private long opPointer = 0;
        private Dictionary<long, long> intCode;
        private long relativeBase = 0;

        public Queue<long> Inputs { get; set; }

        public Stack<long> Outputs { get; set; }

        public bool IsHalted { get; set; }

        public bool IsFinished { get; set; }

        private long opCode;

        public Cpu(string program, Queue<long> inputs)
        {
            GetInput(program);

            Inputs = inputs;
        }

        public Cpu()
        {
            opCode = 0;
            IsFinished = false;
        }

        public void Poke(long value, long address)
        {
            intCode[address] = value;
        }

        public long Run()
        {
            IsHalted = false;
            Outputs = new Stack<long>();

            long output = 0;

            while (opCode != 99 && !IsHalted)
            {
                int mode1 = 0;
                int mode2 = 0;
                int mode3 = 0;

                if (intCode[opPointer] > 99)
                {
                    // Parameter Modes
                    string stringCode = Convert.ToString(intCode[opPointer]).PadLeft(5, '0');
                    //Debug.WriteLine($"OpCode: {stringCode}");

                    opCode = Convert.ToInt32(stringCode.Substring(3, 2));

                    mode1 = Convert.ToInt32(stringCode.Substring(2, 1));
                    mode2 = Convert.ToInt32(stringCode.Substring(1, 1));
                    mode3 = Convert.ToInt32(stringCode.Substring(0, 1));
                }
                else
                {
                    // Position Mode
                    opCode = intCode[opPointer];
                    //Debug.WriteLine($"OpCode: {opCode}");
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
                        long input = 0;
                        Inputs.TryDequeue(out input);
                        Input(opPointer + 1, mode1, input);
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
                        LessThan(++opPointer, ++opPointer, ++opPointer, mode1, mode2, mode3);
                        break;
                    case 8:
                        Equals(++opPointer, ++opPointer, ++opPointer, mode1, mode2, mode3);
                        break;
                    case 9:
                        AdjustRelativeBase(opPointer + 1, mode1);
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

        private void AdjustRelativeBase(long p1, int i1)
        {
            relativeBase += i1 == 0 ? ReadMemory(intCode[p1]) : i1 == 1 ? ReadMemory(p1) : ReadMemory(intCode[p1] + relativeBase);
            opPointer += 2;
        }

        private void Add(long p1, long p2, long p3, int i1, int i2, int i3)
        {
            var v1 = i1 == 0 ? ReadMemory(intCode[p1]) : i1 == 1 ? ReadMemory(p1) : ReadMemory(intCode[p1] + relativeBase);
            var v2 = i2 == 0 ? ReadMemory(intCode[p2]) : i2 == 1 ? ReadMemory(p2) : ReadMemory(intCode[p2] + relativeBase);

            if (i3 == 2)
            {
                intCode[intCode[p3] + relativeBase] = v1 + v2;
            }
            else
            {
                intCode[intCode[p3]] = v1 + v2;
            }

            opPointer += 4;
        }

        private void Multiply(long p1, long p2, long p3, int i1, int i2, int i3)
        {
            var v1 = i1 == 0 ? ReadMemory(intCode[p1]) : i1 == 1 ? ReadMemory(p1) : ReadMemory(intCode[p1] + relativeBase);
            var v2 = i2 == 0 ? ReadMemory(intCode[p2]) : i2 == 1 ? ReadMemory(p2) : ReadMemory(intCode[p2] + relativeBase);

            if (i3 == 2)
            {
                intCode[intCode[p3] + relativeBase] = v1 * v2;
            }
            else
            {
                intCode[intCode[p3]] = v1 * v2;
            }

            opPointer += 4;
        }

        private void Input(long p1, int i1, long input)
        {
            if (i1 == 2)
            {
                intCode[intCode[p1] + relativeBase] = input;
            }
            else
            {
                intCode[intCode[p1]] = input;
            }

            opPointer += 2;
        }

        private long Output(long p1, int i1)
        {
            var v1 = i1 == 0 ? ReadMemory(intCode[p1]) : i1 == 1 ? ReadMemory(p1) : ReadMemory(intCode[p1] + relativeBase);
            Outputs.Push(v1);
            IsHalted = true;
            opPointer += 2;

            return v1;
        }

        private void JumpNZ(long p1, long p2, long i1, int i2)
        {
            var v1 = i1 == 0 ? ReadMemory(intCode[p1]) : i1 == 1 ? ReadMemory(p1) : ReadMemory(intCode[p1] + relativeBase);

            if (v1 != 0)
            {
                opPointer = i2 == 0 ? ReadMemory(intCode[p2]) : i2 == 1 ? ReadMemory(p2) : ReadMemory(intCode[p2] + relativeBase);
            }
            else
            {
                opPointer++;
            }
        }

        private void JumpZ(long p1, long p2, long i1, int i2)
        {
            var v1 = i1 == 0 ? ReadMemory(intCode[p1]) : i1 == 1 ? ReadMemory(p1) : ReadMemory(intCode[p1] + relativeBase);

            if (v1 == 0)
            {
                opPointer = i2 == 0 ? ReadMemory(intCode[p2]) : i2 == 1 ? ReadMemory(p2) : ReadMemory(intCode[p2] + relativeBase);
            }
            else
            {
                opPointer++;
            }
        }

        private void LessThan(long p1, long p2, long p3, int i1, int i2, int i3)
        {
            var v1 = i1 == 0 ? ReadMemory(intCode[p1]) : i1 == 1 ? ReadMemory(p1) : ReadMemory(intCode[p1] + relativeBase);
            var v2 = i2 == 0 ? ReadMemory(intCode[p2]) : i2 == 1 ? ReadMemory(p2) : ReadMemory(intCode[p2] + relativeBase);

            long result;

            if (v1 < v2)
            {
                result = 1;
            }
            else
            {
                result = 0;
                intCode[intCode[p3]] = 0;
            }

            if (i3 == 2)
            {
                intCode[intCode[p3] + relativeBase] = result;
            }
            else
            {
                intCode[intCode[p3]] = result;
            }

            

            opPointer += 1;
        }

        private void Equals(long p1, long p2, long p3, int i1, int i2, int i3)
        {
            var v1 = i1 == 0 ? ReadMemory(intCode[p1]) : i1 == 1 ? ReadMemory(p1) : ReadMemory(intCode[p1] + relativeBase);
            var v2 = i2 == 0 ? ReadMemory(intCode[p2]) : i2 == 1 ? ReadMemory(p2) : ReadMemory(intCode[p2] + relativeBase);

            long result;

            if (v1 == v2)
            {
                result = 1;
            }
            else
            {
                result = 0;
                intCode[intCode[p3]] = 0;
            }

            if (i3 == 2)
            {
                intCode[intCode[p3] + relativeBase] = result;
            }
            else
            {
                intCode[intCode[p3]] = result;
            }

            opPointer += 1;
        }

        private long ReadMemory(long address)
        {
            if (intCode.ContainsKey(address))
                return intCode[address];

            return 0;
        }

        private void GetInput(string input)
        {
            var stringCode = input.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            intCode = new Dictionary<long, long>();

            for (int i = 0; i < stringCode.Count; i++)
            {
                intCode[i] = Convert.ToInt64(stringCode[i]);
            }
        }
    }
}
