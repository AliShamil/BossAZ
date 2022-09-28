using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BossAZ.Helper
{
    class WorkHistory
    {
        private bool ValidateName(string? name) => Regex.IsMatch(name!, @"^[a-zA-Z]+$");
        private string? company;
        private DateTime? firstDay;
        private DateTime? lastDay;


        public string? Company
        {
            get { return company; }
            set
            {
                if (!ValidateName(value))
                    throw new ArgumentException("Invalid Name");

                company = value;
            }
        }



        public DateTime? FirstDay
        {
            get { return firstDay; }
            set
            {
                if (value < new DateTime(1950, 1, 1) || value > DateTime.Now)
                    throw new ArgumentException("Invalid Date");

                firstDay = value;
            }
        }



        public DateTime? LastDay
        {
            get { return lastDay; }
            set
            {
                if (value < new DateTime(1950, 1, 1) || value > DateTime.Now)
                    throw new ArgumentException("Invalid Date");

                lastDay = value;
            }
        }




        public WorkHistory(string? company, DateTime firstDay, DateTime lastDay)
        {
            Company=company;
            FirstDay=firstDay;
            LastDay=lastDay;
        }

        public WorkHistory(string? company, DateTime firstDay)
        {
            Company=company;
            FirstDay=firstDay;
            lastDay = null;
        }


        public override string ToString()
        {
            if (lastDay is null)
            {
                return $@"Company: {Company}
First Day: {FirstDay?.ToShortDateString()}";

            }
            
            else if (firstDay is null && lastDay is null)
            {
                return $@"Company: {Company}";
            }

            else
            {
                return $@"Company: {Company}
First Day: {FirstDay?.ToShortDateString()}
Last Day: {LastDay?.ToShortDateString()}";

            }

        }
    }
}
