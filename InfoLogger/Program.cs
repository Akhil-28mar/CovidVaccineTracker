

namespace InfoLogger
{
    public class Logger
    {
        public readonly string infoType;
        public readonly string message;
        public readonly string trace;
        private readonly string _path = @"C:\Users\AkSharma\source\repos\CovidVaccineTracker\InfoLogger\Store\Logs.txt";

        public Logger(string typeOfInfo, string mssg)
        {
            infoType = typeOfInfo;
            message = mssg;
            trace = " ";
        }
        public Logger(string infoType, string message, string stackTrace) : this(infoType, message)
        {
            trace = stackTrace;
        }

        public void WriteInfo(string error)
        {
            StreamWriter writer = new StreamWriter(_path, true);
            writer.WriteLine(error);
            writer.Close();
        }
    }
}