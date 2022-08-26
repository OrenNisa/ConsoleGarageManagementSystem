using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private const float k_60Minutes = 60f;
        private readonly Dictionary<string, Order> m_OrdersCollection = new Dictionary<string, Order>();

        public void AddNewOrder(
            string i_LicenseNumber,
            eVehicleType i_VehicleType,
            string i_OwnerName,
            string i_OwnerPhone,
            params object[] i_ListOfParameters)
        {
            if (i_VehicleType == eVehicleType.ElectricCar)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new ElectricCar(i_LicenseNumber), i_VehicleType));
                m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricCar).BatteryHoursRemaining = (float)i_ListOfParameters[2];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricCar).Color = (eColor)i_ListOfParameters[3];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricCar).DoorsNumber = (eDoors)i_ListOfParameters[4];
            }
            else if (i_VehicleType == eVehicleType.ElectricMotorcycle)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new ElectricMotorcycle(i_LicenseNumber), i_VehicleType));
                m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricMotorcycle).BatteryHoursRemaining = (float)i_ListOfParameters[2];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricMotorcycle).EngineVolumeCC = (int)i_ListOfParameters[3];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricMotorcycle).LicenseType = (eLicenseType)i_ListOfParameters[4];
            }
            else if (i_VehicleType == eVehicleType.FuelCar)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new FuelCar(i_LicenseNumber), i_VehicleType));
                m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelCar).CurrentFuelLiters = (float)i_ListOfParameters[2];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelCar).Color = (eColor)i_ListOfParameters[3];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelCar).DoorsNumber = (eDoors)i_ListOfParameters[4];
            }
            else if (i_VehicleType == eVehicleType.FuelMotorcycle)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new FuelMotorcycle(i_LicenseNumber), i_VehicleType));
                m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelMotorcycle).CurrentFuelLiters = (float)i_ListOfParameters[2];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelMotorcycle).EngineVolumeCC = (int)i_ListOfParameters[3];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelMotorcycle).LicenseType = (eLicenseType)i_ListOfParameters[4];
            }
            else if (i_VehicleType == eVehicleType.FuelTruck)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(i_OwnerName, i_OwnerPhone, new FuelTruck(i_LicenseNumber), i_VehicleType));
                m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelTruck).CurrentFuelLiters = (float)i_ListOfParameters[2];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelTruck).MaxLoadWeight = (float)i_ListOfParameters[3];
                (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelTruck).TransportsRefrigerated = (bool)i_ListOfParameters[4];
            }
            else
            {
                throw new FormatException("Vehicle Type format is wrong");
            }
        }

        public List<string> GetAllLicenseNumbers()
        {
            return new List<string>(m_OrdersCollection.Keys);
        }

        public List<string> GetAllLicenseNumbers(eVehicleState i_State)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (string order in m_OrdersCollection.Keys)
            {
                if (m_OrdersCollection[order].VehicleState == i_State)
                {
                    licenseNumbers.Add(order);
                }
            }

            return licenseNumbers;
        }

        public void EditOrder(
            string i_LicenseNumber,
            eVehicleType i_VehicleType,
            string i_OwnerName,
            string i_OwnerPhone,
            params object[] i_ListOfParameters)
        {
            if (m_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                if (m_OrdersCollection[i_LicenseNumber].VehicleType == i_VehicleType)
                {
                    m_OrdersCollection[i_LicenseNumber].OwnerName = i_OwnerName;
                    m_OrdersCollection[i_LicenseNumber].OwnerPhone = i_OwnerPhone;

                    if (i_VehicleType == eVehicleType.ElectricCar)
                    {
                        m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricCar).BatteryHoursRemaining = (float)i_ListOfParameters[2];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricCar).Color = (eColor)i_ListOfParameters[3];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricCar).DoorsNumber = (eDoors)i_ListOfParameters[4];
                    }
                    else if (i_VehicleType == eVehicleType.ElectricMotorcycle)
                    {
                        m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricMotorcycle).BatteryHoursRemaining = (float)i_ListOfParameters[2];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricMotorcycle).EngineVolumeCC = (int)i_ListOfParameters[3];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as ElectricMotorcycle).LicenseType = (eLicenseType)i_ListOfParameters[4];
                    }
                    else if (i_VehicleType == eVehicleType.FuelCar)
                    {
                        m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelCar).CurrentFuelLiters = (float)i_ListOfParameters[2];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelCar).Color = (eColor)i_ListOfParameters[3];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelCar).DoorsNumber = (eDoors)i_ListOfParameters[4];
                    }
                    else if (i_VehicleType == eVehicleType.FuelMotorcycle)
                    {
                        m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelMotorcycle).CurrentFuelLiters = (float)i_ListOfParameters[2];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelMotorcycle).EngineVolumeCC = (int)i_ListOfParameters[3];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelMotorcycle).LicenseType = (eLicenseType)i_ListOfParameters[4];
                    }
                    else if (i_VehicleType == eVehicleType.FuelTruck)
                    {
                        m_OrdersCollection[i_LicenseNumber].Vehicle.ModelName = (string)i_ListOfParameters[0];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[0].CurrentAirPressure = (float)i_ListOfParameters[1];
                        m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection[1].CurrentAirPressure = (float)i_ListOfParameters[1];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelTruck).CurrentFuelLiters = (float)i_ListOfParameters[2];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelTruck).MaxLoadWeight = (float)i_ListOfParameters[3];
                        (m_OrdersCollection[i_LicenseNumber].Vehicle as FuelTruck).TransportsRefrigerated = (bool)i_ListOfParameters[4];
                    }
                    else
                    {
                        throw new FormatException("Vehicle Type format is wrong");
                    }
                }
                else
                {
                    throw new ArgumentException("Vehicle Type is not compatible");
                }
            }
            else
            {
                throw new KeyNotFoundException("License number does not exists");
            }
        }

        public void ChangeVehicleState(string i_LicenseNumber, eVehicleState i_NewState)
        {
            if (m_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                m_OrdersCollection[i_LicenseNumber].VehicleState = i_NewState;
            }
            else
            {
                throw new KeyNotFoundException("License number does not exists");
            }
        }

        public void InflateWheels(string i_LicenseNumber)
        {
            if (m_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                foreach (Wheel wheel in m_OrdersCollection[i_LicenseNumber].Vehicle.WheelsCollection)
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
            if (m_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                if (m_OrdersCollection[i_LicenseNumber].Vehicle is IFulleable)
                {
                    (m_OrdersCollection[i_LicenseNumber].Vehicle as IFulleable).Refuel(i_LittersToFill, i_FuelType);
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
            if (m_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                if (m_OrdersCollection[i_LicenseNumber].Vehicle is IChargeable)
                {
                    (m_OrdersCollection[i_LicenseNumber].Vehicle as IChargeable).Charge(i_MinutesToCharge / k_60Minutes);
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
            return m_OrdersCollection.ContainsKey(i_LicenseNumber);
        }

        public string GetOrderDetails(string i_LicenseNumber)
        {
            string orderDetails;

            if (m_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                orderDetails = m_OrdersCollection[i_LicenseNumber].ToString();
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

            public Order(Vehicle i_Vehicle, eVehicleType i_VehicleType)
            {
                m_Vehicle = i_Vehicle;
                m_VehicleType = i_VehicleType;
                m_VehicleState = eVehicleState.InRepair;
            }

            public Order(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle, eVehicleType i_VehicleType)
            {
                m_OwnerName = i_OwnerName;
                m_OwnerPhone = i_OwnerPhone;
                m_Vehicle = i_Vehicle;
                m_VehicleType = i_VehicleType;
                m_VehicleState = eVehicleState.InRepair;
            }

            public string OwnerName
            {
                get
                {
                    return m_OwnerName;
                }

                set
                {
                    m_OwnerName = value;
                }
            }

            public string OwnerPhone
            {
                get
                {
                    return m_OwnerPhone;
                }

                set
                {
                    m_OwnerPhone = value;
                }
            }

            public eVehicleState VehicleState
            {
                get
                {
                    return m_VehicleState;
                }

                set
                {
                    m_VehicleState = value;
                }
            }

            public Vehicle Vehicle
            {
                get
                {
                    return m_Vehicle;
                }

                set
                {
                    m_Vehicle = value;
                }
            }

            public eVehicleType VehicleType
            {
                get
                {
                    return m_VehicleType;
                }

                set
                {
                    m_VehicleType = value;
                }
            }

            public override string ToString()
            {
                string details = null;

                if (this.VehicleType == eVehicleType.FuelCar)
                {
                    string[] args = new string[4];
                    args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                    args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                    args[2] = string.Format("Vehicle Type: Car");
                    args[3] = (Vehicle as FuelCar).ToString();
                    details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                }
                else if (this.VehicleType == eVehicleType.FuelMotorcycle)
                {
                    string[] args = new string[4];
                    args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                    args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                    args[2] = string.Format("Vehicle Type: Motorcycle");
                    args[3] = (Vehicle as FuelMotorcycle).ToString();
                    details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                }
                else if (this.VehicleType == eVehicleType.ElectricCar)
                {
                    string[] args = new string[4];
                    args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                    args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                    args[2] = string.Format("Vehicle Type: Electric Car");
                    args[3] = (Vehicle as ElectricCar).ToString();
                    details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                }
                else if (VehicleType == eVehicleType.ElectricMotorcycle)
                {
                    string[] args = new string[4];
                    args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                    args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                    args[2] = string.Format("Vehicle Type: Electric Motorcycle");
                    args[3] = (Vehicle as ElectricMotorcycle).ToString();
                    details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                }
                else if (VehicleType == eVehicleType.FuelTruck)
                {
                    string[] args = new string[4];
                    args[0] = string.Format("\nOwner Name: {0}", OwnerName);
                    args[1] = string.Format("State in garage: {0}", m_VehicleState.ToString());
                    args[2] = string.Format("Vehicle Type: Truck");
                    args[3] = (Vehicle as FuelTruck).ToString();
                    details = string.Format("{0}\n{1}\n{2}\n{3}", args);
                }

                return details;
            }
        }


    }
}
