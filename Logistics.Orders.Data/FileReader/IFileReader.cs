using Logistics.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Data.FileReader
{
    public interface IFileReader
    { 
        bool FileExists();
        List<string> ReadAllLines();
    }
}
