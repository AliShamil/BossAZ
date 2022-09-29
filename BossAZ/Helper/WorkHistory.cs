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
        private string? company;

        public string? Company
        {
            get { return company; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Invalid Name");

                company = value;
            }
        }

        public (DateTime FirstDay, DateTime LastDay) WorkedTimes { get; set; }


        public WorkHistory(string? company, (DateTime FirstDay, DateTime LastDay) workedTimes)
        {
            Company=company;
            WorkedTimes=workedTimes;
        }

        public override string ToString()
        {


            return $@"Company: {Company}
Worked Times: {WorkedTimes.FirstDay.ToShortDateString()} - {WorkedTimes.LastDay.ToShortDateString()}";

        }
    }
}
