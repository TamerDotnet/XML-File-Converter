using Logistics.Orders.Data.FileReader;
using Logistics.Orders.Data.FileReader.CSV;
using Logistics.Orders.Data.FileReader.CSV.Mappers;
using Logistics.Orders.Data.FileWriter;
using Logistics.Orders.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;


namespace Logistics.Orders.Console
{
    public class Startup
    {
        private IServiceProvider serviceProvider;
        public Startup()
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
              serviceProvider = serviceCollection.BuildServiceProvider();
        }
        public void ExecuteApplication()
        {
            // run app
            serviceProvider.GetService<App>().Run();
        }
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", false)
                .Build();

            serviceCollection.AddOptions();

            // add services
            serviceCollection.AddTransient<IOrderDetailsMapper, OrderDetailsMapper>();
            serviceCollection.AddTransient<IOrderHeaderMapper, OrderHeaderMapper>();

            serviceCollection.AddTransient<IDataRepository, CSVDataRepository>();
            serviceCollection.AddTransient<IDataManager, DataManager>();
            serviceCollection.AddTransient<IFileReader, FileReader>(fileProvider =>
            {
                DirectoryInfo directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory());
                string inputFilePath = Path.Combine(directoryInfo.ToString(), configuration["DataSource:FilePathInput"]);
                return new FileReader(inputFilePath);
            });
            serviceCollection.AddTransient<IDataWriter, XMLDataWriter>(fileProvider =>
            {
                DirectoryInfo directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory());
                string outputFilePath = Path.Combine(directoryInfo.ToString(), configuration["DataSource:FilePathOutput"]);
                return new XMLDataWriter( outputFilePath);
            });

            // add the application 
            serviceCollection.AddTransient<App>();
        }

    }
}
