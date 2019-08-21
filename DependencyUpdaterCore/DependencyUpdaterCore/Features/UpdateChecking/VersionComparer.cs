using System.Collections.Generic;

namespace DependencyUpdaterCore.Features.UpdateChecking
{
    public class VersionComparer : IComparer<string>
    {
        public int Compare(string first, string second)
        {
            var versions = first.Split('.');
            var otherVersions = second.Split('.');

            var majorVersionComparison = CompareNumbers(versions[0], otherVersions[0]);

            if (majorVersionComparison != 0)
            {
                return majorVersionComparison;
            }

            var minorVersionComparison = CompareNumbers(versions[1], otherVersions[1]);

            if (minorVersionComparison != 0)
            {
                return minorVersionComparison;
            }

            return CompareNumbers(versions[2], otherVersions[2]);
        }

        private int CompareNumbers(string first, string second)
        {
            int.TryParse(first, out int firstInt);
            int.TryParse(second, out int secondInt);

            return firstInt.CompareTo(secondInt);
        }
    }
}
