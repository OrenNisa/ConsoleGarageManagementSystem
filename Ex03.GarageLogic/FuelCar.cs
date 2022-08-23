using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car, IFulleable
    {
        private const float k_MaxAirPressure = 27f;
        private const float k_MaxFuelLiters = 52f;
        private const int k_NumberOfWheels = 4;
        private readonly FuelInfo r_FuelInfo = new FuelInfo();

        public FuelCar(string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
            m_WheelsCollection = new Wheel[k_NumberOfWheels];

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                m_WheelsCollection[i] = new Wheel(k_MaxAirPressure);
            }

            r_FuelInfo.m_FuelType = eFuelType.Octan95;
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

        public override Wheel[] WheelsCollection
        {
            get
            {
                return m_WheelsCollection;
            }
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }

            set
            {
                m_Color = value;
            }
        }

        public eDoors DoorsNumber
        {
            get
            {
                return m_DoorsNumber;
            }

            set
            {
                m_DoorsNumber = value;
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

        public void Refuel(float i_LitersToAdd, eFuelType i_FuelType)
        {
            // do
        }
    }
}
