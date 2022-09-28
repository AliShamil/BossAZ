using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
enum LanguageLevels
{
    A1,
    A2,
    B1,
    B2,
    C1,
    C2
}

namespace BossAZ.Helper
{

    class Language
    {
        private bool ValidateName(string? name) => Regex.IsMatch(name!, @"^[a-zA-Z]+$");
        private string? name;


        public string? Name
        {
            get { return name; }
            set
            {
                if (!ValidateName(value))
                    throw new ArgumentException("Invalid Name");

                name = value;
            }
        }

        public LanguageLevels Level { get; set; }

        public Language(string? name, LanguageLevels level)  
        {
            Name=name;
            Level=level;
        }


        public override string ToString()
=> $@"Language name: {Name}
Level: {Level}";

    }



 
}
