using Xunit;

namespace advent_of_code_2019_tests.Day16
{
    public class Day16Tests
    {
        private readonly string input = "59769638638635227792873839600619296161830243411826562620803755357641409702942441381982799297881659288888243793321154293102743325904757198668820213885307612900972273311499185929901117664387559657706110034992786489002400852438961738219627639830515185618184324995881914532256988843436511730932141380017180796681870256240757580454505096230610520430997536145341074585637105456401238209187118397046373589766408080120984817035699228422366952628344235542849850709181363703172334788744537357607446322903743644673800140770982283290068502972397970799328249132774293609700245065522290562319955768092155530250003587007804302344866598232236645453817273744027537630";
        private readonly string example1 = "12345678";
        private readonly string example2 = "80871224585914546619083218645595";
        private readonly string part2Example1 = "03036732577212944063491565474664";

        [Fact]
        public void Day16Example1()
        {
            var day = new advent_of_code_2019.Day16.Day16();

            var actual = day.Part1(example1, 4);

            Assert.True(actual.Equals(01029498));
        }

        [Fact]
        public void Day16Example2()
        {
            var day = new advent_of_code_2019.Day16.Day16();

            var actual = day.Part1(example2, 100);

            Assert.True(actual.Equals(24176176));
        }

        [Fact]
        public void Day16Part1()
        {
            var day = new advent_of_code_2019.Day16.Day16();

            var actual = day.Part1(input, 100);

            Assert.True(actual.Equals(29956495));
        }

        [Fact (Skip = "LongTime")]
        public void Day16Part2()
        {
            var day = new advent_of_code_2019.Day16.Day16();

            var actual = day.Part2(input, 100);

            Assert.True(actual.Equals(73556504));
        }

        [Fact]
        public void Day16Part2Example1()
        {
            var day = new advent_of_code_2019.Day16.Day16();

            var actual = day.Part2(part2Example1, 100);

            Assert.True(actual.Equals(84462026));
        }
    }
}
