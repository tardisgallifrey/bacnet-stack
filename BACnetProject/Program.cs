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

            Discover whois = new Discover();

            ReadAI get_ai_value = new ReadAI("8212", "1");

            Console.WriteLine(get_ai_value.Output());

            ReadAllObjects get_obj_list = new ReadAllObjects("8212");

            var result = get_obj_list.ListOutput();
            foreach(var item in result)
            {
                Console.Write($"{item}, ");
            }

            Console.WriteLine();

            var result2 = whois.Output();
            foreach(var item2 in result2)
            {
                Console.WriteLine((item2));
            }
        }
    }

}
