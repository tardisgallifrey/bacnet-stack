using System;

namespace BACnetProject
{
    public class ReadAI : ReadProperty
    {
        //ReadAI inherits all properties and methods from ReadProperty
        
        //Read an analog input
        private const string analog_in_property = "0";

        //Read the point's present value
        private const string read_pv = "85";

        public ReadAI(string device, string ai_point)
        {
            //We set up a constructor to fill the properties just to read analog inputs
            //device number and point number are passed in
            this.objectDevice = device;
            this.objectNumber = analog_in_property;
            this.objectInstance = ai_point;
            this.objectProperty = read_pv;
        }
    }
}