using System;

namespace Currency_Converter.Console_Commands
{
    static partial class Commands
    {
        public static void Quota(string input)
        {
            string[] args = input.Split(' ');
            int position;

            if (args.Length < 2)
            {
                Console.WriteLine("Отсутсвует код валюты");
                return;
            }

            string from = args[1].ToUpper();
            if (from.Length != 3)
            {
                Console.WriteLine("Некоректная валюта");
                return;
            }

            position = Array.FindIndex(args, (string str) => Formalize(str) == "-date");
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

            double result = Currency_Converter.Converter.Convert(from, "RUB", date, 1);
            if (result == 0)
            {
                Console.WriteLine("Данные не найденны");
                return;
            }

            Print_in_a_border($"Курс {from}: {result} RUB на {date.ToShortDateString()}");
            Console.WriteLine();
        }
    }
}
