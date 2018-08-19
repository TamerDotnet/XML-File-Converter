using Logistics.Orders.Domain;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Logistics.Orders.Data.FileWriter
{
    public  class XMLDataWriter : IDataWriter
    { 
        private string _filePath;
        public XMLDataWriter(string filePath)
        { 
            this._filePath = filePath;
        }
        public void WriteToFile(List<PurchaseOrder> PurchaseOrders)
        {
            XmlSerializer writer =  new  XmlSerializer(typeof(List<PurchaseOrder>));
            using (FileStream file = File.Create(this._filePath))
            {
                writer.Serialize(file, PurchaseOrders);
            } 
        }
    }
}
