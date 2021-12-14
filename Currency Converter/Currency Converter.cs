using System;
using System.Collections.Generic;

namespace Currency_Converter
{
    static class Converter
    {
        private static DBWorker dB = new DBWorker("config.cfg");
        public static double Convert(string From, string To, DateTime OnDate, double Amount)
        {
            var data = dB.GetDataFromDB(OnDate, From, To);
            return Amount * (data.Item1 / data.Item2);
        }
        public static List<string> Get_Availible()
        {
            return dB.GetAllCodes();
        }
    }
}
