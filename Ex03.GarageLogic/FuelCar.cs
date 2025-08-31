namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        public FuelCar(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            m_MaxEnergyAmount = 48;
            m_EnergyType = eEnergyType.Octan95;
            m_NumOfWheels = 5;
            m_MaxAirPressure = 32;
        }
    }
}
