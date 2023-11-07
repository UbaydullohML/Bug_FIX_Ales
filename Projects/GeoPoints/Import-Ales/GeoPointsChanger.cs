using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Accord.MachineLearning;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace argosgcs.argosGCS.PatrolScheduler
{
    internal class GeoClassChanger
    {
        public class GeoPointstest
        {
            public int Index { get; set; }
            public string Type { get; set; }
            public double WP_Lat { get; set; }
            public double WP_Lon { get; set; }
            public double WP_Alt { get; set; }
            public int Caution { get; set; }
        }

        public class GeopointsTaker
        {
            public void ProcessData(string inputCsvFile)
            {
                var records = LoadDataFromCSV(inputCsvFile);
                // Use the original latitude and longitude values for clustering
                var X1 = records.Select(r => new double[] { r.WP_Lat, r.WP_Lon }).ToArray();

                int n_clusters = 200;
                KMeans kmeans = new KMeans(n_clusters);
                KMeansClusterCollection clusters = kmeans.Learn(X1);

                var centroids = clusters.Centroids;
                // Create a HashSet to store the selected data points closest to each centroid
                HashSet<GeoClassChanger.GeoPointstest> selectedPoints = new HashSet<GeoClassChanger.GeoPointstest>();

                // Iterate through each centroid in the list of centroids
                foreach (double[] centroid in centroids)
                {
                    // Initialize a variable to keep track of minimum distance, starting with a large value
                    double minDistance = double.MaxValue;
                    // Initialize a variable to store data point that is currently the closest to centroid
                    GeoClassChanger.GeoPointstest closestPoint = null;

                    // Iterate through each data point in the list of records
                    foreach (GeoClassChanger.GeoPointstest dataPoint in records)
                    {
                        // Check if the data point has a non-zero caution value
                        if (dataPoint.Caution != 0)
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
                                // If it is, update min distance and set closest point to the current data point
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

                WriteSelectedPointsToCSV(sortedSelectedPoints, "ProgramOrderedCaution.csv");
            }

            private List<GeoClassChanger.GeoPointstest> LoadDataFromCSV(string csvFileName)
            {
                using (var reader = new StreamReader(csvFileName))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    return csv.GetRecords<GeoClassChanger.GeoPointstest>().ToList();
                }
            }

            private void WriteSelectedPointsToCSV(IEnumerable<GeoClassChanger.GeoPointstest> selectedPoints, string outputFileName)
            {
                using (var writer = new StreamWriter(outputFileName))
                using (var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csvWriter.WriteRecords(selectedPoints);
                }
            }

            private double CalculateDistance(double[] centroid, GeoClassChanger.GeoPointstest dataPoint)
            {
                double latDistance = centroid[0] - dataPoint.WP_Lat;
                double lonDistance = centroid[1] - dataPoint.WP_Lon;
                return Math.Sqrt(latDistance * latDistance + lonDistance * lonDistance);
            }
        }


    }
}

