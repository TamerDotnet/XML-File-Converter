using Logistics.Orders.Data.FileReader.CSV.Mappers;
using Logistics.Orders.Data.FileReader.CSV.Readers;
using Logistics.Orders.Domain;

namespace Logistics.Orders.Data.FileReader.CSV
{
    public class DataManager:IDataManager
    {
        private IOrderHeaderMapper orderHeaderMapper;
        private IOrderDetailsMapper orderDetailsMapper;
        public DataManager(IOrderHeaderMapper  pOrderHeaderMapper , IOrderDetailsMapper pOrderDetailsMapper)
        {
            this.orderHeaderMapper = pOrderHeaderMapper;
            this.orderDetailsMapper = pOrderDetailsMapper;
        }
        /// <summary>
        /// Get Purchase order based on both the Header and Details
        /// </summary>
        /// <param name="orderDataLine"></param>
        /// <returns></returns>
        public PurchaseOrder GetPurchaseOrder( OrderDataLines orderDataLine)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();

            //Get the Header for the orderKey
            OrderHeaderReader header = new OrderHeaderReader(orderDataLine, orderHeaderMapper);
            purchaseOrder = header.GetPurchaseOrder();

            //Then Get the Order Line list for that orderKey
            OrderDetailsReader content = new OrderDetailsReader(orderDataLine, orderDetailsMapper);
            purchaseOrder.Lines = content.GetPurchaseOrderLineList();

            return purchaseOrder;
        }
    }
}
