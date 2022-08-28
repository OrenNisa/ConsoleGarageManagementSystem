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
        private const int k_MaxVehicleType = 5;
        private const int k_MaxVehicleStates = 3;
        private const float k_MaxFuelChoice = 4;
        private static readonly GarageManager sr_Garage = new GarageManager();

        public void RunGarageApp()
        {
            Console.WriteLine("Welcome to David&Oren garage application");
            string userChoice = null;
            do
            {
                try
                {
                    showMenu();
                    userChoice = getUsersChoice();
                    Enum.TryParse(userChoice, out eMenuOptions userParsedChoice);
                    switch (userParsedChoice)
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

                        case eMenuOptions.GetAllVehicleDetails:
                            {
                                getAllVehicleDetails();
                                break;
                            }

                        case eMenuOptions.Exit:
                            {
                                Console.WriteLine("Thank you, goodbye.");
                                Console.ReadLine();
                                break;
                            }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input type");
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.WriteLine(valueOutOfRangeException.Message);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Input can't be null");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (userChoice != k_ExitOption);

            Console.WriteLine("Thank you");
        }

        private void showMenu()
        {
            Console.WriteLine(@"
Please enter your choice:
1. Register a new entered vehicle to garage
2. Show ID of vehicles in the garage
3. Change a vehicle's status
4. Inflate a vehicle's wheels to its maximum
5. Refuel a gasoline operated vehicle
6. Charge an electric vehicle
7. Show full vehicle details that in the garage
8. Exit");
        }

        private string getUsersChoice()
        {
            string choice = Console.ReadLine();
            int choiceAsInt = int.Parse(choice);
            isValueInRange(choiceAsInt, k_MinOption, k_MaxOptionInMenu);
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
            GetAllVehicleDetails,
            Exit,
        }

        private static void registerNewVehicle()
        {
            string licenseNumInput = getLicenseNumberFromUser();
            bool isVehicleExist = sr_Garage.Exists(licenseNumInput);
            if (isVehicleExist)
            {
                Console.WriteLine("This vehicle is already in the garage, status changed to 'InRepair'");
                sr_Garage.ChangeVehicleState(licenseNumInput, eVehicleState.InRepair);
            }
            else
            {
                int vehicleType = getVehicleType();
                string[] clientDetails = getClientDetails();
                object[] vehicleParameters = null;

                switch ((eVehicleType)vehicleType)
                {
                    case eVehicleType.FuelCar:
                        getFuelCarParameters(out vehicleParameters);
                        break;
                    case eVehicleType.FuelMotorcycle:
                        getFuelMotorcycleParameters(out vehicleParameters);
                        break;
                    case eVehicleType.ElectricCar:
                        getElectricCarParameters(out vehicleParameters);
                        break;
                    case eVehicleType.ElectricMotorcycle:
                        getElectricMotorcycleParameters(out vehicleParameters);
                        break;
                    case eVehicleType.FuelTruck:
                        getFuelTruckParameters(out vehicleParameters);
                        break;
                }

                sr_Garage.AddNewOrder(licenseNumInput, (eVehicleType)vehicleType, clientDetails[0], clientDetails[1], vehicleParameters);
                Console.WriteLine("This vehicle was successfully added in the garage, status 'InRepair'");
            }
        }

        private static void getFuelCarParameters(out object[] i_VehicleParameters)
        {
            i_VehicleParameters = new object[5];
            i_VehicleParameters[0] = getModelName();
            i_VehicleParameters[1] = getAirPressure();
            i_VehicleParameters[2] = getFuelLiters();
            i_VehicleParameters[3] = getColor();
            i_VehicleParameters[4] = getDoorsNumber();
        }

        private static void getFuelMotorcycleParameters(out object[] i_VehicleParameters)
        {
            i_VehicleParameters = new object[5];
            i_VehicleParameters[0] = getModelName();
            i_VehicleParameters[1] = getAirPressure();
            i_VehicleParameters[2] = getFuelLiters();
            i_VehicleParameters[3] = getEngineVolume();
            i_VehicleParameters[4] = getLicenseType();
        }

        private static void getElectricCarParameters(out object[] i_VehicleParameters)
        {
            i_VehicleParameters = new object[5];
            i_VehicleParameters[0] = getModelName();
            i_VehicleParameters[1] = getAirPressure();
            i_VehicleParameters[2] = getBatteryRemaining();
            i_VehicleParameters[3] = getColor();
            i_VehicleParameters[4] = getDoorsNumber();
        }

        private static void getElectricMotorcycleParameters(out object[] i_VehicleParameters)
        {
            i_VehicleParameters = new object[5];
            i_VehicleParameters[0] = getModelName();
            i_VehicleParameters[1] = getAirPressure();
            i_VehicleParameters[2] = getBatteryRemaining();
            i_VehicleParameters[3] = getEngineVolume();
            i_VehicleParameters[4] = getLicenseType();
        }

        private static void getFuelTruckParameters(out object[] i_VehicleParameters)
        {
            i_VehicleParameters = new object[5];
            i_VehicleParameters[0] = getModelName();
            i_VehicleParameters[1] = getAirPressure();
            i_VehicleParameters[2] = getFuelLiters();
            i_VehicleParameters[3] = getMaxLoadWeight();
            i_VehicleParameters[4] = getTransportsRefrigerated();
        }

        private void getIDsInGarage()
        {
            List<string> licenseList = sr_Garage.GetAllLicenseNumbers();
            Console.WriteLine("Here's all license numbers in garage:");
            foreach (string licenseNumber in licenseList)
            {
                Console.WriteLine(licenseNumber);
            }

            Console.WriteLine("Filter by:");
            printStateTypes();
            Console.WriteLine("4. Back to main menu");
            string usrChoice = Console.ReadLine();
            isValueInRange(int.Parse(usrChoice), k_MinOption, k_MaxVehicleStates + 1);
            eVehicleState useChoiceAsEnum = (eVehicleState)int.Parse(usrChoice);
            licenseList = sr_Garage.GetAllLicenseNumbers(useChoiceAsEnum);
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

        private void changeVehicleStatus()
        {
            Console.WriteLine("Enter license number of vehicle you want to change state:");
            string licenseNumber = getLicenseNumberFromUser();
            Console.WriteLine("Update to:");
            printStateTypes();
            string usrChoice = Console.ReadLine();
            isValueInRange(int.Parse(usrChoice), k_MinOption, k_MaxVehicleStates);
            eVehicleState useChoiceAsEnum = (eVehicleState)int.Parse(usrChoice);
            sr_Garage.ChangeVehicleState(licenseNumber, useChoiceAsEnum);
            Console.WriteLine("State changed");
        }

        private void inflateToMax()
        {
            Console.WriteLine("Enter license number of vehicle you want to inflate to max:");
            string licenseNumber = getLicenseNumberFromUser();
            sr_Garage.InflateWheels(licenseNumber);
            Console.WriteLine("Wheels inflated to maximum");
        }

        private void refuel()
        {
            Console.WriteLine("Enter license number of vehicle you want to refuel:");
            string licenseNumber = getLicenseNumberFromUser();
            Console.WriteLine("Select fuel type:");
            printFuelTypes();
            string usrChoice = Console.ReadLine();
            isValueInRange(int.Parse(usrChoice), k_MinOption, k_MaxFuelChoice);
            eFuelType useChoiceAsEnum = (eFuelType)int.Parse(usrChoice);
            Console.WriteLine("Enter the amount to fill");
            float amount = float.Parse(Console.ReadLine());
            sr_Garage.Refuel(licenseNumber, useChoiceAsEnum, amount);
            Console.WriteLine("Vehicle has been refueled");
        }

        private void printFuelTypes()
        {
            Console.WriteLine(@"1. Octan95
2. Octan96
3. Octan98
4. Soler");
        }

        private void recharge()
        {
            Console.WriteLine("Enter license number of vehicle you want to recharge:");
            string licenseNumber = Console.ReadLine();
            Console.WriteLine("Enter the amount of minutes to charge");
            float amount = float.Parse(Console.ReadLine());
            sr_Garage.Charge(licenseNumber, amount);
            Console.WriteLine("Vehicle has been recharged");
        }

        private void getAllVehicleDetails()
        {
            Console.WriteLine("Enter license number of vehicle you want to get details of:");
            string licenseNumber = getLicenseNumberFromUser();
            Console.WriteLine(sr_Garage.GetOrderDetails(licenseNumber));
        }

        private static string getLicenseNumberFromUser()
        {
            bool isValid = false;
            string inputStr;
            Console.Clear();
            do
            {
                Console.WriteLine("Please Enter License Number: (only 7 or 8 digits length available)");
                inputStr = Console.ReadLine();
                if (inputStr.Length >= 8 || inputStr.Length <= 7)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid Input. try again");
                }
            }
            while (!isValid);

            return inputStr;
        }

        private static int getVehicleType()
        {
            bool isInputIncorrect = true;
            int choiceInt;

            do
            {
                Console.WriteLine(@"Please choose a vehicle type:
1. Fuel Car
2. Fuel Motorcycle
3. Electric Car
4. Electric Motorcycle
5. Fuel Truck");

                string inputStr = Console.ReadLine();
                if (int.TryParse(inputStr, out choiceInt))
                {
                    isValueInRange(choiceInt, k_MinOption, k_MaxVehicleType);
                    isInputIncorrect = false;
                }
                else
                {
                    Console.WriteLine("Wrong input, Try Again!");
                }
            }
            while (isInputIncorrect);

            return choiceInt;
        }

        private static string[] getClientDetails()
        {
            string[] clientDetails = new string[2];
            Console.WriteLine("Please enter client name: ");
            clientDetails[0] = Console.ReadLine();
            Console.WriteLine("Please enter client phone: ");
            clientDetails[1] = Console.ReadLine();

            return clientDetails;
        }

        private static string getModelName()
        {
            Console.WriteLine("Enter Model name of vehicle: ");
            string inputStr = Console.ReadLine();

            return inputStr;
        }

        private static float getAirPressure()
        {
            bool isInputIncorrect = true;
            float inputFloat;
            do
            {
                Console.WriteLine("Enter Air Pressure in wheels: ");
                string inputStr = Console.ReadLine();

                if (float.TryParse(inputStr, out inputFloat))
                {
                    isInputIncorrect = false;
                }
                else
                {
                    Console.WriteLine("Wrong input, try again! ");
                }
            }
            while (isInputIncorrect);

            return inputFloat;
        }

        private static float getFuelLiters()
        {
            bool isInputIncorrect = true;
            float inputFloat;
            do
            {
                Console.WriteLine("Enter current fuel liters: ");
                string inputStr = Console.ReadLine();

                if (float.TryParse(inputStr, out inputFloat))
                {
                    isInputIncorrect = false;
                }
                else
                {
                    Console.WriteLine("Wrong input, try again! ");
                }
            }
            while (isInputIncorrect);

            return inputFloat;
        }

        private static int getColor()
        {
            bool isInputIncorrect = true;
            int inputInt;
            do
            {
                Console.WriteLine("Enter Car Color: ");
                Console.WriteLine("1.White");
                Console.WriteLine("2.Grey");
                Console.WriteLine("3.Black");
                Console.WriteLine("4.Blue");
                string inputStr = Console.ReadLine();

                if (int.TryParse(inputStr, out inputInt))
                {
                    if (inputInt > 0 & inputInt <= 4)
                    {
                        isInputIncorrect = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong choice, try again! ");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input, try again! ");
                }
            }
            while (isInputIncorrect);

            return inputInt;
        }

        private static int getDoorsNumber()
        {
            bool isInputIncorrect = true;
            int inputInt;
            do
            {
                Console.WriteLine("Enter Doors number: ");
                Console.WriteLine("1. 2 doors");
                Console.WriteLine("2. 3 doors");
                Console.WriteLine("3. 4 doors");
                Console.WriteLine("4. 5 doors");
                string inputStr = Console.ReadLine();

                if (int.TryParse(inputStr, out inputInt))
                {
                    if (inputInt > 0 & inputInt <= 4)
                    {
                        isInputIncorrect = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong choice, try again! ");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input, try again! ");
                }
            }
            while (isInputIncorrect);

            return inputInt;
        }

        private static int getEngineVolume()
        {
            bool isInputIncorrect = true;
            int inputInt;
            do
            {
                Console.WriteLine("Enter Engine volume in cc: ");
                string inputStr = Console.ReadLine();

                if (int.TryParse(inputStr, out inputInt))
                {
                    if (inputInt > 0)
                    {
                        isInputIncorrect = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong choice, try again! ");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input, try again! ");
                }
            }
            while (isInputIncorrect);

            return inputInt;
        }

        private static int getLicenseType()
        {
            bool isInputIncorrect = true;
            int inputInt;
            do
            {
                Console.WriteLine("Enter License type: ");
                Console.WriteLine("1. A");
                Console.WriteLine("2. AA");
                Console.WriteLine("3. B1");
                Console.WriteLine("4. BB");
                string inputStr = Console.ReadLine();

                if (int.TryParse(inputStr, out inputInt))
                {
                    if (inputInt > 0 & inputInt <= 4)
                    {
                        isInputIncorrect = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong choice, try again! ");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input, try again! ");
                }
            }
            while (isInputIncorrect);

            return inputInt;
        }

        private static float getBatteryRemaining()
        {
            bool isInputIncorrect = true;
            float inputFloat;
            do
            {
                Console.WriteLine("Enter current battery remaining hours: ");
                string inputStr = Console.ReadLine();

                if (float.TryParse(inputStr, out inputFloat))
                {
                    if (inputFloat > 0)
                    {
                        isInputIncorrect = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, try again! ");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input, try again! ");
                }
            }
            while (isInputIncorrect);

            return inputFloat;
        }

        private static float getMaxLoadWeight()
        {
            bool isInputIncorrect = true;
            float inputFloat;
            do
            {
                Console.WriteLine("Enter Maximum load weight: ");
                string inputStr = Console.ReadLine();

                if (float.TryParse(inputStr, out inputFloat))
                {
                    if (inputFloat > 0)
                    {
                        isInputIncorrect = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, try again! ");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input, try again! ");
                }
            }
            while (isInputIncorrect);

            return inputFloat;
        }

        private static bool getTransportsRefrigerated()
        {
            bool isInputIncorrect = true;
            int inputInt;
            do
            {
                Console.WriteLine("Does Truck transports refrigerated? ");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                string inputStr = Console.ReadLine();

                if (int.TryParse(inputStr, out inputInt))
                {
                    if (inputInt > 0 & inputInt < 3)
                    {
                        isInputIncorrect = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong choice, try again! ");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input, try again! ");
                }
            }
            while (isInputIncorrect);

            bool transportsRefrigerated = inputInt == 1;

            return transportsRefrigerated;
        }
    }
}
