using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Define parameters for the circle
        int circleRadius = 1000;    // Radius of the circle
        string csvFileName = "circle_data.csv";  // Name of the CSV file to store circle data

        // Create a bitmap for drawing
        using (Bitmap bitmap = new Bitmap(2 * circleRadius, 2 * circleRadius))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);  // Clear the bitmap with a white background

                // Draw a circle using a black pen
                g.DrawEllipse(Pens.Black, 0, 0, 2 * circleRadius, 2 * circleRadius);

                // Generate data for the circle
                List<string> circleData = new List<string>();  // Create a list to store data

                // Iterate through 360 degrees (a full circle)
                for (int i = 0; i < 360; i++)
                {
                    // Convert degrees to radians
                    double angle = i * Math.PI / 180.0;

                    // Calculate X and Y coordinates based on the circle's radius and the angle
                    int x = circleRadius + (int)(circleRadius * Math.Cos(angle));
                    int y = circleRadius + (int)(circleRadius * Math.Sin(angle));

                    // Create a data string for the current point and add it to the list
                    circleData.Add($"{i},{x},{y}");
                }

                // Write circle data to the CSV file
                WriteToCSV(csvFileName, circleData);  // Call a function to write data to the CSV file
            }

            // Save the bitmap as an image
            bitmap.Save("circle.png");  // Save the bitmap as "circle.png" image
        }
    }

    // Function to write data to a CSV file
    static void WriteToCSV(string fileName, List<string> data)
    {
        using (StreamWriter sw = new StreamWriter(fileName))
        {
            // Write each line of data from the list to the CSV file
            foreach (var line in data)
            {
                sw.WriteLine(line);
            }
        }
    }
}
