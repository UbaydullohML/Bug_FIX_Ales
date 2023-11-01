![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/af6ecc91-b32d-488d-a1af-a584120a62e4)

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/ba7b8506-f956-489b-9ac4-6d37c9556038)

- Program2.cs output
  
![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/06471da9-ccd9-4fd3-a917-24193a4be140)


# 2 with some additions
            int id = 0;
        // write the unique records with an incrementing wp id and path id 
        for (int i = 0; i < records.Count; i++)
        {
            // write the unqiue record the new csv file with additional columns
            csvWriter1.WriteField(i);
            csvWriter1.WriteField(i);
            csvWriter1.WriteField(records[i].Type);
            csvWriter1.WriteField(records[i].WP_Lat);
            csvWriter1.WriteField(records[i].WP_Lon);
            csvWriter1.WriteField(records[i].WP_Alt);
            csvWriter1.WriteField(records[i].Caution);
            /*            csvWriter1.WriteField(records[i].Gimbal_aw);
            csvWriter1.WriteField(records[i].Gimbal_Pitch);*/
            csvWriter1.NextRecord();
        }
  

# 3 code where apply the K-Means Clustering
- program3.cs

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/a3310ed8-3662-400e-ba0b-4bffcdacfa73)

- from this

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/2ddd00fc-0e38-4afa-8b6e-5852012e6ed0)

- to below result
- 
![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/86bf26ec-aa2d-43c7-8abe-baba5f294fe2)
