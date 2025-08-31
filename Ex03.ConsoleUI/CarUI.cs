using Ex03.GarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public class CarUI : VehicleUI
    {
        public override void GetAndSetAttributes(Dictionary<eVehicleAttributes, string> i_VehicleAttributes)
        {
            string carColor = VehicleInputManager.GetCarColor();
            int numCarDoors = VehicleInputManager.GetNumCarDoors();
            i_VehicleAttributes.Add(eVehicleAttributes.CarColor, carColor);
            i_VehicleAttributes.Add(eVehicleAttributes.NumCarDoors, numCarDoors.ToString());
        }


        public override void PrintSpecificData(Vehicle i_Vehicle)
        {
            if(i_Vehicle is Car car)
            {
                Console.WriteLine($"Color: {car.CarColor}");
                Console.WriteLine($"Number of Doors: {car.NumberOfDoors}");
            }
            else 
            {
                throw new FormatException("Error: Vehicle is not a car");
            }
        }
    }
}
