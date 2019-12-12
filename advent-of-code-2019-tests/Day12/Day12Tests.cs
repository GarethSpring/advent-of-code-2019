using Xunit;

namespace advent_of_code_2019_tests.Day12
{
    public class Day12Tests
    {
        [Fact]
        public void Day12Part1()
        {
            var day = new advent_of_code_2019.Day12.Day12();

            int actual = day.Part1();

            Assert.True(actual.Equals(7687));
        }

        [Fact]
        public void Day12Part2()
        {
            var day = new advent_of_code_2019.Day12.Day12();

            long actual = day.Part2();

            Assert.True(actual.Equals(334945516288044));
        }
    }
}
