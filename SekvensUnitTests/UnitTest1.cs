using System;
using System.Linq;
using FluentAssertions;
using Sekvens;
using Xunit;

namespace SekvensUnitTests
{
    public class SequenceTest
    {

        [Fact]
        public void IsSequence()
        {
            var sequenceA = new Sequence("CATAGCCCACCAGGAGGCA");

            var actual = sequenceA.GetPositions(new Sequence("TAG"));

            actual.Should().BeEquivalentTo(new[] { 2 });
        }

        [Fact]
        public void IsSequence_B()
        {
            var sequenceA = new Sequence("CATAGCCCACCAGGAGGCA");

            var actual = sequenceA.GetPositions(new Sequence("GGCA"));

            actual.Should().BeEquivalentTo(new[] { 15 });
        }

        [Theory]
        [InlineData("CATA", "TA", 2)]
        [InlineData("CAGA", "TA")]
        [InlineData("CAGACATA", "TA", 6)]
        [InlineData("CAGACATACAGA", "TA", 6)]
        [InlineData("CAGACAGACAGACAGACAGACATA", "TA", 22)]
        [InlineData("CAGACAGACAGACAGACAGACATA", "CTA")]
        [InlineData("CAGACAGACTAGACAGACAGACATACAAACAGACATAACAGACATAACAGATTGTGGTATTATATTTTTTTTAACAGACATAACAGATTGTGGTACCGCGGGC", "TAACAGACATAACAGATTGTGGT", 35, 71)]
        [InlineData("CAGACAGACTAGACAGACAGACATA", "CA", 0, 4, 13, 17, 21)]
        [InlineData("CAGACAGACAGACAGACAGACATA", "TAG")]
        [InlineData("CACACACA", "CA", 0, 2, 4, 6)]
        [InlineData("CACANTGAGGACAGT", "CA", 0, 2, 11)]
        [InlineData("CATACAGA", "TA", 2)]
        [InlineData("CATACAGACGGCGCACTCACCAC", "TA", 2)]
        [InlineData("TA", "CATACAGATA")]
        [InlineData("CATG", "W", 1, 2)]
        [InlineData("CATG", "N", 0, 1, 2, 3)]
        [InlineData("CATG", "V", 0, 1, 3)]
        [InlineData("CATG", "Y", 0, 2)]
        [InlineData("CATG", "S", 0, 3)]
        [InlineData("CATG", "R", 1, 3)]
        [InlineData("CATG", "M", 0, 1)]
        [InlineData("CATG", "K", 2, 3)]
        [InlineData("CATG", "H", 0, 1, 2)]
        [InlineData("CATG", "D", 1, 2, 3)]
        [InlineData("CATG", "B", 0, 2, 3)]
        public void IsSequence_(string longString, string subString, params int[] expected)
        {
            var sequenceA = new Sequence(longString);

            var actual = sequenceA.GetPositions(new Sequence(subString)).ToArray();

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("C")]
        [InlineData("CA")]
        [InlineData("CCT")]
        [InlineData("CGTA")]
        [InlineData("CAGAT")]
        [InlineData("CAGACA")]
        [InlineData("CAGACATACAGA")]
        [InlineData("CAGACAGACAGACAGACAGACATA")]
        [InlineData("CAGACAGACAGAAAAAAAAACAGACAGACATA")]
        [InlineData("CAGACAGACTAAGACAGACAGACATA")]
        [InlineData("CAGACAGACTAGACAGACAGACATA")]
        [InlineData("CAGACAGACATAGACAGACAGACATA")]
        [InlineData("CATACAGA")]
        [InlineData("CAWACAGA")]
        [InlineData("CANACAGA")]
        [InlineData("TACHWNBDHKMRSYV")]
        [InlineData("T")]
        [InlineData("A")]
        [InlineData("H")]
        [InlineData("W")]
        [InlineData("N")]
        [InlineData("B")]
        [InlineData("D")]
        [InlineData("K")]
        [InlineData("M")]
        [InlineData("R")]
        [InlineData("S")]
        [InlineData("Y")]
        [InlineData("V")]
        [InlineData("KMRSYV")]
        [InlineData("")]
        public void ToString_CanReverseEncode(string longString)
        {
            var sequenceA = new Sequence(longString);

            var actual = sequenceA.ToString();

            actual.ToString().Should().Be(longString);
        }

        [Theory]
        [InlineData("C")]
        [InlineData("CA")]
        [InlineData("CCT")]
        [InlineData("CGTA")]
        [InlineData("CAGAT")]
        [InlineData("CAGACA")]
        [InlineData("CAGACATACAGA")]
        [InlineData("CAGACAGACAGACAGACAGACATA")]
        [InlineData("CAGACAGACAGAAAAAAAAACAGACAGACATA")]
        [InlineData("CAGACAGACTAAGACAGACAGACATA")]
        [InlineData("CAGACAGACTAGACAGACAGACATA")]
        [InlineData("CAGACAGACATAGACAGACAGACATA")]
        [InlineData("CATACAGA")]
        [InlineData("W")]
        [InlineData("N")]
        public void Ctor_ThenInitializeWithoutExceptions(string longString)
        {
            Action act = () => new Sequence(longString);

            act.Should().NotThrow<Exception>();
        }

    }
}
