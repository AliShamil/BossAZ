using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossAZ.Helper
{
    internal class Vacancy
    {

        public string Profession { get; set; }

        public (byte min, byte max) AgeLimit { get; set; }

        public double Salary { get; set; }

        public List<string> RequiredKnowledge { get; set; }


        public Vacancy(string profession, (byte min, byte max) ageLimit, double salary, List<string> requiredKnowledge)
        {
            Profession=profession;
            AgeLimit=ageLimit;
            Salary=salary;
            RequiredKnowledge=requiredKnowledge;
        }


        public override string ToString()
        {
            Console.WriteLine($@"Profession: {Profession}");

            Console.WriteLine("\n ~ Required Knowledge ~ ");
            foreach (var rk in RequiredKnowledge!)
                Console.Write($"{rk} \n");
            Console.WriteLine();

            return $@"Age Limit: {AgeLimit.min} - {AgeLimit.max}
Salary: {Salary}
";
        }

    }
}
