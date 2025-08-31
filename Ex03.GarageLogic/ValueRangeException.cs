using System;

namespace Ex03.GarageLogic
{
    public class ValueRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueRangeException(float i_MinValue, float i_MaxValue)
            : base($"Error, only values between {i_MinValue} and {i_MaxValue} are valid")
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}
