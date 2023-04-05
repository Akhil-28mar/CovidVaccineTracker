using System;
using System.Collections.Generic;
using InfoLogs;

namespace CovidVaccineTracker
{
    class Employee: IComparable<Employee>
    {
        private static int _noOfEmployeesHired = 0;
        private DateTime lastVaccineDate;
        internal readonly int id;
        internal readonly string name;
        internal readonly string phone;

        internal Employee(string Name, string phone)
        {
            _noOfEmployeesHired++;
            id = _noOfEmployeesHired;
            name = Name;
            this.phone = phone;
        }

        public int CompareTo(Employee other)
        {
            if (id < other.id) return -1;
            else if (id > other.id) return 1;
            else return 0;
        }
        internal void VaccinateEmployee()
        {
            int monthsToNextDose = Math.Abs((lastVaccineDate.Month - DateTime.Now.Month) + 12 * (lastVaccineDate.Year - DateTime.Now.Year));
            if (monthsToNextDose >= 6)
            {
                lastVaccineDate = DateTime.Now;
                Vaccine tempVac = new Vaccine("CoviShield");
                EmployeeToVaccineMap.vaccines.Add(tempVac);
                Console.WriteLine("Employee {0}, Id: {1}, vaccinated on {2} by vaccine: {3}", name, id, DateTime.Now.ToString("dd/MM/yyyy"), "CoviShield");
                EmployeeToVaccineMap.UpdateMapping(id, tempVac.id);
                InfoLogger newInfo = new InfoLogger("Info","Employee Vaccinated");
                InfoLogger.LogToFile(newInfo.ToString());
            }
            else
            {
                InfoLogger newInfo = new InfoLogger("Warning", "Employee not Eligible for vaccination");
                InfoLogger.LogToFile(newInfo.ToString());
                Console.WriteLine("Employee not Eligible for vaccination...(Dose must be taken post 6 months of previous dosage)");
                Console.WriteLine("Employee can Take Next Dose after: "+ lastVaccineDate.AddMonths(6).ToString("dd/MM/yyyy"));
            }
        }

        internal void DisplayInfo()
        {
            Console.WriteLine("\t\tEmployee ID: " + id);
            Console.WriteLine("\t\tEmployee Name: " + name);
            Console.WriteLine("\t\tPhone Number: "+ phone);
            List<int> vaccineIdsForEmployee = EmployeeToVaccineMap.GetEmployeeVaccineIds(id);
            if(vaccineIdsForEmployee.Count == 0)
            {
                Console.WriteLine("\t\tEmployee Not vaccinated");
                return;
            }
            foreach(Vaccine v in EmployeeToVaccineMap.vaccines)
            {
                if(vaccineIdsForEmployee.Contains(v.id))
                {
                    v.DisplayVaccineInfo();
                }
            }
        }
    }
}
