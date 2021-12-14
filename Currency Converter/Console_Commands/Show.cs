using System;
using System.Linq;

namespace Currency_Converter.Consloe_Commands
{
    static partial class Commands
    {
        public static void Show(int width = 5)
        {
            if (Codes == null)
                Codes = Currency_Converter.Converter.Get_Availible();

            int i = 1;
            string msg = "== Доступные валюты == ";
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
            foreach (string item in Codes.OrderBy(x => x))
            {
                Console.Write(" " + item + "\t");
                if (i++ % width == 0)
                    Console.Write('\n');
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
