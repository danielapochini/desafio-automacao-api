using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioAutomacaoAPI.Model.Users
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public string Timezone { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public List<object> Projects { get; set; }
    }
}
