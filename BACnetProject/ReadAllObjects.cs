using System;
using System.Collections.Generic;
using System.Linq;


namespace BACnetProject
{

   
    public class ReadAllObjects : ReadProperty
    {
        public ReadAllObjects(string device)
        {
            const string device_property = "8";
            const string read_all = "76";

            this.objectDevice = device;
            this.objectNumber = device_property;
            this.objectInstance = device;
            this.objectProperty = read_all;

        }

        public List<string> ListOutput()
            {
                List<string> results = new List<string>();

                var object_List = this.Output().Split(',').ToList();

                foreach(string str in object_List)
                {
                    TextThings filter = new TextThings();

                    var output = filter.FilterString(str);
                    //Console.Write(output);
                    results.Add(output);

                }

                return results;
            }

        

        
    }
}