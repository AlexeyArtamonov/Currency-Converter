using System;

namespace Currency_Converter.Consloe_Commands
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
            string From = args[position + 1].ToUpper();
            if (From.Length != 3)
            {
                Console.WriteLine("Агрумент -from имеет некоректнное значение");
                return;
            }

            position = Array.FindIndex(args, (string str) => Formalize(str) == "-to");
            if (position < 0)
            {
                Console.WriteLine("Агрумент -to отсутствует");
                return;
            }
            string To = args[position + 1].ToUpper();
            if (To.Length != 3)
            {
                Console.WriteLine("Агрумент -to имеет некоректнное значение");
                return;
            }

            position = Array.FindIndex(args, (string str) => Formalize(str) == "-amount");
            string temp_amount = args[position + 1];
            double amount = 1;

            if (position >= 0 && !Double.TryParse(temp_amount.Replace('.', ','), out amount))
            {
                Console.WriteLine("Агрумент -amount имеет некоректнное значение");
                return;
            }

            position = Array.FindIndex(args, (string str) => Formalize(str) == "-date");
            string temp_date = args[position + 1];
            DateTime date = DateTime.Now;

            if (position >= 0 && !DateTime.TryParse(temp_date, out date))
            {
                Console.WriteLine("Агрумент -date имеет некоректнное значение");
                return;
            }
            double temp_result = Currency_Converter.Converter.Convert(From, To, date, amount);
            if (temp_result == 0)
            {
                Console.WriteLine("Данные не найденны");
                return;
            }
            string result = $"{amount} {From} = {temp_result} {To} на {date.ToShortDateString()}";

            Console.Write('+');
            for (int i = 1; i < result.Length + 1; i++)
            {
                Console.Write('-');
            }
            Console.Write("+\n");

            Console.Write('|');
            Console.Write(result);
            Console.Write("|\n");

            Console.Write('+');
            for (int i = 1; i < result.Length + 1; i++)
            {
                Console.Write('-');
            }
            Console.Write('+');
            Console.WriteLine();
        }
    }
}
