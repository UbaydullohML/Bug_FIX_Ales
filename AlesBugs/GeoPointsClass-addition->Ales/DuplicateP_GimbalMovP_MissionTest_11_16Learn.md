## Table of Tasks:
* [Duplicate_Points](#duplicate_Points)
* [Gimbal_Movement](#gimbal_Movement)
* [SITL_Mission](#sitlmission)
* [Test_Mission](#code)
  

## Duplicate Points

Task:
- the first and last points' duplicates have been fixed like the below (in order not to take the duplicates, cause the last point needs only 1 waypoint, no duplicate):

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/87692154-de18-496f-8289-5b22071d75a2)

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/1faf2d71-cd2a-43b9-8c3f-14078845fed9)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/96766845-06ed-4507-ab2c-bf0928bf919f)

## Gimbal Movement

Task:
- The gimbal yaw and pitch catching code has been inserted, and it catches gimbal moved waypoints:
- like below where it compares to the previous state, and if there is gimbal degree movement, it stores that state waypoint
- whenever gimbal camera movements happen, code stores that change point

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/1b83628e-a132-4ea7-96ba-eb980dfdbbe1)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/41701f95-7d3f-45e9-bafc-6f171f34737a)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/d408439a-ffcc-4a50-9058-21045ae5399d)

## Test Mission:

Task:
- the code successfully down-sampled with caution values included/YawPitch degree movement waypoints caught/ with k-means clustering
- Mission test outputs SITL have been done as below. It works well:
- drone going up and down images:

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/a394efa1-f544-4b83-be06-595b3623522c)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/b9bb8deb-edf0-4f12-92bd-6208d3eab493)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/04a7937c-a47e-4c9c-bbc0-9b64a9f5ef72)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/11bac9b1-276e-472a-a67f-f16ec14ca784)


