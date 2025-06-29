using NameSorter.Models;
using NameSorter.Utilities;

namespace NameSorterTests
{
    public class FileUtilsTests
    {
        [Fact]
        public async Task ReadNamesFromFile_WhenFileDoesntExist_ReturnsEmptyArray()
        {
            var result = await FileUtils.ReadNamesFromFile("nonexistent.txt");

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReadNamesFromFile_WhenFileReadException_ReturnsEmptyArray()
        {
            var tempFile = Path.GetTempFileName();
            await File.WriteAllTextAsync(tempFile, "Test");

            // Lock the file by opening a FileStream with no sharing allowed
            using (var stream = new FileStream(tempFile, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var result = await FileUtils.ReadNamesFromFile(tempFile);

                Assert.NotNull(result);
                Assert.Empty(result);
            }

            File.Delete(tempFile);
        }

        [Fact]
        public async Task ReadNamesFromFile_WhenOk_ReturnsArrayOfNames()
        {
            var expectedNames = new[] { "Janet Parsons", "Vaughn Lewis", "Adonis Julius Archer", "Shelby Nathan Yoder", "Marin Alvarez" };
            var tempFile = Path.GetTempFileName();
            await File.WriteAllLinesAsync(tempFile, expectedNames);

            try
            {
                var result = await FileUtils.ReadNamesFromFile(tempFile);

                Assert.NotNull(result);
                Assert.NotEmpty(result);
                Assert.Equal(expectedNames, result);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public async Task WriteNamesToFile_WhenOk_WritesNamesSuccessfully()
        {
            var tempFile = Path.GetTempFileName();
            var nameStrings = new[] { "Janet Parsons", "Vaughn Lewis", "Adonis Julius Archer", "Shelby Nathan Yoder", "Marin Alvarez" };
            var names = nameStrings.Select(s => new Name(s)).ToList();

            try
            {
                var result = await FileUtils.WriteNamesToFile(tempFile, names);

                var written = await File.ReadAllLinesAsync(tempFile);
                Assert.True(result);
                Assert.NotNull(written);
                Assert.NotEmpty(written);
                Assert.Equal(nameStrings, written);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public async Task WriteNamesToFile_ReturnsFalse_WhenFilePathIsNull()
        {
            string filePath = null;
            var names = new[] { new Name("Janet Parsons"), new Name("Vaughn Lewis") };

            var result = await FileUtils.WriteNamesToFile(filePath, names);

            Assert.False(result);
        }

        [Fact]
        public async Task WriteNamesToFile_ReturnsFalse_WhenFilePathIsEmpty()
        {
            string filePath = "";
            var names = new[] { new Name("Janet Parsons"), new Name("Vaughn Lewis") };

            var result = await FileUtils.WriteNamesToFile(filePath, names);
            Assert.False(result);
        }

        [Fact]
        public async Task WriteNamesToFile_ReturnsFalse_WhenNamesIsNull()
        {
            var tempFile = Path.GetTempFileName();
            IEnumerable<Name> names = null;

            try
            {
                var result = await FileUtils.WriteNamesToFile(tempFile, names);
                Assert.False(result);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public async Task WriteNamesToFile_WritesEmptyFile_WhenNamesIsEmpty()
        {
            var tempFile = Path.GetTempFileName();
            var names = Enumerable.Empty<Name>();

            try
            {
                await FileUtils.WriteNamesToFile(tempFile, names);

                var written = await File.ReadAllLinesAsync(tempFile);

                Assert.NotNull(written);
                Assert.Empty(written);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }
}