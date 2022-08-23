using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle, IChargeable
    {
        private const float k_MaxAirPressure = 31f;
        private const float k_MaxBatteryHours = 2.8f;
        private const int k_NumberOfWheels = 2;
        private readonly ElectricInfo r_ElectricInfo = new ElectricInfo();

        public ElectricMotorcycle(string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
            m_WheelsCollection = new Wheel[k_NumberOfWheels];

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                m_WheelsCollection[i] = new Wheel(k_MaxAirPressure);
            }

            r_ElectricInfo.m_MaxBatteryHours = k_MaxBatteryHours;
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
                return r_ElectricInfo.m_BatteryHoursRemaining / r_ElectricInfo.m_MaxBatteryHours;
            }
        }

        public override Wheel[] WheelsCollection
        {
            get
            {
                return m_WheelsCollection;
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineVolumeCC
        {
            get
            {
                return m_EngineVolumeCC;
            }

            set
            {
                m_EngineVolumeCC = value;
            }
        }

        public float BatteryHoursRemaining
        {
            get
            {
                return r_ElectricInfo.m_BatteryHoursRemaining;
            }

            set
            {
                r_ElectricInfo.m_BatteryHoursRemaining = value;
            }
        }

        public float MaxBatteryHours
        {
            get
            {
                return r_ElectricInfo.m_MaxBatteryHours;
            }
        }

        public void Charge(float i_HoursToCharge)
        {
            r_ElectricInfo.m_BatteryHoursRemaining += i_HoursToCharge;
        }
    }
}
