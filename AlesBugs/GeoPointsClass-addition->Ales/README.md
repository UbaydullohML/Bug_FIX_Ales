## Table of Content
* [Intro](#intro)
* [QuickF rtl](#quickf_rtl)

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

- main changes to ExternalTriggerEx.cs
  
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/8f9b8cdf-83b9-414f-8cff-936dd2ae54ca)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/80bf62ef-7c2d-4054-93fd-74101825b8c4)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/a0cde3e6-f7c8-41fe-a9b7-faa982c2a613)

- old

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/c08bfa80-6db1-421f-8780-2e47ce5e2169)

- another change in ExRoad_DroneTwin.cs

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/f617854c-04ef-4d1a-b2e4-624d9599e115)



