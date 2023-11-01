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

        int n_clusters = 300; // sets number of clusters to partition the data into

        KMeans kmeans = new KMeans(n_clusters); // it creates new KMeans object with specified number of clusters 
        KMeansClusterCollection clusters = kmeans.Learn(X1); // it trains K-means algorithm on data in X1
        // Learn method returns KMeansClusterCollection object that contains centroids of clusters.

        var centroids = clusters.Centroids; // it gets centroids of clusters

        var downsampledData = centroids.Select((centroid, index) => new GeoPointstest // it creates new list downsa.Data 
        {                               // Select method is creating geopointstest object for each centroid in in centroids
            Type = "Centroid",
            WP_Lat = centroid[0], // set WP_Lat property to first element of centroid array
            WP_Lon = centroid[1], // sets to second element
            WP_Alt = 0.0,
            Caution = 0,

        }).ToList(); // converts Select method result to list.

        // create csv writer to write the unique records to a new csv file
        using var writer1 = new StreamWriter("unique_records1test4.csv");  // create new StreamWriter called writer1 to write downsampled data.
        using var csvWriter1 = new CsvWriter(writer1, new CsvConfiguration(CultureInfo.InvariantCulture)); // CsvWriter then writes data to csv
        csvWriter1.WriteRecords(downsampledData);

    }
}
