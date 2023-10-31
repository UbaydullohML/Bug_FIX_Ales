using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

// define a class that representes your data structure
public class GeoPointstest
{
    /*    public int WP_ID { get; set; }
        public int PATH_ID { get; set; }*/
    public string Type { get; set; } // class named Geopointstest shows structure of data
    public double WP_Lat { get; set; }
    public double WP_Lon { get; set; }
    public double WP_Alt { get; set; }
    public int Gimbal_aw { get; set; }
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

        // create csv writer to write the unique records to a new csv file
        using var writer1 = new StreamWriter("unique_records1.csv");
        using var csvWriter1 = new CsvWriter(writer1, new CsvConfiguration(CultureInfo.InvariantCulture));

        // write the header
        csvWriter1.WriteField("WP_ID");
        csvWriter1.WriteField("PATH_ID");
        csvWriter1.WriteField("Type");
        csvWriter1.WriteField("Lat");
        csvWriter1.WriteField("Lon");
        csvWriter1.WriteField("Alt");
        csvWriter1.NextRecord();

        int id = 0;

        // write the unique records with an incrementing wp id and path id 
        for (int i = 0; i < records.Count; i++)
        {
            // write the unqiue record the new csv file with additional columns
            csvWriter1.WriteField(id);
            csvWriter1.WriteField(id);
            csvWriter1.WriteField(records[i].Type);
            csvWriter1.WriteField(records[i].WP_Lat);
            csvWriter1.WriteField(records[i].WP_Lon);
            csvWriter1.WriteField(records[i].WP_Alt);
            csvWriter1.NextRecord();

            if (records[i].Gimbal_aw != -1000)
            {
                id++;
            }

            // write yaw and pitch values in the next row
            csvWriter1.WriteField(id);
            csvWriter1.WriteField(id);
            csvWriter1.WriteField("Cam_Command");
            csvWriter1.WriteField(records[i].Gimbal_aw);
            csvWriter1.WriteField(records[i].Gimbal_Pitch);
            csvWriter1.NextRecord();

            if (records[i].Gimbal_Pitch != -1000)
            {
                id++;
            }


        }
    }
}
