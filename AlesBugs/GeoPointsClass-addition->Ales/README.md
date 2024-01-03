## Table of Content
* [Intro](#intro)
* [QuickF rtl](#quickf_rtl)
* [CSV](#csv)
* [Tech Doc](#tech_doc)
* [Test](#test)
* [Bugs](#bugs)
   
Correct outputs with the sorted data:

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/7f27f631-a082-495a-9851-8d17aab0b689)

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/e7b4ffa0-a65c-48e7-a527-399a0b8f8251)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/7e714366-3fe6-48c4-b521-3b8b3326bfca)

30 percent output result:
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/1248dbb3-5db2-4116-964d-003690e21264)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/0cb81cf2-a1f1-48d6-8ca1-3f0525a374c5)


20 percent output result:
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/98833cf5-5f2e-462c-bb17-f340a55e364a)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/46cdeb5a-53ee-4f7f-bc08-93981434e325)


Output after sorted downsampled result:

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/137b1664-5aa4-4c23-9a6d-8d419567c81f)

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/24c54a47-e7fe-4a15-9bf5-3a1a8320c5ae)

Error fix for importing the document in D folder and changing the location of strFilePath 

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/a62b83aa-176b-4698-aafe-399c873d43a1)



original one:

        public string GetUploadRoadMapFilePath(int nRoadNo)
        {
            if (nRoadNo == 0)
                return "";
            string strFilePath;
            string strRoadString = GetUploadRoadNameFromRoadNo(nRoadNo);
            if (strRoadString.Length < 1)
                return "";
            strFilePath = string.Format($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}" + @"\" + $"{strRoadString}.csv");
            return strFilePath;
        }



changed one:

    namespace argosgcs.argosGCS.ExternalTriggerController
    {
        public class ExternalTriggerEx
        {
            public string ConvertSummaryToString(string summary)
            {
                public string GetUploadRoadMapFilePath(int nRoadNo)
                {
                    if (nRoadNo == 0)
                      return "";
                    string strFilePath;
                    string strRoadString = GetUploadRoadNameFromRoadNo(nRoadNo);
                    if (strRoadString.Length < 1)
                        return "";
                    strFilePath = string.Format($"D:\\models\\7.GeoPoints\\" + $"{strRoadString}.csv");
                    return strFilePath;
                }
            }
        }
    }

## QuickF_rtl
- Additions to GeoPointsChanger

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/830de4aa-54a9-4963-b9cc-64bce02928c6)

main changes to ExternalTriggerEx.cs
  
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/8f9b8cdf-83b9-414f-8cff-936dd2ae54ca)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/80bf62ef-7c2d-4054-93fd-74101825b8c4)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/a0cde3e6-f7c8-41fe-a9b7-faa982c2a613)

old

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/c08bfa80-6db1-421f-8780-2e47ce5e2169)

another change in ExRoad_DroneTwin.cs

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/f617854c-04ef-4d1a-b2e4-624d9599e115)

- test
Quick Flight and Return to near station
the code for creating down sample for quick flight part has been added and tested:

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/8df490ea-d5c2-4491-a8f2-6ab9865986a8)

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/88c673be-17f0-4402-9d5c-26142eb02a0d)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/be789722-fd15-4ad8-854c-fd324b9210b5)

Return to near station

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/88a99bb2-6849-4fcc-9805-079cd6d6f17f)

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/ead57f39-bf02-4069-b84d-70b26cd251f7)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/68b09d96-ef5e-4bb4-96cd-7450f10d3b9b)


- Battery percent improver code: (if exception happens)

code

    param set SIM_BATT_VOLTAGE 27
    
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/167c6bc7-35a4-4df1-8ab9-3ffb04ecbcc8)

    param set WPNAV_SPEED 50  // for improving speed of drone

    wp list // for showing waypoints on sitl



## CSV

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/bf38d32c-26bc-49b3-aa38-8dac0e5d9abf)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/47a7fd0d-915b-4374-826b-f874d37a86ae)


## Tech_Doc



## Test

- to comment the battery and weather failsafe
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/c3ff0646-e8d6-4c72-8302-5f303b197534)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/688f6679-548d-4d6f-b579-8a292608640a)


## Bugs
- links for fixing installer part issue

https://stackoverflow.com/questions/17681432/how-can-i-enable-assembly-binding-logging
https://stackoverflow.com/questions/255669/how-to-enable-assembly-bind-failure-logging-fusion-in-net



## Fix_Bugs
