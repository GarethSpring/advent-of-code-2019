using Xunit;

namespace advent_of_code_2019_tests.Day08
{
    public class Day08Tests
    {
        [Fact]
        public void Day08Part1()
        {
            var day = new advent_of_code_2019.Day08.Day08();

            int actual = day.Part1();

            Assert.True(actual.Equals(1677));
        }

        [Fact]
        public void Day08Part2()
        {
            var day = new advent_of_code_2019.Day08.Day08();

            day.Part2();
        }
    }
}
