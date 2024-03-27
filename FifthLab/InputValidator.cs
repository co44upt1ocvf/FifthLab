using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FifthLab
{
    internal class InputValidator
    {
        private static readonly Regex regexForText = new Regex("[^a-zA-Zа-яА-Я]+");
        private static readonly Regex regexForPass = new Regex("[^a-zA-Z0-9!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>/?]+");

        public static bool IsNumeric(string input)
        {
            string pattern = @"^\d+$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsInvalidInputPass(string text)
        {
            return regexForPass.IsMatch(text);
        }

        public static bool IsInvalidInputText(string text)
        {
            return regexForText.IsMatch(text);
        }

        public static bool IsValidTimeFormat(string input)
        {
            string pattern = @"^([01][0-9]|2[0-3]):[0-5][0-9]$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsValidFormatCgDn(string input)
        {
            string pattern = @"^[A-Za-zА-Яа-я]{2}-[A-Za-zА-Яа-я]{2}$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsValidTimeIntervalFormat(string input)
        {
            string pattern = @"^([01][0-9]|2[0-3]):[0-5][0-9]\s*-\s*([01][0-9]|2[0-3]):[0-5][0-9]$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
