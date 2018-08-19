using System;
using System.Collections.Generic;
using System.Text;
using Logistics.Orders.Data;
using Logistics.Orders.Domain;

namespace Logistics.Orders.Data.FileReader.CSV.Mappers
{
    public class OrderHeaderMapper : IOrderHeaderMapper
    { 
        /// <summary>
        /// Validate The ORder Header if it has any missing data or incorrect data type
        /// Map the Order Header to the PurchaseOrder object
        /// </summary>
        /// <param name="orderDataLines"></param>
        /// <returns>PurchaseOrderLine</returns>
        public PurchaseOrder Map(OrderDataLines orderDataLines)
        {
            if (orderDataLines.Lines.Count == 0)
                throw new Exception(String.Format(Messages.No_Header_Found_For, orderDataLines.OrderKey));

            //Perform validation on data and throw exception if there is any before proceed to perform mapping
            this.Validate(orderDataLines);

            var Lines = orderDataLines.Lines.SplitDataArray();
                
            return new PurchaseOrder()
            {
                CustomerPo = Lines[1],
                Supplier = Lines[2].GenerateCodeId(),
                Origin = Lines[3],
                Destination = Lines[4],
                CargoReady = DateTime.Parse(Lines[5])
            };
        }
        /// <summary>
        ///  Validate The ORder Header if it has any missing data or incorrect data type
        ///  throw an exception if any data or incorrect data type
        /// </summary>
        /// <param name="orderDataLines"></param>
        private void Validate(OrderDataLines orderDataLines)
        {
            int index = 0;
            string FieldsNewReview = "";


            var Lines = orderDataLines.Lines.SplitDataArray();
            foreach (string item in Lines) 
            {
                if (index == 1 && item == "")
                    FieldsNewReview += "CustomerPo,";
                else if (index == 2 && item == "")
                    FieldsNewReview += "Supplier,";
                else if (index == 3 && item == "")
                    FieldsNewReview += "Origin,";
                else if (index == 4 && item == "")
                    FieldsNewReview += "Destination,";
                else if (index == 5 && (item == "" || !item.IsValidDate()))
                    FieldsNewReview += "CargoReady,";
                index += 1;
            }
            if (FieldsNewReview.EndsWith(","))
                FieldsNewReview = FieldsNewReview.Substring(0, FieldsNewReview.Length - 1);

            if (FieldsNewReview.Length > 0)
                throw new Exception(String.Format(Messages.Header_Fields_Need_Review, FieldsNewReview, orderDataLines.OrderKey));
        }
    }
}
