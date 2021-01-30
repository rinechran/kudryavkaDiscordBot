using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JCronTab
{
    public interface ICronSchedule
    {
        bool IsValid(string expression);
        bool IsTime(DateTime date_time);
    }
    public class CronSchedule : ICronSchedule
    {
        #region Readonly Class Members

        readonly static Regex divided_regex = new Regex(@"(\*/\d+)");
        readonly static Regex range_regex = new Regex(@"(\d+\-\d+)\/?(\d+)?");
        readonly static Regex wild_regex = new Regex(@"(\*)");
        readonly static Regex list_regex = new Regex(@"(((\d+,)*\d+)+)");
        readonly static Regex validation_regex = new Regex(divided_regex + "|" + range_regex + "|" + wild_regex + "|" + list_regex);

        #endregion

        #region Private Instance Members

        private readonly string _expression;
        public List<int> minutes;
        public List<int> hours;
        public List<int> days_of_month;
        public List<int> months;
        public List<int> days_of_week;

        #endregion

        #region Public Constructors

        public CronSchedule()
        {
        }

        public CronSchedule(string expressions)
        {
            this._expression = expressions;
            Generate();
        }

        #endregion

        #region Public Methods

        private bool IsValid()
        {
            return IsValid(this._expression);
        }

        public bool IsValid(string expression)
        {
            MatchCollection matches = validation_regex.Matches(expression);
            return matches.Count == 5;
        }

        public bool IsTime(DateTime date_time)
        {
            return minutes.Contains(date_time.Minute) &&
                   hours.Contains(date_time.Hour) &&
                   days_of_month.Contains(date_time.Day) &&
                   months.Contains(date_time.Month) &&
                   days_of_week.Contains((int)date_time.DayOfWeek);
        }

        private void Generate()
        {
            if (!IsValid())
            {
                throw new InvalidOperationException("expression is not valid");
            }

            MatchCollection matches = validation_regex.Matches(this._expression);

            generate_minutes(matches[0].ToString());

            if (matches.Count > 1)
                generate_hours(matches[1].ToString());
            else
                generate_hours("*");

            if (matches.Count > 2)
                Generate_days_of_month(matches[2].ToString());
            else
                Generate_days_of_month("*");

            if (matches.Count > 3)
                Generate_months(matches[3].ToString());
            else
                Generate_months("*");

            if (matches.Count > 4)
                Generate_days_of_weeks(matches[4].ToString());
            else
                Generate_days_of_weeks("*");
        }

        private void generate_minutes(string match)
        {
            this.minutes = Generate_values(match, 0, 60);
        }

        private void generate_hours(string match)
        {
            this.hours = Generate_values(match, 0, 24);
        }

        private void Generate_days_of_month(string match)
        {
            this.days_of_month = Generate_values(match, 1, 32);
        }

        private void Generate_months(string match)
        {
            this.months = Generate_values(match, 1, 13);
        }

        private void Generate_days_of_weeks(string match)
        {
            this.days_of_week = Generate_values(match, 0, 7);
        }

        private List<int> Generate_values(string configuration, int start, int max)
        {
            if (divided_regex.IsMatch(configuration)) return Divided_array(configuration, start, max);
            if (range_regex.IsMatch(configuration)) return Range_array(configuration);
            if (wild_regex.IsMatch(configuration)) return Wild_array(configuration, start, max);
            if (list_regex.IsMatch(configuration)) return List_array(configuration);

            return new List<int>();
        }

        private List<int> Divided_array(string configuration, int start, int max)
        {
            if (!divided_regex.IsMatch(configuration))
                return new List<int>();

            List<int> ret = new List<int>();
            string[] split = configuration.Split("/".ToCharArray());
            int divisor = int.Parse(split[1]);

            for (int i = start; i < max; ++i)
                if (i % divisor == 0)
                    ret.Add(i);

            return ret;
        }

        private List<int> Range_array(string configuration)
        {
            if (!range_regex.IsMatch(configuration))
                return new List<int>();

            List<int> ret = new List<int>();
            string[] split = configuration.Split("-".ToCharArray());
            int start = int.Parse(split[0]);
            int end = 0;
            if (split[1].Contains("/"))
            {
                split = split[1].Split("/".ToCharArray());
                end = int.Parse(split[0]);
                int divisor = int.Parse(split[1]);

                for (int i = start; i < end; ++i)
                    if (i % divisor == 0)
                        ret.Add(i);
                return ret;
            }
            else
                end = int.Parse(split[1]);

            for (int i = start; i <= end; ++i)
                ret.Add(i);

            return ret;
        }

        private List<int> Wild_array(string configuration, int start, int max)
        {
            if (!wild_regex.IsMatch(configuration))
                return new List<int>();

            List<int> ret = new List<int>();

            for (int i = start; i < max; ++i)
                ret.Add(i);

            return ret;
        }

        private List<int> List_array(string configuration)
        {
            if (!list_regex.IsMatch(configuration))
                return new List<int>();

            List<int> ret = new List<int>();

            string[] split = configuration.Split(",".ToCharArray());

            foreach (string s in split)
                ret.Add(int.Parse(s));

            return ret;
        }

        #endregion
    }
}
