using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

// define a class that representes your data structure
public class GeoPointstest
{
    public string Type { get; set; } // class named Geopointstest shows structure of data
    public float WP_Lat { get; set; }
    public float WP_Lon { get; set; }
    public float WP_Alt {  get; set; }
    public int Gimbal_aw {  get; set; }
    public int Gimbal_Pitch { get; set; }


}

class Program // main method 
{
    static void Main(string[] args)
    {
        using var reader = new StreamReader("1bembooth.csv");  // stream reader opens csv file
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)); // csvconfiguration is set to use invariantculture 
        // to properly parse of numeric values 

        var records = csv.GetRecords<GeoPointstest>().ToList(); // GetRecords method is used to read and parse csv file into list of objects
        // type GeoPointstest, records contains all data from csv file, records list

        // create a set to store unique records based on the wp lat, wp lon, wp alt
        var uniquerecords = new HashSet<(float,float,float)>(); // A HashSet named uniquerecords is created to store unique records based on a unique key.

        foreach (var record in records)
        {
            // create a unique key based on the columns you want to chek for duplicate
            var uniqueKey1 = (record.WP_Lat,record.WP_Lon,record.WP_Alt);

            //var uniqueKey1 = $"{record.WP_Lat}_{record.WP_Lon}_{record.WP_Alt}_{record.Gimbal_aw}_{record.Gimbal_Pitch}";
            // Inside the loop, a unique key, uniqueKey1, is created by concatenating the values of the "WP_Lat," 

            // check if the unqiue key is not in the set
            if (!uniquerecords.Contains(uniqueKey1))
            // It checks whether the uniqueKey1 is not already in the uniquerecords set, which ensures that only unique records are processed
            {
                // print the values
                Console.WriteLine($"Lat: {record.WP_Lat}");
                Console.WriteLine($"Lon: {record.WP_Lon}");
                Console.WriteLine($"Alt: {record.WP_Alt}");
                Console.WriteLine($"Yaw: {record.Gimbal_aw}");
                Console.WriteLine($"Pitch: {record.Gimbal_Pitch}");

                // add the unqieu key to the set to mark it as seen
                uniquerecords.Add(uniqueKey1);
                // The uniqueKey1 is added to the uniquerecords set to mark it as seen and prevent duplicates from being printed.
            }
        }
    }
}