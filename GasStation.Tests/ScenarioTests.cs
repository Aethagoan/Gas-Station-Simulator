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
        var initialRevenue = Logic.GasStation.Revenue;
        await gasStation.CarArrives(); 
        await Task.Delay(20000); // Wait enough time for the car to be served
        Assert.Greater(Logic.GasStation.Revenue, initialRevenue, "Revenue should increase after serving a car.");
    }

    [Test]
    public async Task CarWaitsForOccupiedPumpAndThenUsesIt()
    {
        await gasStation.CarArrives(); 
        var initialCarCountAtPumps = gasStation.Pumps.Count(p => p.Current != null);

        await gasStation.CarArrives();
        await Task.Delay(4000);

        var carCountAtPumpsAfter = gasStation.Pumps.Count(p => p.Current != null);
        Assert.AreEqual(initialCarCountAtPumps, carCountAtPumpsAfter, "A waiting car should use a pump after it becomes available.");
    }

    [Test]
    public async Task CarLeavesIfNoAvailablePumpOrWaitingSpace()
    {
        for (int i = 0; i < gasStation.Pumps.Count * 2; i++)
        {
            await gasStation.CarArrives();
        }
        var initialCarCountAtPumps = gasStation.Pumps.Count(p => p.Current != null);
        var initialCarCountWaiting = gasStation.Pumps.Count(p => p.Next != null);

        await gasStation.CarArrives();
        await Task.Delay(1000);

        var carCountAtPumpsAfter = gasStation.Pumps.Count(p => p.Current != null);
        var carCountWaitingAfter = gasStation.Pumps.Count(p => p.Next != null);
        Assert.AreEqual(initialCarCountAtPumps, carCountAtPumpsAfter, "No additional cars should be served.");
        Assert.AreEqual(initialCarCountWaiting, carCountWaitingAfter, "No additional cars should be waiting.");
    }

    [Test]
    public async Task CarLeavesIfNotEnoughGas()
    {
        gasStation.Pumps.ForEach(p => {
            p.HighOctaneTank.Subtract(p.HighOctaneTank.TotalAmount - 1);
            p.LowOctaneTank.Subtract(p.LowOctaneTank.TotalAmount - 1);
        });

        var initialCarCountAtPumps = gasStation.Pumps.Count(p => p.Current != null);


        await gasStation.CarArrives();
    

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
}