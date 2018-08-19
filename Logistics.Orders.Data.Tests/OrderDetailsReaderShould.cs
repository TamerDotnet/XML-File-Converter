using Logistics.Orders.Data.FileReader.CSV.Mappers;
using Logistics.Orders.Data.FileReader.CSV.Readers;
using Logistics.Orders.Domain;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Logistics.Orders.Data.Tests
{
    public class OrderDetailsReaderShould
    {
        private List<string> fakeOrderList;
        public OrderDetailsReaderShould()
        {
            //arrange
            fakeOrderList = new List<string>();
            fakeOrderList.Add("H,PO2008 - 01,SHANGHAI FURNITURE COMPANY,CNSHA,AUMEL,1 / 05 / 2014");
            fakeOrderList.Add("D,PO2008 - 01,1,RED LOUNGES,100,");
            fakeOrderList.Add("D,PO2008 - 01,2,GREY LOUNGES,50,");
        }
        private OrderDataLines SetupOrderDataLines_Single(string DataList, string Key)
        {
            OrderDataLines orderLines = new OrderDataLines();
            List<string> OrderDetailList = new List<string>();
            OrderDetailList.Add(DataList);

            orderLines.OrderKey = Key;
            orderLines.Lines = OrderDetailList;
            return orderLines;
        }
        private OrderDataLines SetupOrderDataLines_List(List<string> OrderList, string Key)
        {
            OrderDataLines orderLines = new OrderDataLines();
            orderLines.OrderKey = Key;
            orderLines.Lines = OrderList;
            return orderLines;
        }

        [Fact]
        public void OrderDetailsNotFoundForIncorrectOrderNumber()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_List(fakeOrderList, "PO20007 - 01");
            Mock<OrderDetailsMapper> mockOrderDetailsMapper = new Mock<OrderDetailsMapper>();
            
            //Assert
            Assert.Throws<Exception>(() => new OrderDetailsReader(dataLines, mockOrderDetailsMapper.Object));
        }

        [Fact]
        public void OrderDetailsFoundForOrderNumber()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_List(fakeOrderList, "PO2008 - 01");
            Mock<OrderDetailsMapper> mockOrderDetailsMapper = new Mock<OrderDetailsMapper>();
            OrderDetailsReader sut = new OrderDetailsReader(dataLines, mockOrderDetailsMapper.Object);
            //Assert
            Assert.NotNull(sut);
        }
        [Fact]
        public void GetMatchingDetailsForOrderNumber()
        { 
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_List(fakeOrderList, "PO2008 - 01");
            Mock<OrderDetailsMapper> mockOrderDetailsMapper = new Mock<OrderDetailsMapper>();
            OrderDetailsReader sut = new OrderDetailsReader(dataLines, mockOrderDetailsMapper.Object);
            //Assert
            Assert.Equal(2, sut.GetPurchaseOrderLineList().Count);
        }
        [Fact]
        public void MissingOrderDetailsForOrderNumber()
        {
            //arrange
            OrderDataLines orderLines = SetupOrderDataLines_Single("H,PO2008 - 01,SHANGHAI FURNITURE COMPANY,CNSHA,AUMEL,1 / 05 / 2014", "PO2008 - 01");
            Mock<OrderDetailsMapper> mockOrderDetailsMapper = new Mock<OrderDetailsMapper>();
            //Assert
            Assert.Throws<Exception>(() => new OrderDetailsReader(orderLines, mockOrderDetailsMapper.Object));
        }


        [Fact]
        public void MissingLineNumberForOrderDetails()
        {
            //arrange
            OrderDataLines orderLines = SetupOrderDataLines_Single("D,PO2008 - 01,,RED LOUNGES,100,", "PO2008 - 01");
            Mock<OrderDetailsMapper> mockOrderDetailsMapper = new Mock<OrderDetailsMapper>();
            OrderDetailsReader sut = new OrderDetailsReader(orderLines, mockOrderDetailsMapper.Object);

            //assert
            Assert.Throws<Exception>(() => sut.GetPurchaseOrderLineList());
        }
    }
}
