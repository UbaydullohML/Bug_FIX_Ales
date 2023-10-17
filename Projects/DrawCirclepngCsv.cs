using System;
using System.Drawing;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Define parameters for the circular numbers
        int numberOfCircles = 1;         // Number of circles to draw
        int circleRadius = 1000;        // Radius of the circles
        string csvFileName = "circles.csv";  // Name of the CSV file to store circle parameters

        // Create a bitmap for drawing
        using (Bitmap bitmap = new Bitmap(2 * circleRadius * numberOfCircles, 2 * circleRadius))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);  // Clear the bitmap with a white background

                for (int i = 0; i < numberOfCircles; i++)
                {
                    int x = circleRadius + 2 * circleRadius * i;  // X-coordinate of the circle's center
                    int y = circleRadius;  // Y-coordinate of the circle's center

                    // Draw a circle using a black pen
                    g.DrawEllipse(Pens.Black, x - circleRadius, y - circleRadius, 2 * circleRadius, 2 * circleRadius);

                    // Calculate the angle for the number
                    double angle = 2 * Math.PI * i / numberOfCircles;

                    // Calculate the position for the number using trigonometry
                    int numberX = x + (int)(circleRadius * Math.Cos(angle));  // X-coordinate for the number
                    int numberY = y + (int)(circleRadius * Math.Sin(angle));  // Y-coordinate for the number

                    // Draw the number inside the circle
                    string number = (i + 1).ToString();  // Convert the circle index to a string
                    Font font = new Font("Arial", 10000);  // Define font for the number
                    g.DrawString(number, font, Brushes.Black, numberX - 6, numberY - 6);  // Draw the number with a black brush

                    // Write circle parameters to the CSV file
                    string circleData = $"{i + 1},{x},{y},{circleRadius}";  // Create a CSV string with circle parameters
                    AppendToCSV(csvFileName, circleData);  // Append the string to the CSV file
                }

                // Save the bitmap as an image
                bitmap.Save("circles.png");  // Save the bitmap as "circles.png" image
            }
        }
    }

    // Function to append data to a CSV file
    static void AppendToCSV(string fileName, string data)
    {
        using (StreamWriter sw = File.AppendText(fileName))
        {
            sw.WriteLine(data);  // Append the data string to the CSV file
        }
    }
}
