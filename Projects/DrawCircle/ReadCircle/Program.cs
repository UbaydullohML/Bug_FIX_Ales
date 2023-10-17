using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main(string[] args)
    {
        // Specify the name of the CSV file to read
        string csvFileName = "circle_data.csv";

        // Read data from the CSV file into a list of tuples
        List<(int, int, int)> circleData = ReadFromCSV(csvFileName);

        // Check if there's no data in the CSV file
        if (circleData.Count == 0)
        {
            Console.WriteLine("No data found in the CSV file.");
            return;
        }

        // Calculate the circle's radius based on the maximum X-coordinate in the data
        int circleRadius = circleData.Max(p => p.Item2) + 10;

        // Create a bitmap with a size that can enclose the circle
        using (Bitmap bitmap = new Bitmap(2 * circleRadius, 2 * circleRadius))
        {
            // Create a Graphics object for drawing on the bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Clear the background with white color
                g.Clear(Color.White);

                // Calculate the center of the circle
                int centerX = circleRadius;
                int centerY = circleRadius;

                // Iterate through the data points and draw them as black points
                foreach (var data in circleData)
                {
                    int i = data.Item1;
                    int x = centerX + data.Item2;
                    int y = centerY + data.Item3;

                    DrawPoint(g, x, y); // Draw a point at (x, y)
                }

                // Draw a bolder and sharper circle around the data points
                using (GraphicsPath circlePath = new GraphicsPath())
                {
                    circlePath.AddEllipse(centerX - circleRadius, centerY - circleRadius, 2 * circleRadius, 2 * circleRadius);

                    Pen circlePen = new Pen(Color.Black, 4); // Increase the line width for a bolder circle
                    circlePen.Alignment = PenAlignment.Inset; // Makes the circle appear sharper

                    g.DrawPath(circlePen, circlePath);
                }
            }

            // Save the resulting bitmap as "circle.png"
            bitmap.Save("circle.png");

        }
    }

    // Function to read data from a CSV file
    static List<(int, int, int)> ReadFromCSV(string fileName)
    {
        var circleData = new List<(int, int, int)>();

        try
        {
            // Read all lines from the specified CSV file
            string[] lines = File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                // Split each line into parts using a comma as the delimiter
                string[] parts = line.Split(',');

                // Check if the line contains three parts and can be parsed into integers
                if (parts.Length == 3 && int.TryParse(parts[0], out int i) && int.TryParse(parts[1], out int x) && int.TryParse(parts[2], out int y))
                {
                    // Add the parsed data as a tuple to the circleData list
                    circleData.Add((i, x, y));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the CSV file: {ex.Message}");
        }

        // Return the list of parsed data
        return circleData;
    }

    // Function to draw a point on the bitmap
    static void DrawPoint(Graphics g, int x, int y)
    {
        // Fill a small rectangle at the specified (x, y) coordinates to represent a data point
        g.FillRectangle(Brushes.Black, x, y, 1, 1);
    }
}
