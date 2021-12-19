using System;

namespace Currency_Converter.Console_Commands
{
    static partial class Commands
    {
        public static void Help()
        {
            Console.WriteLine("convert -from \"Char_Code\" -to \"Char_Code\" [-amount] [-date] | Без кавычек, с квадратных скобках необязательные параметры");
            Console.WriteLine("quota \"Char_Code\" [-date] -- Курс валюты на определённую дату");
            Console.WriteLine("clear \t--\t Очистить консоль");
            Console.WriteLine("show \t--\t Список доступных валют");
            Console.WriteLine("showex \t--\t Расширенный список доступных валют");
            Console.WriteLine("alias \t--\t Показать cписок псевдонимов команд и аргументов");
            Console.WriteLine("help \t--\t Показать справку");
            Console.WriteLine("exit \t--\t Выйти");
            Console.WriteLine("Примеры: ");
            Console.WriteLine("convert -from USD -to RUB -amount 0.5 -date 10.05.2020");
            Console.WriteLine("c f usd t rub a 0,5 d 10/05/2020");
        }
    }
}
