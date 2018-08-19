using Logistics.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Logistics.Orders.Data.FileReader.CSV.Mappers;

namespace Logistics.Orders.Data.FileReader.CSV.Readers
{
    public class OrderHeaderReader
    {
        private OrderDataLines _orderDataLines;
        private IOrderHeaderMapper _mapper;
        private string HeaderLine;
        public OrderHeaderReader(OrderDataLines orderDataLines, IOrderHeaderMapper mapper)
        {
            this._orderDataLines = orderDataLines;
            this._mapper = mapper;
            // Get Single Line that start with H for Order Header
            this.HeaderLine = orderDataLines.Lines.Where(x => x.ToUpper().StartsWith("H")
                                && x.Split(",")[1].ToString() == orderDataLines.OrderKey)
                                   .FirstOrDefault();

            if (this.HeaderLine == null)
                throw new Exception(String.Format(Messages.No_Header_Found_For, this._orderDataLines.OrderKey));

        }
        /// <summary>
        /// Return Single PurchaseOrder without any orderlines
        /// </summary>
        /// <returns>PurchaseOrder</returns>
        public PurchaseOrder GetPurchaseOrder()
        {
            var lines = HeaderLine.Split(",").ToList();
            if (lines == null)
                throw new Exception(String.Format(Messages.No_Header_Found_For, this._orderDataLines.OrderKey));
            OrderDataLines order = new OrderDataLines()
            {
                Lines = lines,
                OrderKey = this._orderDataLines.OrderKey
            };
            return this._mapper.Map(order);
        }
    }
}
