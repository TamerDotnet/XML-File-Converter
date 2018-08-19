using Logistics.Orders.Data.FileReader.CSV.Mappers;
using Logistics.Orders.Domain;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Logistics.Orders.Data.Tests
{
    public class OrderDetailsMapperShould
    {
        public OrderDataLines SetupOrderDataLines_Single(string orderDetailsLine, string orderKey)
        {
            List<string> Lines = new List<string>();
            Lines.Add(orderDetailsLine);
            OrderDataLines order = new OrderDataLines();
            order.OrderKey = orderKey;
            order.Lines = Lines;
            return order;
        }
        [Fact]
        public void OrderDetailsHaveMissingOrderNumber()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("D,,1,RED LOUNGES,100,", "PO2008-01");
            Mock<OrderDetailsMapper> sut = new Mock<OrderDetailsMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }

        [Fact]
        public void OrderDetailsHaveMissingLineNumber()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("D,PO2008-01,,RED LOUNGES,100,", "PO2008-01");
            Mock<OrderDetailsMapper> sut = new Mock<OrderDetailsMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }

        [Fact]
        public void OrderDetailsHaveMissingProductDescription()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("D,PO2008-01,1,,100,", "PO2008-01");
            Mock<OrderDetailsMapper> sut = new Mock<OrderDetailsMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }
        [Fact]
        public void OrderDetailsHaveMissingOrderQty()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("D,PO2008-01,1,RED LOUNGES,,", "PO2008-01");
            Mock<OrderDetailsMapper> sut = new Mock<OrderDetailsMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }
        [Fact]
        public void OrderDetailsHaveIncorrectDataFormatForOrderQty()
        {
            //Arrange
            OrderDataLines dataLines = SetupOrderDataLines_Single("D,PO2008-01,1,RED LOUNGES,ABC,", "PO2008-01");
            Mock<OrderDetailsMapper> sut = new Mock<OrderDetailsMapper>();

            //Assert
            Assert.Throws<Exception>(() => sut.Object.Map(dataLines));
        }
    }
}
