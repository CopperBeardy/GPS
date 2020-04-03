namespace MapUtils
{
    using System;
    using Geocoder.API.Address;
    using static System.String;
    using static System.StringSplitOptions;

    public static partial class Lookup
    {
        private static readonly GeoSearcher geo = new GeoSearcher();
        public static string DisplayName(double latitude, double longitude) // Work in progress..
        {
            geo.Lookup(latitude, longitude, out var displayName);

            return displayName;
        }

        public static string ShortName(double latitude, double longitude) // Still under construction...
        {
            var adr = geo.Lookup(latitude, longitude, out _);

            if (adr is null) return Empty;

            string grp1, grp2, grp3, grp4, grp6, grp7, grp8; // Find out the `right´ order for each group.

            grp1 = adr.Name.Z('n');
            grp2 = TakeN(new string[] { Join(' ', new string[] { adr.HouseNumber.Z('h'), adr.Road.Z('r') }).Trim(), adr.PostCode.Z('p') });
            grp3 = TakeN(new string[] { adr.Hamlet.Z('h'), adr.Village.Z('v'), adr.Suburb.Z('s') });
            grp4 = TakeN(new string[] { adr.Town.Z('t'), adr.City.Z('c') });
            grp6 = TakeN(new string[] { adr.County.Z('c'), adr.District.Z('d') });
            grp7 = TakeN(new string[] { adr.Region.Z('r'), adr.State.Z('s') });
            grp8 = TakeN(new string[] { adr.Country.Z('c'), adr.CountryCode.Z('y') });

            return TakeN(new string[] { grp1, grp2, grp3, grp4, grp6, grp7, grp8 }, 3); // Return the 3 most significant fields (if they exist).
        }
        static string Z(this string s, char ch) => IsNullOrEmpty(s) ? Empty : $"{s} ({ch})"; // Add temp debug suffix.
        static string TakeN(string[] fields, int n = 1) // Return max N fields (default 1 field).
        {
            var arr = Join('|', fields).TrimStart('|').Split('|', RemoveEmptyEntries);
            return Join(", ", arr, 0, Math.Min(n, arr.Length));
        }
    }
}

/*               Group   Order  (regroup/reorder?)
------------------------------
Pedestrian         0       -

Name               1       ?

HouseNumber        2       1
Road               2       2
PostCode           2       3

Hamlet             3       1
Village            3       2
Suburb             3       3

Town               4       1
City               4       2

Neighborhood       5       -

County             6       1
District           6       2

Region             7       1
State              7       2

CountryCode        8
Country            8
*/
