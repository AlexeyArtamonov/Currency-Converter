using System;

namespace Currency_Converter.Console_Commands
{
    static partial class Commands
    {
       public static void Convert(string input)
        {
            string[] args = input.Split(' ');
            int position;

            position = Array.FindIndex(args, (string str) => Formalize(str) == "-from");
            if (position < 0)
            {
                Console.WriteLine("Агрумент -from отсутствует");
                return;
            }
            if (position == args.Length - 1)
            {
                Console.WriteLine("У аргумента -from отсутствует значение");
                return;
            }
            string From = args[position + 1].ToUpper();
            if (From.Length != 3)
            {
                Console.WriteLine("Агрумент -from имеет некоректнное значение");
                return;
            }

            position = Array.FindIndex(args, (string str) => Formalize(str) == "-to");
            if (position < 0 || position == args.Length -1)
            {
                Console.WriteLine("Агрумент -to отсутствует");
                return;
            }
            if (position == args.Length - 1)
            {
                Console.WriteLine("У аргумента -from отсутствует значение");
                return;
            }
            string To = args[position + 1].ToUpper();
            if (To.Length != 3)
            {
                Console.WriteLine("Агрумент -to имеет некоректнное значение");
                return;
            }

            position = Array.FindIndex(args, (string str) => Formalize(str) == "-amount");
            if (position == args.Length - 1)
            {
                Console.WriteLine("У аргумента -amount отсутствует значение");
                return;
            }
            string temp_amount = args[position + 1];
            double amount = 1;
            if (position >= 0 && !Double.TryParse(temp_amount.Replace('.', ','), out amount))
            {
                Console.WriteLine("Агрумент -amount имеет некоректнное значение");
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

            double temp_result = Currency_Converter.Converter.Convert(From, To, date, amount);
            if (temp_result == 0 && amount != 0)
            {
                Console.WriteLine("Данные не найденны");
                return;
            }
            string result = $"{amount} {From} = {temp_result} {To} на {date.ToShortDateString()}";

            Print_in_a_border(result);
            Console.WriteLine();
        }
    }
}
