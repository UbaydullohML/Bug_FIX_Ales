using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

class Program
{
    class DataPoint
    {
        public int Item1 { get; set; }
        public int Item2 { get; set; }
        public int Item3 { get; set; }
        public bool IsTurningPoint { get; set; }
    }

    static void Main(string[] args)
    {
        // Specify the name of the CSV file containing data points
        string inputCsvFileName = "circle_data.csv";
        string outputCsvFileName = "labeled_circle_data.csv";

        // Read data from the input CSV file into a list of DataPoint objects
        List<DataPoint> circleData = ReadFromCSV(inputCsvFileName);

        // Check if there is no data in the CSV file
        if (circleData.Count == 0)
        {
            Console.WriteLine("No data found in the input CSV file.");
            return;
        }

        // Set the radius of the polygon
        int polygonRadius = 2000;

        using (Bitmap bitmap = new Bitmap(2 * polygonRadius, 2 * polygonRadius))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                // Calculate the center of the polygon as the average of the data points
                int centerX = (int)circleData.Average(data => data.Item2);
                int centerY = (int)circleData.Average(data => data.Item3);

                // Convert data points to PointF, adding the center coordinates
                PointF[] points = circleData.Select(data => new PointF(centerX + data.Item2, centerY + data.Item3)).ToArray();

                // Simplify the polygon using the Ramer-Douglas-Peucker algorithm
                PointF[] simplifiedPoints = SimplifyPolygon(points, 10);

                // Create a bold pen for drawing the polygon
                Pen polygonPen = new Pen(Color.Black, 4);
                polygonPen.Alignment = PenAlignment.Inset;

                // Draw the polygon on the bitmap and label turning points
                g.DrawPolygon(polygonPen, simplifiedPoints);

                for (int i = 0; i < simplifiedPoints.Length; i++)
                {
                    if (IsTurningPoint(simplifiedPoints, i))
                    {
                        g.FillEllipse(Brushes.Red, simplifiedPoints[i].X - 5, simplifiedPoints[i].Y - 5, 10, 10);
                        circleData[i].IsTurningPoint = true; // Mark the point as a turning point in the data
                    }
                }
            }

            // Save the resulting bitmap as "polygon_approximation.png"
            bitmap.Save("polygon_approximation.png");

            // Update the CSV file with the turning point information
            UpdateCSVFile(outputCsvFileName, circleData);
        }
    }

    // Function to read data from a CSV file and store it in a list of DataPoint objects
    static List<DataPoint> ReadFromCSV(string fileName)
    {
        var circleData = new List<DataPoint>();

        try
        {
            string[] lines = File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length == 3 && int.TryParse(parts[0], out int i) && int.TryParse(parts[1], out int x) && int.TryParse(parts[2], out int y))
                {
                    circleData.Add(new DataPoint { Item1 = i, Item2 = x, Item3 = y, IsTurningPoint = false });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the CSV file: {ex.Message}");
        }

        return circleData;
    }

    // Function to check if a point is a turning point
    static bool IsTurningPoint(PointF[] points, int index)
    {
        int prevIndex = (index - 1 + points.Length) % points.Length;
        int nextIndex = (index + 1) % points.Length;

        PointF prev = points[prevIndex];
        PointF current = points[index];
        PointF next = points[nextIndex];

        // Implement your turning point detection logic here.
        // For example, you can check if the angle at the current point is greater than a threshold.

        // For demonstration, let's assume all points are turning points.
        return true;
    }

    // Function to update the CSV file with turning point information
    static void UpdateCSVFile(string fileName, List<DataPoint> circleData)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var data in circleData)
                {
                    writer.WriteLine($"{data.Item1},{data.Item2},{data.Item3},{data.IsTurningPoint}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating the CSV file: {ex.Message}");
        }
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

        if (dx == 0 && dy == 0)
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
