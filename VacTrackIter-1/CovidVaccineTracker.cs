using InfoLogs;
using System;

namespace CovidVaccineTracker
{
    internal class CovidVaccineTracker
    {
        private const string directionForMenu = "(Enter 'm' to Go back to Main menu)";
        private const string invalidInputAlert = "Invalid Input....";

        static void Main()
        {
            Organisation organisation = new Organisation("WatchGuard");
            Logger.Main();
            TemporaryEmployeeData.LoadData(organisation);
            //EmployeeData.AddTemporaryEmployees(organisation);   // To preload some data
            StartApp(organisation);
        }

        static void DisplayMenu()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Enter from the choices: ");
            Console.WriteLine("1: Add Employee");
            Console.WriteLine("2: Remove Employee");
            Console.WriteLine("3: Display All Employees And Vaccine Info");
            Console.WriteLine("4: Vaccinate an Employee");
            Console.WriteLine("5: Undo Employee Deletion");
            Console.WriteLine("6: Clear Console");
            Console.WriteLine("Enter 'q' to Exit from application");
        }

        static int GetIdByUser()
        {
            Console.WriteLine("Enter Employee ID: " + directionForMenu);
            string id = Console.ReadLine().ToUpper();
            if (id != "M")
            {
                try
                {
                    int empId = Convert.ToInt32(id);
                    return empId;
                }
                catch(Exception ex)
                {
                    InfoLogger newLog = new InfoLogger("Error", "Invalid Id Entered", ex.StackTrace);
                    InfoLogger.LogToFile(newLog.ToString());
                    Console.WriteLine(invalidInputAlert);
                    return 0;
                }
            }
            else
            {
                DisplayMenu();
                return -1; ;
            }
        }

        static void StartApp(Organisation organisation)
        {
            Console.Clear();
            while (true)
            {
                DisplayMenu();
                Console.WriteLine("Enter choice from above menu: ");
                string userChoice = Console.ReadLine().ToUpper();
                if (userChoice == "Q")
                {
                    Console.WriteLine("Enter 'Y' to quit or Enter any other value to continue");
                    string quitConfirm = Console.ReadLine().ToUpper();
                    if (quitConfirm == "Y")
                    {
                        InfoLogger newLog = new InfoLogger("Info","Application Closed");
                        InfoLogger.LogToFile(newLog.ToString());
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (userChoice == "1")
                {
                    while(true)
                    {
                        Console.WriteLine("Enter Name: " + directionForMenu);
                        string name = Console.ReadLine().ToUpper();
                        if (name == "M")
                        {
                            break;
                        }
                        else if (Validator.ValidateName(name))
                        {
                            string phone;
                            bool isRevertedToMenu = false;
                            string phoneNo;
                            while(true)
                            {
                                Console.WriteLine("Enter 10 digit phone No.: " + directionForMenu);
                                phoneNo = Console.ReadLine().ToUpper();
                                phone = Validator.ValidatePhoneNo(phoneNo);
                                if (phone == phoneNo + " - Invalid Phone No.")
                                {
                                    Console.WriteLine("Inalid Input. Please provide a 10 digit valid Phone No.: " + directionForMenu);
                                    continue;
                                }
                                else if(phone == "M")
                                {
                                    isRevertedToMenu = true;
                                    break;
                                }
                                else
                                {
                                    isRevertedToMenu = false;
                                    break;
                                }
                            }
                            if (isRevertedToMenu) break;
                            organisation.AddEmployee(name,phone);
                            Console.WriteLine("Press any key to continue to main menu...");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Name Input. Name should only have Alphabets, Whitespaces and numerics");
                            Console.WriteLine("Try Again: ");
                        }
                    }
                }
                else if (userChoice == "2")
                {
                    while (true)
                    {
                        organisation.ShowEmployeeSummary();
                        int id = GetIdByUser();
                        if (id == -1) break;
                        if (id != 0)
                        {
                            bool isSuccessfull = organisation.RemoveEmployee(id);
                            if (!isSuccessfull)
                            {
                                Console.WriteLine("Employee with this id is not present");
                                continue;
                            }
                            else break;
                        }
                    }
                    Console.WriteLine("Press any key to continue to main menu...");
                    Console.ReadKey();
                }
                else if (userChoice == "3")
                {
                    organisation.DisplayOrganisationVaccinationStatus();
                    Console.WriteLine("Press any key to continue to main menu...");
                    Console.ReadKey();
                }
                else if (userChoice == "4")
                {
                    while(true)
                    {
                        organisation.ShowEmployeeSummary();
                        int id = GetIdByUser();
                        if (id != 0)
                        {
                            Employee employee = organisation.employees.Find(emp => emp.id == id);
                            if (employee == null)
                            {
                                InfoLogger newInfo = new InfoLogger("Info", "Searched for non Existing Employee");
                                InfoLogger.LogToFile(newInfo.ToString());
                                Console.WriteLine("Invalid Input, Please try again...");
                                continue;
                            }
                            else
                            {
                                employee.VaccinateEmployee();
                                Console.WriteLine("Press any key to continue to main menu...");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else continue;
                    }
                }
                else if (userChoice == "5")
                {
                    organisation.UndoEmployeeDeletion();
                    Console.WriteLine("Press any key to continue to main menu...");
                    Console.ReadKey();
                    continue;
                }
                else if (userChoice == "6")
                {
                    Console.Clear();
                    continue;
                }
                else
                {
                    InfoLogger newInfo = new InfoLogger("Warning", "Invalid choice entered by user");
                    InfoLogger.LogToFile(newInfo.ToString());
                    Console.WriteLine("Invalid Input, Enter Choice again...");
                }
            }
        }
    }
}