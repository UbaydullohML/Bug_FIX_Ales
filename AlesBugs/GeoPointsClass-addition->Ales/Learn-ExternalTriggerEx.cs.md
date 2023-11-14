1st: accessing the another Class method inside one file:
           
        public class RoadWayPointItem
        {
            public string strRoadName { get; set; }  // properties for road information
            public string strRoadNo { get; set; }
            public string strDirection { get; set; }
            public string strDistance { get; set; }
            public double dLat { get; set; }
            public double dLon { get; set; }
            public double dAlt { get; set; }

            public double dWP_Lat { get; set; }
            public double dWP_Lon { get; set; }
            public double dWP_Alt { get; set; }
            public int nGimbal_Pitch { get; set; }
            public int nGimbal_Yaw { get; set; }

            public bool bSkipable { get; set; }

            public int nCaution { get; set; }
            public bool bSafeDown { get; set; }

            public RoadWayPointItem()
            {
                Clear();  // initialize properties uding Clear method
            }
            public RoadWayPointItem(double dWPLat, double dWPLon, double dWPAlt)
            {
                Clear();
                dWP_Lat = dWPLat;
                dWP_Lon = dWPLon;
                dWP_Alt = dWPAlt;
            }
            public void Clear()
            {
                strRoadName = string.Empty;  // clear road information
                strRoadNo = string.Empty;
                strDirection = string.Empty;
                strDistance = string.Empty;
                dLat = 0.0f; 
                dLon = 0.0f;
                dAlt = 0.0f;

                dWP_Lat = 0.0f; // clear waypoint information
                dWP_Lon = 0.0f;
                dWP_Alt = 0.0f;

                nGimbal_Pitch = 0;  // clear gimbal orientation
                nGimbal_Yaw = 0;

                bSkipable = false;  // set waypoint as non-skipable by default
            }
       }
       public class RoadWayPointItem1
       {
           public string strRoadName1 { get; set; } // Properties for road information
           public string strRoadNo1 { get; set; }
           public string strDirection1 { get; set; }
           public string strDistance1 { get; set; }
           public double dLat1 { get; set; }
           public double dLon1 { get; set; }
           public double dAlt1 { get; set; }

           public double dWP_Lat1 { get; set; }
           public double dWP_Lon1 { get; set; }
           public double dWP_Alt1 { get; set; }
           public int nGimbal_Pitch1 { get; set; }
           public int nGimbal_Yaw1 { get; set; }

           public bool bSkipable1 { get; set; }

           public int nCaution1 { get; set; }
           public bool bSafeDown1 { get; set; }
           public int nIndex1 { get; set; }
           public string sType { get; set; }

           public RoadWayPointItem1()
           {
               RoadWayPointItem roadWayPointItem = new RoadWayPointItem();  // creating instance of roadWayPointItem 
               roadWayPointItem.Clear(); // Call the Clear method on instance.

               strRoadName1 = roadWayPointItem.strRoadName; // even its properties is accessed if needed. 
           }
       }

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/47294c5b-63f6-4daf-b4a3-1bbec83c6634)



    GeoClassChanger.GeoPointsTaker geopointsTaker = new GeoClassChanger.GeoPointsTaker();
    geopointsTaker.ProcessData(/* pass the required parameters here */);

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/e70e00b5-5132-49aa-9176-c75b28599a33)

