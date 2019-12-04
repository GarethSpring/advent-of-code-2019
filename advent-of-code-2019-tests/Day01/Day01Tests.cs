using Xunit;

namespace advent_of_code_2019_tests.Day01
{
    public class Day01Tests
    {
        [Fact]
        public void Day01Part1()
        {
            var day = new advent_of_code_2019.Day01.Day01();

            int actual = day.Part1();

            Assert.True(actual.Equals(3520097));
        }

        [Fact]
        public void Day01Part2()
        {
            var day = new advent_of_code_2019.Day01.Day01();

            int actual = day.Part2();

            Assert.True(actual.Equals(5277255));
        }
    }
}
