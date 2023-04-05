using System;
using System.Text.RegularExpressions;
using InfoLogs;

namespace CovidVaccineTracker
{
    internal class Validator
    {
        internal static bool ValidateName(string name)
        {
            if (name == null)
            {
                InfoLogger newInfo = new InfoLogger("Error", "Null name Entered", new NullReferenceException().StackTrace);
                InfoLogger.LogToFile(newInfo.ToString());
                return false;
            }
            if (name.Length == 0)
            {
                InfoLogger newInfo = new InfoLogger("Warning", "Invalid name Entered");
                InfoLogger.LogToFile(newInfo.ToString());
                return false;
            }
            if (name[0] == ' ')
            {
                InfoLogger newInfo = new InfoLogger("Warning", "Invalid name Entered");
                InfoLogger.LogToFile(newInfo.ToString());
                return false;
            }
            if(!Regex.IsMatch(name, @"^[a-zA-Z][a-zA-Z]*(?: [a-zA-Z][a-zA-Z]*)*$"))
            {
                InfoLogger newInfo = new InfoLogger("Warning", "Invalid name Entered");
                InfoLogger.LogToFile(newInfo.ToString());
                return false;
            }
            return true;
        }
        internal static string ValidatePhoneNo(string phoneNo)
        {
            if (phoneNo == null)
            {
                InfoLogger newInfo = new InfoLogger("Error", "Null Phone No. Entered", new NullReferenceException().StackTrace);
                InfoLogger.LogToFile(newInfo.ToString());
                return "No Phone No. Found";
            }
            else if (phoneNo == "M" || phoneNo == "m")
            {
                InfoLogger newInfo = new InfoLogger("Warning", "Invalid Phone No. Entered");
                InfoLogger.LogToFile(newInfo.ToString());
                return "M";
            }
            else if (phoneNo.Length != 10)
            {
                InfoLogger newInfo = new InfoLogger("Warning", "Invalid Phone No. Entered");
                InfoLogger.LogToFile(newInfo.ToString());
                return phoneNo + " - Invalid Phone No.";
            }
            else
            {
                if (long.TryParse(phoneNo, out long result)) return phoneNo;
                else
                {
                    InfoLogger newInfo = new InfoLogger("Warning", "Invalid Phone No. Entered");
                    InfoLogger.LogToFile(newInfo.ToString());
                    return phoneNo + " - Invalid Phone No.";
                }
            }
        }
    }
}
