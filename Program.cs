using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileInfo
{
    class Program
    {
        /// <summary>
        /// Константы-Флаги
        /// </summary>
        const int No_File = -1;
        const int File_Not_Found = -2;

        const string help = "Информация о файле \r\n";

        static string fName = "";

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(GetHelp());
                return;
            }
            if (args[0] == "-?" || args[0] == "/?")
            {
                Console.WriteLine(GetHelp());
                return;
            }

            fName = args[0];
            if (File.Exists(fName) == false)
            {
                Console.WriteLine("Error: {0}", File_Not_Found);
                return;
            }
            if (args.Length < 2) return;
            string info = "";
            switch (args[1])
            {
                case "-?":
                case "/?":
                    Console.WriteLine(GetHelp());
                    return;
                   // break;
                case "-fc":
                case "/fc":
                    info = GetFileInfo(0);
                    Console.WriteLine(info);
                    break;
                case "-la":
                case "/la":
                    info = GetFileInfo(1);
                    Console.WriteLine(info);
                    break;
                case "-lw":
                case "/lw":
                    info = GetFileInfo(2);
                    Console.WriteLine(info);
                    break;
                default:
                    break;
            }
        }

        private static string GetFileInfo(int flag)
        {
            DateTime time;
            string result = "";
            try
            {
                switch (flag)
                {
                    case 0:
                        time = File.GetCreationTime(fName);
                        break;
                    case 1:
                        time = File.GetLastAccessTime(fName);
                        break;
                    case 2:
                        time = File.GetLastWriteTime(fName);
                        break;
                    default:
                        time = File.GetCreationTime(fName);
                        break;
                }
                result = String.Format("{0}", time);
            }
            catch (Exception ex)
            {
                result = String.Format("Error: {0}", ex.Message);
            }

            return result; 
        }

        static string GetHelp()
        {
            StringBuilder sb = new StringBuilder(help);
            sb.AppendLine("Формат вызова:");
            sb.AppendFormat("{0} имя_файла [/параметр]\r\n", AppDomain.CurrentDomain.FriendlyName);
            sb.AppendLine("Параметры:");
            sb.AppendLine("/? -  Эта справка");
            sb.AppendLine("/fc - Время создания указанного файла");
            sb.AppendLine("/la - Время последнего доступа к файлу");
            sb.AppendLine("/lw - Время последнего изменения файла");

            return sb.ToString();
        }

    }
}
