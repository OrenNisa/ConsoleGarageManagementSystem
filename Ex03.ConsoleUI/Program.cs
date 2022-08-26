using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program : UI
    {
        public static void Main()
        {
            UI myGarage = new UI();
            myGarage.runGarageApp();

            Console.WriteLine("Hello");
            GarageManager gm = new GarageManager();
            gm.AddNewOrder(
                "123213",
                eVehicleType.FuelCar,
                "Jhon Wick",
                "0523415556",
                "Mercedez-Benz",
                24f,
                45f,
                2,
                3);

            // gm.AddNewOrder("8763459", GarageManager.eVehicleType.FuelTruck);
            // gm.InflateWheels("8763459");
            // gm.Refuel("8763459", eFuelType.Soler, 15);
            // gm.EditOrder("123213", GarageManager.eVehicleType.FuelCar, "Jhon Wick", "0523415556", "Mercedez-Benz", 24f, 45f, 2, 3);
            foreach(string str in gm.GetAllLicenseNumbers())
            {
                Console.WriteLine(str);
            }

            Console.WriteLine(gm.GetOrderDetails("123213"));
            Console.ReadLine();
        }
    }

}
