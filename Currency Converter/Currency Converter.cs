using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency_Converter
{
    static class Converter
    {
        public static double Convert(double From_normalized_value, double To_normailez_value, double amount)
        {
            return amount * (From_normalized_value / To_normailez_value);
        }
    }
}
