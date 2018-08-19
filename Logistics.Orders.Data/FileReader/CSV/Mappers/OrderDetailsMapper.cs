using Logistics.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Data.FileReader.CSV.Mappers
{
    public class OrderDetailsMapper :IOrderDetailsMapper
    {
        /// <summary>
        /// Validate The ORder Details if it has any missing data or incorrect data type
        /// Map the Order Details to the PurchaseOrderLine object
        /// </summary>
        /// <param name="orderDataLines">PurchaseOrderLine</param>
        /// <returns></returns>
        public PurchaseOrderLine Map(OrderDataLines orderDataLines)
        {
            if (orderDataLines.Lines.Count == 0)
                throw new Exception(String.Format(Messages.No_Order_Details_Found_For, orderDataLines.OrderKey));
            
            //Perform validation on data and throw exception if there is any before proceed to perform mapping
            this.Validate(orderDataLines);

            var Lines = orderDataLines.Lines.SplitDataArray();
             
            return new PurchaseOrderLine()
            {
                LineNumber = int.Parse(Lines[2]),
                ProductDescription = Lines[3],
                OrderQty = int.Parse(Lines[4])
            };
             
        }
        /// <summary>
        ///  Validate The ORder Details if it has any missing data or incorrect data type
        ///  throw an exception if any data or incorrect data type
        /// </summary>
        /// <param name="orderDataLines"></param>
        /// <returns></returns>
        private void Validate(OrderDataLines orderDataLines)
        {
            int index = 0;
            string FieldsNewReview = "";

            var Lines = orderDataLines.Lines.SplitDataArray();
            foreach (string item in Lines)
            {

                if (index == 1 && item == "")
                    FieldsNewReview += "CustomerPo,";
                else if (index == 2 && (item == ""|| !item.IsValidNumber()))
                    FieldsNewReview += "LineNumber,";
                else if (index == 3 && item == "")
                    FieldsNewReview += "ProductDescription,";
                else if (index == 4 && (item == ""|| !item.IsValidNumber()))
                    FieldsNewReview += "OrderQty,";
                index += 1;
            }
            if (FieldsNewReview.EndsWith(","))
                FieldsNewReview = FieldsNewReview.Substring(0, FieldsNewReview.Length - 1);

            if (FieldsNewReview.Length > 0)
                throw new Exception(String.Format(Messages.Header_Fields_Need_Review, FieldsNewReview, 
                                                     orderDataLines.OrderKey));
        }


    }
}
