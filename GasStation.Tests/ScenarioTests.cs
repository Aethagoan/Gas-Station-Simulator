using NUnit.Framework;
using GasStation.Logic;
using System.Threading.Tasks;

namespace GasStation.Tests;

[TestFixture]
public class GasStationSimulationTests
{
    private Logic.GasStation gasStation;

    [SetUp]
    public void SetUp()
    {
        gasStation = new Logic.GasStation(2);
    }

    [Test]
    public async Task CarArrivesAndUsesAvailablePump()
    {
        var initialRevenue = gasStation.Revenue;
        gasStation.CarArrives();
        await Task.Delay(20000); // Wait enough time for the car to be served
        Assert.Greater(gasStation.Revenue, initialRevenue, "Revenue should increase after serving a car.");
    }

    [Test]
    public async Task CarWaitsForOccupiedPumpAndThenUsesIt()
    {
        // load pumps up
        for (int i = 0; i < gasStation.Pumps.Count; i++)
        {
            gasStation.CarArrives();
        }
        var initialCarCountAtPumps = gasStation.Pumps.Count(p => p.Current != null);

        // push another car
        gasStation.CarArrives();
        // wait a bit
        await Task.Delay(4000);

        // at this point the car should be gone and the next one moved into place
        var carCountAtPumpsAfter = gasStation.Pumps.Count(p => p.Current != null);
        Assert.AreEqual(initialCarCountAtPumps, carCountAtPumpsAfter, "A waiting car should use a pump after it becomes available.");
    }

    [Test]
    public async Task CarLeavesIfNoAvailablePumpOrWaitingSpace()
    {
        for (int i = 0; i < gasStation.Pumps.Count * 2; i++)
        {
            gasStation.CarArrives();
        }
        var initialCarCountAtPumps = gasStation.Pumps.Count(p => p.Current != null);
        var initialCarCountWaiting = gasStation.Pumps.Count(p => p.Next != null);

        gasStation.CarArrives();
        await Task.Delay(1000);

        var carCountAtPumpsAfter = gasStation.Pumps.Count(p => p.Current != null);
        var carCountWaitingAfter = gasStation.Pumps.Count(p => p.Next != null);
        Assert.AreEqual(initialCarCountAtPumps, carCountAtPumpsAfter, "No additional cars should be served.");
        Assert.AreEqual(initialCarCountWaiting, carCountWaitingAfter, "No additional cars should be waiting.");
    }

    [Test]
    public async Task CarLeavesIfNotEnoughGas()
    {
        gasStation.Pumps.ForEach(p =>
        {
            p.HighOctaneTank.Subtract(p.HighOctaneTank.TotalAmount - 1);
            p.LowOctaneTank.Subtract(p.LowOctaneTank.TotalAmount - 1);
        });

        var initialCarCountAtPumps = gasStation.Pumps.Count(p => p.Current != null);


        gasStation.CarArrives();


        var carCountAtPumpsAfter = gasStation.Pumps.Count(p => p.Current != null);
        Assert.AreEqual(initialCarCountAtPumps, carCountAtPumpsAfter, "Car should leave if there's not enough gas.");
    }

    [Test]
    public async Task SimulationCreatesCarsAtRandomIntervals()
    {

        var task = Task.Run(() => gasStation.StartSimulation());
        await Task.Delay(5000);

        Assert.IsTrue(gasStation.Pumps.Any(p => p.Current != null || p.Next != null), "Simulation should create cars and attempt to serve them.");
    }


    [Test]
    public async Task CarChoosesPumpWithSufficientSpecificGasType()
    {
        gasStation.Pumps[0].HighOctaneTank.Subtract(gasStation.Pumps[0].HighOctaneTank.TotalAmount - 500); 
        gasStation.Pumps[1].HighOctaneTank.Add(500); 

        gasStation.CarArrives();

        await Task.Delay(1000);

        var isServed = gasStation.Pumps.Any(p => p.Current != null && p.HighOctaneTank.TotalAmount >= 500);
        Assert.IsTrue(isServed, "Car should choose a pump with sufficient specific gas type.");
    }

    [Test]
    public async Task CarLeavesDueToLongWaitTimesDuringBusyPeriod()
    {
        for (int i = 0; i < gasStation.Pumps.Count * 2; i++)
        {
            gasStation.CarArrives();
        }

        var initialCarCount = gasStation.Pumps.Sum(p => p.Current != null ? 1 : 0) + gasStation.Pumps.Sum(p => p.Next != null ? 1 : 0);

        gasStation.CarArrives(); 
        await Task.Delay(2000); 

        var finalCarCount = gasStation.Pumps.Sum(p => p.Current != null ? 1 : 0) + gasStation.Pumps.Sum(p => p.Next != null ? 1 : 0);

        Assert.AreEqual(initialCarCount, finalCarCount, "Car count should not increase, indicating the new car left due to long wait times.");
    }

}