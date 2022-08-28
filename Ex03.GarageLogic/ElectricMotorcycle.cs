using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle, IChargeable
    {
        private const float k_MaxAirPressure = 31f;
        private const float k_MaxBatteryHours = 2.8f;
        private const int k_NumberOfWheels = 2;
        private const float k_100Percent = 100f;
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
            get => m_LicenseNumber;

            set => m_LicenseNumber = value;
        }

        public override string ModelName
        {
            get => m_ModelName;

            set => m_ModelName = value;
        }

        public override float PercentageEnergyRemaining => r_ElectricInfo.m_BatteryHoursRemaining / r_ElectricInfo.m_MaxBatteryHours;

        public override Wheel[] WheelsCollection => m_WheelsCollection;

        public eLicenseType LicenseType
        {
            get => m_LicenseType;

            set => m_LicenseType = value;
        }

        public int EngineVolumeCC
        {
            get => m_EngineVolumeCC;

            set => m_EngineVolumeCC = value;
        }

        public float BatteryHoursRemaining
        {
            get => r_ElectricInfo.m_BatteryHoursRemaining;

            set => r_ElectricInfo.m_BatteryHoursRemaining = value;
        }

        public float MaxBatteryHours => r_ElectricInfo.m_MaxBatteryHours;

        public void Charge(float i_HoursToCharge)
        {
            if (this.BatteryHoursRemaining + i_HoursToCharge <= this.MaxBatteryHours | i_HoursToCharge >= 0)
            {
                this.BatteryHoursRemaining += i_HoursToCharge;
            }
            else
            {
                if (i_HoursToCharge < 0)
                {
                    throw new ValueOutOfRangeException("Cannot Add Negative Amount!", this.MaxBatteryHours, 0);
                }
                else
                {
                    throw new ValueOutOfRangeException("Too much Charge!", this.MaxBatteryHours, 0);
                }
            }
        }

        public override string ToString()
        {
            string[] args = new string[8];
            args[0] = string.Format("License Number: {0}", LicenseNumber);
            args[1] = string.Format("Model Name: {0}", ModelName);
            args[2] = string.Format("License Type: {0}", LicenseType.ToString());
            args[3] = string.Format("Volume of Engine: {0} cc", EngineVolumeCC);
            args[4] = string.Format("Charge Percentage: {0:0.0}%", PercentageEnergyRemaining * k_100Percent);
            args[5] = string.Format("Remaining Battery: {0:0.00} Hours", BatteryHoursRemaining);
            args[6] = string.Format("Number of wheels: {0}", k_NumberOfWheels.ToString());
            args[7] = string.Format("Wheels state \n{0}", WheelsCollection[0]);
            return string.Format(
                "{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}", args);
        }
    }
}
