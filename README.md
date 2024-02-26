# The Problem  
We are making a gas station simulation where cars arrive at a station, line up at a pump, pump gas and pay, and leave or/and buy things from the store.  
  
Our simulation will need to keep track of stockpiles for gasoline (tanks), amount of money paid, and sort of realistically simulate the time at the pump.  
  
## Wanted Features  
1. Car structure with an amount of gas requested plus the amount of time it would take to pump  
0. Tank strucutre that can keep track/has a method to subtract from the total.  
0. Some type of queue for cars to wait in, whether that be a literal line or available parking spaces.  



### Records of group sessions  
Monday 26 February 2024 -  
Time start: 2:26 pm  
Time stop: 4:00 pm  
Goal: Create basic underlying functionality for the Gas station objects.  
Accomplished: Wrote and tested the following classes: GasStation, Car, Pump, Tank.  
  
### Records of individual sessions - Agustin  

  
### Records of individual sessions - Aidan  
  

### Records of individual sessions - Helen  


  
# Scenario outlines  

1. New cars arrive at the station at a pseudo-random rate  
2. A car will remain at a pump for a pseudo-random amount of time  
3. A car will pump a pseudo-random quantity of a pseudo-randomly selected grade of gas  
4. If a pump is out of gas then a car must go to another pump  
5. If there are no available pumps a car will wait in one of the available spaces next to a pump for a pseudo-random amount of time  
6. If there are no available spaces to wait the car will drive away and give business to someone else  

