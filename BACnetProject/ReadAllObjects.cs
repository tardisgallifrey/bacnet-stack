using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace BACnetProject
{

    public struct Points
    {
        public int id;  
        public string property_name;
        public int property_number;
        public int point_number;
    }
   
    public class ReadAllObjects : ReadProperty
    {
        public ReadAllObjects(string device)
        {
            //Does the same as in ReadAI, but different values
            const string device_property = "8";
            const string read_all = "76";

            this.objectDevice = device;
            this.objectNumber = device_property;
            this.objectInstance = device;
            this.objectProperty = read_all;

        }

            //Base class only returns a string
            //We implement a new method that returns an array
            //Will return later to have it returna Tuple or hashed Tuple
        public List<string> ListOutput()
            {
                List<string> results = new List<string>();

                var object_List = this.Output().Split(',').ToList();

                foreach(string str in object_List)
                {
                    TextThings filter = new TextThings();

                    var output = filter.FilterString(str);
                    
                    results.Add(output);

                }

                return results;
            }

        

        
    }
}