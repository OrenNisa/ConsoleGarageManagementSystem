using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected Wheel[] m_WheelsCollection;

        public abstract string ModelName { get; set; }

        public abstract string LicenseNumber { get; set; }

        public abstract float PercentageEnergyRemaining { get; }

        public abstract Wheel[] WheelsCollection { get; }
    }
}
