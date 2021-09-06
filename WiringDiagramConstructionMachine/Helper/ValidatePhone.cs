using System;
using System.Text.RegularExpressions;

namespace WiringDiagramConstructionMachine.Helper
{
    public class ValidatePhone
    {
        public static bool InValidatePhone(string phone)
        {
            phone = phone.Trim();
            return Regex.Match(phone, @"([\+84|84|0]+(3|5|7|8|9|1[2|6|8|9]))+([0-9]{8})\b").Success;
        }
    }
}
