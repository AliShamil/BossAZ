using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossAZ.User
{
    internal class Employer : User
    {
        public Employer(string? name, string? surname, Azerbaijan city, string? phone, byte age) 
            : base(name, surname, city, phone, age)
        {
        }
    }
}
