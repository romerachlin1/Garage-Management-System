using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        public enum eColor
        {
            Yellow,
            Black,
            White,
            Silver
        }

        private eColor m_Color;
        private int m_NumOfDoors;

        public override void AddSpecificAttributs(Dictionary<eVehicleAttributes, string> i_VehicleAttributes, string i_CarColor, string i_NumCarDoors)
        {
            i_VehicleAttributes.Add(eVehicleAttributes.CarColor, i_CarColor);
            i_VehicleAttributes.Add(eVehicleAttributes.NumCarDoors, i_NumCarDoors);
        }
        
        public override void initVehicleParams(Dictionary<eVehicleAttributes, string> i_VehicleDictionary)
        {
            base.initVehicleParams(i_VehicleDictionary);
            try
            {
                m_Color = (eColor)Enum.Parse(typeof(eColor), i_VehicleDictionary[eVehicleAttributes.CarColor]);
                m_NumOfDoors = int.Parse(i_VehicleDictionary[eVehicleAttributes.NumCarDoors]);
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error setting vehicle properties: {ex.Message}");
            }
        }

        public eColor CarColor
        {
            get 
            { 
                return m_Color; 
            }
            set 
            { 
                m_Color = value; 
            }
        }

        public int NumberOfDoors
        {
            get 
            { 
                return m_NumOfDoors; 
            }
            set 
            {
                if (value >= 2 && value <= 5)
                {
                    m_NumOfDoors = value;
                }
                else
                {
                    throw new ValueRangeException(2,5);
                }
            }
        }

        public Car(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
        }
    }
}
