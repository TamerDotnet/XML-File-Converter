using Logistics.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Data.FileReader.CSV.Mappers
{
    public interface IOrderDetailsMapper
    {
        PurchaseOrderLine Map(OrderDataLines orderDataLines);
    }
}
