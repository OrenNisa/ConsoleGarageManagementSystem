namespace Ex03.GarageLogic
{
    internal class FuelTruck : Truck, IFulleable
    {
        private const float k_MaxAirPressure = 25f;
        private const float k_MaxFuelLiters = 135f;
        private const int k_NumberOfWheels = 16;
        private readonly FuelInfo r_FuelInfo = new FuelInfo();

        public FuelTruck(string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
            m_WheelsCollection = new Wheel[k_NumberOfWheels];

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                m_WheelsCollection[i] = new Wheel(k_MaxAirPressure);
            }

            r_FuelInfo.m_FuelType = eFuelType.Soler;
            r_FuelInfo.m_MaxFuelLiters = k_MaxFuelLiters;
        }

        public override string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public override string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public override float PercentageEnergyRemaining
        {
            get
            {
                return r_FuelInfo.m_CurrentFuelLiters / r_FuelInfo.m_MaxFuelLiters;
            }
        }

        public bool TransportsRefrigerated
        {
            get
            {
                return m_TransportRefrigerated;
            }

            set
            {
                m_TransportRefrigerated = value;
            }
        }

        public float MaxLoadWeight
        {
            get
            {
                return m_MaxLoadWeight;
            }

            set
            {
                m_MaxLoadWeight = value;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelInfo.m_FuelType;
            }
        }

        public float CurrentFuelLiters
        {
            get
            {
                return r_FuelInfo.m_CurrentFuelLiters;
            }

            set
            {
                r_FuelInfo.m_CurrentFuelLiters = value;
            }
        }

        public float MaxFuelLiters
        {
            get
            {
                return r_FuelInfo.m_MaxFuelLiters;
            }
        }

        public override Wheel[] WheelsCollection
        {
            get
            {
                return m_WheelsCollection;
            }
        }

        public void Refuel(float i_LitersToAdd, eFuelType i_FuelType)
        {
            // do
        }

        public override string ToString()
        {
            return string.Format("License Number: {0}\nModel Name: {1}\nCurrent Fuel: {2} Liters\nFuel Type: {3}", this.LicenseNumber, this.ModelName, this.CurrentFuelLiters, this.FuelType);
        }
    }
}
