namespace NameSorter.Models
{
    public class Name
    {
        public IReadOnlyCollection<string> GivenNames { get; }
        public string LastName { get; }

        public Name(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                // TODO: replace with proper error logger.
                throw new ArgumentException("The full name cannot be null or empty");
            }

            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (names.Length < 2 || names.Length > 4)
                throw new FormatException("A name must have 1 to 4 words (1-3 given names, 1 last name).");

            var givenNames = names.Take(names.Length - 1).ToList();
            var lastName = names.Last();


            GivenNames = givenNames;
            LastName = lastName;
        }

        public override string ToString()
        {
            return string.Join(" ", GivenNames.Append(LastName));
        }
    }
}
