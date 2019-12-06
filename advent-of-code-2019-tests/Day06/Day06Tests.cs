using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace advent_of_code_2019_tests.Day06
{
    public class Day06Tests
    {
        [Fact]
        public void Day06Part1()
        {
            var day = new advent_of_code_2019.Day06.Day06();

            int actual = day.Part1();

            Assert.True(actual.Equals(308790));
        }

        [Fact]
        public void Day06Part2()
        {
            var day = new advent_of_code_2019.Day06.Day06();

            int actual = day.Part2();

            Assert.True(actual.Equals(472));
        }
    }
}
