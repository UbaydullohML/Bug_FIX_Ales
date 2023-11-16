## Table of Tasks:
* [Intro](#intro)
* [Reverse](#reverse)

 
## Intro
create csv file
Task:
- input reverse waypoints have been added for going and returning points of drone into CSV file like the below:
- it is the full data for the drone to go there and return [1 - 737]
- the 737 point is also the S point for Station coordinates.
- so the drone starts from 737 or S goes to 1st and continues according to full data.
- after applying the downSampleLogic, these points will be down sampled like 2nd image

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/5f8b5e74-9587-49b6-a46c-801db6ce4730)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/7ef65e05-2245-47cb-a546-eb31aae1bba0)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/12e4e38f-e062-4c9f-a5ad-538d1244104b)

## Reverse
input reverse waypoints on map
task:
- on mapping of waypoints:
- the issue of going and not returning correctly is because waypoints are not precisely the same on one way up and the second way down. The reason is that k-means clustering groups nearby waypoints and selects the mean of the group and, every time, the selection isn't exact on one way and another way.
- so the solution is to apply k-means clustering one way and use exact same on return.

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/9b2729a7-0671-42d9-ad09-3bd82dc10023)

2nd task:
- putting the downsampled waypoints in reverse on map is done like the below image:

![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/d6096026-ee29-429b-8ae1-01ad7f7a6e79)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/2ae27757-afc6-4052-b78b-932227afd85d)
![image](https://github.com/UbaydullohML/VS-Projects/assets/75980506/5f5747be-3f44-43a5-a392-d8f19a9abe4e)
