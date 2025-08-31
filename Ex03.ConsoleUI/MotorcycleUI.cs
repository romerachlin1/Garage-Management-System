using Ex03.GarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    internal class MotorcycleUI : VehicleUI
    {
        public override void GetAndSetAttributes(Dictionary<eVehicleAttributes, string> i_VehicleAttributes)
        {
            string morotcycLicenseType = VehicleInputManager.GetMotorcycleLicenseType();
            int morotcycleEngineVolume = VehicleInputManager.GetMotorcycleEngineVolume();
            i_VehicleAttributes.Add(eVehicleAttributes.MorotcycLicenseType, morotcycLicenseType);
            i_VehicleAttributes.Add(eVehicleAttributes.MorotcycleEngineVolume, morotcycleEngineVolume.ToString());
        }

        public override void PrintSpecificData(Vehicle i_Vehicle)
        {
            if (i_Vehicle is Motorcycle motorcycle)
            {
                Console.WriteLine($"License Type: {motorcycle.LicenseTypeMotorcycle}");
                Console.WriteLine($"Engine Volume: {motorcycle.EngineVolume}");
            }
            else
            {
                throw new FormatException("Error: Vehicle is not a motorcycle");
            }
        }
    }
}
