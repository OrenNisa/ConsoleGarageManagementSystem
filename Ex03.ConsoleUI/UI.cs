using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        private const string k_ExitOption = "8";
        private const int k_MinOption = 1;
        private const int k_MaxOptionInMenu = 8;
        private const int k_MaxVehicleStates = 4;
        private const float k_MaxFuelChoice = 4;
        private static readonly GarageManager r_Garage = new GarageManager();

        public UI()
        {
            r_Garage.AddNewOrder(
                "123213",
                eVehicleType.FuelCar,
                "Jhon Wick",
                "0523415556",
                "Mercedez-Benz",
                24f,
                45f,
                2,
                3);
        }

        public void runGarageApp()
        {
            Console.WriteLine("Welcome to David&Oren garage application");
            showMenu();
            string userChoice = null;
            do
            {
                userChoice = getUsersChoice();
                Enum.TryParse(userChoice, out eMenuOptions userParsedChoice);
                switch(userParsedChoice)
                {
                    case eMenuOptions.RegisterNewVehicle:
                        {
                            registerNewVehicle();
                            break;
                        }

                    case eMenuOptions.GetIDsInGarage:
                        {
                            getIDsInGarage();
                            break;
                        }

                    case eMenuOptions.ChangeVehicleStatus:
                        {
                            changeVehicleStatus();
                            break;
                        }

                    case eMenuOptions.InflateToMax:
                        {
                            inflateToMax();
                            break;
                        }

                    case eMenuOptions.Refuel:
                        {
                            refuel();
                            break;
                        }

                    case eMenuOptions.Recharge:
                        {
                            recharge();
                            break;
                        }

                    case eMenuOptions.GetAllVehiclesDetails:
                        {
                            getAllVehiclesDetails();
                            break;
                        }

                    case eMenuOptions.Exit:
                        {
                            break;
                        }
                }
            }
            while (userChoice != k_ExitOption);
            Console.WriteLine("Thank you");
        }

        public void showMenu()
        {
            Console.WriteLine(@"Please enter your choice:
1. Register a new entered vehicle to garage
2. Show ID of vehicles in the garage
3. Change a vehicle's status
4. Inflate a vehicle's wheels to its maximum
5. Refuel a gasoline operated vehicle
6. Charge an electric vehicle
7. Show full vehicle details that in the garage
8. Exit");
        }

        private static string getUsersChoice()
        {
            string choice = null;
            try
            {
                choice = Console.ReadLine();
                int choiceAsInt = int.Parse(choice);
                isValueInRange(choiceAsInt, k_MinOption, k_MaxOptionInMenu);
            }
            catch(FormatException)
            {
                Console.WriteLine("Invalid input type");
            }
            catch(ValueOutOfRangeException i_ValueOutOfRangeException)
            {
                Console.WriteLine(i_ValueOutOfRangeException.Message);
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("Input can't be null");
            }
            finally
            {
               // Console.Clear();
            }

            return choice;
        }

        private static void isValueInRange(float i_ValueToCheck, float i_MinValue, float i_MaxValue)
        {
            if (i_ValueToCheck < i_MinValue || i_ValueToCheck > i_MaxValue)
            {
                throw new ValueOutOfRangeException("Invalid input, not in range", i_MaxValue, i_MinValue);
            }
        }

        private enum eMenuOptions
        {
            RegisterNewVehicle = 1,
            GetIDsInGarage,
            ChangeVehicleStatus,
            InflateToMax,
            Refuel,
            Recharge,
            GetAllVehiclesDetails,
            Exit,
        }

        private static void registerNewVehicle()
        {
            Console.WriteLine("Please enter license number of vehicle you want to handle:");
            string licenseNumInput = Console.ReadLine();
            bool isVehicleExist = r_Garage.Exists(licenseNumInput);
            if(isVehicleExist)
            {
                Console.WriteLine("This vehicle is already in the garage, status changed to 'InRepair'");
                r_Garage.ChangeVehicleState(licenseNumInput, eVehicleState.InRepair);
            }
            else
            {
                insertNewVehicle(licenseNumInput);
            }
        }

        private static void getIDsInGarage()
        {
            List<string> licenseList = r_Garage.GetAllLicenseNumbers();
            Console.WriteLine("Here's all license numbers in garage:");
            foreach(string licenseNumber in licenseList)
            {
                Console.WriteLine(licenseNumber);
            }

            Console.WriteLine("Filter by:");
            printStateTypes();
            Console.WriteLine("4. Back to main menu");
            string usrChoice = Console.ReadLine();
            isValueInRange(int.Parse(usrChoice), k_MinOption, k_MaxVehicleStates);
            eVehicleState useChoiceAsEnum = (eVehicleState)int.Parse(usrChoice);
            licenseList = r_Garage.GetAllLicenseNumbers(useChoiceAsEnum);
            foreach (string licenseNumber in licenseList)
            {
                Console.WriteLine(licenseNumber);
            }
        }

        private static void printStateTypes()
        {
            Console.WriteLine(@"1. In repair
2. Repaired
3. Paid");
        }

        private static void changeVehicleStatus()
        {
            Console.WriteLine("Enter license number of vehicle you want to change state:");
            string licenseNumber = Console.ReadLine();
            Console.WriteLine("Update to:");
            printStateTypes();
            string usrChoice = Console.ReadLine();
            eVehicleState useChoiceAsEnum = (eVehicleState)int.Parse(usrChoice);
            r_Garage.ChangeVehicleState(licenseNumber, useChoiceAsEnum);
            Console.WriteLine("State changed");
        }

        private static void inflateToMax()
        {
            Console.WriteLine("Enter license number of vehicle you want to inflate to max:");
            string licenseNumber = Console.ReadLine();
            r_Garage.InflateWheels(licenseNumber);
            Console.WriteLine("Wheels inflated to maximum");
        }

        private static void refuel()
        {
            Console.WriteLine("Enter license number of vehicle you want to refuel:");
            string licenseNumber = Console.ReadLine();
            Console.WriteLine("Select fuel type:");
            printFuelTypes();
            string usrChoice = Console.ReadLine();
            eFuelType useChoiceAsEnum = (eFuelType)int.Parse(usrChoice);
            Console.WriteLine("Enter the amount to fill");
            float amount = float.Parse(Console.ReadLine());
            r_Garage.Refuel(licenseNumber, useChoiceAsEnum, amount);
            }

        private static void printFuelTypes()
        {
            Console.WriteLine(@"1. Octan95
2. Octan96
3. Octan98
4. Soler");
        }

        private static void recharge()
        {
            // to fill
        }

        private static void getAllVehiclesDetails()
        {
            // to fill
        }

        private static void insertNewVehicle(string i_License)
        {
            eVehicleType parsedVehicleType;
            object[] listOfParameters = new object[5];
            string customerName;
            string customerPhone;
            string vehicleModel;
            float currentWheelsAirPressure;
            int userInput = 0;
            bool isElectric = false;

            bool isValid = false;
            do
            {
                try
                {
                    printVehicleType();
                    int vehicleType = int.Parse(Console.ReadLine());
                    isValueInRange(vehicleType, 1, 5);
                    isValid = true;
                    Enum.TryParse(vehicleType.ToString(), out eVehicleType userParsedChoice);
                    parsedVehicleType = userParsedChoice;
                }
                catch (FormatException)
                {
                    throw new FormatException();
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    throw i_ValueOutOfRangeException;
                }
            }
            while(!isValid);

            getGeneralInfo(listOfParameters, out customerName, out customerPhone);

            switch (parsedVehicleType)
            {
                case eVehicleType.FuelMotorcycle:
                    {
                        getFuelMotorcycleDetails(listOfParameters);
                        break;
                    }

                case eVehicleType.ElectricMotorcycle:
                    {
                        getElectricMotorcycleDetails(listOfParameters);
                        break;
                    }

                case eVehicleType.FuelCar:
                    {
                        getFuelCarDetails(listOfParameters);
                        break;
                    }

                case eVehicleType.ElectricCar:
                    {
                        getElectricCarDetails(listOfParameters);
                        break;
                    }

                case eVehicleType.FuelTruck:
                    {
                        getFuelTruckDetails(listOfParameters);
                        break;
                    }
            }

            r_Garage.AddNewOrder(i_License, parsedVehicleType, customerName, customerPhone, listOfParameters);
            Console.WriteLine("Vehicle added successfully");
        }

        private static void printVehicleType()
        {
            Console.WriteLine(@"Please choose a vehicle type number:
1. Motorcycle
2. Electric Motorcycle
3. Car
4. Electric Car
5. Truck");
        }

        private static void getGeneralInfo(object[] i_ListOfParameters, out string i_CustomerName, out string i_CustomerPhone)
        {
            Console.WriteLine("Enter customer name:");
            i_CustomerName = Console.ReadLine();
            Console.WriteLine("Enter customer's phone number:");
            i_CustomerPhone = Console.ReadLine();
            Console.WriteLine("Enter model name:");
            string modelName = Console.ReadLine();
            i_ListOfParameters[0] = modelName;
            Console.WriteLine("Enter current air pressure for tires:");
            float airPressure = float.Parse(Console.ReadLine());
            i_ListOfParameters[1] = airPressure;
        }

        private static void getFuelMotorcycleDetails(object[] i_ListOfParameters)
        {
            Console.WriteLine("Please enter battery hours remaining:");
            float batteryHoursRemaining = float.Parse(Console.ReadLine());
            i_ListOfParameters[2] = batteryHoursRemaining;
            Console.WriteLine("Please enter engine volume CC:");
            float EngineVolumeCC = int.Parse(Console.ReadLine());
            i_ListOfParameters[3] = EngineVolumeCC;
        }

        private static void getElectricMotorcycleDetails(object[] i_ListOfParameters)
        {
            // to fill
        }

        private static void getFuelCarDetails(object[] i_ListOfParameters)
        {
            // to fill
        }

        private static void getElectricCarDetails(object[] i_ListOfParameters)
        {
            // to fill
        }

        private static void getFuelTruckDetails(object[] i_ListOfParameters)
        {
            // to fill
        }
    }
}
