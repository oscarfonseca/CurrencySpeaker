using CurrencySpeakerServer.Parser;
using NUnit.Framework;

namespace CurrencySpeakerServerTests
{
    public class SpeakerParserTests
    {
        [TestCase("0", "zero dollars")]
        [TestCase("0,01", "zero dollars and one cent")]
        [TestCase("1", "one dollar")]
        [TestCase("25,1", "twenty-five dollars and ten cents")]
        [TestCase("38", "thirty-eight dollars")]
        [TestCase("99", "ninety-nine dollars")]
        [TestCase("90", "ninety dollars")]
        [TestCase("931", "nine hundred thirty-one dollars")]
        [TestCase("1000", "one thousand dollars")]
        [TestCase("45100", "forty-five thousand one hundred dollars")]
        [TestCase("500000000,02", "five hundred million dollars and two cents")]
        [TestCase("513234657,12", "five hundred thirteen million two hundred thirty-four thousand six hundred fifty-seven dollars and twelve cents")]
        [TestCase("999999999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public void ItParsesNumbers(string input, string expected)
        {
            var parser = new SpeakerParser();

            var result = parser.Parse(input);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}