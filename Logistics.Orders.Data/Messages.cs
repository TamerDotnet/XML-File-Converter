using System;
namespace Logistics.Orders.Data
{
    public static class Messages
    {
        public  const string File_Not_Found = "Cannot find a file";
        public const string File_Name_Not_Provided = "File Name not Provided.";
        public const string Invalid_File_Name_Extension = "Invalid File Extension";
        public const string No_Header_Found_For = "No header found for Order ID: {0}";
        public const string No_Order_Details_Found_For = "No Order Details Found for Order ID: {0} ";
        public const string Header_Fields_Need_Review = "The Following Fields for the order header are either Missing/Incorrect {0} for Order ID: {1}";
        public const string Details_Fields_Need_Review = "The Following Fields for the order Details are either Missing/Incorrect {0} for Order ID: {1}";
        public const string No_OrderId_Found = "No Order Id Found"; 
    }
}
