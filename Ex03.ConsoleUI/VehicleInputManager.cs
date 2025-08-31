using Ex03.GarageLogic;
using System;

namespace Ex03.ConsoleUI
{
    public class VehicleInputManager
    {
        public static string GetLicenseNumber()
        {
            string licenseNumber = "";
            bool isValidInput = false;

            Console.WriteLine("Please insert your vehicle license number:");
            while (!isValidInput)
            {
                try
                {
                    licenseNumber = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(licenseNumber))
                    {
                        throw new ArgumentException("License number cannot be empty or whitespace.");
                    }

                    isValidInput = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid License number:");
                }
            }

            return licenseNumber;
        }

        // $G$ NTT-004 (-3) You should have used string.Empty instead of "".
        public static string GetVehicleType()
        {
            string vehicleType = "";
            bool isValidInput = false;

            Console.WriteLine("Please insert your vehicle type:");
            while (!isValidInput)
            {
                try
                {
                    vehicleType = Console.ReadLine().Replace(" ", "");
                    if (!VehicleCreator.SupportedTypes.Contains(vehicleType))
                    {
                        throw new ArgumentException("Vehicle type is not supported.");
                    }

                    isValidInput = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid vehicle type:");
                }
            }

            return vehicleType;
        }

        public static float GetVehicleEnergy()
        {
            float energyPercentege = 0.0f;
            bool isValidInput = false;

            Console.WriteLine("Please insert your vehicle's current remaining fuel or battery percentege:");
            while (!isValidInput)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (float.TryParse(input, out energyPercentege))
                    {
                        if (energyPercentege < 0 || energyPercentege > 100)
                        {
                            throw new ValueRangeException(0, 100);
                        }

                        isValidInput = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid number format.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid percentege value.");
                }
            }

            return energyPercentege;
        }

        public static float GetCurrentAirPressure()
        {
            float airPressure = 0.0f;
            bool isValidInput = false;

            Console.WriteLine("Please insert the current air pressure of the wheels:");
            while (!isValidInput)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (float.TryParse(input, out airPressure))
                    {
                        if (airPressure < 0)
                        {
                            throw new ValueRangeException(0, float.MaxValue);
                        }

                        isValidInput = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid number format.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid air pressure value:");
                }
            }

            return airPressure;
        }

        public static string GetCarColor()
        {
            string carColor = "";
            bool isValidInput = false;

            Console.WriteLine("Please choose the car color from the following options:");
            Console.WriteLine("Available colors: Yellow, Black, White, Silver");
            while (!isValidInput)
            {
                try
                {
                    carColor = Console.ReadLine();
                    if (Enum.TryParse(carColor, out GarageLogic.Car.eColor output))
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid color. Please choose from: Yellow, Black, White, Silver");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please enter a valid car color:");
                }
            }

            return carColor;
        }

        public static string GetMotorcycleLicenseType()
        {
            string licenseType = "";
            bool isValidInput = false;

            Console.WriteLine("Please choose the motorcycle license type from the following options:");
            Console.WriteLine("Available types: A, A2, AB, B2");
            while (!isValidInput)
            {
                try
                {
                    licenseType = Console.ReadLine();
                    if (Enum.TryParse(licenseType, out Motorcycle.eLicenseType output))
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid license type. Please choose from: A, A2, AB, B2");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please enter a valid license type:");
                }
            }

            return licenseType;
        }

        public static string GetTruckDangerousMaterials()
        {
            string hasDangerousMaterials = "";
            bool isValidInput = false;

            Console.WriteLine("Does the truck carry dangerous materials? (true/false):");
            while (!isValidInput)
            {
                try
                {
                    hasDangerousMaterials = Console.ReadLine();
                    if (bool.TryParse(hasDangerousMaterials, out bool output))
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid input. Please enter 'true' or 'false'");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please enter if the truck carries dangerous materials (true/false):");
                }
            }

            return hasDangerousMaterials;
        }

        public static string GetWheelManufacturerName()
        {
            string manufacturerName = "";
            bool isValidInput = false;

            Console.WriteLine("Please insert the wheel manufacturer name:");
            while (!isValidInput)
            {
                try
                {
                    manufacturerName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(manufacturerName))
                    {
                        throw new ArgumentException("Manufacturer name cannot be empty or whitespace.");
                    }

                    isValidInput = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid manufacturer name:");
                }
            }

            return manufacturerName;
        }

        public static string GetOwnerName()
        {
            string ownerName = "";
            bool isValidInput = false;

            Console.WriteLine("Please insert the owner's name:");
            while (!isValidInput)
            {
                try
                {
                    ownerName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(ownerName))
                    {
                        throw new ArgumentException("Owner name cannot be empty or whitespace.");
                    }
                    foreach (char c in ownerName)
                    {
                        if (!char.IsLetter(c))
                        {
                            throw new ArgumentException("Owner name must contain only letters."); ;
                        }
                    }

                    isValidInput = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid owner name:");
                }
            }

            return ownerName;
        }

        public static string GetPhoneNumber()
        {
            string phoneNumber = "";
            bool isValidInput = false;

            Console.WriteLine("Please insert the owner's phone number:");
            while (!isValidInput)
            {
                try
                {
                    phoneNumber = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(phoneNumber))
                    {
                        throw new ArgumentException("Phone number cannot be empty or whitespace.");
                    }

                    bool containsOnlyDigits = true;
                    foreach (char digit in phoneNumber)
                    {
                        if (!char.IsDigit(digit))
                        {
                            containsOnlyDigits = false;
                            break;
                        }
                    }
                    
                    if (!containsOnlyDigits)
                    {
                        throw new ArgumentException("Phone number can only contain digits.");
                    }

                    isValidInput = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid phone number:");
                }
            }

            return phoneNumber;
        }

        public static int GetMotorcycleEngineVolume()
        {
            int engineVolume = 0;
            bool isValidInput = false;

            Console.WriteLine("Please insert the motorcycle engine volume (in cc):");
            while (!isValidInput)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out engineVolume))
                    {
                        if (engineVolume <= 0)
                        {
                            throw new ValueRangeException(1, int.MaxValue);
                        }

                        isValidInput = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid number format.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid engine volume:");
                }
            }

            return engineVolume;
        }

        public static int GetNumCarDoors()
        {
            int numDoors = 0;
            bool isValidInput = false;

            Console.WriteLine("Please insert the number of doors (2-5):");
            while (!isValidInput)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out numDoors))
                    {
                        if (numDoors < 2 || numDoors > 5)
                        {
                            throw new ValueRangeException(2, 5);
                        }

                        isValidInput = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid number format.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid number of doors:");
                }
            }

            return numDoors;
        }

        public static float GetTruckCargoSize()
        {
            float cargoSize = 0.0f;
            bool isValidInput = false;

            Console.WriteLine("Please insert the truck's cargo size:");
            while (!isValidInput)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (float.TryParse(input, out cargoSize))
                    {
                        if (cargoSize < 0)
                        {
                            throw new ValueRangeException(0, float.MaxValue);
                        }

                        isValidInput = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid number format.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid cargo size:");
                }
            }

            return cargoSize;
        }

        public static string GetModelName()
        {
            string modelName = "";
            bool isValidInput = false;

            Console.WriteLine("Please insert the vehicle model name:");
            while (!isValidInput)
            {
                try
                {
                    modelName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(modelName))
                    {
                        throw new ArgumentException("Model name cannot be empty or whitespace.");
                    }

                    isValidInput = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please insert a valid model name:");
                }
            }
                
            return modelName;
        }
    }
}
