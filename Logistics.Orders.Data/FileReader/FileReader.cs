using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Logistics.Orders.Domain;

namespace Logistics.Orders.Data.FileReader
{
    public class FileReader: IFileReader
    {
        private string _filePath; 
        public FileReader( string filePath)
        { 
            this._filePath = filePath;
        }
       public  bool FileExists()
        {
            return File.Exists(_filePath);
        }

        public List<string> ReadAllLines()
        {
            return File.ReadAllLines(_filePath)
                       .ToList();
        }
    }
}
