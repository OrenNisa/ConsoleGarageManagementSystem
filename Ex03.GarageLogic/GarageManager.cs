using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private const float k_60Minutes = 60f;
        private readonly Dictionary<string, Order> r_OrdersCollection = new Dictionary<string, Order>();

        public void AddNewOrder(
            string i_LicenseNumber,
            eVehicleType i_VehicleType,
            string i_OwnerName,
            string i_OwnerPhone,
            params object[] i_ListOfParameters)
        {
            switch(i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                    r_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new ElectricCar(i_LicenseNumber), i_VehicleType));
                    r_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as ElectricCar).BatteryHoursRemaining = (float)i_ListOfParameters[2];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as ElectricCar).Color = (eColor)i_ListOfParameters[3];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as ElectricCar).DoorsNumber = (eDoors)i_ListOfParameters[4];
                    break;
                case eVehicleType.ElectricMotorcycle:
                    r_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new ElectricMotorcycle(i_LicenseNumber), i_VehicleType));
                    r_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as ElectricMotorcycle).BatteryHoursRemaining = (float)i_ListOfParameters[2];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as ElectricMotorcycle).EngineVolumeCC = (int)i_ListOfParameters[3];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as ElectricMotorcycle).LicenseType = (eLicenseType)i_ListOfParameters[4];
                    break;
                case eVehicleType.FuelCar:
                    r_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new FuelCar(i_LicenseNumber), i_VehicleType));
                    r_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as FuelCar).CurrentFuelLiters = (float)i_ListOfParameters[2];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as FuelCar).Color = (eColor)i_ListOfParameters[3];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as FuelCar).DoorsNumber = (eDoors)i_ListOfParameters[4];
                    break;
                case eVehicleType.FuelMotorcycle:
                    r_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new FuelMotorcycle(i_LicenseNumber), i_VehicleType));
                    r_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as FuelMotorcycle).CurrentFuelLiters = (float)i_ListOfParameters[2];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as FuelMotorcycle).EngineVolumeCC = (int)i_ListOfParameters[3];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as FuelMotorcycle).LicenseType = (eLicenseType)i_ListOfParameters[4];
                    break;
                case eVehicleType.FuelTruck:
                    r_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new FuelTruck(i_LicenseNumber), i_VehicleType));
                    r_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                    r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as FuelTruck).CurrentFuelLiters = (float)i_ListOfParameters[2];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as FuelTruck).MaxLoadWeight = (float)i_ListOfParameters[3];
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as FuelTruck).TransportsRefrigerated = (bool)i_ListOfParameters[4];
                    break;
                default:
                    throw new FormatException("Vehicle Type format is wrong");
            }
        }

        public List<string> GetAllLicenseNumbers()
        {
            return new List<string>(r_OrdersCollection.Keys);
        }

        public List<string> GetAllLicenseNumbers(eVehicleState i_State)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (string order in r_OrdersCollection.Keys)
            {
                if (r_OrdersCollection[order].VehicleState == i_State)
                {
                    licenseNumbers.Add(order);
                }
            }

            return licenseNumbers;
        }

        public void ChangeVehicleState(string i_LicenseNumber, eVehicleState i_NewState)
        {
            if (r_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                r_OrdersCollection[i_LicenseNumber].VehicleState = i_NewState;
            }
            else
            {
                throw new KeyNotFoundException("License number does not exists");
            }
        }

        public void InflateWheels(string i_LicenseNumber)
        {
            if (r_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                foreach (Wheel wheel in r_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection)
                {
                    wheel.InflateWheel(wheel.MaxAirPressure - wheel.CurrentAirPressure);
                }
            }
            else
            {
                throw new KeyNotFoundException("License number does not exists");
            }
        }

        public void Refuel(string i_LicenseNumber, eFuelType i_FuelType, float i_LittersToFill)
        {
            if (r_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                if (r_OrdersCollection[i_LicenseNumber].Vehicle is IFuelable)
                {
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as IFuelable).Refuel(i_LittersToFill, i_FuelType);
                }
                else
                {
                    throw new ArgumentException("This vehicle is not based on fuel");
                }
            }
            else
            {
                throw new KeyNotFoundException("License number does not exists");
            }
        }

        public void Charge(string i_LicenseNumber, float i_MinutesToCharge)
        {
            if (r_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                if (r_OrdersCollection[i_LicenseNumber].Vehicle is IChargeable)
                {
                    (r_OrdersCollection[i_LicenseNumber].Vehicle as IChargeable).Charge(i_MinutesToCharge / k_60Minutes);
                }
                else
                {
                    throw new ArgumentException("This vehicle is not based on battery");
                }
            }
            else
            {
                throw new KeyNotFoundException("License number does not exists");
            }
        }

        public bool Exists(string i_LicenseNumber)
        {
            return r_OrdersCollection.ContainsKey(i_LicenseNumber);
        }

        public string GetOrderDetails(string i_LicenseNumber)
        {
            string orderDetails;

            if (r_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                orderDetails = r_OrdersCollection[i_LicenseNumber].ToString();
            }
            else
            {
                throw new KeyNotFoundException("License number does not exists");
            }

            return orderDetails;
        }

        internal class Order
        {
            private string m_OwnerName;
            private string m_OwnerPhone;
            private eVehicleState m_VehicleState;
            private Vehicle m_Vehicle;
            private eVehicleType m_VehicleType;

            public Order(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle, eVehicleType i_VehicleType)
            {
                OwnerName = i_OwnerName;
                OwnerPhone = i_OwnerPhone;
                Vehicle = i_Vehicle;
                VehicleType = i_VehicleType;
                VehicleState = eVehicleState.InRepair;
            }

            public string OwnerName
            {
                get => m_OwnerName;

                set => m_OwnerName = value;
            }

            public string OwnerPhone
            {
                get => m_OwnerPhone;

                set => m_OwnerPhone = value;
            }

            public eVehicleState VehicleState
            {
                get => m_VehicleState;

                set => m_VehicleState = value;
            }

            public Vehicle Vehicle
            {
                get => m_Vehicle;

                set => m_Vehicle = value;
            }

            public eVehicleType VehicleType
            {
                get => m_VehicleType;

                set => m_VehicleType = value;
            }

            public override string ToString()
            {
                string details = null;

                switch(this.VehicleType)
                {
                    case eVehicleType.FuelCar:
                        {
                            string[] args = new string[4];
                            args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                            args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                            args[2] = string.Format("Vehicle Type: Car");
                            args[3] = (Vehicle as FuelCar).ToString();
                            details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                            break;
                        }
                    case eVehicleType.FuelMotorcycle:
                        {
                            string[] args = new string[4];
                            args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                            args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                            args[2] = string.Format("Vehicle Type: Motorcycle");
                            args[3] = (Vehicle as FuelMotorcycle).ToString();
                            details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                            break;
                        }
                    case eVehicleType.ElectricCar:
                        {
                            string[] args = new string[4];
                            args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                            args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                            args[2] = string.Format("Vehicle Type: Electric Car");
                            args[3] = (Vehicle as ElectricCar).ToString();
                            details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                            break;
                        }
                    case eVehicleType.ElectricMotorcycle:
                        {
                            string[] args = new string[4];
                            args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                            args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                            args[2] = string.Format("Vehicle Type: Electric Motorcycle");
                            args[3] = (Vehicle as ElectricMotorcycle).ToString();
                            details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                            break;
                        }
                    case eVehicleType.FuelTruck:
                        {
                            string[] args = new string[4];
                            args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                            args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                            args[2] = string.Format("Vehicle Type: Truck");
                            args[3] = (Vehicle as FuelTruck).ToString();
                            details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                            break;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return details;
            }
        }
    }
}
