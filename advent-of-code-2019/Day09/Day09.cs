using System.Collections.Generic;
using advent_of_code_2019.Common;

namespace advent_of_code_2019.Day09
{
    public class Day09
    {
        public long Part1(string program, long initialInput)
        {
            var CPU = new Cpu(program, new Queue<long>());
            CPU.Inputs.Enqueue(initialInput);
            return CPU.Run();
        }

        public long Test(string program)
        {
            var CPU = new Cpu(program, new Queue<long>());
            return CPU.Run();
        }
    }
}
