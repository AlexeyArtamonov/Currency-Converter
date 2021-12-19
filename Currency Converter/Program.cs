using System;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Currency_Converter.Console_Commands;

namespace Currency_Converter
{
    class Window
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static void Show()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, 5);
        }
        public static void Hide()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, 0);
        }
         
    }
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] != "--update")
            {
                Converter();
            }
            else
            {
                Update();
            }
        } 
        static void Converter()
        {
            Console.WriteLine("\t\t== Конвертер валют ==");
            Console.WriteLine("\tВведите help для получения списка комманд");

            string input;

            Console.OutputEncoding = System.Text.Encoding.Unicode;

            while (true)
            {
                input = Console.ReadLine();
                Console.WriteLine(new String('\u203E', input.Length)); // '-';
                input = Regex.Replace(input, @"\s+", " ");

                switch (Commands.Formalize(input.Split(' ')[0]))
                {
                    case "exit":
                        {
                            return;
                        }
                    case "show":
                        {
                            Commands.Show(input);
                            break;
                        }
                    case "showex":
                        {
                            Commands.ShowEx(input);
                            break;
                        }
                    case "convert":
                        {
                            Commands.Convert(input);
                            break;
                        }
                    case "alias":
                        {
                            Commands.Show_Alias();
                            break;
                        }
                    case "quota":
                        {
                            Commands.Quota(input);
                            break;
                        }
                    case "clear":
                        {
                            Console.Clear();
                            Console.WriteLine("\t\t== Конвертер валют ==");
                            break;
                        }
                    case "help":
                        {
                            Commands.Help();
                            break;
                        }
                    default:
                        {
                            Console.Write("Неизвестная команда");
                            break;
                        }
                }
                Console.WriteLine();
            }
        }
        static void Update()
        {
            Window.Hide();
            DBWorker db = new DBWorker("config.cfg");
            db.Update();
        } 
    }
}
