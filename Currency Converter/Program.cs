using System;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Currency_Converter.Consloe_Commands;


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
            if (args.Length == 0)
                Converter();
            switch (args[0])
            {
                case "--update":
                    Update();
                    break;
                default:
                    Converter();
                    break;
            }
        } 
        static void Converter()
        {
            string input;
            while (true)
            { 
                input = Console.ReadLine();
                input = Regex.Replace(input, @"\s+", " ");

                switch (Commands.Formalize(input.Split(' ')[0]))
                {
                    case "exit":
                        {
                            return;
                        }
                    case "show":
                        {
                            Commands.Show();
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
                            continue;
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
