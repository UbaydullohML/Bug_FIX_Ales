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

        int n_clusters = 200;
        KMeans kmeans = new KMeans(n_clusters);
        KMeansClusterCollection clusters = kmeans.Learn(X1);


        var centroids = clusters.Centroids;
        // Create a HashSet to store the selected data points closest to each centroids
        HashSet<GeoPointstest> selectedPoints = new HashSet<GeoPointstest>();
        // Iterate through each centroid in the list of centroids
        foreach (double[] centroid in centroids)
        {
            // initialize a variable to keep track of minimum distance, starting with large value
            double minDistance = double.MaxValue;
            // initialize a variable to store data point that is currently the closesest to centroid
            GeoPointstest closestPoint = null;

            // iterate through each data point in list of records
            foreach (GeoPointstest dataPoint in records)
            {
                // calculate distance between current centroid and the current data point
                double distance = CalculateDistance(centroid, dataPoint);
                // check if th calculated distance is smaller than current minimum distance
                if (distance < minDistance)
                {
                    // if it is, update min distance and set closest point to current data point
                    minDistance = distance;
                    closestPoint = dataPoint;
                }
            }

            // add data point that is closest to current centroid to selectedPoints hashset
            if (closestPoint != null)
            {
                selectedPoints.Add(closestPoint);
            }
        }

        // sort the selected data points by Index column in ascending order 
        var sortedSelectedPoints1 = selectedPoints.OrderBy(p => p.Index);

        // Create a CSV writer to write the unique selected data points to a new CSV file
        using var writer1 = new StreamWriter("ProgramOrderedCaution.csv");
        using var csvWriter1 = new CsvWriter(writer1, new CsvConfiguration(CultureInfo.InvariantCulture));
        csvWriter1.WriteRecords(sortedSelectedPoints1);
    }

    
    private static double CalculateDistance(double[] centroid, GeoPointstest dataPoint)
    {
        // calculate the difference in lat between centroid and data point
        double latDistance = centroid[0] - dataPoint.WP_Lat;
        double lonDistance = centroid[1] - dataPoint.WP_Lon; // calculate difference in lon bet centroid and data point
        return Math.Sqrt(latDistance * latDistance + lonDistance * lonDistance); // calculate eucledian distance by taking square root of sum of squared difference
    }
}
