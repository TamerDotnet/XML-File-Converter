using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Orders.Data
{
    public static class DataHelper
    {
        public static bool IsValidDate(this string data)
        {
            DateTime date = DateTime.Now;
            return DateTime.TryParse(data.Trim(), out date);
        }
        public static bool IsValidNumber(this string data)
        {
            int result = 0;
            return int.TryParse(data.Trim(), out result);
        }
        public static string GenerateCodeId(this string data)
        {
            if (data == null || data == "")
                return data;

            string code =string.Join("", data.Split(" ").Select(x=>x.Substring(0,1)).ToArray());
            if (code.Length > 0)
                code += "01";
            return code;
        }

        public static List<string> SplitDataArray(this List<string> Data)
        {
            if(Data.Count == 1 && Data[0].Contains(","))
               return Data[0].Split(",").ToList();

            return Data;
        }

    }
}
