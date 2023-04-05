using System;

namespace CovidVaccineTracker
{
    class Vaccine
    {
        private static int _noOfVaccines=0;
        private readonly string _brand;
        private readonly string _dateOfIntake;
        internal readonly int id;

        internal Vaccine(string brand)
        {
            _noOfVaccines++;
            id = _noOfVaccines;
            _brand= brand;
            _dateOfIntake = DateTime.Now.ToString("dd/MM/yy");
        }

        internal void DisplayVaccineInfo()
        {
            Console.WriteLine("\t\tVaccine Name: " + _brand);
            Console.WriteLine("\t\tDate of Intake: " + _dateOfIntake);
        }
    }
}
