
namespace CovidVaccineTracker
{
    internal class EmployeeData
    {
        static internal void AddTemporaryEmployees(Organisation org)
        {
            org.employees.Add(new Employee("Arpit", "1234567890"));
            org.employees.Add(new Employee("Himanshu", "1234567890"));
            org.employees.Add(new Employee("Hitesh", "7987441045"));
            org.employees.Add(new Employee("Garv", "7412058963"));
            org.employees.Add(new Employee("Vaibhav", "1230456879"));
            org.employees.Add(new Employee("Gaurav", "2547896321"));
        }
    }
}
