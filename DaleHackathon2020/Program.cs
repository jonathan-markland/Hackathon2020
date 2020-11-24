using System;

namespace DaleHackathon2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"C:\Users\ukjmak\OneDrive - Waters Corporation\Documents\Hackathon 2020 - Dale Beardsall - Quadrupole discs\TEST2 -- output from MCOSMOS software.txt";
            var loadedFile = MCosmosClassLibrary.DiscFileLoader.LoadFromFile(filePath);
            Console.WriteLine("Hello World!");
        }
    }
}
