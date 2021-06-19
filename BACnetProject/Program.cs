using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace BACnetProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A BACnet Utility in C#");

            //create a discovery object
            Discover whois = new Discover();

            //create an analog input read object for address 8212, point 1
            ReadAI get_ai_value = new ReadAI("8212", "1");

            //get analog input present value
            Console.WriteLine(get_ai_value.Output());

            //create an object to read array of all points in 8212
            ReadAllObjects get_obj_list = new ReadAllObjects("8212");

            //use ListOutput to get array of bacnet objects from 8212
            var result = get_obj_list.ListOutput();
            foreach(var item in result)
            {
                Console.Write($"{item}, ");
            }

            Console.WriteLine();

            //use Output method to create address list array
            var address_list = whois.Output();
            foreach(var item2 in address_list)
            {
                Console.WriteLine((item2));
            }
        }
    }

}
