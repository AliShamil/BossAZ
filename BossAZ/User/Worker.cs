using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossAZ.User
{
    
    using BossAZ.Helper;
    internal sealed class Worker : User
    {

        public List<CV> WorkerCV { get; set; }

        public Worker(string? name, string? surname, Azerbaijan city, string? phone, byte age)
            : base(name, surname, city, phone, age)
        {
            WorkerCV = new List<CV>();
        }

        public override string ToString()
        {

            Console.WriteLine($"{base.ToString()}");

            if (WorkerCV.Count != 0)
            {
                Console.WriteLine("\n ~ CV ~ ");
                foreach (var c in WorkerCV!)
                    Console.Write($" {c} \n");
                Console.WriteLine();

            }

            return "";
        }


    }
}
