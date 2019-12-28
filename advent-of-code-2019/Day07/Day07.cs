using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using advent_of_code_2019.Common;

namespace advent_of_code_2019.Day07
{
    public class Day07
    {
        public long GetHighestSignal(string program)
        {
            long output = 0;
            string ps = string.Empty;

            var combinations = GetPermutations(new List<int> { 0, 1, 2, 3, 4 });

            foreach (var c in combinations)
            {
                var d = c.ToList();
                var result = Amplify(program, d[0],d[1],d[2],d[3],d[4], 0);
                if (result > output)
                {
                    output = result;
                    ps = $"{d[0]},{d[1]},{d[2]},{d[3]},{d[4]}";
                }
            }

            Debug.WriteLine(ps);
            return output;
        }


        public long GetHighestSignal2(string program)
        {
            long output = 0;
            string ps = string.Empty;
            long result = 0;

            var combinations = GetPermutations(new List<int> { 5, 6, 7, 8, 9 });

            int counter = 0;
            foreach (var c in combinations)
            {
                counter++;
                var d = c.ToList();
                result = Amplify2(program, d[0], d[1], d[2], d[3], d[4], 0);
                if (result > output)
                {
                    output = result;
                    ps = $"Count: {counter} : {d[0]},{d[1]},{d[2]},{d[3]},{d[4]}";
                    Debug.WriteLine(ps);
                }

                Debug.WriteLine($"Count: {counter} Result: {result}");
            }

            Debug.WriteLine(ps);
            return output;
        }

        public long Amplify(string program, long p1, long p2, long p3, long p4, long p5, long initialInput)
        {
            var CPU = new Cpu(program, GetQueue(p1, initialInput));
            var output = CPU.Run();

            CPU = new Cpu(program, GetQueue(p2, output));
            output = CPU.Run();

            CPU = new Cpu(program, GetQueue(p3, output));
            output = CPU.Run();

            CPU = new Cpu(program, GetQueue(p4, output));
            output = CPU.Run();

            CPU = new Cpu(program, GetQueue(p5, output));
            output = CPU.Run();

            return output;
        }

        private Queue<long> GetQueue(int input1)
        {
            var q = new Queue<long>();
            q.Enqueue(input1);
            return q;
        }

        private Queue<long> GetQueue(long input1, long input2)
        {
            var q = new Queue<long>();
            q.Enqueue(input1);
            q.Enqueue(input2);
            return q;
        }

        public long Amplify2(string program, int p1, int p2, int p3, int p4, int p5, int initialInput)
        {
            var CPUs = new List<Cpu>();
            var CPU1 = new Cpu(program, GetQueue(p1));
            var CPU2 = new Cpu(program, GetQueue(p2));
            var CPU3 = new Cpu(program, GetQueue(p3));
            var CPU4 = new Cpu(program, GetQueue(p4));
            var CPU5 = new Cpu(program, GetQueue(p5));
            CPUs.Add(CPU1);
            CPUs.Add(CPU2);
            CPUs.Add(CPU3);
            CPUs.Add(CPU4);
            CPUs.Add(CPU5);

            long output = initialInput;

            while (CPUs.Any(c => !c.IsFinished))
            {
                CPU1.Inputs.Enqueue(output);
                CPU1.Run();
                if (!CPU1.IsFinished)
                {
                    output = CPU1.Outputs.Pop();
                }

                CPU2.Inputs.Enqueue(output);
                CPU2.Run();
                if (!CPU2.IsFinished)
                {
                    output = CPU2.Outputs.Pop();
                }

                CPU3.Inputs.Enqueue(output);
                CPU3.Run();
                if (!CPU3.IsFinished)
                {
                    output = CPU3.Outputs.Pop();
                }

                CPU4.Inputs.Enqueue(output);
                CPU4.Run();
                if (!CPU4.IsFinished)
                {
                    output = CPU4.Outputs.Pop();
                }

                CPU5.Inputs.Enqueue(output);
                CPU5.Run();
                if (!CPU5.IsFinished)
                { 
                    output = CPU5.Outputs.Pop();
                }
            }

            return output;
        }

        private ICollection<ICollection<T>> GetPermutations<T>(ICollection<T> list)
        {
            var result = new List<ICollection<T>>();
            if (list.Count == 1)
            {
                // Just one
                result.Add(list);
                return result;
            }

            foreach (var element in list)
            {
                var remainingList = new List<T>(list);
                remainingList.Remove(element);

                foreach (var perm in GetPermutations(remainingList))
                {
                    perm.Add(element);
                    result.Add(perm);
                }
            }

            return result;
        }
    }
}