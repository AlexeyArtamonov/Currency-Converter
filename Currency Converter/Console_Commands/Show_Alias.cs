using System;
using System.Linq;

namespace Currency_Converter.Console_Commands
{
    static partial class Commands
    {
        public static void Show_Alias()
        {
            string last_value = "";
            string temp;

            bool first = true;

            // Обход по значениям с игнорированием повторов
            foreach (var item in dictionary.OrderBy(a => a.Value))
            {
                dictionary.TryGetValue(item.Key, out temp);

                if (temp != last_value && last_value != "")
                {
                    first = true;
                    Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
                    Console.Write(" ");
                    Console.WriteLine();
                }

                if (first)
                {
                    Console.Write(temp + "\t\t");
                    Console.Write(item.Key + ", ");
                    first = false;
                }
                else
                {
                    Console.Write(item.Key + ", ");
                }

                last_value = temp;
            }
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
            Console.Write(" ");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
