using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, Order> m_OrdersCollection = new Dictionary<string, Order>();

        public void Test()
        {
            FuelTruck ft1 = new FuelTruck("1234563");
            ElectricCar ec1 = new ElectricCar("1234569");
            FuelCar fc1 = new FuelCar("1234561");
            FuelMotorcycle fm1 = new FuelMotorcycle("1234568");
            ElectricMotorcycle em1 = new ElectricMotorcycle("1234567");

            Vehicle v1 = ft1;
            (v1 as IChargeable).Charge(2);

            ft1.TransportsRefrigerated = true;
            ec1.Charge(2);
        }

        public void AddNewOrder(string i_LicenseNumber, eVehicleType i_VehicleType)
        {
            if (i_VehicleType == eVehicleType.FuelCar)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(new FuelCar(i_LicenseNumber), eVehicleType.FuelCar));
            }
            else if (i_VehicleType == eVehicleType.FuelMotorcycle)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(new FuelMotorcycle(i_LicenseNumber), eVehicleType.FuelMotorcycle));
            }
            else if (i_VehicleType == eVehicleType.ElectricCar)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(new ElectricCar(i_LicenseNumber), eVehicleType.ElectricCar));
            }
            else if (i_VehicleType == eVehicleType.ElectricMotorcycle)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(new ElectricMotorcycle(i_LicenseNumber), eVehicleType.ElectricMotorcycle));
            }
            else if (i_VehicleType == eVehicleType.FuelTruck)
            {
                m_OrdersCollection.Add(i_LicenseNumber, new Order(new FuelTruck(i_LicenseNumber), eVehicleType.FuelTruck));
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

        public void ChangeVehicleState(string i_LicenseNumber, eVehicleState i_NewState)
        {
            if (m_OrdersCollection.ContainsKey(i_LicenseNumber))
            {
                m_OrdersCollection[i_LicenseNumber].VehicleState = i_NewState;
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
        }

        public void Refuel(string i_LicenseNumber, eFuelType i_FuelType, float i_LittersToFill)
        {
            if (m_OrdersCollection[i_LicenseNumber].Vehicle is IFulleable)
            {
                (m_OrdersCollection[i_LicenseNumber].Vehicle as IFulleable).Refuel(i_LittersToFill, i_FuelType);
            }
        }

        public void Charge(string i_LicenseNumber, float i_MinutesToCharge)
        {
            if (m_OrdersCollection[i_LicenseNumber].Vehicle is IChargeable)
            {
                (m_OrdersCollection[i_LicenseNumber].Vehicle as IChargeable).Charge(i_MinutesToCharge / 60);
            }
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
                orderDetails = null;
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
            }

            public Order(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
            {
                m_OwnerName = i_OwnerName;
                m_OwnerPhone = i_OwnerPhone;
                m_Vehicle = i_Vehicle;
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
                    details = string.Format("License Number: {0}\n");
                }
                else if (this.VehicleType == eVehicleType.FuelMotorcycle)
                {
                    // m_OrdersCollection.Add(i_LicenseNumber, new Order(new FuelMotorcycle(i_LicenseNumber)));
                }
                else if (this.VehicleType == eVehicleType.ElectricCar)
                {
                    // m_OrdersCollection.Add(i_LicenseNumber, new Order(new ElectricCar(i_LicenseNumber)));
                }
                else if (this.VehicleType == eVehicleType.ElectricMotorcycle)
                {
                    // m_OrdersCollection.Add(i_LicenseNumber, new Order(new ElectricMotorcycle(i_LicenseNumber)));
                }
                else if (this.VehicleType == eVehicleType.FuelTruck)
                {
                    // m_OrdersCollection.Add(i_LicenseNumber, new Order(new FuelTruck(i_LicenseNumber)));
                }

                return details;
            }
        }

        public enum eVehicleState
        {
            InRepair = 1,
            Repaired,
            Paid,
        }

        public enum eVehicleType
        {
            FuelCar = 1,
            FuelMotorcycle,
            ElectricCar,
            ElectricMotorcycle,
            FuelTruck,
        }
    }
}
