using System.Collections.Generic;

namespace Currency_Converter.Consloe_Commands
{
    static partial class Commands
    {
        static List<string> Codes;
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

        };
        public static string Formalize(string word)
        {
            string temp;
            if (dictionary.TryGetValue(word.ToLower(), out temp))
                return temp;
            else
                return "Not Found";

        }
    }
}
