using System.IO;
using System;

namespace InfoLogs
{
    public class InfoLogger
    {
        private readonly string type;
        private readonly string message;
        private readonly string stackTrace;
        private static readonly string _path = @"C:\Users\AkSharma\source\repos\CovidVaccineTracker\InfoLogs\Files\Logs.txt";

        public InfoLogger(string type, string message)
        {
            this.type = type;
            this.message = message;
        }
        public InfoLogger(string type, string message, string stackTrace)
            :this(type, message) 
        {
            this.stackTrace = stackTrace;
        }

        public override string ToString()
        {
            if(stackTrace != null) return DateTime.Now.ToString() + "\nLogger" + type + ": " + message + "\n\tStack Trace ==>" + stackTrace ;
            else return DateTime.Now.ToString() + "\nLogger" + type + ": " + message;
        }
        public static void LogToFile(string log)
        {
            try
            {
                StreamWriter writer = new StreamWriter(_path, true);
                writer.WriteLine(log);
                writer.WriteLine();
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
