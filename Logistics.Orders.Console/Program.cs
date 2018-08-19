using Logistics.Orders.Data.FileReader;
using Logistics.Orders.Data.FileReader.CSV;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;


namespace Logistics.Orders.Console
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Startup startup = new Startup();
            startup.ExecuteApplication();

        }

   }
}
