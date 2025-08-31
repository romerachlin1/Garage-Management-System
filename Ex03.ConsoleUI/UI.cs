using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.IO;
using static Ex03.GarageLogic.Vehicle;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        private Garage m_Garage;

        public UI()
        {
            m_Garage = new Garage();
        }

        public enum eGarageMenuOption
        {
            LoadVehicleData = 1,
            InsertNewVehicle = 2,
            DisplayLicenseNumbers = 3,
            ChangeVehicleStatus = 4,
            InflateWheelsToMax = 5,
            RefuelVehicle = 6,
            RechargeVehicle = 7,
            DisplayVehicleInfo = 8
        }

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Garage! \nPlease insert a number for required service:\n");
            Console.WriteLine("1. Load the system with vehicle data from file");
            Console.WriteLine("2. Insert a new vehicle into the garage");
            Console.WriteLine("3. Display all license numbers of vehicles in the garage, with optional filtering by their status");
            Console.WriteLine("4. Change the status of a vehicle in the garage");
            Console.WriteLine("5. Inflate all wheels of a vehicle to their maximum");
            Console.WriteLine("6. Refuel a fuel-based vehicle");
            Console.WriteLine("7. Recharge an electric vehicle");
            Console.WriteLine("8. Display full information of a vehicle by license number");
            string userInput = Console.ReadLine();
            try
            {
                if (Enum.TryParse(userInput, out eGarageMenuOption userChoice) && Enum.IsDefined(typeof(eGarageMenuOption), userChoice))
                {
                    switch (userChoice)
                    {
                        case eGarageMenuOption.LoadVehicleData:
                            LoadVehicleData();
                            break;
                        case eGarageMenuOption.InsertNewVehicle:
                            InsertVehicleToGarage();
                            break;
                        case eGarageMenuOption.DisplayLicenseNumbers:
                            PrintLicenseNumbers();
                            break;
                        case eGarageMenuOption.ChangeVehicleStatus:
                            ChangeVehicleStatus();
                            break;
                        case eGarageMenuOption.InflateWheelsToMax:
                            FillAirToMax();
                            break;
                        case eGarageMenuOption.RefuelVehicle:
                            AddFuel();
                            break;
                        case eGarageMenuOption.RechargeVehicle:
                            AddBatteryTime();
                            break;
                        case eGarageMenuOption.DisplayVehicleInfo:
                            GetVehicleData();
                            break;
                    }
                }
                else
                {
                    throw new FormatException("Invalid choice. Please enter a number between 1 and 8.");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // $G$ NTT-999 (-5) You should have use: Environment.NewLine instead of "\n
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();
            }
        }

        public void GetVehicleData()
        {
            Console.Clear();
            string licenseNumber = VehicleInputManager.GetLicenseNumber();
            try
            {
                if (m_Garage.IsInGarage(licenseNumber))
                {
                    Vehicle vehicle = m_Garage.GetVehicleInGarage(licenseNumber);

                    Console.WriteLine($"License Number: {vehicle.LicenseNumber}");
                    Console.WriteLine($"Model Name: {vehicle.ModelName}");
                    Console.WriteLine($"Owner Name: {vehicle.OwnerName}");
                    Console.WriteLine($"Phone Number: {vehicle.PhoneNumber}");
                    Console.WriteLine($"Status in Garage: {vehicle.StateInGarage}");
                    Console.WriteLine($"Energy Type: {vehicle.EnergyType}");
                    Console.WriteLine($"Current Energy: {vehicle.CurrentEnergyAmount}");
                    Console.WriteLine($"Max Energy: {vehicle.MaxEnergyAmount}");
                    Console.WriteLine($"Energy Level: {vehicle.EnergyPercentageLeft}%");
                    Console.WriteLine($"Number of Wheels: {vehicle.NumberOfWheels}");
                    Console.WriteLine($"Wheels Manufacturer: {vehicle.Wheels[0].Manufacturer}");
                    Console.WriteLine($"Current Air Pressure: {vehicle.Wheels[0].CurrentAirPressure}");
                    Console.WriteLine($"Maximum Air Pressure: {vehicle.Wheels[0].MaxAirPressure}");
                    string vehicleType = vehicle.GetType().Name;
                    VehicleUI vehicleUI = VehicleUICreator.CreateVehicleUI(vehicleType);
                    vehicleUI.PrintSpecificData(vehicle);
                }
                else
                {
                    Console.WriteLine($"Vehicle with license number {licenseNumber} is not in the garage.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();
            }
        }

        public void LoadVehicleData()
        {
            Dictionary<eVehicleAttributes, string> vehicleAttributes = new Dictionary<eVehicleAttributes, string>();
            Console.Clear();
            try
            {
                Console.WriteLine("Loading vehicles from file vehicles.db");
                string[] lines = File.ReadAllLines("vehicles.db");
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        try
                        {
                            string[] vehicleData = line.Split(',');
                            vehicleAttributes.Clear();
                            vehicleAttributes.Add(eVehicleAttributes.VehicleType, vehicleData[0]);
                            vehicleAttributes.Add(eVehicleAttributes.LicenseNumber, vehicleData[1]);
                            vehicleAttributes.Add(eVehicleAttributes.ModelName, vehicleData[2]);
                            vehicleAttributes.Add(eVehicleAttributes.CurrentEnergyPercentege, vehicleData[3]);
                            vehicleAttributes.Add(eVehicleAttributes.WheelManufacturerName, vehicleData[4]);
                            vehicleAttributes.Add(eVehicleAttributes.CurrentAirPressure, vehicleData[5]);
                            vehicleAttributes.Add(eVehicleAttributes.OwnerName, vehicleData[6]);
                            vehicleAttributes.Add(eVehicleAttributes.PhoneNumber, vehicleData[7]);
                            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleData[0], vehicleData[1], vehicleData[2]);
                            vehicle.AddSpecificAttributs(vehicleAttributes, vehicleData[8], vehicleData[9]);
                            if (!m_Garage.IsInGarage(vehicleData[1]))
                            {
                                m_Garage.InsertVehicleToGarage(vehicleAttributes);
                                Console.WriteLine($"Successfully loaded vehicle: {vehicleData[1]}");
                            }
                            else
                            {
                                Console.WriteLine($"Vehicle {vehicleData[1]} is already in the garage, skipping...");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing line: {line}");
                            Console.WriteLine($"Error details: {ex.Message}");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Please load a file named vehicles.db");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();
            }
        }

        public void AddFuel()
        {
            Console.Clear();
            string licenseNumber = VehicleInputManager.GetLicenseNumber();
            float energy = 0.0f;
            bool isValidInput = false;
            try
            {
                if (m_Garage.IsInGarage(licenseNumber))
                {
                    Vehicle vehicle = m_Garage.GetVehicleInGarage(licenseNumber);
                    bool isElectric = (vehicle.EnergyType == eEnergyType.Electric);
                    eEnergyType fuelType = vehicle.EnergyType;
                    if (!isElectric)
                    {
                        Console.WriteLine("Please select the type of fuel:");
                        Console.WriteLine("1. Soler");
                        Console.WriteLine("2. Octan95");
                        Console.WriteLine("3. Octan96");
                        Console.WriteLine("4. Octan98");
                        string fuelChoice = Console.ReadLine();
                        if (Enum.TryParse(fuelChoice, out eEnergyType userChoice) && Enum.IsDefined(typeof(eEnergyType), userChoice))
                        {
                            switch (userChoice)
                            {
                                case eEnergyType.Soler:
                                    fuelType = eEnergyType.Soler;
                                    break;
                                case eEnergyType.Octan95:
                                    fuelType = eEnergyType.Octan95;
                                    break;
                                case eEnergyType.Octan96:
                                    fuelType = eEnergyType.Octan96;
                                    break;
                                case eEnergyType.Octan98:
                                    fuelType = eEnergyType.Octan98;
                                    break;
                            }
                        }
                        else
                        {
                            throw new FormatException("Invalid status format. Please enter a number between 1 and 4");
                        }
                        Console.WriteLine($"Please insert the amount of Fuel to add:");
                        string input = Console.ReadLine();
                        if (float.TryParse(input, out energy))
                        {
                            if (energy < 0)
                            {
                                throw new ValueRangeException(0, vehicle.MaxEnergyAmount);
                            }
                        }
                        else
                        {
                            throw new FormatException("Invalid number format.");
                        }
                        vehicle.AddEnergy(energy, fuelType);
                        Console.WriteLine($"Successfully added {energy} units of {fuelType} to the vehicle.");
                    }
                    else
                    {
                        throw new ArgumentException($"Vehicle with license number {licenseNumber} is electric");
                    }
                }
                else
                {
                    throw new ArgumentException($"Vehicle with license number {licenseNumber} is not in the garage.");
                }
            }
            catch (ValueRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();
            }
        }

        public void AddBatteryTime()
        {
            Console.Clear();
            string licenseNumber = VehicleInputManager.GetLicenseNumber();
            float energy = 0.0f;
            bool isValidInput = false;
            try
            {
                if (m_Garage.IsInGarage(licenseNumber))
                {
                    Vehicle vehicle = m_Garage.GetVehicleInGarage(licenseNumber);
                    bool isElectric = (vehicle.EnergyType == eEnergyType.Electric);
                    eEnergyType fuelType = vehicle.EnergyType;
                    if (isElectric)
                    {
                        while (!isValidInput)
                        {
                            Console.WriteLine($"Please insert the amount of battery time to add:");
                            string input = Console.ReadLine();
                            if (float.TryParse(input, out energy))
                            {
                                if (energy > 0)
                                {
                                    isValidInput = true;
                                }
                                else
                                {
                                    throw new ValueRangeException(0, vehicle.MaxEnergyAmount);
                                }
                            }
                            else
                            {
                                throw new FormatException("Invalid number format.");
                            }
                        }
                        vehicle.AddEnergy(energy, fuelType);
                        Console.WriteLine($"Successfully added {energy} units of {fuelType} to the vehicle.");
                    }
                    else
                    {
                        throw new ArgumentException($"Vehicle with license number {licenseNumber} is fuel based");
                    }
                }
                else
                {
                    throw new ArgumentException($"Vehicle with license number {licenseNumber} is not in the garage.");
                }
            }
            catch (ValueRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();
            }
        }

        public void ChangeVehicleStatus()
        {
            Console.Clear();
            string licenseNumber = VehicleInputManager.GetLicenseNumber();
            try
            {
                if (m_Garage.IsInGarage(licenseNumber))
                {
                    Vehicle vehicle = m_Garage.GetVehicleInGarage(licenseNumber);
                    Console.WriteLine($"Current status of vehicle {licenseNumber}: {vehicle.StateInGarage}");
                    Console.WriteLine("Please select a new status for the vehicle:");
                    Console.WriteLine("1. UnderRepair");
                    Console.WriteLine("2. Repaired");
                    Console.WriteLine("3. PayedFor");
                    string userInput = Console.ReadLine();
                    if (Enum.TryParse(userInput, out eStateOfVehicleInGarage userChoice) && Enum.IsDefined(typeof(eStateOfVehicleInGarage), userChoice))
                    {
                        switch (userChoice)
                        {
                            case eStateOfVehicleInGarage.UnderRepair:
                                vehicle.StateInGarage = eStateOfVehicleInGarage.UnderRepair;
                                break;
                            case eStateOfVehicleInGarage.Repaired:
                                vehicle.StateInGarage = eStateOfVehicleInGarage.Repaired;
                                break;
                            case eStateOfVehicleInGarage.PayedFor:
                                vehicle.StateInGarage = eStateOfVehicleInGarage.PayedFor;
                                break;
                        }
                        Console.WriteLine($"Vehicle {licenseNumber} status updated to: {vehicle.StateInGarage}");
                    }
                    else
                    {
                        throw new FormatException("Invalid status format. Please enter a number between 1 and 3");
                    }
                }
                else
                {
                    Console.WriteLine($"Vehicle with license number {licenseNumber} is not in the garage.");
                }
            }
            catch (ValueRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();
            }
        }

        public void InsertVehicleToGarage()
        {
            Dictionary<eVehicleAttributes, string> vehicleAttributes = new Dictionary<eVehicleAttributes, string>();
            Console.Clear();
            string licenseNumber = VehicleInputManager.GetLicenseNumber();
            if (m_Garage.IsInGarage(licenseNumber))
            {
                Console.WriteLine($"{licenseNumber} is already in the garage.");
                m_Garage.GetVehicleInGarage(licenseNumber).StateInGarage = Vehicle.eStateOfVehicleInGarage.UnderRepair;
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();
            }
            else
            {
                string ownerName = VehicleInputManager.GetOwnerName();
                string ownerPhone = VehicleInputManager.GetPhoneNumber();
                string vehicleType = VehicleInputManager.GetVehicleType();
                string modelName = VehicleInputManager.GetModelName();
                float currentEnergyPercentege = VehicleInputManager.GetVehicleEnergy();
                string wheelManufacturerName = VehicleInputManager.GetWheelManufacturerName();
                float curWheelAirPressure = VehicleInputManager.GetCurrentAirPressure();
                VehicleUI vehicleUI = VehicleUICreator.CreateVehicleUI(vehicleType);
                vehicleUI.GetAndSetAttributes(vehicleAttributes);
                vehicleAttributes.Add(eVehicleAttributes.VehicleType, vehicleType);
                vehicleAttributes.Add(eVehicleAttributes.ModelName, modelName);
                vehicleAttributes.Add(eVehicleAttributes.LicenseNumber, licenseNumber);
                vehicleAttributes.Add(eVehicleAttributes.WheelManufacturerName, wheelManufacturerName);
                vehicleAttributes.Add(eVehicleAttributes.CurrentAirPressure, curWheelAirPressure.ToString());
                vehicleAttributes.Add(eVehicleAttributes.OwnerName, ownerName);
                vehicleAttributes.Add(eVehicleAttributes.PhoneNumber, ownerPhone);
                vehicleAttributes.Add(eVehicleAttributes.CurrentEnergyPercentege, currentEnergyPercentege.ToString());
                try
                {
                    m_Garage.InsertVehicleToGarage(vehicleAttributes);
                    Console.WriteLine($"Successfully loaded vehicle: {licenseNumber}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                    Menu();
                }
            }
        }

        public void FillAirToMax()
        {
            Console.Clear();
            string licenseNumber = VehicleInputManager.GetLicenseNumber();
            if (!m_Garage.IsInGarage(licenseNumber))
            {
                Console.WriteLine($"{licenseNumber} is not in the garage.");
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();
            }
            else
            {
                foreach (Vehicle vehicle in m_Garage.GarageVehicles)
                {
                    if (vehicle.LicenseNumber == licenseNumber)
                    {
                        foreach (Vehicle.Wheel wheel in vehicle.Wheels)
                        {
                            wheel.FillAir();
                        }
                    }
                }
                Console.WriteLine($"{licenseNumber}'s Wheels Has Been Successfully Filled To Max.");
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();
            }
        }

        public void PrintLicenseNumbers()
        {
            Console.Clear();
            Console.WriteLine("Which License Numbers Do You Want?");
            Console.WriteLine("1. All");
            Console.WriteLine("2. Under Repair");
            Console.WriteLine("3. Repaired");
            Console.WriteLine("4. Payed For");
            try
            {
                string userInput = Console.ReadLine();
                List<string> licenseNumbers = new List<string>();
                if (Enum.TryParse(userInput, out ePrintLicenseNumbersMenu userChoice) && Enum.IsDefined(typeof(ePrintLicenseNumbersMenu), userChoice))
                {
                    switch (userChoice)
                    {
                        case ePrintLicenseNumbersMenu.All:
                            licenseNumbers = m_Garage.GetAllVehicleLicenseNumber();
                            PrintLicenseNumbersHelper("All Vehicles", licenseNumbers);
                            break;
                        case ePrintLicenseNumbersMenu.UnderRepair:
                            licenseNumbers = m_Garage.GetFilteredVehicleLicenseNumber(Vehicle.eStateOfVehicleInGarage.UnderRepair);
                            PrintLicenseNumbersHelper("Vehicles Under Repair", licenseNumbers);
                            break;
                        case ePrintLicenseNumbersMenu.Repaired:
                            licenseNumbers = m_Garage.GetFilteredVehicleLicenseNumber(Vehicle.eStateOfVehicleInGarage.Repaired);
                            PrintLicenseNumbersHelper("Repaired Vehicles", licenseNumbers);
                            break;
                        case ePrintLicenseNumbersMenu.PayedFor:
                            licenseNumbers = m_Garage.GetFilteredVehicleLicenseNumber(Vehicle.eStateOfVehicleInGarage.PayedFor);
                            PrintLicenseNumbersHelper("Payed For Vehicles", licenseNumbers);
                            break;
                    }
                }
                else
                {
                    throw new FormatException("Invalid status format. Please enter a number between 1 and 4");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Menu();

            }
        }
        private void PrintLicenseNumbersHelper(string i_Category, List<string> i_LicenseNumbers)
        {
            Console.WriteLine($"\n{i_Category}:");
            if (i_LicenseNumbers.Count > 0)
            {
                foreach (string licenseNumber in i_LicenseNumbers)
                {
                    Console.WriteLine($"License Number: {licenseNumber}");
                }
            }
            else
            {
                Console.WriteLine("No vehicles found in this category.");
            }
        }
    }
}