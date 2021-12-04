using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Linq;
using System.Collections.Generic;

namespace Currency_Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Converter.Convert("EUR", "USD", DateTime.Now, 1));

            string input;
            while (true)
            {
                Console.WriteLine();
                input = Console.ReadLine();

                switch (Formalize(input))
                {
                    case "exit":
                        {
                            return;
                        }
                    case "show":
                        {
                            Show();
                            break;
                        }
                    default:
                        {
                            Console.Write("Неизвестная команда");
                            break;
                        }
                }
            }
        } 
        static SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>()
        {
            {"quit","exit" },
            {"exit","exit" },
            {"ex", "exit" },
            {"q", "exit" },
            {"show", "show"}
        };
        static List<string> Codes;
        static string Formalize(string word)
        {
            string temp;
            if (dictionary.TryGetValue(word.ToLower(), out temp))
                return temp;
            else
                return "Not Found";
            
        }
        static void Show(int width = 5)
        {
            if (Codes == null)
                Codes = Converter.Get_Availible();

            int i = 1;
            string msg = "Доступные валюты: ";
            int wide = width * 3 + 5 * (width - 1) + 1;

            // Вывод сообщения по центру
            for (int j = 0; j < (wide - msg.Length) / 2; j++)
                Console.Write(" ");
            Console.Write(msg + "\n");

            // Вывод рамки
            Console.Write("|");
            for (int j = 1; j < wide; j++) 
                Console.Write('-');
            Console.Write("|\n");

            // Вывод данных
            foreach (string item in Codes)
            {
                Console.Write(" " + item + "\t");
                if (i++ % width == 0)
                    Console.Write('\n');
            }
            Console.WriteLine();
        }
        
        
    }
}
