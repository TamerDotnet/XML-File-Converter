using Logistics.Orders.Data.FileReader.CSV.Mappers;
using Logistics.Orders.Data.FileReader.CSV.Readers;
using Logistics.Orders.Domain;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Logistics.Orders.Data.Tests
{
    public class OrderHeaderReaderShould
    {
        private List<string> fakeOrderList;
        public OrderHeaderReaderShould()
        {
            //arrange
            fakeOrderList = new List<string>();
            fakeOrderList.Add("H,PO2008 - 01,SHANGHAI FURNITURE COMPANY,CNSHA,AUMEL,1 / 05 / 2014");
            fakeOrderList.Add("D,PO2008 - 01,1,RED LOUNGES,100,");
            fakeOrderList.Add("D,PO2008 - 01,2,GREY LOUNGES,50,");
        }
        private OrderDataLines SetupOrderDataLines_List(List<string> OrderList, string Key)
        {
            OrderDataLines orderLines = new OrderDataLines();
            orderLines.OrderKey = Key;
            orderLines.Lines = OrderList;
            return orderLines;
        }

        [Fact]
        public void OrderHeaderNotFoundForIncorrectOrderNumber()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_List(fakeOrderList, "PO20007 - 01");
            Mock<OrderHeaderMapper> mockOrderDetailsMapper =
                new Mock<OrderHeaderMapper>();

            //Assert
            Assert.Throws<Exception>(() => new OrderHeaderReader(dataLines, mockOrderDetailsMapper.Object));
        }
        [Fact]
        public void OrderHeaderFoundForOrderNumber()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_List(fakeOrderList, "PO2008 - 01");
            Mock<OrderHeaderMapper> mockOrderDetailsMapper = new Mock<OrderHeaderMapper>();
            OrderHeaderReader sut = new OrderHeaderReader(dataLines, mockOrderDetailsMapper.Object);
            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public void GetMatchingHeaderForOrderNumber()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_List(fakeOrderList, "PO2008 - 01");
            Mock<OrderHeaderMapper> mockOrderHeaderMapper = new Mock<OrderHeaderMapper>();
            OrderHeaderReader sut = new OrderHeaderReader(dataLines, mockOrderHeaderMapper.Object);
            //Assert
            Assert.NotNull( sut.GetPurchaseOrder());
        }
        [Fact]
        public void MissingHeaderForOrderDetails()
        {
            //arrange
            List<string> OrderDetailsList = new List<string>();
            OrderDetailsList.Add("D,PO2008 - 01,1,RED LOUNGES,100,");
            OrderDetailsList.Add("D,PO2008 - 01,2,GREY LOUNGES,50,");
            OrderDataLines dataLines = SetupOrderDataLines_List(OrderDetailsList, "PO2008 - 01");

             Mock<OrderHeaderMapper> sut = new Mock<OrderHeaderMapper>();
            //Assert
            Assert.Throws<Exception>(() => new OrderHeaderReader(dataLines, sut.Object));
        }


    
    }
}
