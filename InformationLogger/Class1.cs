
namespace InformationLogger
{
    public static class Logger
    {
        private static readonly string _path = @"C:\Users\AkSharma\source\repos\CovidVaccineTracker\InformationLogger\Logs.txt";
        public static void LogInfo(string info, object obj)
        {
            StreamWriter writer = new StreamWriter(_path, true);
            writer.WriteLine("Logger.Info ==> " + info);
            writer.WriteLine("\t\t" + obj.GetType().ToString());
            writer.WriteLine();
            writer.Close();
        }
        public static void LogWarning(string warning, object obj)
        {
            StreamWriter writer = new StreamWriter(_path, true);
            writer.WriteLine("Logger.Warning ==> " + warning);
            writer.WriteLine("\t\t" + obj.GetType().ToString());
            writer.WriteLine();
            writer.Close();
        }
        public static void LogError(string error, Exception exception)
        {
            StreamWriter writer = new StreamWriter(_path, true);
            writer.WriteLine("Logger.Error ==> " + error);
            writer.WriteLine("\t\t" + exception.StackTrace);
            writer.WriteLine();
            writer.Close();
        }
    }
}
