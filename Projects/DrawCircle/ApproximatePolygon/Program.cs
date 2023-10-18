using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Specify the name of the CSV file containing data points
        string csvFileName = "circle_data.csv";

        // Read data from the CSV file into a list of tuples
        List<(int, int, int)> circleData = ReadFromCSV(csvFileName);

        // Check if there's no data in the CSV file
        if (circleData.Count == 0)
        {
            Console.WriteLine("No data found in the CSV file.");
            return;
        }

        // Set the radius of the polygon
        int polygonRadius = 1000;

        // Create a new bitmap with the specified dimensions
        using (Bitmap bitmap = new Bitmap(2 * polygonRadius, 2 * polygonRadius))
        {
            // Create a Graphics object for drawing on the bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Clear the background with white color
                g.Clear(Color.White);

                // Calculate the center of the polygon
                int centerX = polygonRadius;
                int centerY = polygonRadius;

                // Convert data points to PointF, adding the center coordinates
                PointF[] points = circleData.Select(data => new PointF(centerX + data.Item2, centerY + data.Item3)).ToArray();

                // Simplify the polygon using the Ramer-Douglas-Peucker algorithm
                PointF[] simplifiedPoints = SimplifyPolygon(points, 10);

                // Calculate the translation to center the polygon
                float translateX = (polygonRadius - simplifiedPoints.Max(p => p.X) - simplifiedPoints.Min(p => p.X)) / 2;
                float translateY = (polygonRadius - simplifiedPoints.Max(p => p.Y) - simplifiedPoints.Min(p => p.Y)) / 2;

                // Apply the translation to each point to center the polygon
                for (int i = 0; i < simplifiedPoints.Length; i++)
                {
                    simplifiedPoints[i] = new PointF(simplifiedPoints[i].X + translateX, simplifiedPoints[i].Y + translateY);
                }

                // Create a bold pen for drawing the polygon
                Pen polygonPen = new Pen(Color.Black, 4);
                polygonPen.Alignment = PenAlignment.Inset; // Makes the polygon appear sharper

                // Draw the polygon on the bitmap
                g.DrawPolygon(polygonPen, simplifiedPoints);
            }

            // Save the resulting bitmap as "polygon_approximation.png"
            bitmap.Save("polygon_approximation.png");
        }
    }

    // Function to read data from a CSV file and store it in a list of tuples
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

    // Function to simplify a polygon using the Ramer-Douglas-Peucker algorithm
    static PointF[] SimplifyPolygon(PointF[] points, float tolerance)
    {
        if (points.Length < 3)
            return points;

        List<PointF> simplified = new List<PointF>();
        simplified.Add(points[0]);

        SimplifyRDP(points, 0, points.Length - 1, tolerance, simplified);

        simplified.Add(points[points.Length - 1]);
        return simplified.ToArray();
    }

    // Recursive function to apply the Ramer-Douglas-Peucker algorithm
    static void SimplifyRDP(PointF[] points, int start, int end, float tolerance, List<PointF> simplified)
    {
        float maxDistance = 0;
        int index = 0;

        for (int i = start + 1; i < end; i++)
        {
            float distance = PointToLineDistance(points[i], points[start], points[end]);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                index = i;
            }
        }

        if (maxDistance > tolerance)
        {
            SimplifyRDP(points, start, index, tolerance, simplified);
            simplified.Add(points[index]);
            SimplifyRDP(points, index, end, tolerance, simplified);
        }
    }

    // Function to calculate the distance between a point and a line segment
    static float PointToLineDistance(PointF point, PointF start, PointF end)
    {
        float dx = end.X - start.X;
        float dy = end.Y - start.Y;

        if ((dx == 0) && (dy == 0))
            return (float)Math.Sqrt((point.X - start.X) * (point.X - start.X) + (point.Y - start.Y) * (point.Y - start.Y));

        float t = ((point.X - start.X) * dx + (point.Y - start.Y) * dy) / (dx * dx + dy * dy);

        if (t < 0)
            return (float)Math.Sqrt((point.X - start.X) * (point.X - start.X) + (point.Y - start.Y) * (point.Y - start.Y));

        if (t > 1)
            return (float)Math.Sqrt((point.X - end.X) * (point.X - end.X) + (point.Y - end.Y) * (point.Y - end.Y));

        float nearestX = start.X + t * dx;
        float nearestY = start.Y + t * dy;

        return (float)Math.Sqrt((point.X - nearestX) * (point.X - nearestX) + (point.Y - nearestY) * (point.Y - nearestY));
    }
}
