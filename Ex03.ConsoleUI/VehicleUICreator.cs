using System.Collections.Generic;
// $G$ DSN-002 (-10) There is no separation between the project of the garage logic and the UI project. All these UI class are unnecessary: VehicleUICreator, TruckUI, MotorcycleUI, CarUI, VehicleUI. You needed to create one method "setSpecificVehicleProperties" and it should work for all vehicles types- polymorphism.
// $G$ DSN-011 (-7) Adding a new type will require changes here, violating the guideline to modify only the creator class(logic).

namespace Ex03.ConsoleUI
{
    public abstract class VehicleUICreator
    {
        public static VehicleUI CreateVehicleUI(string i_VehicleType)
        {
            VehicleUI newVehicle = null;

            switch (i_VehicleType)
            {
                case "FuelCar":
                    newVehicle = new CarUI();
                    break;
                case "ElectricCar":
                    newVehicle = new CarUI();
                    break;
                case "FuelMotorcycle":
                    newVehicle = new MotorcycleUI();
                    break;
                case "ElectricMotorcycle":
                    newVehicle = new MotorcycleUI();
                    break;
                case "Truck":
                    newVehicle = new TruckUI();
                    break;
            }

            return newVehicle;
        }

        public static List<string> SupportedTypes
        {
            get { return new List<string> { "FuelCar", "ElectricCar", "FuelMotorcycle", "ElectricMotorcycle", "Truck" }; }
        }
    }
}
