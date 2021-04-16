using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DesafioAutomacaoAPI.Utils.Helpers
{
    public class RegexHelper
    {
        //Pattern Email Valido HTML5 utilizado pelo MantisBT
        public static bool IsValidAddress(string emailAddress)
        {
            Regex regex = new Regex(@"/^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/");
            return regex.IsMatch(emailAddress);
        }

        //Pattern Username Valido utilizado pelo MantisBT
        public static bool IsValidUsername(string userName)
        {
            Regex regex = new Regex(@"/^([a-z\d\-.+_ ]+(@[a-z\d\-.]+\.[a-z]{2,18})?)$/i");
            return regex.IsMatch(userName);
        }
    }
}
