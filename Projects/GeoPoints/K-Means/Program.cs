using Accord.MachineLearning; // contains classes needed to perform K-means clustering
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class GeoPointstest  // define a class that representes your data structure
{
    /*    public int WP_ID { get; set; }
        public int PATH_ID { get; set; }*/
    public string Type { get; set; } // class named Geopointstest shows structure of data
    public double WP_Lat { get; set; }
    public double WP_Lon { get; set; }
    public double WP_Alt { get; set; }

    /*    public int Gimbal_aw { get; set; }
        public int Gimbal_Pitch { get; set; }*/
    public int Caution { get; set; }
}

class Program 
{
    static void Main(string[] args) // main method is entry point for program
    {
        using var reader = new StreamReader("영동순찰길_01_26imhh_수정_1004v3.csv");  // stream reader read csv file 
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)); // csvconfiguration is set to use invariantculture 
        // to properly parse of numeric values 


        
        var records = csv.GetRecords<GeoPointstest>().ToList(); // GetRecords method is used to read and parse csv file into list of objects 
        // type GeoPointstest object, records contains all data from csv file, records list
        var X1 = records.Select(r => new double[] { r.WP_Lat, r.WP_Lon }).ToArray(); // new array X1 created to ontain lat, lon of each data point

        
        int n_clusters = 200; // sets number of clusters to partition the data into
        KMeans kmeans = new KMeans(n_clusters); // it creates new KMeans object with specified number of clusters 
        KMeansClusterCollection clusters = kmeans.Learn(X1); // it trains K-means algorithm on data in X1
        // Learn method returns KMeansClusterCollection object that contains centroids of clusters.


        
        var centroids = clusters.Centroids; // it gets centroids of clusters
        var downsampledData = centroids.Select((centroid, index) =>
        {
            var originalDataPoint = records[index]; // Get the original data point
            return new GeoPointstest
            {
                Type = originalDataPoint.Type,      // Set Type to the original value
                WP_Lat = centroid[0],               // Set WP_Lat to the centroid value
                WP_Lon = centroid[1],               // Set WP_Lon to the centroid value
                WP_Alt = originalDataPoint.WP_Alt,  // Set WP_Alt to the original value
                Caution = originalDataPoint.Caution // Set Caution to the original value
            };
        }).ToList();


        // create csv writer to write the unique records to a new csv file
        using var writer1 = new StreamWriter("unique_records1test4.csv");  // create new StreamWriter called writer1 to write downsampled data.
        using var csvWriter1 = new CsvWriter(writer1, new CsvConfiguration(CultureInfo.InvariantCulture)); // CsvWriter then writes data to csv
        csvWriter1.WriteRecords(downsampledData);

    }
}
