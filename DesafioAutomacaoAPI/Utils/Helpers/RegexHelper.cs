using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DesafioAutomacaoAPI.Utils.Helpers
{
    public class RegexHelper
    {
        public static bool IsValidAddress(string emailAddress)
        {
            Regex regex = new Regex(@"^[\w0-9._%+-]+@[\w0-9.-]+\.[\w]{2,6}$");
            return regex.IsMatch(emailAddress);
        }
    }
}
