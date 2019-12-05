using Xunit;

namespace advent_of_code_2019_tests.Day04
{
    public class Day04Tests
    {
        [Fact]
        public void Day04Part1()
        {
            var day = new advent_of_code_2019.Day04.Day04();

            int actual = day.Part1("353096-843212");

            Assert.True(actual.Equals(579));
        }

        [Fact]
        public void Day04Part2()
        {
            var day = new advent_of_code_2019.Day04.Day04();

            int actual = day.Part2("353096-843212");

            Assert.True(actual.Equals(358));
        }
    }
}
