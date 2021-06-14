using System;

namespace BACnetProject
{
    public class ReadAI : ReadProperty
    {
        private const string analog_in_property = "0";
        private const string read_pv = "85";

        public ReadAI(string device, string ai_point)
        {
            this.objectDevice = device;
            this.objectNumber = analog_in_property;
            this.objectInstance = ai_point;
            this.objectProperty = read_pv;
        }
    }
}