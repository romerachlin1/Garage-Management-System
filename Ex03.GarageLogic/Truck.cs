using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_HasDangerousItems;
        private float m_CargoVolume;

        public bool HasDangerousItems
        {
            get
            {
                return m_HasDangerousItems;
            }
            set
            {
                m_HasDangerousItems = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                if (value >= 0)
                {
                    m_CargoVolume = value;
                }
                else
                {
                    throw new ArgumentException("Cargo volume cannot be negative");
                }
            }
        }

        public Truck(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            m_MaxEnergyAmount = 135;
            m_EnergyType = eEnergyType.Soler;
            m_NumOfWheels = 12;
            m_MaxAirPressure = 27;
        }

        public override void AddSpecificAttributs(Dictionary<eVehicleAttributes, string> i_VehicleAttributes, string i_TruckDangerousMaterials, string i_TruckCargoSize)
        {
            i_VehicleAttributes.Add(eVehicleAttributes.TruckDangerousMaterials, i_TruckDangerousMaterials);
            i_VehicleAttributes.Add(eVehicleAttributes.TruckCargoSize, i_TruckCargoSize);
        }

        public override void initVehicleParams(Dictionary<eVehicleAttributes, string> i_VehicleDictionary)
        {
            base.initVehicleParams(i_VehicleDictionary);
            try
            {
                m_CargoVolume = float.Parse(i_VehicleDictionary[eVehicleAttributes.TruckCargoSize]);
                m_HasDangerousItems = bool.Parse(i_VehicleDictionary[eVehicleAttributes.TruckDangerousMaterials]);
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error setting vehicle properties: {ex.Message}");
            }
        }
    }
}
