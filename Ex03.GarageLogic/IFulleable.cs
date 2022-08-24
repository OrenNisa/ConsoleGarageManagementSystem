﻿namespace Ex03.GarageLogic
{
    internal interface IFulleable
    {
        eFuelType FuelType { get; }

        float CurrentFuelLiters { get; set; }

        float MaxFuelLiters { get; }

        void Refuel(float i_LitersToAdd, eFuelType i_FuelType);
    }
}
