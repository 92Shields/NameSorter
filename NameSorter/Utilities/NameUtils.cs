using NameSorter.Models;

namespace NameSorter.Utilities
{
    public static class NameUtils
    {
        public static List<Name> SortNames(List<Name> names)
        {
            if (names == null || names.Count == 0)
            {
                return [];
            }

            return names
                .OrderBy(n => n.LastName)
                .ThenBy(n => string.Join(" ", n.GivenNames))
                .ToList();
        }
    }
}
