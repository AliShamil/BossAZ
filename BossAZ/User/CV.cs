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

namespace BossAZ.User
{
    internal class CV
    {
        private bool ValidateGitLink(string? name) => Regex.IsMatch(name!, @"((git|ssh|http(s)?)|(git@[\w.-]+))(:(//)?)([\w.@\:/~-]+)(\.git)(/)?"); // https://github.com/AliShamil/AliShamil.git Bu sekilde olmalidi
        private bool ValidateLinkedIn(string? name) => Regex.IsMatch(name!, @"((http(s?)://)*([www])*\.|[linkedin])[linkedin/~\-]+\.[a-zA-Z0-9/~\-_,&=\?\.;]+[^\.,\s<]"); //https://www.linkedin.com/feed/

        private void PrintList<T>(List<T> list)
        {
            if (list == null)
                Console.WriteLine("EMPTY");
            else
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
        }
        private bool CheckParse(string[] array)
        {
            foreach (var i in array)
            {
                if (!int.TryParse(i, out int result))
                    return false;
            }
            return true;
        }
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
                if (value < 0 || value > 700)
                    throw new ArgumentNullException("Invalid AcceptanceRate (0-700)!");
                acceptance_rate = value;
            }
        }

        public List<string?> Skills { get; set; }

        public List<WorkHistory?> WorkHistories { get; set; }

        public List<Language?> Languages { get; set; }

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

        public void CreateCv()
        {
            bool check = false;
            string? input;
            string[] strings;
            bool checkList = false;
            while (!check)
            {
                try
                {
                    Console.WriteLine("Enter Profession: ");
                    input = Console.ReadLine();
                    Profession = input;

                    Console.WriteLine("Enter School: ");
                    input = Console.ReadLine();
                    School = input;

                    Console.WriteLine("Enter School: ");
                    input = Console.ReadLine();
                    School = input;

                    Console.WriteLine("Enter Acceptance Rate: ");
                    input = Console.ReadLine();

                    if (double.TryParse(input, out acceptance_rate))
                        AcceptanceRate = double.Parse(input);
                    else
                        throw new InvalidCastException(input);


                    while (!checkList)
                    {
                        Console.WriteLine("Do you want to add WorkHistory?");
                        switch (Console.ReadLine().ToLower())
                        {
                            case "yes":

                                Console.WriteLine("Enter company: ");
                                input= Console.ReadLine();


                                Console.WriteLine("Enter FirstDay of Working(year|month|day): ");
                                strings = Console.ReadLine().Split('|');

                                if (strings is null)
                                    throw new ArgumentNullException("First day cannot be null");

                                else
                                {
                                    if (!CheckParse(strings))
                                        throw new InvalidCastException();
                                }
                                WorkHistory newHistory = new(input, new DateTime(int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2])));

                                Console.WriteLine("Enter LastDay of Working(year|month|day): ");
                                strings = Console.ReadLine().Split('|');

                                if (strings is null)
                                    newHistory.LastDay = null;

                                else
                                {

                                    if (!CheckParse(strings))
                                        throw new InvalidCastException();

                                    newHistory.FirstDay = new DateTime(int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]));

                                }

                                WorkHistories.Add(newHistory);
                                break;
                            case "no":
                                WorkHistories = null;
                                checkList = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Unkown Command!");
                                Thread.Sleep(1500);
                                continue;

                        }
                    }

                    checkList = false;
                    while (!checkList)
                    {
                        Console.WriteLine("Do you want to add Language?");
                        switch (Console.ReadLine().ToLower())
                        {
                            case "yes":

                                Console.WriteLine("Enter Language Name: ");
                                input = Console.ReadLine();

                                LanguageLevels Level;
                                Console.WriteLine("Enter Level: ");
                                if (!Enum.TryParse(Console.ReadLine(), out Level))
                                    throw new InvalidCastException();

                                Language language = new(input, Level);

                                Languages.Add(language);


                                break;
                            case "no":
                                Languages = null;
                                checkList = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Unkown Command!");
                                Thread.Sleep(1500);
                                continue;

                        }
                    }


                    Console.WriteLine("Has Honors Diploma (true or false): ");
                    if (!bool.TryParse(Console.ReadLine(), out bool diploma))
                        throw new InvalidCastException();

                    hasHonorsDiploma = diploma;

                    Console.WriteLine("Enter GitLink(like this 'https://github.com/AliShamil/AliShamil.git'): ");
                    input = Console.ReadLine();

                    Gitlink =  input;

                    Console.WriteLine("Enter LinkedIn(like this 'https://www.linkedin.com/feed/'): ");
                    input = Console.ReadLine();

                    LinkedIn =  input;
                    check = true;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(1500);
                    continue;
                }

            }
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
Honor Diploma{hasHonorsDiploma}
Gitlink: {Gitlink}
LinkedIn: {LinkedIn}";

        }
    }

}
