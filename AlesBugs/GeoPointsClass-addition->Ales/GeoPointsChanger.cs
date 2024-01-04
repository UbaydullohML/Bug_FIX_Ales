using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Accord.MachineLearning;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using static argosgcs.argosGCS.ExternalTriggerController.ExternalTriggerEx;
using System.ComponentModel;
using System.Drawing;
using Telerik.Windows.Controls.Timeline;

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
                        //dAlt = (strCol[10] == "") ? 0.0 : double.Parse(strCol[10]),
                        dAlt = double.Parse(strCol[13]),
                        dWP_Lat = double.Parse(strCol[11]),
                        dWP_Lon = double.Parse(strCol[12]),
                        //dWP_Alt = double.Parse(strCol[13]),
                        nCaution = int.Parse(strCol[14]),
                        bSafeDown = bool.Parse(strCol[15]),
                        nGimbal_Pitch = int.Parse(strCol[18]),
                        nGimbal_Yaw = int.Parse(strCol[19]),
                        nIndex = int.Parse(strCol[20]),
                        WPIndex = (strCol[21] == "") ? 0 : int.Parse(strCol[21]),
                        nFlag = (strCol[22] == "") ? 0 : int.Parse(strCol[22])

                    };

                    listSignCoord.Add(item);
                }

                // Downsampling logic using k-means clustering
                var records = listSignCoord.Select(r => new double[] { r.dWP_Lat, r.dWP_Lon }).ToArray();
                //int nClusters = 100;

                double percentage = 0.17;

                bool FlagTwoRoundTrip = listSignCoord.Any(point => point.nFlag == 2);

                if (FlagTwoRoundTrip)
                {
                    // downsampling logic for flag 2 = round trip

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

                        //bool firstPointAdded1 = false;
                        bool lastPointAdded1 = false;

                        // initialize a variable to track the previous state of the gimbal movement 
                        (int, int)? previousState = null;

                        foreach (RoadWayPointItem dataPoint in listSignCoord)
                        {

                            // extract the gimbal pitch and gimbal yaw from the data point
                            int gimbalPitch = dataPoint.nGimbal_Pitch;
                            int gimbalYaw = dataPoint.nGimbal_Yaw;

                            // Check if the data point has a non-zero caution value
                            if (dataPoint.nCaution != 0)
                            {
                                // Add data point to selectedPoints
                                selectedPoints.Add(dataPoint);
                            }

                            // add first and last points to selectedpoints if not already added
                            //else if (!firstPointAdded1 && dataPoint.nIndex == listSignCoord.Min(p => p.nIndex))
                            //{
                            //    selectedPoints.Add(dataPoint);
                            //    firstPointAdded1 = true;
                            //}
                            else if (!lastPointAdded1 && dataPoint.nIndex == listSignCoord.Max(p => p.nIndex))
                            {
                                selectedPoints.Add(dataPoint);
                                lastPointAdded1 = true;
                            }

                            // check if there was a previous state and if the current state is different
                            else if (previousState != null && (gimbalPitch != previousState?.Item1 || gimbalYaw != previousState?.Item2))
                            {
                                // if the gimbal camera movement has changed, store this data point
                                selectedPoints.Add(dataPoint);
                            }

                            else
                            {
                                // calculate the distance and set closest point
                                double distance = CalculateDistance(centroid, dataPoint);
                                if (distance < minDistance)
                                {
                                    minDistance = distance;
                                    closestPoint = dataPoint;
                                }
                            }
                            previousState = (gimbalPitch, gimbalYaw);
                        }

                        if (closestPoint != null)
                        {
                            selectedPoints.Add(closestPoint);
                        }
                    }
                    var selectedPointsList = selectedPoints.ToList();
                    //listSignCoord = selectedPointsList;
                    // Sort the selectedPointsList based on their original order in listSignCoord
                    var orderedSelectedPoints = selectedPointsList
                        .Join(listSignCoord.Select((item, index) => new { item, index }),
                              point => point,
                              signCoord => signCoord.item,
                              (point, signCoord) => new { point, signCoord.index })
                        .OrderBy(result => result.index)
                        .Select(result => result.point)
                        .ToList();
                    // Update listSignCoord with the ordered selected points
                    listSignCoord = orderedSelectedPoints;

                    //duplicate the sorted list
                    //var reversedListSignCoord1 = listSignCoord.Skip(1).Take(listSignCoord.Count - 2).ToList();
                    var reversedListSignCoord1 = listSignCoord.Take(listSignCoord.Count - 1).ToList();
                    //var reversedListSignCoord1 = listSignCoord.ToList();

                    // reverse the duplicate list
                    reversedListSignCoord1.Reverse();

                    // append the reversed list to the sorted list
                    listSignCoord.AddRange(reversedListSignCoord1);

                }

                else
                {
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

                        //bool firstPointAdded1 = false;
                        bool lastPointAdded1 = false;

                        // initialize a variable to track the previous state of the gimbal movement 
                        (int, int)? previousState = null;

                        foreach (RoadWayPointItem dataPoint in listSignCoord)
                        {

                            // extract the gimbal pitch and gimbal yaw from the data point
                            int gimbalPitch = dataPoint.nGimbal_Pitch;
                            int gimbalYaw = dataPoint.nGimbal_Yaw;

                            // Check if the data point has a non-zero caution value
                            if (dataPoint.nCaution != 0)
                            {
                                // Add data point to selectedPoints
                                selectedPoints.Add(dataPoint);
                            }

                            // add first and last points to selectedpoints if not already added
                            //else if (!firstPointAdded1 && dataPoint.nIndex == listSignCoord.Min(p => p.nIndex))
                            //{
                            //    selectedPoints.Add(dataPoint);
                            //    firstPointAdded1 = true;
                            //}
                            else if (!lastPointAdded1 && dataPoint.nIndex == listSignCoord.Max(p => p.nIndex))
                            {
                                selectedPoints.Add(dataPoint);
                                lastPointAdded1 = true;
                            }

                            // check if there was a previous state and if the current state is different
                            else if (previousState != null && (gimbalPitch != previousState?.Item1 || gimbalYaw != previousState?.Item2))
                            {
                                // if the gimbal camera movement has changed, store this data point
                                selectedPoints.Add(dataPoint);
                            }

                            else
                            {
                                // calculate the distance and set closest point
                                double distance = CalculateDistance(centroid, dataPoint);
                                if (distance < minDistance)
                                {
                                    minDistance = distance;
                                    closestPoint = dataPoint;
                                }
                            }
                            previousState = (gimbalPitch, gimbalYaw);
                        }

                        if (closestPoint != null)
                        {
                            selectedPoints.Add(closestPoint);
                        }
                    }
                    var selectedPointsList = selectedPoints.ToList();
                    //listSignCoord = selectedPointsList;
                    // Sort the selectedPointsList based on their original order in listSignCoord
                    var orderedSelectedPoints = selectedPointsList
                        .Join(listSignCoord.Select((item, index) => new { item, index }),
                              point => point,
                              signCoord => signCoord.item,
                              (point, signCoord) => new { point, signCoord.index })
                        .OrderBy(result => result.index)
                        .Select(result => result.point)
                        .ToList();
                    // Update listSignCoord with the ordered selected points
                    listSignCoord = orderedSelectedPoints;
                }

#if false
                // in order to store the csv file 
                string outputPath = @"D:\models\7.GeoPoints\argosALES_desktop\output2.4.13.csv";
                using (var writer = new StreamWriter(outputPath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(listSignCoord);
                }
#endif
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
