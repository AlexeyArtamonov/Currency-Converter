using System;
using System.Linq;

namespace Currency_Converter.Console_Commands
{
    static partial class Commands
    {
        public static void Show(string input, int width = 5)
        { 
            string[] args = input.Split(' ');
            int position = Array.FindIndex(args, (string str) => Commands.Formalize(str) == "-date");

            if (position == args.Length - 1)
            {
                Console.WriteLine("У аргумента -date отсутствует значение");
                return;
            }
            string temp_date = args[position + 1];

            DateTime date = DateTime.Now;
            if (position >= 0 && !DateTime.TryParse(temp_date, out date))
            {
                Console.WriteLine("Агрумент -date имеет некоректнное значение");
                return;
            }

            if (Codes == null || Codes_date != date.Date)
            {
                Codes_date = date.Date;
                Codes = Currency_Converter.Converter.Get_Availible(date);
            }

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
            int i = 1;
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
