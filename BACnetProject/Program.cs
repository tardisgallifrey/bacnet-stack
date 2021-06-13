using System;
using System.Threading;


namespace BACnetProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A BACnet Utility in C#");

            var readprop = new ReadProperty();

            while (true)
            {
                readprop.objectDevice = 8212.ToString();
                readprop.objectNumber = 0.ToString();
                readprop.objectInstance = 1.ToString();
                Console.WriteLine(readprop.Output());
                Thread.Sleep(5000);
            }

        }
    }

}
