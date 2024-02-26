namespace GasStation.Logic;


public class RNG 
{
    public static Random Random = new Random(42);
}

public class GasStation
{
    public List<Pump> Pumps {get; private set;}
}

public class Pump
{
    public Tank HighOctaneTank {get; private set;}
    public Tank LowOctaneTank {get; private set;}

    public bool PumpPremium(double amount){
        HighOctaneTank.Subtract(amount);
        return true;
    }

    public bool PumpRegular(double amount){
        LowOctaneTank.Subtract(amount);
        return true;
    }

    public bool PumpMixed(double amount){
        HighOctaneTank.Subtract(amount/2);
        LowOctaneTank.Subtract(amount/2);
        return true;
    }

    public Pump(Tank highOctaneTank, Tank lowOctaneTank){
        HighOctaneTank = highOctaneTank;
        LowOctaneTank = lowOctaneTank;
    }
}

public class Car
{
    public double GasDemand {get; private set;}
    public int TimeInSeconds {get; private set}
    
    public Car(double gasDemand, int timeInSeconds){
        GasDemand = gasDemand;
        TimeInSeconds = TimeInSeconds;
    }
}

public enum TankType {
    HighOctane,
    LowOctane
}

public class Tank {
    public double TotalAmount{get; private set;}
    public TankType Type {get; private set;}

    public Tank(TankType type, double totalAmount = 10000) {
        Type = type;
        TotalAmount = totalAmount;
    }

    public bool Subtract(double gasAmount) {
        
        if (gasAmount < TotalAmount && gasAmount > 0) {
            TotalAmount -= gasAmount;
            return true;
        }
        return false;
    }

    public bool Add(double gasAmount) {
        TotalAmount += gasAmount;
        return true;
    }
}