using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace PrviProjektniZadatakHCI.Util
{
   

    public static class ValidationHelper
    {
        private static readonly string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private static readonly string IdPattern = @"^\d+$";

        public static bool IsValidId(string id)
        {
            return Regex.IsMatch(id, IdPattern);
        }
       

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        public static bool IsValidStudyYear(int year)
        {
            return year >= 1 && year <= 5;
        }
        public static bool IsValidECTS(int ects)
        {
            return ects >= 1 && ects <= 8;
        }
        public static bool IsInteger(object value)
        {
            return int.TryParse(value?.ToString(), out _);
        }
    }

}
