using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A2,
            AB,
            B2
        }

        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public override void AddSpecificAttributs(Dictionary<eVehicleAttributes, string> i_VehicleAttributes, string i_MorotcycLicenseType, string i_MorotcycleEngineVolume)
        {
            i_VehicleAttributes.Add(eVehicleAttributes.MorotcycLicenseType, i_MorotcycLicenseType);
            i_VehicleAttributes.Add(eVehicleAttributes.MorotcycleEngineVolume, i_MorotcycleEngineVolume);
        }
        public override void initVehicleParams(Dictionary<eVehicleAttributes, string> i_VehicleDictionary)
        {
            base.initVehicleParams(i_VehicleDictionary);
            try
            {
                m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_VehicleDictionary[eVehicleAttributes.MorotcycLicenseType]);
                m_EngineVolume = int.Parse(i_VehicleDictionary[eVehicleAttributes.MorotcycleEngineVolume]);
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error setting vehicle properties: {ex.Message}");
            }
        }

        public eLicenseType LicenseTypeMotorcycle
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                if (Enum.IsDefined(typeof(eLicenseType), value))
                {
                    m_LicenseType = value;
                }
                else
                {
                    throw new ArgumentException("Invalid license type");
                }
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                if (value > 0)
                {
                    m_EngineVolume = value;
                }
                else
                {
                    throw new ArgumentException("Engine volume must be greater than 0");
                }
            }
        }

        public Motorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
        }
    }
}
