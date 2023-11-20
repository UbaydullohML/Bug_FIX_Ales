My inputs:

[ExternalTriggerEx.cs]

    nampespace argosgcs.argosGCS.ExternalTriggerController
    {
        using argosgcs.argosGCS.PathPlanner;
    }

inside the public class RoadWayPointItem

    public class RoadWayPointItem
    {
        public int nIndex { get; set; }
        public string sType { get; set; }
    }

this is for calling the PathPlanner folder class method [DownsampleLogic]

    public bool MakeUploadPatrp;Mission(DroneTwin droneTwin, int nMissionNo)
    {

    #if false
        //1. 이정표지 CSV 좌표파일 Load and Parsing
        string strRoadMapPath = GetUploadRoadMapFilePath(nMissionNo);
        List<RoadWayPointItem> listSignCoord = new List<RoadWayPointItem>();
        if (UploadLoadCoordMap(strRoadMapPath, out listSignCoord) < 1)
            return false;
    #endif
    
        // New method of loading and parsing CSV 
        string strRoadMapPath = GetUploadRoadMapFilePath(nMissionNo);
        List<RoadWayPointItem> listSignCoord = new List<RoadWayPointItem>();
        GeoClassChanger.GeopointsTaker Downsample = new GeoClassChanger.GeopointsTaker(); // creating instance of geopointsTaker
        if (Downsample.DownsampleLogic(strRoadMapPath, out listSignCoord) < 1 )  // changes
            return false;
    }


[ExRoad_DroneTwin.cs]

first one 

    namespace argosgcs.argosGCS.ExternalTriggerController
    {
        internal class ExRoad_DroneTwin
        {
            private class MissionStateMonitor
            {
                public bool CheckDroneCaution()
                {
                    bool isProblem = true:
                    string filePath = string.Empty;
                    try
                    {
                        // read missionno csv file
                        List<RoadWayPointItem> listSignCoord = new List<RoadWayPointItem>();
                        if (exRoad_DroneTwin.PreviousMissionNo != 0)
                        {
                            filePath = extExWay.GetUploadRoadMapFilePath(exRoad_DroneTwin.PreviousMissionNo);
                        }
                        else 
                        {
                            filePath = extExWay.GetExQuickFlightMisionFilePath(exRoad_DroneTwin.PreviousQuickFlightRoadNo, exRoad_DroneTwin.PreviousQuickFlightDirection, exRoad_DroneTwin.PreviousQuickFlightDistance);
                        }
    #if false
                        extExWay.UploadLoadCoordMap(filePath, out listSignCoord);
    #endif
                        GeoClassChanger.GeopointsTaker downsample = new GeoClassChanger.GeoPointsTaker();
                        downsample.DownsampleLogic(filePath, out listSignCoord);                                 // FIRST input modification



                        if (exRoad_DroneTwin.IsRTHenabled && exRoad_DroneTwin.PreviousWayPointIndex != -1)
                        {
                            List<string> Wp_IndexDatas = new List<string>();
                            Wp_IndexDatas.Clear();

                            for (int i = 0; i < listSignCoord.Count; i++)
                            {
                                string Value = listSignCoord[i].WPIndex.ToString();
                                Wp_IndexDatas.Add(Value);
                            }

                            for (int i = 0; i < listSignCoord.Count; i++)
                            {   
                                Wp_IndexDatas[i] = "";
                            }

                            for(int i = 0; i < exRoad_DroneTwin.PreviousWayPointIndex; i++)
                            {
                                Wp_IndexDatas[i] = "0";
                            }

                            for (int i = exRoad_DroneTwin.PreviousWayPointIndex - 1; i >= 0; i--)
                            {
                                int value;
                                if (int.TryParse(Wp_IndexDatas[i], out value))
                                {
                                    int previousValue = i < exRoad_DroneTwin.PreviousWayPointIndex - 1 ? int.Parse(Wp_IndexDatas[i + 1]) : 0;
                                    Wp_IndexDatas[i] = (previousValue + 1).ToString();
                                }
                            }
    #if false
                            if (exRoad_DroneTwin.isUpdateCSVFile == false)
                            {
                                UpdateCsvFile(filePath, 20, Wp_IndexDatas);
                                extExWay.UploadLoadCoordMap(filePath, out listSignCoord);
                                exRoad_DroneTwin.isUpdateCSVFile = true;
                            }
    #endif
                            if (exRoad_DroneTwin.isUpdateCSVFile == false)
                            {
                                UpdateCsvFile(filePath, 20, Wp_IndexDatas);
                                downsample.DownsampleLogic(filePath, out listSignCoord);                  // SECOND input modification
                                exRoad_DroneTwin.isUpdateCSVFile = true;
                            }
                        }
                    }
                }
            }
        }
    }

