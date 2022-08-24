namespace Ex03.GarageLogic
{
    internal interface IChargeable
    {
        float BatteryHoursRemaining { get; set; }

        float MaxBatteryHours { get; }

        void Charge(float i_HoursToCharge);
    }
}
