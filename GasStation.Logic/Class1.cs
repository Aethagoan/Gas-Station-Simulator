namespace GasStation.Logic;


public class RNG 
{
    public static Random Random = new Random(42);
}

public class GasStation
{
    private double Revenue = 0;
    public List<Pump> Pumps {get; private set;}
    public List<Car> Cars {get; private set;}

    private void CarArrives()
    {
        double gasDemand = RNG.Random.Next(5,20);
        int timeSpent = (int)gasDemand;
        TankType tankType;
        
        switch (RNG.Random.Next(0,3))
        {
            case 0:
                tankType = TankType.HighOctane;
                break;
            case 1:
                tankType = TankType.LowOctane;
                break;
            case 2:
                tankType = TankType.Mixed;
                break;
            default:
                tankType = TankType.Mixed;
                break;
        }
        
        Car car = new Car(gasDemand, timeSpent, tankType);

        // find available pump
        // bool foundpump = false;
        foreach (Pump p in Pumps)
        {
            if (p.CanServe(car))
            {
                p.Serve(car); // must not be awaited to run in the background
                break;
            }
        }
        // if no pump can serve the car it drives away
    }
    // car goes to pump

    // car pumps gas and pays -> tanks subtracted from -> Revenue is added to.

}

public class Pump
{
    public Tank HighOctaneTank {get; private set;}
    public Tank LowOctaneTank {get; private set;}

    public Car? Current = null;
    public Car? Next = null;
    
    public bool IsPumpOccupied() => Current != null;
    public bool IsSpaceOccupied() => Next != null;

    public async Task Serve(Car car)
    {
        if (CanServe(car))
        {
            if (!IsPumpOccupied())
            {
                Current = car;
                SubstractCarGasolineType(car);
                await Task.Delay(car.TimeInSeconds * 1000);
            }
            if (!IsSpaceOccupied())
            {
                Next = car;
            }
        }
    }

    private async void MakeCarWait(Car car, Task servingTask)
    {
        await servingTask;
        if (IsPumpOccupied())
            throw new Exception("Pump was occupied when it should be free");
        Serve(car);
    }

    private void SubstractCarGasolineType(Car car)
    {
        switch (car.TankType)
        {
            case TankType.HighOctane:
                PumpPremium(car.GasDemand);
                break;
            case TankType.LowOctane:
                PumpRegular(car.GasDemand);
                break;
            case TankType.Mixed:
                PumpMixed(car.GasDemand);
                break;
        }
    }

    public bool CanServe(Car car)
    {
        if (Current != null && Next != null)
            return false;
        if (car.TankType == TankType.HighOctane)
            return HighOctaneTank.TotalAmount > car.GasDemand;
        if (car.TankType == TankType.LowOctane)
            return LowOctaneTank.TotalAmount > car.GasDemand;
        return LowOctaneTank.TotalAmount > car.GasDemand / 2 && HighOctaneTank.TotalAmount > car.GasDemand / 2;
    }

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

    public Pump(Tank highOctaneTank, Tank lowOctaneTank, bool isPumpOccupied = false, bool isSpaceOccupied = false){
        HighOctaneTank = highOctaneTank;
        LowOctaneTank = lowOctaneTank;
        // IsPumpOccupied = isPumpOccupied;
        // IsSpaceOccupied = isSpaceOccupied;
    }
}

public class Car
{
    public double GasDemand {get; private set;}
    public int TimeInSeconds {get; private set;}
    public bool HasBeenServed {get; private set;} = false;
    public TankType TankType {get; private set;} 

    
    public Car(double gasDemand, int timeInSeconds, TankType tankType){
        GasDemand = gasDemand;
        TimeInSeconds = TimeInSeconds;
        TankType = tankType;
    }
}

public enum TankType {
    HighOctane,
    LowOctane,
    Mixed
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