# The Problem  
We are making a gas station simulation where cars arrive at a station, line up at a pump, pump gas and pay, and leave or/and buy things from the store.  
  
Our simulation will need to keep track of stockpiles for gasoline (tanks), amount of money paid, and sort of realistically simulate the time at the pump.  
  
## Wanted Features  
1. Car structure with an amount of gas requested plus the amount of time it would take to pump  
0. Tank strucutre that can keep track/has a method to subtract from the total.  
0. Some type of queue for cars to wait in, whether that be a literal line or available parking spaces.  
0. Revenue inside of gas station to keep track of money  
0. Some function that creates random cars and passes them along at random intervals.



### Records of group sessions  
Monday 26 February 2024 -  
Time start: 2:26 pm  
Time stop: 4:00 pm  
Attendees: All
Goal: Create basic underlying functionality for the Gas station objects.  
Accomplished: Wrote and tested the following classes: GasStation, Car, Pump, Tank.  
  
Tuesday 27 February 2024 -  
Time start: 10:00AM  
Time stop:  11:00AM  
Atendees: All  
Goal: Work on the project simulation logic.  
Accomplished: Created a lot of logic to verify if we can serve a car. And made a bit of logic to create cars we may need to refactor later. Updated Scenario Outlines.  

### Records of individual sessions - Agustin 
Wednesday 28 February 2024 -  
Time start: 9:00 AM  
Time stop:  9:35 AM  
Goal: Finish the project simulation logic.  
Accomplished: Simulation can be run and works as expected. Can be displayed to the console.  
  
  
### Records of individual sessions - Aidan  
Friday 1 March 2024 -  
Time start: 10:20AM  
Time stop: 11:05AM  
Goal: Fix some Simulation logic to better reflect the real world.  
Accomplished: Made the logic work better, runs as expected.  
  
### Records of individual sessions - Helen  

  
  
# Scenario outlines  

[FULLFILLED]  
I) A car arrives at the station.  
The car finds an available pump.  
The car takes time to pump gas with no issues (and pays).  
The car leaves.  

[FULLFILLED]  
II) A car arrives at the station.  
The car cannot find an available pump.  
The car finds an available spot to wait at the pump.  
The car is willing to wait for a period longer than the person at the pump takes.  
Car at the pump finishes.  
The car moves to the pump.  
The car takes time to pump gas with no issues (and pays).  
The car leaves.  

[FULLFILLED]  
III) A car arrives at the station.  
The car cannot find an available pump.  
The car cannot find an available space.  
The car leaves.   

[FULFILLED]  
IV) A car arrives at the station.  
The car finds an available pump.  
The station does not have enough gas to serve the car.  
The car leaves.  

[FULLFILLED]  
V) The simulation creates cars at random intervals.  
The simulation gives the cars to the station.  
The station handles the cars.  
  
VI)  

