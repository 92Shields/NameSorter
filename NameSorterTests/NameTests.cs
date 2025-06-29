using NameSorter.Models;

namespace NameSorterTests
{
    public class NameTests
    {
        [Theory]
        [InlineData("A B C ", new[] { "A", "B" }, "C")]
        [InlineData(" A B C", new[] { "A", "B" }, "C")]
        [InlineData("Janet Parsons", new[] { "Janet" }, "Parsons")]
        [InlineData("Adonis Julius Archer", new[] { "Adonis", "Julius" }, "Archer")]
        [InlineData("Hunter Uriah Mathew Clarke", new[] { "Hunter", "Uriah", "Mathew" }, "Clarke")]
        [InlineData("A B C D", new[] { "A", "B", "C" }, "D")]
        public void Constructor_ParsesValidFullName(string fullName, string[] expectedGiven, string expectedLast)
        {
            var name = new Name(fullName);

            Assert.Equal(expectedGiven, name.GivenNames);
            Assert.Equal(expectedLast, name.LastName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_ThrowsArgumentException_OnNullOrEmpty(string fullName)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Name(fullName));
            Assert.Contains("cannot be null or empty", ex.Message);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("A ")]
        [InlineData("Smith")]
        [InlineData(" A B C D E")]
        [InlineData("A B C D E ")]
        [InlineData("Hunter Uriah Mathew Maxwell Clarke")]
        public void Constructor_ThrowsFormatException_OnInvalidWordCount(string fullName)
        {
            // Act & Assert
            var ex = Assert.Throws<FormatException>(() => new Name(fullName));
            Assert.Contains("A name must have 1 to 4 words", ex.Message);
        }

        [Theory]
        [InlineData("Janet Parsons")]
        [InlineData("Adonis Julius Archer")]
        [InlineData("Hunter Uriah Mathew Clarke")]
        public void ToString_ReturnsConcatenatedString(string fullName)
        {
            var name = new Name(fullName);
            
            var result = name.ToString();

            Assert.Equal(fullName, result);
        }
    }
}
