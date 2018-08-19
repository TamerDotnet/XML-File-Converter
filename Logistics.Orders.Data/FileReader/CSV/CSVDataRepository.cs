using Logistics.Orders.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Logistics.Orders.Data.FileReader.CSV.Readers;
using Logistics.Orders.Data.FileWriter;

namespace Logistics.Orders.Data.FileReader.CSV
{
    public class CSVDataRepository : IDataRepository
    {
         
        private IFileReader _fileReader;
        private IDataWriter _dataWriter;
        private IDataManager _dataManager;
        public CSVDataRepository( IFileReader fileReader, IDataWriter dataWriter, IDataManager dataManager)  
        { 
            this._fileReader = fileReader;
            this._dataWriter = dataWriter;
            this._dataManager = dataManager;
        }
        public  List<PurchaseOrder> GetPurchaseOrders()
        {
            // throw an exception if the file does not exists
            if (!this._fileReader.FileExists())
                throw new FileNotFoundException(Messages.File_Not_Found);

            List<string> csvLines = this._fileReader.ReadAllLines();
            var orders = (from lines in csvLines
                             group lines by lines.Split(",")[1] into orderLines // grouping is based on 2nd column (OrderID)
                             orderby orderLines.Key
                             select new OrderDataLines
                             {
                                 Lines = orderLines.ToList(),
                                 OrderKey = orderLines.Key
                             }).ToList();   // Get the CSV lines grouped by Order Group
            return this.GeneratePurchaseOrderList(orders); 
        }

        public void SaveData(List<PurchaseOrder> PurchaseOrders)
        {
            this._dataWriter.WriteToFile(PurchaseOrders);
        }

        private List<PurchaseOrder> GeneratePurchaseOrderList(List<OrderDataLines> orders)
        {
            List<PurchaseOrder> PurchaseOrderList = new List<PurchaseOrder>();

            foreach (OrderDataLines order in orders)
            {
                PurchaseOrderList.Add(this._dataManager.GetPurchaseOrder(order));
            }
            return PurchaseOrderList;
        }
    }
}
