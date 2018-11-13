using System;
using System.Diagnostics;
using System.ServiceModel;
using VetTracker2.Services;

namespace VetTracker2.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(VetTracker2Service));
            try
            {
                host.Open();
                Console.WriteLine("Self hosting!");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                host.Close();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
