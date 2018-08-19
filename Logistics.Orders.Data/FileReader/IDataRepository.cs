using Logistics.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Data.FileReader
{
    public interface IDataRepository
    {
        List<PurchaseOrder> GetPurchaseOrders();
        void SaveData(List<PurchaseOrder> PurchaseOrders);
    }
}
