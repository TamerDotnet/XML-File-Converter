using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Domain
{
    public class OrderDataLines
    {
        public List<string> Lines;
        public string OrderKey;
        public OrderDataLines()
        {
            Lines = new List<string>();
        }
    }
}
