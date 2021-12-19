using System;

namespace Currency_Converter.Console_Commands
{
    static partial class  Commands
    {
        public static void ShowEx(string input)
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

            if (CodesEx == null || CodesEx_date != date.Date)
            {
                CodesEx_date = date.Date;
                CodesEx = Converter.Get_Availible_Ex(date);
            }

            foreach( var item in CodesEx)
            {
                Console.WriteLine($"{item.Item1} ---  {item.Item2}");
            }
        }
    }
}
