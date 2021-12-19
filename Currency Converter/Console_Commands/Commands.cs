using System.Collections.Generic;
using System;
namespace Currency_Converter.Console_Commands
{
    static partial class Commands
    {
        static List<string> Codes;
        static DateTime Codes_date;

        static List<(string, string)> CodesEx;
        static DateTime CodesEx_date;

        static Dictionary<string, string> dictionary = new Dictionary<string, string>()
        {
            {"quit",    "exit" },
            {"exit",    "exit" },
            {"ex",      "exit" },
            {"q",       "exit" },


            {"show",    "show"},
            {"s",       "show" },

            {"convert", "convert"},
            {"c",       "convert"},

            {"-to",     "-to" },
            {"-t",      "-to" },
            {"to",      "-to" },
            {"t",       "-to" },

            {"-from",   "-from" },
            {"-f",      "-from" },
            {"from",    "-from" },
            {"f",       "-from" },

            {"-amount", "-amount" },
            {"-a",      "-amount" },
            {"amount",  "-amount" },
            {"a",       "-amount" },

            {"-date",   "-date" },
            {"-d",      "-date" },
            {"date",    "-date" },
            {"d",       "-date" },

            {"alias",   "alias" },

            {"quota",   "quota" },

            {"help",    "help" },

            {"clear",   "clear" },
            {"cl",   "clear" },


            {"showex",  "showex" },
            {"sx",      "showex" },
        };
        public static string Formalize(string word)
        {
            string temp;
            if (dictionary.TryGetValue(word.ToLower(), out temp))
                return temp;
            else
                return "Not Found";

        }
        public static void Print_in_a_border(string str)
        {
            Console.Write('+');
            for (int i = 1; i < str.Length + 1; i++)
            {
                Console.Write('-');
            }
            Console.Write("+\n");

            Console.Write('|');
            Console.Write(str);
            Console.Write("|\n");

            Console.Write('+');
            for (int i = 1; i < str.Length + 1; i++)
            {
                Console.Write('-');
            }
            Console.Write('+');
        }
    }
}
