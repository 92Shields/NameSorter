using NameSorter.Models;
using NameSorter.Utilities;

namespace NameSorterTests
{
    public class NameUtilsTests
    {
        [Fact]
        public void SortNames_SortsByLastNameThenGivenNames()
        {
            var names = new List<Name>
            {
                new Name("John Smith"),
                new Name("Alice Smith"),
                new Name("Bob Anderson"),
                new Name("Jane Doe")
            };

            var sorted = NameUtils.SortNames(names);

            var expectedOrder = new[]
            {
                "Bob Anderson",
                "Jane Doe",
                "Alice Smith",
                "John Smith"
            };
            Assert.Equal(expectedOrder, sorted.ConvertAll(n => n.ToString()));
        }

        [Fact]
        public void SortNames_SortsByGivenNames_WhenLastNamesAreEqual()
        {
            var names = new List<Name>
            {
                new Name("Alice Catherine Smith"),
                new Name("John Smith"),
                new Name("Alice Catherine June Smith"),
                new Name("Alice Cate Smith"),
                new Name("Alice Smith"),
                new Name("Bob Smith"),
                new Name("Alice Catherine Jane Smith"),
            };

            var sorted = NameUtils.SortNames(names);

            var expectedOrder = new[]
            {
                "Alice Smith",
                "Alice Cate Smith",
                "Alice Catherine Smith",
                "Alice Catherine Jane Smith",
                "Alice Catherine June Smith",
                "Bob Smith",
                "John Smith"
            };
            Assert.Equal(expectedOrder, sorted.ConvertAll(n => n.ToString()));
        }

        [Fact]
        public void SortNames_ReturnsEmptyCollection_WhenInputIsNull()
        {
            var result = NameUtils.SortNames(null);
            
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void SortNames_ReturnsEmptyCollection_WhenInputIsEmpty()
        {
            var result = NameUtils.SortNames(new List<Name>());
            
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
