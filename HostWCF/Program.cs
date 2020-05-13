using System;
using System.ServiceModel;
using ObjectWCF;
using System.ServiceModel.Description;
namespace HostWCF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lansare server WCF...");
            ServiceHost host = new ServiceHost(typeof(PostComment),
           new Uri("http://localhost:8000/PC"));
            foreach (ServiceEndpoint se in host.Description.Endpoints)
                Console.WriteLine($"A (address): {se.Address} \nB (binding): {se.Binding.Name}\nC(Contract): {se.Contract.Name}\n");
            host.Open();
            Console.WriteLine("Server in executie. Se asteapta conexiuni...");
            Console.WriteLine("Apasati Enter pentru a opri serverul!");
            Console.ReadKey();
            host.Close();
        }
    }
}