namespace GPS_Distance.Helpers
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text.Json;
    using System.Windows.Documents;
    using DistanceCalculator.Helpers;
    using DistanceCalculator.Models;
    using Microsoft.Win32;

    public static partial class Helper // These methods and structure will change during development.
    {
        private static readonly string filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

        public static string ImportFromJson(out Location? startPoint, out Collection<Location> endPoints)
        {
            startPoint = default; // Set out parameters.
            endPoints = new Collection<Location>();

            var open = new OpenFileDialog { Filter = filter };
            if (open.ShowDialog() == false) return string.Empty; // Import canceled.

            try
            {
                var data = new Data();
                try { data = JsonSerializer.Deserialize<Data>(File.ReadAllText(open.FileName)); }
                catch (JsonException) { }

                if (data.startpoint?.Length == 2 && data.endpoints?[0].Length == 2) // At least 1 input ok?
                {
                    startPoint = new Location(data.startpoint[0], data.startpoint[1]);

                    foreach (var item in data.endpoints)// May change as might add a reference name for each of the locations.
                        if (item.Length == 2)
                            endPoints.Add(new Location(item[0], item[1]));
                }

                return Path.GetFileName(open.FileName);
            }
            catch { return "?"; }
        }

        public static string ExportToJson(string startLat, string startLon, Collection<Location> endPoints)
        {
            var save = new SaveFileDialog { FileName = "GPS_Distance", DefaultExt = ".json", Filter = filter };
            if (save.ShowDialog() == false) return string.Empty; // Export canceled.

            try
            {
                var data = new Data();
                data.startpoint = new double[] { startLat.ToDouble(), startLon.ToDouble() };
                data.endpoints = new double[endPoints.Count][];

                for (var i = 0; i < endPoints.Count; i++) // May change as might add a reference name for each of the locations.
                    data.endpoints[i] = new double[] { endPoints[i].Latitude, endPoints[i].Longitude };

                File.WriteAllText(save.FileName, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
                return Path.GetFileName(save.FileName);
            }
            catch { return "?"; }
        }

        private class Data // Will change later on.
        {
            public double[]? startpoint { get; set; }
            public double[][]? endpoints { get; set; }
        }
    }
}
