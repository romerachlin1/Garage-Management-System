using System;
using System.Collections.Generic;
namespace Ex03.GarageLogic
{
    public class Garage
    {
        private List<Vehicle> m_GarageVehicles;

        public List<Vehicle> GarageVehicles
        {
            get
            {
                return m_GarageVehicles;
            }
            set
            {
                m_GarageVehicles = value;
            }
        }

        public Garage()
        {
            m_GarageVehicles = new List<Vehicle>();
        }

        public void InsertVehicleToGarage(Dictionary<eVehicleAttributes, string> i_Dictionary)
        {
            Vehicle vehicle = VehicleCreator.CreateVehicle(
                i_Dictionary[eVehicleAttributes.VehicleType],
                i_Dictionary[eVehicleAttributes.LicenseNumber],
                i_Dictionary[eVehicleAttributes.ModelName]);

            try
            {
                vehicle.OwnerName = i_Dictionary[eVehicleAttributes.OwnerName];
                vehicle.PhoneNumber = i_Dictionary[eVehicleAttributes.PhoneNumber];
                vehicle.StateInGarage = Vehicle.eStateOfVehicleInGarage.UnderRepair;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error setting vehicle properties: {ex.Message}");
            }

            vehicle.initVehicleParams(i_Dictionary);
            m_GarageVehicles.Add(vehicle);
        }

        public bool IsInGarage(string i_LicenseNumber)
        {
            bool inGarage = false;

            foreach(Vehicle vehicle in m_GarageVehicles)
            {
                if (i_LicenseNumber == vehicle.LicenseNumber)
                {
                    inGarage = true;
                    break;
                }
            }

            return inGarage;
        }

        public Vehicle GetVehicleInGarage(string i_LicenseNumber)
        {
            Vehicle requestedVehicle = null;
            
            foreach (Vehicle vehicle in m_GarageVehicles)
            {
                if (i_LicenseNumber == vehicle.LicenseNumber)
                {
                    requestedVehicle = vehicle;
                    break;
                }
            }
            
            if (requestedVehicle == null)
            {
                throw new ArgumentException($"Vehicle with license number {i_LicenseNumber} was not found in the garage.");
            }
            
            return requestedVehicle;
        }

        public List<string> GetAllVehicleLicenseNumber()
        {
            List<string> licenseNumbers = new List<string>();

            foreach (Vehicle vehicle in m_GarageVehicles)
            {
                licenseNumbers.Add(vehicle.LicenseNumber);
            }

            return licenseNumbers;
        }

        public List<string> GetFilteredVehicleLicenseNumber(Vehicle.eStateOfVehicleInGarage i_StateToFilterBy)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (Vehicle vehicle in m_GarageVehicles)
            {
                if(vehicle.StateInGarage == i_StateToFilterBy)
                {
                licenseNumbers.Add(vehicle.LicenseNumber);
                }
            }

            return licenseNumbers;
        }
    }
}
