using Ex03.GarageLogic;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public abstract class VehicleUI
    {
        public abstract void GetAndSetAttributes(Dictionary<eVehicleAttributes, string> i_VehicleAttributes);

        public abstract void PrintSpecificData(Vehicle i_Vehicle);
    }
}
