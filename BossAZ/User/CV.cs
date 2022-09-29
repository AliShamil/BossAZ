using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BossAZ.Helper;
using System.Text.RegularExpressions;
using System.Transactions;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;

namespace BossAZ.User
{
    
    internal class CV
    {
        private bool ValidateGitLink(string? name) => Regex.IsMatch(name!, @"((git|ssh|http(s)?)|(git@[\w.-]+))(:(//)?)([\w.@\:/~-]+)(\.git)(/)?"); // https://github.com/AliShamil/AliShamil.git Bu sekilde olmalidi
        private bool ValidateLinkedIn(string? name) => Regex.IsMatch(name!, @"((http(s?)://)*([www])*\.|[linkedin])[linkedin/~\-]+\.[a-zA-Z0-9/~\-_,&=\?\.;]+[^\.,\s<]"); //https://www.linkedin.com/feed/


        private string? profession;
        private string? school;
        private double acceptance_rate;
        private string? gitlink;
        private string? linkedIn;


        public string? Profession
        {
            get { return profession; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Invalid Profession!");

                profession = value;
            }
        }

        public string? School
        {
            get { return school; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Invalid School!");

                school = value;
            }
        }


        public double AcceptanceRate
        {
            get { return acceptance_rate; }
            set
            {
                if (value < 0.0 || value > 700.0)
                    throw new ArgumentNullException("Invalid AcceptanceRate (0-700)!");
                acceptance_rate = value;
            }
        }

        public List<string?> Skills { get; set; }  = new List<string>();

        public List<WorkHistory?> WorkHistories { get; set; }= new List<WorkHistory?>();

        public List<Language?> Languages { get; set; } = new List<Language?>(); 

        public bool hasHonorsDiploma { get; set; }


        public string? Gitlink
        {
            get { return gitlink; }
            set
            {
                if (!ValidateGitLink(value))
                    throw new ArgumentException("Invalid GitLink");
                gitlink = value;
            }
        }


        public string? LinkedIn
        {
            get { return linkedIn; }
            set
            {
                if (!ValidateLinkedIn(value))
                    throw new ArgumentException("Invalid LinkedIn");

                linkedIn = value;
            }
        }

        public CV(string? profession, string? school, double acceptanceRate, List<string?> skills, List<WorkHistory?> workHistories, List<Language?> languages, bool hasHonorsDiploma, string? gitlink, string? linkedIn)
        {
            Profession=profession;
            School=school;
            AcceptanceRate=acceptanceRate;
            Skills=skills;
            WorkHistories=workHistories;
            Languages=languages;
            this.hasHonorsDiploma=hasHonorsDiploma;
            Gitlink=gitlink;
            LinkedIn=linkedIn;
        }

        


        public override string ToString()
        {

            Console.WriteLine($"Profession: {Profession}");
            Console.WriteLine($"Finished School: {School}");

            Console.WriteLine("\n ~ Skills ~ ");
            foreach (var s in Skills!)
                Console.Write($"{s} ");
            Console.WriteLine();


            Console.WriteLine("\n ~ Work Histories ~ ");
            foreach (var w in WorkHistories!)
                Console.Write($"{w} ");
            Console.WriteLine();


            Console.WriteLine("\n ~ Languages ~ ");
            foreach (var l in Languages!)
                Console.Write($" {l} ");
            Console.WriteLine();

            return $@"
Honor Diploma: {hasHonorsDiploma}
Gitlink: {Gitlink}
LinkedIn: {LinkedIn}";

        }
    }

}
