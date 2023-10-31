# 1 st download the file - Ubuntu18.04.zip after entering to argosdyne NAS \\argodsyneNAS
id argosdyne, p Argos2022@)@@
![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/32d3a892-5c1b-4059-90cc-315fb13cbf7a)

- and upload the vdi in VM box
![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/bf678d01-b238-4f3d-acd9-ac7704ed0956)


# 2 check everything in Birdcom config while connecting the ab120

- connect the ab120 with connecting power cable, d2 and e2 (ethernet) with internet. 

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/eced892f-b842-4e69-8f2f-88ad002b6adb)


![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/a5796226-ccf6-4b45-b240-f0c89897f6c1)

- check the ip address part

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/2a953783-f0c9-4578-ad07-756a22a08fb8)



### 2.1 and upgrade the latest ab120 version after making aif file. like below

https://github.com/argosdyne/BirdCom_AB120/releases

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/eb1aa26e-c6fa-444a-831f-5dc7bff6d9a5)

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/bbcdc931-46da-4c30-bd77-28cac778a133)

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/e0fab601-924b-4dab-8faf-bf72f4506e33)

- make aif file like below using .bin file for birdcom new firmware upgrade



![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/5bd5232e-e7e3-4fd8-b55a-8593dc5c00ef)

### 2.2 check the mqtt explorer connecting with below method:

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/3d26dfdb-d42d-4c08-95cf-4b60d2461339)

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/585ab87a-f27c-48cf-8a1d-8a4a3ae525a7)



# 3 connecting the ALES with SITL

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/dd9e6874-d7be-45dc-a085-fb7b84be060b)

connect the f1 cable and ethernet to e2 and power cable like below image 

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/c05b291a-1219-4e72-8eb6-3ee39ce56fc3)

- find the same silicon labs com4  in pc and linux VM like below

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/a7599213-c264-4055-80c4-f65da98220b4)

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/e033ca15-ad8c-4f72-bb39-ca50937e999a)

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/6642d962-87e5-4cb6-b2b6-a9c0b0a46ef5)

- here is the SITL

      sim_vehicle.py -w --map --console -v ArduCopter --out=/dev/ttyUSB0,115200 

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/4301afb5-f5d0-4d75-9b9e-b0c58d9a7d7f)

- and the ALES simulation

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/2a703b5e-8fb4-4fa6-a0bf-3b110e1c6e8e)

# 4 to change the locations 

- in linux search file like below 

![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/b33e8c50-8aa9-413e-bff4-4c2450f47efc)

- locations.txt
- inside file, there is a information about name, latitude, longitude, altitude and heading.
 
![image](https://github.com/UbaydullohML/VS-Projects_BugsFix/assets/75980506/10a270d1-4d57-4cc0-9ffc-6fa0e7d9a20f)

enter the code like below 

    sim_vehicle.py -w --map --console -v ArduCopter --out=/dev/ttyUSB0,115200 -L AVC_copter
- and just connect the sitl with ales,and everthing is fine. 
