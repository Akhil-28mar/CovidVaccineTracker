using System.Collections.Generic;

namespace CovidVaccineTracker
{
    static class EmployeeToVaccineMap
    {
        private readonly static Dictionary<int,List<int>> employeeVaccineMap = new Dictionary<int, List<int>>();
        internal readonly static List<Vaccine> vaccines = new List<Vaccine>();

        internal static void UpdateMapping(int empId , int vaccineId)
        {
            if(!employeeVaccineMap.ContainsKey(empId))
            {
                employeeVaccineMap[empId] = new List<int>(vaccineId);
            }
            employeeVaccineMap[empId].Add(vaccineId);
        }

        internal static List<int> GetEmployeeVaccineIds(int empId) 
        { 
            List<int> vaccIds = new List<int>();
            if (employeeVaccineMap.ContainsKey(empId))
            {
                foreach (int i in employeeVaccineMap[empId])
                {
                    vaccIds.Add(i);
                }
            }
            return vaccIds;
        }
    }
}
