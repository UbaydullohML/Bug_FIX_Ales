using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Accord.MachineLearning;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class GeoPointstest
{
    public int Index { get; set; }
    public string Type { get; set; }
    public double WP_Lat { get; set; }
    public double WP_Lon { get; set; }
    public double WP_Alt { get; set; }
    public int Caution { get; set; }
    public bool Sampled { get; set; }
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

        int n_clusters = 200;
        KMeans kmeans = new KMeans(n_clusters);
        KMeansClusterCollection clusters = kmeans.Learn(X1);

        var centroids = clusters.Centroids;
        // Create a HashSet to store the selected data points closest to each centroid
        HashSet<GeoPointstest> selectedPoints = new HashSet<GeoPointstest>();

        // Iterate through each centroid in the list of centroids
        foreach (double[] centroid in centroids)
        {
            // Initialize a variable to keep track of minimum distance, starting with a large value
            double minDistance = double.MaxValue;
            // Initialize a variable to store data point that is currently the closest to centroid
            GeoPointstest closestPoint = null;

            // Iterate through each data point in the list of records
            foreach (GeoPointstest dataPoint in records)
            {
                // check if the data points sampled property is true
                if (dataPoint.Sampled)
                {
                    selectedPoints.Add(dataPoint);
                }

                // Check if the data point has a non-zero caution value
                else if (dataPoint.Caution != 0)
                {
                    // Add data point to selectedPoints
                    selectedPoints.Add(dataPoint);
                }
                else
                {
                    // Calculate distance between current centroid and the current data point
                    double distance = CalculateDistance(centroid, dataPoint);
                    // Check if the calculated distance is smaller than the current minimum distance
                    if (distance < minDistance)
                    {
                        // If it is, update min distance and set closest point to current data point
                        minDistance = distance;
                        closestPoint = dataPoint;
                    }
                }
            }

            // Add data point that is closest to the current centroid to selectedPoints hashset
            if (closestPoint != null)
            {
                selectedPoints.Add(closestPoint);
            }
        }

        // Sort the selected data points by Index column in ascending order 
        var sortedSelectedPoints = selectedPoints.OrderBy(p => p.Index);

        // Create a CSV writer to write the unique selected data points to a new CSV file
        using var writer = new StreamWriter("ProgramOrderedCaution1.csv");
        using var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
        csvWriter.WriteRecords(sortedSelectedPoints);
    }

    private static double CalculateDistance(double[] centroid, GeoPointstest dataPoint)
    {
        // Calculate the difference in lat between centroid and data point
        double latDistance = centroid[0] - dataPoint.WP_Lat;
        double lonDistance = centroid[1] - dataPoint.WP_Lon;
        // Calculate Euclidean distance by taking the square root of the sum of squared differences
        return Math.Sqrt(latDistance * latDistance + lonDistance * lonDistance);
    }
}
