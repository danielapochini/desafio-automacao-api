using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioAutomacaoAPI.Utils.Queries.Users
{
    public class UsersQueries
    {
        public static UsersEntity ListarTodosUsuarios()
        {
            var query = "select * from mantis_user_table";

            return Database.ExecuteDbCommand<UsersEntity>(query).FirstOrDefault();
        }
    }
}
