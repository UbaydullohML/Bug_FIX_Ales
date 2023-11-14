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
        public int nRindex { get; set; }
    }

this is for calling the PathPlanner folder class method [DownsampleLogic]

    public bool MakeUploadPatrp;Mission(DroneTwin droneTwin, int nMissionNo)
    {
        // New method of loading and parsing CSV 
        string strRoadMapPath = GetUploadRoadMapFilePath(nMissionNo);
        List<RoadWayPointItem> listSignCoord = new List<RoadWayPointItem>();
        GeoClassChanger.GeopointsTaker Downsample = new GeoClassChanger.GeopointsTaker(); // creating instance of geopointsTaker
        if (Downsample.DownsampleLogic(strRoadMapPath, out listSignCoord) < 1 )  // changes
            return false;
    }


[ExRoa_DroneTwin.cs]

    public async Task StartCheckCaution(int MissionNo, bool isRTLMission)
    {
        lock (_cautionLock)
        {
            _checkCautionTokenSource = new CancellationTokenSource();
        }
        try
        {
            //Read MissionNo Csv File
            string filePath = extExWay.GetUploadRoadMapFilePath(MissionNo);
            List<RoadWayPointItem> listSignCoord = new List<RoadWayPointItem>();
            GeoClassChanger.GeopointsTaker downsample = new GeoClassChanger.GeopointsTaker(); // changes 
            downsample.DownsampleLogic(filePath, out listSignCoord);   // changes 
        }
        catch (TaskCanceledException)
        {                    
            // do nothing as its expected
        }
    }
