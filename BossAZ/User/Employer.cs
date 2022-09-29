using BossAZ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossAZ.User
{
    internal class Employer : User
    {
        public List<Vacancy> Vacancies { get; set; }

        public Employer(string? name, string? surname, Azerbaijan city, string? phone, byte age) 
            : base(name, surname, city, phone, age)
        {
            Vacancies = new List<Vacancy>();
        }

        public override string ToString()
        {
           
            Console.WriteLine($"{base.ToString()}");

            if(Vacancies.Count != 0)
            {
            Console.WriteLine("\n ~ Vacancies ~ ");
            foreach (var v in Vacancies)
                Console.Write($" {v} \n");
            Console.WriteLine();
            }


            return "";
        }

    }
}
