namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        public ElectricMotorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            m_MaxEnergyAmount = 4.8f;
            m_EnergyType = eEnergyType.Electric;
            m_NumOfWheels = 2;
            m_MaxAirPressure = 30;
        }
    }
}
