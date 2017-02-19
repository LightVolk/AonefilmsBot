using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AonefilmsBot
{
    enum Severity
    {
        Debug,

        Message,

        Error
    }

    /// <summary>
    /// Класс обеспечивает протоколирование в журнал.
    /// </summary>
    static class Logger
    {
        private static string LogFileName = Environment.ExpandEnvironmentVariables(@"%ALLUSERSPROFILE%\AonefilmsBot\Logs\Logs.txt");

        public static void Initialize()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(LogFileName));
        }

        public static void Log(Severity severity, string message)
        {
            Console.WriteLine(message);

            var sb = new StringBuilder();

            sb.Append(DateTime.Now.ToString());

            sb.Append("\t");

            sb.Append(severity);

            sb.Append("\t");

            sb.Append(message);

            sb.Append("\r\n");

            File.AppendAllText(LogFileName, sb.ToString());
        }

        public static void LogMessage(string message)
        {
            Log(Severity.Message, message);
        }

        public static void LogDebug(string message)
        {
            Log(Severity.Debug, message);
        }

        public static void LogError(string message)
        {
            Log(Severity.Error, message);
        }
    }

}

