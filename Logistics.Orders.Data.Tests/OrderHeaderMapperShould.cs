using Logistics.Orders.Data.FileReader.CSV.Mappers;
using Logistics.Orders.Domain;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Logistics.Orders.Data.Tests
{
    public class OrderHeaderMapperShould
    {

        public OrderDataLines SetupOrderDataLines_Single(string orderHeaderLine, string orderKey)
        {
            List<string> Lines = new List<string>();
            Lines.Add(orderHeaderLine);
            OrderDataLines order = new OrderDataLines();
            order.OrderKey = orderKey;
            order.Lines = Lines;
            return order;
        }
        [Fact]
        public void OrderHeaderHaveMissingSupplierName()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("H,PO2008-01,,CNSHA,AUMEL,1/05/2014", "PO2008-01");
            Mock<OrderHeaderMapper> sut = new Mock<OrderHeaderMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }
        [Fact]
        public void OrderHeaderHaveMissingOrigin()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("H,PO2008-01,SHANGHAI FURNITURE COMPANY,,AUMEL,1/05/2014", "PO2008-01");
            Mock<OrderHeaderMapper> sut = new Mock<OrderHeaderMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }

        [Fact]
        public void OrderHeaderHaveMissingDestination()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("H,PO2008-01,SHANGHAI FURNITURE COMPANY,CNSHA,,1/05/2014", "PO2008-01");
            Mock<OrderHeaderMapper> sut = new Mock<OrderHeaderMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }
        [Fact]
        public void OrderHeaderHaveMissingCargoReady()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("H,PO2008-01,SHANGHAI FURNITURE COMPANY,CNSHA,AUMEL,", "PO2008-01");
            Mock<OrderHeaderMapper> sut = new Mock<OrderHeaderMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }
        [Fact]
        public void OrderHeaderHaveIncorrectDateFormatForCargoReady()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("H,PO2008-01,SHANGHAI FURNITURE COMPANY,CNSHA,AUMEL,25/25/20", "PO2008-01");
            Mock<OrderHeaderMapper> sut = new Mock<OrderHeaderMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }
        [Fact]
        public void DoMappingForPurchaseOrderSuccessfully()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("H,PO2008-01,SHANGHAI FURNITURE COMPANY,CNSHA,AUMEL,01/02/2014", "PO2008-01");
            Mock<OrderHeaderMapper> sut = new Mock<OrderHeaderMapper>();

            //Assert
            Assert.NotNull(sut.Object.Map(dataLines));
        }
    }
}
