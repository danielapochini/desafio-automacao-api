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
        public static UsersEntity ListarInformacoesMeuUsuario(string userName)
        {
            var query = "SELECT * FROM bugtracker.mantis_user_table " +
                "WHERE username = '$USERNAME'".Replace("$USERNAME", userName);

            //FirstOrDefault pois o método chamado retorna um Inumerable
            return Database.ExecuteDbCommand<UsersEntity>(query).FirstOrDefault();
        }
    }
}
