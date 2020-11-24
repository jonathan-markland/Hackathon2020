using System;

namespace DaleHackathon2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var filePath = @"C:\Users\ukjmak\OneDrive - Waters Corporation\Documents\Hackathon 2020 - Dale Beardsall - Quadrupole discs\TEST2 -- output from MCOSMOS software.txt";
            var loader = new MCosmosClassLibrary.DiscFileLoader(filePath);

        }
    }
}
