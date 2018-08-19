using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Logistics.Orders.Data.FileReader.CSV;
using Logistics.Orders.Data.FileReader;

using System.IO;
using Logistics.Orders.Data.FileWriter;

namespace Logistics.Orders.Data.Tests
{
   public class CSVDataRepositoryShould
    {
        private CSVDataRepository SetupCSVRepository()
        {
            Mock<IFileReader> mockFileReader = new Mock<IFileReader>(MockBehavior.Loose);
            Mock<IDataWriter> mockFileWriter = new Mock<IDataWriter>(MockBehavior.Loose);
            Mock<IDataManager> mockDataManager = new Mock<IDataManager>(MockBehavior.Loose);

            var sut = new CSVDataRepository(mockFileReader.Object, mockFileWriter.Object, mockDataManager.Object);
            return sut;
        }
        [Fact]
        public void FileNotProvidedException()
        {
            var sut = SetupCSVRepository();
            Assert.Throws<FileNotFoundException>(() => sut.GetPurchaseOrders());
        }
        //[Fact]
        //public void FileProvided()
        //{
        //    //Arrange
        //    var sut = SetupCSVRepository();

        //    Assert.Equal(4, sut.GetPurchaseOrders().Count);
        //}

    }
}
