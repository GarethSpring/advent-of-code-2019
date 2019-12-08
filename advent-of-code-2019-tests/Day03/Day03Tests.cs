using Xunit;

namespace advent_of_code_2019_tests.Day03
{
    public class Day03Tests
    {
        [Fact]
        public void Day03Part1()
        {
            var day = new advent_of_code_2019.Day03.Day03();

            int actual = day.Part1();

            Assert.True(actual.Equals(266));
        }


        [Fact]
        public void Day03Part2()
        {
            var day = new advent_of_code_2019.Day03.Day03();

            int actual = day.Part2();

            Assert.True(actual.Equals(19242));
        }
    }
}
