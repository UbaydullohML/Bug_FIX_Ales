using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Accord.MachineLearning;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using static argosgcs.argosGCS.ExternalTriggerController.ExternalTriggerEx;

namespace argosgcs.argosGCS.PathPlanner
{
    public class GeoClassChanger
    {
        public class GeopointsTaker
        {
            public int DownsampleLogic(string strFilePath, out List<RoadWayPointItem> listSignCoord)
            {
                StreamReader reader = new StreamReader(strFilePath);

                listSignCoord = new List<RoadWayPointItem>();
                if (reader == null)
                    return 0;

                string strLine = string.Empty;
                int nCurrentLine = 0;
                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();
                    if (nCurrentLine++ < 1)
                        continue;
                    string[] strCol = strLine.Split(',');
                    if (strCol.Length < (int)enumMapHeader.eTotal)
                        continue;

                    RoadWayPointItem item = new RoadWayPointItem
                    {
                        strRoadName = (strCol[0] == "") ? "" : strCol[4],
                        strRoadNo = (strCol[1] == "") ? "" : strCol[5],
                        strDirection = (strCol[2] == "") ? "" : strCol[6],
                        strDistance = (strCol[3] == "") ? "" : strCol[7],
                        dLat = (strCol[8] == "") ? 0.0 : double.Parse(strCol[8]),
                        dLon = (strCol[9] == "") ? 0.0 : double.Parse(strCol[9]),
                        dAlt = double.Parse(strCol[13]),
                        dWP_Lat = double.Parse(strCol[11]),
                        dWP_Lon = double.Parse(strCol[12]),
                        nCaution = int.Parse(strCol[14]),
                        bSafeDown = bool.Parse(strCol[15]),
                        nIndex = int.Parse(strCol[20])
                    };

                    listSignCoord.Add(item);
                }

                // Downsampling logic using k-means clustering
                var records = listSignCoord.Select(r => new double[] { r.dWP_Lat, r.dWP_Lon }).ToArray();
                //int nClusters = 100;

                double percentage = 0.20;
                // calculate number of clusters based on percentage of total data points
                int nClusters = (int)Math.Ceiling(listSignCoord.Count * percentage);

                KMeans kmeans = new KMeans(nClusters);
                KMeansClusterCollection clusters = kmeans.Learn(records);
                var centroids = clusters.Centroids;
                HashSet<RoadWayPointItem> selectedPoints = new HashSet<RoadWayPointItem>();

                foreach (double[] centroid in centroids)
                {
                    double minDistance = double.MaxValue;
                    RoadWayPointItem closestPoint = null;

                    foreach (RoadWayPointItem dataPoint in listSignCoord)
                    {
                        // Check if the data point has a non-zero caution value
                        if (dataPoint.nCaution != 0)
                        {
                            // Add data point to selectedPoints
                            selectedPoints.Add(dataPoint);
                        }
                        else
                        {
                            double distance = CalculateDistance(centroid, dataPoint);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                closestPoint = dataPoint;
                            }
                        }
                    }

                    if (closestPoint != null)
                    {
                        selectedPoints.Add(closestPoint);
                    }
                }

                var sortedSelectedPoints1 = selectedPoints.OrderBy(p => p.nIndex);
                listSignCoord = sortedSelectedPoints1.ToList();

                // duplicate the sorted list
                var reversedListSignCoord1 = sortedSelectedPoints1.ToList();

                // reverse the duplicate list
                reversedListSignCoord1.Reverse();

                // append the reversed list to the sorted list
                listSignCoord.AddRange(reversedListSignCoord1 );


                // Store listSignCoord to a CSV file
                string outputPath = "output.csv"; // Change this to your desired output file path

                using (var writer = new StreamWriter(outputPath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(listSignCoord);
                }


                return listSignCoord.Count;
            }

            private double CalculateDistance(double[] centroid, RoadWayPointItem dataPoint)
            {
                double latDistance = centroid[0] - dataPoint.dWP_Lat;
                double lonDistance = centroid[1] - dataPoint.dWP_Lon;
                return Math.Sqrt(latDistance * latDistance + lonDistance * lonDistance);
            }
        }
    }
}
