using System.IO;
using System;

namespace InfoLogs
{
    public static class Logger
    {
        private static readonly string _path = @"C:\Users\AkSharma\source\repos\CovidVaccineTracker\InfoLogs\Files\Logs.txt";
        
        public static void Main()
        {
            try
            {
                StreamWriter writer = new StreamWriter(_path, true);
                writer.WriteLine("Application started :" + DateTime.Now.ToString());
                writer.WriteLine();
                writer.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
