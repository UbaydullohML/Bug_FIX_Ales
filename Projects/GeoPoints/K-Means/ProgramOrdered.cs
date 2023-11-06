using Accord.MachineLearning;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;

public class GeoPointstest
{
    public int Index { get; set; }
    public string Type { get; set; }
    public double WP_Lat { get; set; }
    public double WP_Lon { get; set; }
    public double WP_Alt { get; set; }
    public int Caution { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        using var reader = new StreamReader("영동순찰길_01_26imhh_수정_1004v3.csv");
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

        var records = csv.GetRecords<GeoPointstest>().ToList();

        // Use the original latitude and longitude values for clustering
        var X1 = records.Select(r => new double[] { r.WP_Lat, r.WP_Lon }).ToArray();

        int n_clusters = 100;
        KMeans kmeans = new KMeans(n_clusters);
        KMeansClusterCollection clusters = kmeans.Learn(X1);


        var centroids = clusters.Centroids;
        // Select the data points closest to each centroid as representatives
        HashSet<GeoPointstest> selectedPoints = new HashSet<GeoPointstest>();
        foreach (double[] centroid in centroids)
        {
            double minDistance = double.MaxValue;
            GeoPointstest closestPoint = null;

            foreach (GeoPointstest dataPoint in records)
            {
                double distance = CalculateDistance(centroid, dataPoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPoint = dataPoint;
                }
            }

            if (closestPoint != null)
            {
                selectedPoints.Add(closestPoint);
            }
        }

        var sortedSelectedPoints1 = selectedPoints.OrderBy(p => p.Index);

        // Create a CSV writer to write the unique selected data points to a new CSV file
        using var writer1 = new StreamWriter("unique_records1test4.csv");
        using var csvWriter1 = new CsvWriter(writer1, new CsvConfiguration(CultureInfo.InvariantCulture));
        csvWriter1.WriteRecords(sortedSelectedPoints1);
    }

    private static double CalculateDistance(double[] centroid, GeoPointstest dataPoint)
    {
        double latDistance = centroid[0] - dataPoint.WP_Lat;
        double lonDistance = centroid[1] - dataPoint.WP_Lon;
        return Math.Sqrt(latDistance * latDistance + lonDistance * lonDistance);
    }
}
