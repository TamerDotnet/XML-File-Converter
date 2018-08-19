using Logistics.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Data.FileReader.CSV.Mappers
{
    public interface IOrderHeaderMapper
    {
        PurchaseOrder Map(OrderDataLines orderDataLines);
    }
}
