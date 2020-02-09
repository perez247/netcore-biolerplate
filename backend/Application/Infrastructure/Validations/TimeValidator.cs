using System;
using System.Text.RegularExpressions;

namespace Application.Infrastructure.Validations
{
    public static class TimeValidator
    {
        public static string ValidPasswordErrorMessage = "Min 5 chars with one lowercase, uppercase, special char, number";

        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        public static bool BeAValidDateRange(DateTime date, int MinAge = 10, int MaxAge = 100)
        {
            return DateTime.Now.AddYears(-MaxAge) <= date && DateTime.Now.AddYears(-MinAge) >= date;
        }

        public static bool NotNow(DateTime date)
        {
            return DateTime.Now >= date;
        }

        public static bool EndDateGreaterThanStartDate(DateTime startDate, DateTime endDate)
        {
            return endDate > startDate;
        }

    }
}