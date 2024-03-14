# The Problem  
We are making a gas station simulation where cars arrive at a station, line up at a pump, pump gas and pay, and leave or/and buy things from the store.  
  
Our simulation will need to keep track of stockpiles for gasoline (tanks), amount of money paid, and sort of realistically simulate the time at the pump.  
  
## Wanted Features  
1. Car structure with an amount of gas requested plus the amount of time it would take to pump  
0. Tank strucutre that can keep track/has a method to subtract from the total.  
0. Some type of queue for cars to wait in, whether that be a literal line or available parking spaces.  
0. Revenue inside of gas station to keep track of money.  
0. Some function that creates random cars and passes them along at random intervals.  
0. Some way to pause / resume the simulation.  
0. A way to modify all values in the simulation. (Over acheiving)  
0. Have a clerk powered by a large language model that answers all questions from customers  

  
  
# Week Retrospective - Week 1  
  
## What Went Well  
- Core Functionality: Created and tested core classes (`GasStation`, `Car`, `Pump`, `Tank`) effectively.  
- Simulation Logic: Developed robust simulation logic that accurately manages car arrivals, servicing, and payment.  
- Gas Blend Handling: Implemented functionality for handling different gas blends and ensuring accurate servicing.  
- UI Development: Successfully developed a functional UI for visualizing the simulation's state.  
- Testing: Achieved comprehensive test coverage for most scenario outlines, ensuring the simulation's reliability.  
  
## New Stuff to Try Next Time  
- Queue Management Algorithms: Investigate advanced queue management algorithms for better peak time handling.  
- Dynamic Pricing Feature: Implement dynamic pricing based on demand and supply for added realism.  
- Customer Feedback Mechanism: Add a mechanism to simulate customer feedback on wait times and service quality.  
  
## Stuff We Did This Time That We Will Not Repeat Next Time  
- Delayed Integration Testing: Early integration testing between `Tank` and `Pump` classes was overlooked, leading to late discovery of issues. Next time, we'll integrate and test components earlier in the development cycle.  
- Concurrency Challenges: Encountered challenges with concurrency and simultaneous car arrivals. Future projects will incorporate concurrency considerations from the start.  
- Manual Testing Reliance: Over-relied on manual testing for certain aspects, which was inefficient. We plan to invest in automated testing, particularly for UI and integration tests, to enhance test efficiency and coverage.  
  
  
### Records of group sessions  
Monday 26 February 2024 -  
Time start: 2:26 pm  
Time stop: 4:00 pm  
Attendees: All  
Goal: Create basic underlying functionality for the Gas station objects.  
Accomplished: Wrote and tested the following classes: GasStation, Car, Pump, Tank.  
  
Tuesday 27 February 2024 -  
Time start: 10:00 am  
Time stop:  11:00 am  
Atendees: All.  
Goal: Work on the project simulation logic.  
Accomplished: Created a lot of logic to verify if we can serve a car. And made a bit of logic to create cars we may need to refactor later. Updated Scenario Outlines.  
  
Thursday 14 March 2024 -  
Time start: 11:30 am  
Time stop: 12:30 am  
Attendees: All.  
Goal: Reach milestone 2.  
Accomplished: fullfilled all requirements in milestone 2.  
  
### Log of group daily standups  
Monday 26 February 2024 -  
Attendees: All.  
Accomplished: Wrote and tested the following classes: GasStation, Car, Pump, Tank.  
Blockers: Had issues integrating Tank and Pump classes to accurately track gas consumption and volume.
  
Tuesday 27 February 2024 -  
Attendees: All.  
Accomplished: Created a lot of logic to verify if we can serve a car. And made a bit of logic to create cars we may need to refactor later. Updated Bl
Blockers: Concurrency issues with car arrivals, especially in handling multiple cars arriving simultaneously and managing their orderly service or waiting process.

Thursday 14 March 2024 -  
Attendees: All  
Log/Accomplished: Went over milestone 2, figured out what we needed and how we'd code it.  
Blockers: none this time around.  

### Records of individual sessions - Agustin 
Wednesday 28 February 2024 -  
Time start: 9:00 AM  
Time stop:  9:35 AM  
Goal: Finish the project simulation logic.  
Accomplished: Simulation can be run and works as expected. Can be displayed to the console.  

Friday 1 March 2024 -  
Time start: 6:00 PM  
Time stop:  7:39 PM  
Goal: Test scenarios
Accomplished: wrote passing tests for 5 of 7 scenarios in ScenarioTests.cs

Friday 2 March 2024 -  
Time start: 6:40 PM  
Time stop:  7:28 PM  
Goal: Finish scenario outline and tests
Accomplished: Added two more scenarius to the outline and wrote passing tests
  
  
### Records of individual sessions - Aidan  
Friday 1 March 2024 -  
Time start: 10:20AM  
Time stop: 11:05AM  
Goal: Fix some Simulation logic to better reflect the real world.  
Accomplished: Made the logic work better, runs as expected.  

Saturday 2 March 2024 -  
Time start: 9:00 AM  
Time stop: 11:25 AM  
Goal: Create a working UI  
Accomplished: Made a UI that works  
  
  
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

[FULFILLED]  
VI) A car arrives needing a specific blend of gas.  
The station calculates the blend using available gas types.  
The car is served with the correct blend (e.g., 50-50 high and low octane for mid-grade).  
The car leaves after paying based on the amount and type of gas.  

[FULFILLED]  
VII) Multiple cars arrive simultaneously when the station has limited availability.  
First car finds an available pump and starts refueling.  
Second car waits due to all pumps being occupied.  
Third car leaves because both pumps and waiting spots are occupied.  
As the first car leaves, the second car moves to the pump, and the simulation handles it accordingly.

VIII) Ability to add cars to the simultion with a button.  
Simulation Starts.  
The Add Car button on the UI is pressed.  
A Car is added to the simulation.  
  
IX) Ability to pause and resume the simulation.  
Simulation starts.  
Pause button is pressed.  
Simulation Pauses.  
Resume button is pressed.  
Simulation resumes.  

X) Ability to modify the simulation during a Pause.  
Simulation Starts.  
Pause button is pressed.  
Values in the simulation, like how much gas is in the station tanks, become modifyable.  
Value(s) modified.  
Simulation Resumes.  
Simulation plays out correctly.  
  
XI) Customer insults the clerk and leaves if there is no space at the station (We will keep track of lost revenue)  

