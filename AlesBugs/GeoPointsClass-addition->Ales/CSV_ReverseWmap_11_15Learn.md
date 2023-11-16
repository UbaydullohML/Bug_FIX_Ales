## Table of Tasks:
* [CSVfile](#csvfile)
* [Reverse](#reverse)
* [Code](#file)

 
## Intro
create csv file
Task:
- input reverse waypoints have been added for going and returning points of drone into CSV file like the below:
- it is the full data for the drone to go there and return [1 - 737]
- the 737 point is also the S point for Station coordinates.
- so the drone starts from 737 or S goes to 1st and continues according to full data.
- after applying the downSampleLogic, these points will be down sampled like 2nd image

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/5f8b5e74-9587-49b6-a46c-801db6ce4730)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/7ef65e05-2245-47cb-a546-eb31aae1bba0)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/12e4e38f-e062-4c9f-a5ad-538d1244104b)

## Reverse
input reverse waypoints on map
task:
- on mapping of waypoints:
- the issue of going and not returning correctly is because waypoints are not precisely the same on one way up and the second way down. The reason is that k-means clustering groups nearby waypoints and selects the mean of the group and, every time, the selection isn't exact on one way and another way.
- so the solution is to apply k-means clustering one way and use exact same on return.

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/9b2729a7-0671-42d9-ad09-3bd82dc10023)

2nd task:
- putting the downsampled waypoints in reverse on map is done like the below image:

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/d6096026-ee29-429b-8ae1-01ad7f7a6e79)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/2ae27757-afc6-4052-b78b-932227afd85d)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/5f5747be-3f44-43a5-a392-d8f19a9abe4e)


## Code
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
    
