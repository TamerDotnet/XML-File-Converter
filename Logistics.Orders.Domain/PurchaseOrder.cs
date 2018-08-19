using System;
using System.Collections.Generic;

namespace Logistics.Orders.Domain
{
    public class PurchaseOrder
    {
        public string CustomerPo { get; set; }
        public string Supplier { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime CargoReady { get; set; }
        public List<PurchaseOrderLine> Lines { get; set; }

    }
}
