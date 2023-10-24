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

        var uniqueRecord = new List<GeoPointstest>();
                
        foreach (var record in records)
        {
            // check if a record witht the same lat, lon, alt already exists
            bool duplicate1 = uniqueRecord.Any(r =>
            r.WP_Lat == record.WP_Lat &&
            r.WP_Lon == record.WP_Lon &&
            r.WP_Alt == record.WP_Alt);

            // iff it is not a duplicate, add it to the list of unique records
            if (!duplicate1)
            // It checks whether the uniqueKey1 is not already in the uniquerecords set, which ensures that only unique records are processed
            {
                uniqueRecord.Add(record);
            }
        }

        // print the unique records
        foreach (var record in uniqueRecord)
        {
            Console.WriteLine($"Lat: {record.WP_Lat}");
            Console.WriteLine($"Lon: {record.WP_Lon}");
            Console.WriteLine($"Alt: {record.WP_Alt}");
            Console.WriteLine($"Yaw: {record.Gimbal_aw}");
            Console.WriteLine($"Pitch: {record.Gimbal_Pitch}");
        }
    }
}
