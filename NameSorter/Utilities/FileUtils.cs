using NameSorter.Models;

namespace NameSorter.Utilities
{
    public static class FileUtils
    {
        public static async Task<string[]> ReadNamesFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"The file '{filePath}' was not found.");
                }

                // Reads all lines and returns them as a string array
                return await File.ReadAllLinesAsync(filePath);
            }
            catch(Exception ex)
            {
                // TODO: replace with proper error logger.
                Console.WriteLine($"Error reading the provided file: {ex.Message}");
                return []; // Return an empty array if an error occurs
            }
        }

        public static async Task<bool> WriteNamesToFile(string filePath, IEnumerable<Name> names)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
                }
                if (names == null)
                {
                    throw new ArgumentException("Names array cannot be null or empty.", nameof(names));
                }

                var lines = names.Select(n => n.ToString());
                await File.WriteAllLinesAsync(filePath, lines);
                return true;
            }
            catch (Exception ex)
            {
                // TODO: replace with proper error logger.
                Console.WriteLine($"Error writing the provided file: {ex.Message}");
                return false;
            }
        }
    }
}
