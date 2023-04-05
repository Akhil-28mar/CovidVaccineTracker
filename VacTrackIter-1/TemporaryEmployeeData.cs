using Excel = Microsoft.Office.Interop.Excel;
using System;
using InfoLogs;
using System.Runtime.InteropServices;

namespace CovidVaccineTracker
{
    internal class TemporaryEmployeeData
    {
        public static void LoadData(Organisation org)
        {
            Excel.Application application = new Excel.Application();
            try
            {
                Excel.Workbook workbook = application.Workbooks.Open(@"C:\Users\AkSharma\Desktop\EmployeeData22.xlsx");
                Excel._Worksheet worksheet = (Excel.Worksheet)application.ActiveSheet;
                Excel.Range usedRange = worksheet.UsedRange;
                int rows = usedRange.Rows.Count;
                int coloumns = usedRange.Columns.Count;
                string name;
                string phoneNo;

                InfoLogger newInfo = new InfoLogger("Info" , "Connection with Excel File Established");
                InfoLogger.LogToFile(newInfo.ToString());
                for (int i = 2; i <= rows; i++)
                {
                    if (worksheet.Cells[i, "A"].Value == null)
                    {
                        InfoLogger info = new InfoLogger("Error", "Null Name read from Excel File", new NullReferenceException().StackTrace);
                        InfoLogger.LogToFile(info.ToString());
                        name = "Name Not Found";
                    }
                    else
                    {
                        name = worksheet.Cells[i, "A"].Value.ToString();
                        if (Validator.ValidateName(name) == false)
                        {
                            name += " This Name is inaccurate";
                        }
                    }

                    if (worksheet.Cells[i, "B"].Value == null)
                    {
                        InfoLogger info = new InfoLogger("Error", "Null Phone no. read from Excel File", new NullReferenceException().StackTrace);
                        InfoLogger.LogToFile(info.ToString());
                        phoneNo = "No Phone No. Found";
                    }
                    else
                    {
                        phoneNo = worksheet.Cells[i, "B"].Value.ToString();
                        phoneNo = Validator.ValidatePhoneNo(phoneNo);
                    }
                    
                    if (name == "Name Not Found" && phoneNo == "No Phone No. Found")
                    {
                        continue;
                    }
                    else
                    {
                        org.employees.Add(new Employee(name, phoneNo));
                        InfoLogger info = new InfoLogger("Info", "New Employee Added from Excel");
                        InfoLogger.LogToFile(info.ToString());
                    }
                }
                Marshal.ReleaseComObject(worksheet);
                workbook.Close(0);
                Marshal.ReleaseComObject(workbook);
                application.Quit();
                Marshal.ReleaseComObject(application);
            }
            catch (Exception ex)
            {
                InfoLogger info = new InfoLogger("Error", "Temporary DataFile is unavailable or crashed", ex.StackTrace);
                InfoLogger.LogToFile(info.ToString());
                application.Quit();
                Marshal.ReleaseComObject(application);
                return;
            }
            finally
            {
                InfoLogger info = new InfoLogger("Info", "Excel Application Closed");
                InfoLogger.LogToFile(info.ToString());
            }
        }
    }
}
