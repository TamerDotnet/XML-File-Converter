using Logistics.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Data.FileReader.CSV
{
    public interface IDataManager
    {
        PurchaseOrder GetPurchaseOrder(OrderDataLines orderDataLine);
    }
}
