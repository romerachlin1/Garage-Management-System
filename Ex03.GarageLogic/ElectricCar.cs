namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            m_MaxEnergyAmount = 4.8f;
            m_EnergyType = eEnergyType.Electric;
            m_NumOfWheels = 5;
            m_MaxAirPressure = 32;
        }
    }
}
