using Logistics.Orders.Domain;
using System.Collections.Generic;

namespace Logistics.Orders.Data.FileWriter
{
    public interface IDataWriter
    {
        void WriteToFile(List<PurchaseOrder> PurchaseOrders);
    }
}
