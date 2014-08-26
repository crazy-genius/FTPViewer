using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
// Добавлено в ручную
using System.Runtime.InteropServices; 

namespace FTPViewer
{
    internal static class NativeMethods
    {
        
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(int dwProcessId);
        [DllImport("kernel32.dll")]
        internal static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        internal static extern bool FreeConsole();
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
         //   new CommandInterpreter(new Commands()).Run(args);
            if (args.Length != 0)
            {
                if (HelpRequired(args[0]))
                {
                    NativeMethods.AttachConsole(-1);
                    DisplayHelp();
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FTPViewer());
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FTPViewer());
            }
         }
        private static void DisplayHelp()
        {
            Console.WriteLine("Привет {0}",Environment.UserName);
            Console.WriteLine("Пример использования: \\all \\start \\exit");
            Console.WriteLine("Для использования доступны следующие команнды");
            Console.WriteLine("\\help -- справка");
            Console.WriteLine("\\start -- запуск загрузки автоматически");
            Console.WriteLine("\\All -- Выбрать все доступные серверы");
            Console.WriteLine("\\help -- Автоматическое закрытие после выполнения всех загрузок");
            Console.WriteLine("Приятного дня");
            
            NativeMethods.FreeConsole();
        }
        private static bool HelpRequired(string param)
        {
            bool ret = false;
            switch (param)
            {
                case @"-h":
                    ret = true;
                    break;
                case @"--help":
                    ret = true;
                    break;
                case @"/?":
                    ret = true;
                    break;
                case @"?":
                    ret = true;
                    break;
                case @"\?":
                    ret = true;
                    break;
                case @"\help":
                    ret = true;
                    break;
            }
            return ret;
        }
     }
}

