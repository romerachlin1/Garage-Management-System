using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
// $G$ DSN-002 (-5) The UI must not know specific types and their properties concretely! It means that when adding a new type you'll have to change the code here too!

namespace Ex03.ConsoleUI
{
    public class TruckUI : VehicleUI
    {
        public override void GetAndSetAttributes(Dictionary<eVehicleAttributes, string> i_VehicleAttributes)
        {
            string truckHasDangerousMateriels = VehicleInputManager.GetTruckDangerousMaterials();
            float truckCargoSize = VehicleInputManager.GetTruckCargoSize();
            i_VehicleAttributes.Add(eVehicleAttributes.TruckDangerousMaterials, truckHasDangerousMateriels);
            i_VehicleAttributes.Add(eVehicleAttributes.TruckCargoSize, truckCargoSize.ToString());
        }


        // $G$ DSN-999 (-5) It's better to override toString which any object have it already in the logic class of each vehicle.
        public override void PrintSpecificData(Vehicle i_Vehicle)
        {
            if (i_Vehicle is Truck truck)
            {
                Console.WriteLine($"Carries Dangerous Materials: {(truck.HasDangerousItems)}");
                Console.WriteLine($"Cargo Volume: {truck.CargoVolume}");
            }
            else
            {
                throw new FormatException("Error: Vehicle is not a truck");
            }
        }
    }
}
