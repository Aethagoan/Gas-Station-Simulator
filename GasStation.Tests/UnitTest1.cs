using NUnit.Framework;
using GasStation.Logic;
using System;

namespace GasStation.Tests;

[TestFixture]
public class GasStationTests
{
    private Tank highOctaneTank;
    private Tank lowOctaneTank;
    private Pump pump;

    [SetUp]
    public void SetUp()
    {
        highOctaneTank = new Tank(TankType.HighOctane, 5000);
        lowOctaneTank = new Tank(TankType.LowOctane, 5000);
        pump = new Pump(highOctaneTank, lowOctaneTank);
    }

    [Test]
    public void PumpRegular_SubtractsCorrectAmountFromLowOctaneTank()
    {
        double amount = 100;
        pump.PumpRegular(amount);
        Assert.AreEqual(4900, lowOctaneTank.TotalAmount);
    }

    [Test]
    public void PumpPremium_SubtractsCorrectAmountFromHighOctaneTank()
    {
        double amount = 100;
        pump.PumpPremium(amount);
        Assert.AreEqual(4900, highOctaneTank.TotalAmount);
    }

    [Test]
    public void PumpMixed_SubtractsCorrectAmountFromBothTanks()
    {
        double amount = 200;
        pump.PumpMixed(amount);
        Assert.AreEqual(4900, highOctaneTank.TotalAmount);
        Assert.AreEqual(4900, lowOctaneTank.TotalAmount);
    }

    [Test]
    public void Subtract_MoreThanAvailable_ReturnsFalse()
    {
        bool result = highOctaneTank.Subtract(6000);
        Assert.IsFalse(result);
    }

    [Test]
    public void Subtract_NegativeAmount_ReturnsFalse()
    {
        bool result = highOctaneTank.Subtract(-100);
        Assert.IsFalse(result);
    }

    [Test]
    public void Add_IncreasesTotalAmount()
    {
        double amountToAdd = 500;
        highOctaneTank.Add(amountToAdd);
        Assert.AreEqual(5500, highOctaneTank.TotalAmount);
    }


    [Test]
     public void DisplaySimulation()
    {
        var GasStation = new GasStation.Logic.GasStation(4);
        GasStation.DisplaySimulation();
    }
}



