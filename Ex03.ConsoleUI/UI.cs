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
        private const int k_MinOptionInMenu = 1;
        private const int k_MaxOptionInMenu = 8;
        private static readonly GarageManager r_Garage = new GarageManager();

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
            Console.WriteLine("Inside getUsersChoice");
            string choice = null;
            try
            {
                choice = Console.ReadLine();
                int choiceAsInt = int.Parse(choice);
                isValueInRange(choiceAsInt, k_MinOptionInMenu, k_MaxOptionInMenu);
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
                throw new ValueOutOfRangeException(i_MaxValue, i_MinValue);
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
                r_Garage.ChangeVehicleState(licenseNumInput, GarageManager.eVehicleState.InRepair);
            }
        }

        private static void getIDsInGarage()
        {

        }

        private static void changeVehicleStatus()
        {

        }

        private static void inflateToMax()
        {

        }

        private static void refuel()
        {

        }

        private static void recharge()
        {

        }

        private static void getAllVehiclesDetails()
        {

        }

    }
}
