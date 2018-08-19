using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Domain
{
    public class PurchaseOrderLine
    {
        public int LineNumber { get; set; }
        public string ProductDescription { get; set; }
        public int OrderQty { get; set; }
    }
}
