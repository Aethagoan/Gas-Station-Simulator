namespace GasStation.Logic;
public class RNG
{
    public static Random Random = new Random(42);
}
public class GasStation
{
    public double Revenue {get; set;} = 0;
    public List<Pump> Pumps { get; private set; }
    public List<Car> Cars { get; private set; }
    public async Task CarArrives()
    {
        double gasDemand = RNG.Random.Next(5, 20);
        int timeSpent = (int)gasDemand;
        TankType tankType;
        switch (RNG.Random.Next(0, 3))
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

        // A new car is created, and then looking at all the pumps CanServe is Called.

        bool foundPump = false;

        foreach (Pump p in Pumps)
        {
            // can serve makes a car wait in line for a pump if it is occupied. This is not how the simulation should work.
            // the simulation should look at all of the pumps first, and then if they are all full it should wait.


            if (p.CanServe(car)) 
            { // if there is enough gas at the station
                /* this is where the problem is. Just because a pump (or more accurately in the current context, a "line" at a pump)
                    can serve a car, it doesn't mean it should.
                    each car should check for empty pumps first before waiting.
                */
                // if someone is here, skip!
                if (p.Current != null){
                    continue;
                }
                else {
                    foundPump = true;
                    p.Serve(car);
                    break;
                }

                // p.Serve(car); // must not be awaited to run in the background
                // break;
            }


        }

        // when the foreach loop above exits, if we haven't found a pump, we then look for a space to sit at.

        if (!foundPump) {
            foreach (Pump p in Pumps) {

                if (p.CanServe(car)) { // if there is enough gas at the station

                    if (p.Next != null) { // if someone is already waiting here, skip
                        continue;
                    }
                    else { // otherwise we then can serve the person (put them into the line.)
                        foundPump = true;
                        p.Serve(car);
                        Revenue += car.GasDemand * 12.45;
                        break;
                    }

                }


            }
            // if the foreach loop exits without anything happening (all spots are full) we can do something here using foundpump if we want to keep track of something
            // example
            if (!foundPump) {
                // we could not serve this person
            }
        }





        // if no pump can serve the car it drives away
    }


    
    public GasStation(int numberOfPumps)
    {
        Pumps = new List<Pump>();
        Tank tank1 = new Tank(TankType.HighOctane);
        Tank tank2 = new Tank(TankType.LowOctane);
        for (int i = 0; i < numberOfPumps; i++)
        {
            Pumps.Add(new Pump(this, tank1, tank2));
        }
    }

    // simulation waits for random amount of time and then calls CarArrives();
    public async void StartSimulation()
    {
        while (true)
        {
            await Task.Delay(RNG.Random.Next(1, 5000));
            CarArrives();
        }
    }

    public void DisplaySimulation()
    {
        Console.Clear();
        Console.WriteLine("Gas Station Simulation Status:");
        Console.WriteLine($"Total Revenue: {Revenue:C}");
        Console.WriteLine("Pumps Status:");
        foreach (var pump in Pumps)
        {
            var currentCarInfo = pump.Current != null ? $"Car demanding {pump.Current.GasDemand}L of {pump.Current.TankType}" : "No car";
            var nextCarInfo = pump.Next != null ? $"Next Car demanding {pump.Next.GasDemand}L of {pump.Next.TankType}" : "No next car";
            Console.WriteLine($"- Pump [{Pumps.IndexOf(pump) + 1}]: Current: {currentCarInfo}, Next: {nextCarInfo}");
        }
        Console.WriteLine("Tanks Status:");
        Pump p = Pumps[0]; // all pumps point to the same tanks
        Console.WriteLine($"- High Octane Tank: {p.HighOctaneTank.TotalAmount}L left");
        Console.WriteLine($"- Low Octane Tank: {p.LowOctaneTank.TotalAmount}L left");
    }
}
public class Pump
{
    public GasStation gasStation;
    public Tank HighOctaneTank { get; private set; }
    public Tank LowOctaneTank { get; private set; }
    public Car? Current = null;
    public Car? Next = null;
    private Task CurrentTask;
    public bool IsPumpOccupied() => Current != null;
    public bool IsSpaceOccupied() => Next != null;
    public async Task Serve(Car car)
    {
        if (CanServe(car))
        {
            if (!IsPumpOccupied())
            {
                Current = car;
                CurrentTask = Task.Run(async () =>
                {
                    SubstractCarGasolineType(car);
                    await Task.Delay(car.TimeInSeconds * 1000);
                    gasStation.Revenue += car.GasDemand * 12.45;
                    Current = null; 
                });
                await CurrentTask;
            }
            else if (!IsSpaceOccupied())
            {
                Next = car;
                MakeCarWait(car, CurrentTask);
            }
        }
    }

    private async void MakeCarWait(Car car, Task servingTask)
    {
        await servingTask;
        if (IsPumpOccupied())
            throw new Exception("Pump was occupied when it should be free");
        Next = null;
        await Serve(car);
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

    public bool PumpPremium(double amount)
    {
        HighOctaneTank.Subtract(amount);
        return true;
    }
    public bool PumpRegular(double amount)
    {
        LowOctaneTank.Subtract(amount);
        return true;
    }
    public bool PumpMixed(double amount)
    {
        HighOctaneTank.Subtract(amount / 2);
        LowOctaneTank.Subtract(amount / 2);
        return true;
    }

    public Pump(GasStation station, Tank highOctaneTank, Tank lowOctaneTank, bool isPumpOccupied = false, bool isSpaceOccupied = false)
    {
        gasStation = station;
        HighOctaneTank = highOctaneTank;
        LowOctaneTank = lowOctaneTank;
        // IsPumpOccupied = isPumpOccupied;
        // IsSpaceOccupied = isSpaceOccupied;
    }
}
public class Car
{
    public double GasDemand { get; private set; }
    public int TimeInSeconds { get; private set; }
    public bool HasBeenServed { get; private set; } = false;
    public TankType TankType { get; private set; }
    public Car(double gasDemand, int timeInSeconds, TankType tankType)
    {
        GasDemand = gasDemand;
        TimeInSeconds = timeInSeconds;
        TankType = tankType;
    }
}
public enum TankType
{
    HighOctane,
    LowOctane,
    Mixed
}
public class Tank
{
    public double TotalAmount { get; private set; }
    public TankType Type { get; private set; }
    public Tank(TankType type, double totalAmount = 10000)
    {
        Type = type;
        TotalAmount = totalAmount;
    }
    public bool Subtract(double gasAmount)
    {
        if (gasAmount < TotalAmount && gasAmount > 0)
        {
            TotalAmount -= gasAmount;
            return true;
        }
        return false;
    }
    public bool Add(double gasAmount)
    {
        TotalAmount += gasAmount;
        return true;
    }
}