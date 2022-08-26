using System;

namespace Ex03.GarageLogic
{
    internal class FuelTruck : Truck, IFulleable
    {
        private const float k_MaxAirPressure = 25f;
        private const float k_MaxFuelLiters = 135f;
        private const int k_NumberOfWheels = 16;
        private const float k_100Percent = 100f;
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
            if (this.FuelType == i_FuelType)
            {
                if (this.CurrentFuelLiters + i_LitersToAdd <= this.MaxFuelLiters & i_LitersToAdd >= 0)
                {
                    this.CurrentFuelLiters += i_LitersToAdd;
                }
                else
                {
                    if (i_LitersToAdd < 0)
                    {
                        throw new ValueOutOfRangeException("Cannot Add Negative Amount!", this.MaxFuelLiters, 0);
                    }
                    else
                    {
                        throw new ValueOutOfRangeException("Too much Fuel!", this.MaxFuelLiters, 0);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Fuel Type Not Compatible");
            }
        }

        public override string ToString()
        {
            string[] args = new string[8];
            args[0] = string.Format("License Number: {0}", LicenseNumber);
            args[1] = string.Format("Model Name: {0}", ModelName);
            args[2] = string.Format("Transport Refrigerated: {0}", TransportsRefrigerated);
            args[3] = string.Format("Maximum Load Weight: {0}", MaxLoadWeight);
            args[4] = string.Format("Fuel Percentage: {0:0.0}%", PercentageEnergyRemaining * k_100Percent);
            args[5] = string.Format("Fuel Type: {0}", FuelType.ToString());
            args[6] = string.Format("Number of wheels: {0}", k_NumberOfWheels.ToString());
            args[7] = string.Format("Wheels state \n{0}", WheelsCollection[0]);
            return string.Format(
                "{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}", args);
        }
    }
}
