using System;
using System.Text.RegularExpressions;

namespace Application.Infrastructure.Validations
{
    public static class CommonValidation
    {
        public static bool BeAValidPassword(string password) {
            var validator = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&_#£~|])[A-Za-z\\d@$!%*?&_#£~|]{5,}$");
            return validator.Match(password).Success;
        }

        public static string ValidPasswordErrorMessage = "Min 5 chars with one lowercase, uppercase, special char, number";

        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        public static bool BeAValidDateRange(DateTime date, int MinAge = 10, int MaxAge = 100)
        {
            // return !date.Equals(default(DateTime));
            return DateTime.Now.AddYears(-MaxAge) <= date && DateTime.Now.AddYears(-MinAge) >= date;
        }
    }
}