namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        public FuelMotorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            m_MaxEnergyAmount = 5.8f;
            m_EnergyType = eEnergyType.Octan98;
            m_NumOfWheels = 2;
            m_MaxAirPressure = 30;
        }
    }
}
