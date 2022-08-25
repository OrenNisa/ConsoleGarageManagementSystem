using System;

namespace Ex03.ConsoleUI
{
    public class Program : UI
    {
        public static void Main()
        {
            UI myGarage = new UI();
            myGarage.runGarageApp();


            GarageLogic.GarageManager gm = new GarageLogic.GarageManager();
            //gm.AddNewOrder("123213", GarageLogic.GarageManager.eVehicleType.FuelCar);
            //gm.AddNewOrder("8763459", GarageLogic.GarageManager.eVehicleType.FuelCar);
            foreach(string str in gm.GetAllLicenseNumbers())
            {
                Console.WriteLine(str);
            }

            Console.ReadLine();
        }

        public enum eVehicleType
        {
            FuelCar = 1,
            FuelMotorcycle,
            ElectricCar,
            ElectricMotorcycle,
            Truck,
        }
    }
}
