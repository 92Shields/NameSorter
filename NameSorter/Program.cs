using NameSorter.Models;
using NameSorter.Utilities;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Incorrect number of parameters provided.");
            return;
        }

        string filePath = args[0];
        try
        {
            // Read names from the file
            var lines = await FileUtils.ReadNamesFromFile(filePath);
            if (lines.Length == 0)
            {
                Console.WriteLine("No names found in the file.");
                return;
            }

            var names = lines.Select(line => new Name(line)).ToList();

            // Sort names alphabetically
            var sortedNames = NameUtils.SortNames(names);

            // Save sorted names back to file
            await FileUtils.WriteNamesToFile("sorted-names-list.txt", sortedNames);

            // Print sorted names
            foreach (var name in sortedNames)
            {
                Console.WriteLine(name.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}