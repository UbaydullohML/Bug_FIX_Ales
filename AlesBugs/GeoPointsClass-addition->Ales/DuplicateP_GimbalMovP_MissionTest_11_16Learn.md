## Table of Tasks:
* [Duplicate Points](#duplicate_Points)
* [Gimbal Movement](#gimbal_Movement)
* [SITL Mission Planning](#sitl_mission_planning)
* [Test Mission Outputs](#code)
  

## Duplicate_Points

Task:
- the first and last points' duplicates have been fixed like the below (in order not to take the duplicates, cause the last point needs only 1 waypoint, no duplicate):

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/87692154-de18-496f-8289-5b22071d75a2)

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/1faf2d71-cd2a-43b9-8c3f-14078845fed9)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/96766845-06ed-4507-ab2c-bf0928bf919f)

## Gimbal_Movement

Task:
- The gimbal yaw and pitch catching code has been inserted, and it catches gimbal moved waypoints:
- like below where it compares to the previous state, and if there is gimbal degree movement, it stores that state waypoint
- whenever gimbal camera movements happen, code stores that change point

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/1b83628e-a132-4ea7-96ba-eb980dfdbbe1)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/41701f95-7d3f-45e9-bafc-6f171f34737a)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/d408439a-ffcc-4a50-9058-21045ae5399d)

## SITL_Mission_Planning

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/f89c26ce-3f64-4113-aa14-ab87ef95bcba)

Task:
- upload path (after setting the env)

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/0fdefcd0-067a-4d85-b58b-24ac8fad6e93)

first patrol path upload is done using the above button, 

- door open

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/5e2ffa7a-5199-4052-b4bd-0235026b9d94)

here, the door open 0 is changed to 1 to open the station door, as we can see below

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/0d47fbc3-7416-4a0f-8098-f89e9753af60)

we could check the station number and door open close status.

- arming
before arming the drone, we must change it to guided mode because other modes do not allow drones to be armed. Two methods are available:
 1st is inside Ales:

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/e80637a4-8f2c-4869-89bc-a877a3a916d1)

by double-clicking on the drone, we will open the window, and inside, there is a flight mode button.

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/7a3a4fc9-d81d-4d92-9dbb-9468a85b2ed4)

inside flight mode, we changed it to guided mode. We can change the status inside the SITL terminal, too.

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/69db318d-e2ab-436a-9956-d0a751ac776f)

 2nd is inside SITL

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/54b934cd-3dcd-497d-b8b4-223e032a0183)

like this above, with just typing

      GUIDED
or 

    stabilize // for stabilize mode

- start mission

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/0a933ed3-c94d-4075-bbbf-697b07ce0090)

if, after clicking the drone arming in the window, we get the below output,

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/c4fa7074-ed12-4dad-884e-bba62169ceed)

We set disarm delay a second time like below and rechecked:

    param set DISARM_DELAY 100

After entering disarm delay like above, we DRONE ARM AND START PATROL MISSION.

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/44197dfb-917a-4a4d-a593-246018c41bf2)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/e6186a0b-bf5e-493f-a750-f2716a5d5b03)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/c5e4b55b-1ac8-4ee0-bc22-903511a5805d)

after start mission we could see the mode is changed to AUTO

To see the waypoints in SITL, we code like the below.

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/c0c04ab2-73fb-42b3-933b-d46f1f11d638)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/c6f6460f-5477-4a5f-a49a-dbb23cf2d68f)

## Test_Mission:

Task:
- the code successfully down-sampled with caution values included/YawPitch degree movement waypoints caught/ with k-means clustering
- Mission test outputs SITL have been done as below. It works well:
- drone going up and down images:

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/a394efa1-f544-4b83-be06-595b3623522c)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/b9bb8deb-edf0-4f12-92bd-6208d3eab493)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/04a7937c-a47e-4c9c-bbc0-9b64a9f5ef72)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/11bac9b1-276e-472a-a67f-f16ec14ca784)


