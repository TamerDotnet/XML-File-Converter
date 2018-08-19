using Logistics.Orders.Data.FileReader;
using Logistics.Orders.Data.FileWriter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Console
{
   public  class App
    {
        private IDataRepository _dataRepository;
        public App(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository; 
        }

        public void Run()
        {
            try
            {
                var orders = this._dataRepository.GetPurchaseOrders();
                this._dataRepository.SaveData(orders);
                System.Console.WriteLine("File Has been Saved");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
             
            System.Console.ReadLine(); 
        }
    }
}
