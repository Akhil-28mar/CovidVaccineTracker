using InfoLogs;
using System;
using System.Collections.Generic;

namespace CovidVaccineTracker
{
    class Organisation
    {
        private readonly string _name;
        internal readonly List<Employee> employees = new List<Employee>();
        private readonly List<Employee> deletedEmployees = new List<Employee>();
        private int undoCapacity = 3;

        internal Organisation(string name)
        {
            _name = name;
        }

        internal void AddEmployee(string name, string phone)
        {
            employees.Add(new Employee(name,phone));
            Console.WriteLine("Employee {0}, Id: {1} Added", name, employees[employees.Count - 1].id);
            InfoLogger newInfo = new InfoLogger("Info", "New Employee Added");
            InfoLogger.LogToFile(newInfo.ToString());
        }
        internal bool RemoveEmployee(int id)
        {
            bool isSuccessful = false;
            foreach(Employee emp in employees)
            {
                if(emp.id == id)
                {
                    isSuccessful = true;
                    if (undoCapacity > 0)
                    {
                        deletedEmployees.Add(emp);
                        undoCapacity--;
                    }
                    else
                    {
                        deletedEmployees.RemoveAt(0);
                        deletedEmployees.Add(emp);
                    }
                    Console.WriteLine("Employee {0}, Id: {1} Removed", emp.name, emp.id);
                    InfoLogger newInfo = new InfoLogger("Info", "Employee Removed");
                    InfoLogger.LogToFile(newInfo.ToString());
                    employees.Remove(emp);
                    break;
                }
            }
            return isSuccessful;
        }
        internal void UndoEmployeeDeletion()
        {
            if(deletedEmployees.Count == 0)
            {
                Console.WriteLine("No employee can be restored.");
                return;
            }
            employees.Add(deletedEmployees[deletedEmployees.Count-1]);
            InfoLogger newInfo = new InfoLogger("Info", "Employee data Restored");
            InfoLogger.LogToFile(newInfo.ToString());
            deletedEmployees.RemoveAt(deletedEmployees.Count- 1);
            undoCapacity++;
            Console.WriteLine("Employee {0} has been restored into the record.", employees[employees.Count - 1].name);
        }
        internal void DisplayOrganisationVaccinationStatus()
        {
            Console.WriteLine("\n\t"+_name+"'s Vaccination Status\n");
            
            if(employees.Count == 0)
            {
                Console.WriteLine("\t\tNo employees in the Organisation...");
                return;
            }
            employees.Sort();
            foreach(Employee emp in employees)
            {
                emp.DisplayInfo();
                Console.WriteLine();
            }
        }
        internal void ShowEmployeeSummary()
        {
            foreach(Employee emp in employees)
            {
                Console.WriteLine("\t\tName: {0}\n\t\tId: {1}", emp.name , emp.id);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
