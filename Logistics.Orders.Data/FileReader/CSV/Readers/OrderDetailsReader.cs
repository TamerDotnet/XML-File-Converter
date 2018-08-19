using Logistics.Orders.Data.FileReader.CSV.Mappers;
using Logistics.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logistics.Orders.Data.FileReader.CSV.Readers
{
    public class OrderDetailsReader
    {
        private OrderDataLines _orderLines;
        private IOrderDetailsMapper _mapper;
        public OrderDetailsReader(OrderDataLines orderLines, IOrderDetailsMapper mapper)
        {
            this._orderLines = orderLines;
            this._mapper = mapper;

            // Filter the Lines to have only ones start with "D" for Order details
            // for specific Order
            this._orderLines.Lines = orderLines.Lines.FindAll(x => x.ToUpper().StartsWith("D")
                                        && x.Split(",")[1].ToString() == orderLines.OrderKey)
                                        .ToList();

            if (this._orderLines.Lines.Count() == 0)
                throw new Exception(String.Format(Messages.No_Order_Details_Found_For, this._orderLines.OrderKey));
        }
        /// <summary>
        ///  Get the Purchase Order Lines for specific Order
        /// </summary>
        /// <returns>List<PurchaseOrderLine></returns>
        public List<PurchaseOrderLine> GetPurchaseOrderLineList()
        {
            List<PurchaseOrderLine> purchaseOrderLineList = new List<PurchaseOrderLine>();
            var OrderLines = this._orderLines.Lines.Select(x => x.Split(",")).ToList();

            if (OrderLines.Count() == 0)
                throw new Exception(String.Format(Messages.No_Order_Details_Found_For, this._orderLines.OrderKey ));

           
            foreach (string[] data in OrderLines)
            {
                OrderDataLines order = new OrderDataLines()
                {
                    OrderKey = this._orderLines.OrderKey,
                    Lines = data.ToList()
                };
                purchaseOrderLineList.Add(this._mapper.Map(order));
            }

            return purchaseOrderLineList;
        }
    }
}
