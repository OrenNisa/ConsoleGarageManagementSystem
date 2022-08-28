namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        public Wheel()
        {
            m_ManufacturerName = "Michelin";
        }

        public Wheel(float i_MaxAirPressure)
        {
            m_ManufacturerName = "Michelin";
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get => m_ManufacturerName;

            set => m_ManufacturerName = value;
        }

        public float CurrentAirPressure
        {
            get => m_CurrentAirPressure;

            set => m_CurrentAirPressure = value;
        }

        public float MaxAirPressure => r_MaxAirPressure;

        public void InflateWheel(float i_AirPressure)
        {
            if (this.CurrentAirPressure + i_AirPressure <= this.MaxAirPressure & i_AirPressure >= 0)
            {
                this.CurrentAirPressure += i_AirPressure;
            }
            else
            {
                throw new ValueOutOfRangeException("Too Much Air Pressure!", this.MaxAirPressure, 0);
            }
        }

        public override string ToString()
        {
            return string.Format("Manufacturer: {0}\nCurrent Air: {1}", this.ManufacturerName, this.CurrentAirPressure);
        }
    }
}
