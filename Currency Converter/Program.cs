using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace Currency_Converter
{
    class Program
    {
        // Основная программа
        // Отвечатает только за взаимодействие с пользователем

        static void Main(string[] args)
        {
            DBWorker dBWorker = new DBWorker("config.cfg");
            var d = dBWorker.GetDataFromDB(DateTime.Now, "USD");
            var e = dBWorker.GetDataFromDB(DateTime.Now, "EUR");
            Console.WriteLine(Converter.Convert(d.Item1 * d.Item2, e.Item1 * e.Item2, 1));
            Console.Read();
        }
    }
}
