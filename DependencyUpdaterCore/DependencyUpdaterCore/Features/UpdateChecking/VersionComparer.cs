using DependencyUpdaterCore.Models;
using System.Collections.Generic;

namespace DependencyUpdaterCore.Features.UpdateChecking
{
    public class VersionComparer : IComparer<Version>
    {

        public int Compare(Version first, Version second)
        {
            var majorVersionComparison = first.Major.CompareTo(second.Major);

            if (majorVersionComparison != 0)
            {
                return majorVersionComparison;
            }

            var minorVersionComparison = first.Minor.CompareTo(second.Minor);

            if (minorVersionComparison != 0)
            {
                return minorVersionComparison;
            }

            return first.Patch.CompareTo(second.Patch);
        }
    }
}
