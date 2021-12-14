using System;

namespace Currency_Converter.Consloe_Commands
{
    static partial class Commands
    {
        public static void Help()
        {
            Console.WriteLine("convert -from \"Char_Code\" -to \"Char_Code\" [-amount] [-date] | Без кавычек, с квадратных скобках необязательные параметры" );
            Console.WriteLine("quota \"Char_Code\" [-date] -- Курс валюты на определённую дату");
            Console.WriteLine("Clear \t--\t Очистить консоль");
            Console.WriteLine("Alias \t--\t Показать вписок псевдонимов команд и аргументов");
            Console.WriteLine("Help \t--\t Показать справку");
            Console.WriteLine("Exit \t--\t Выйти");
        }
    }
}
