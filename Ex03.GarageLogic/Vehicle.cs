using System.Collections.Generic;
using System;
using static Ex03.GarageLogic.eEnergyType;
// $G$ DSN-001 (-10) Polymorphism insufficient – Vehicle should hold an Engine base class; FuelEngine and ElectricEngine inherit from it.
// $G$ DSN-001 (-10) There is no logical division for classES. Engine, FuelEngine, ElectricEngine, and GarageVehicle classes are missing completely.

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eStateOfVehicleInGarage
        {
            UnderRepair = 1,
            Repaired,
            PayedFor
        }

        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        protected float m_EnergyPercentageLeft;
        // $G$ DSN-999 (-5) The "energy capacity" and "current energy" fields should be members of base class EnergyProvider.
        protected float m_MaxEnergyAmount;
        private float m_CurEnergyAmount;
        // $G$ SFN-999 (-5) Missing `GarageVehicle` class - these 3 data members belong to this class. 
        private string m_NameOfOwner;
        private string m_PhoneNumber;
        private eStateOfVehicleInGarage m_StateOfVehicleInGarage;
        // $G$ DSN-999 (-4) The "fuel type" field should be readonly member of class FuelEnergyProvider.
        protected eEnergyType m_EnergyType;
        protected int m_NumOfWheels;
        protected float m_MaxAirPressure;
        protected readonly List<Wheel> r_Wheels = new List<Wheel>();

        public string ModelName
        {
            get 
            { 
                return r_ModelName; 
            }
        }

        public string LicenseNumber
        {
            get 
            { 
                return r_LicenseNumber; 
            }
        }

        public float EnergyPercentageLeft
        {
            get 
            { 
                return m_EnergyPercentageLeft; 
            }
            set 
            { 
                m_EnergyPercentageLeft = value; 
            }
        }

        public string OwnerName
        {
            get 
            { 
                return m_NameOfOwner; 
            }
            set 
            { 
                m_NameOfOwner = value; 
            }
        }

        public string PhoneNumber
        {
            get 
            { 
                return m_PhoneNumber; 
            }
            set 
            { 
                m_PhoneNumber = value; 
            }
        }

        public eStateOfVehicleInGarage StateInGarage
        {
            get 
            { 
                return m_StateOfVehicleInGarage; 
            }
            set 
            { 
                m_StateOfVehicleInGarage = value; 
            }
        }

        public eEnergyType EnergyType
        {
            get 
            { 
                return m_EnergyType; 
            }
            set 
            { 
                m_EnergyType = value; 
            }
        }

        public float MaxEnergyAmount
        {
            get 
            { 
                return m_MaxEnergyAmount; 
            }
        }

        public float CurrentEnergyAmount
        {
            get 
            { 
                return m_CurEnergyAmount; 
            }
            set 
            {
                if (value >= 0 && value <= m_MaxEnergyAmount)
                {
                    m_CurEnergyAmount = value;
                    CalculateEnergyPercentage();
                }
                else
                {
                    throw new ValueRangeException(0,MaxEnergyAmount);
                }
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public int NumberOfWheels
        {
            get 
            { 
                return m_NumOfWheels; 
            }
            set 
            {
                if (value > 0)
                {
                    m_NumOfWheels = value;
                }
                else
                {
                    throw new ValueRangeException(0, int.MaxValue);
                }
            }
        }

        public List<Wheel> Wheels
        {
            get 
            { 
                return r_Wheels; 
            }
        }

        public Vehicle(string i_LicenseNumber, string i_ModelName)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
        }

        public abstract void AddSpecificAttributs(Dictionary<eVehicleAttributes, string> i_VehicleAttributes, string i_Attribute1, string i_Attribute2);

        public void AddEnergy(float i_FuelToAdd, eEnergyType i_EnergyTypeToAdd = Electric)
        {
            if (i_EnergyTypeToAdd == m_EnergyType)
            {
                if (i_FuelToAdd + m_CurEnergyAmount <= m_MaxEnergyAmount)
                {
                    m_CurEnergyAmount += i_FuelToAdd;
                    CalculateEnergyPercentage();
                }
                else
                {
                    if (m_EnergyType == Electric)
                    {
                        throw new ArgumentException($"Cannot charge beyond max battery time of {m_MaxEnergyAmount} hours.");
                    }
                    else
                    {
                        throw new ArgumentException($"Cannot add fuel beyond max fuel amount of {m_MaxEnergyAmount} liters.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid fuel type for this vehicle.");
            }
        }

        public void AddWheels(string i_Manufacturer, float i_CurAirPressure)
        {
            try
            {
                for (int i = 0; i < m_NumOfWheels; i++)
                {
                    r_Wheels.Add(new Wheel(i_Manufacturer, i_CurAirPressure, m_MaxAirPressure));
                }
            }
            catch (ValueRangeException ex)
            {
                throw new ValueRangeException(0, m_MaxAirPressure);
            }
            
        }

        // $G$ CSS-011 (-5) Public methods should start with an Uppercase letter.
        public virtual void initVehicleParams(Dictionary<eVehicleAttributes, string> i_VehicleDictionary)
        {
            try
            {
                float parsedEnergy = float.Parse(i_VehicleDictionary[eVehicleAttributes.CurrentEnergyPercentege]);
                if (parsedEnergy >= 0 && parsedEnergy <= 100)
                {
                    m_EnergyPercentageLeft = parsedEnergy;
                    CalculateCurEnergy();
                    AddWheels(i_VehicleDictionary[eVehicleAttributes.WheelManufacturerName],
                    float.Parse(i_VehicleDictionary[eVehicleAttributes.CurrentAirPressure]));
                }
                else
                {
                    throw new ValueRangeException(0, m_MaxEnergyAmount);
                }
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid energy value format");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error setting vehicle properties: {ex.Message}");
            }
        }

        // $G$ DSN-012 (-10) Code duplication – Vehicle should hold Engine and decide type via polymorphism.
        protected void CalculateEnergyPercentage()
        {
            m_EnergyPercentageLeft = (m_CurEnergyAmount / m_MaxEnergyAmount) * 100;
        }

        protected void CalculateCurEnergy()
        {
            m_CurEnergyAmount = (m_MaxEnergyAmount * m_EnergyPercentageLeft) / 100;
        }

        public class Wheel
        {
            // $G$ DSN-999 (-4) The "maximum air pressure" field should be readonly member of class wheel. And why you have it twice? 
            private string m_Manufacturer;
            private float m_CurAirPressure;
            private float m_MaxAirPressure;


            public Wheel(string i_Manufacturer, float i_CurAirPressure, float i_MaxAirPressure)
            {
                if (i_CurAirPressure >= 0 && i_CurAirPressure <= i_MaxAirPressure)
                {
                    m_MaxAirPressure = i_MaxAirPressure;
                    m_Manufacturer = i_Manufacturer;
                    m_CurAirPressure = i_CurAirPressure;
                }
                else
                {
                    throw new ValueRangeException(0, i_MaxAirPressure);
                }
            }

            public float MaxAirPressure
            {
                get
                {
                    return m_MaxAirPressure;
                }
            }

            public string Manufacturer
            {
                get 
                { 
                    return m_Manufacturer; 
                }
                set 
                { 
                    m_Manufacturer = value; 
                }
            }
            
            public float CurrentAirPressure
            {
                get 
                { 
                    return m_CurAirPressure; 
                }
                set 
                {
                    if (value >= 0 && value <= m_MaxAirPressure)
                    {
                        m_CurAirPressure = value;
                    }
                    else
                    {
                        throw new ValueRangeException(0, m_MaxAirPressure);
                    }
                }
            }

            public void FillAir()
            {
                m_CurAirPressure = m_MaxAirPressure;
            }
        }
    }
}
