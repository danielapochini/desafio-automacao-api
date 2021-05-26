using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Settings; 
using System.Linq; 

namespace DesafioAutomacaoAPI.Utils.Queries.Users
{
    public static class UsersQueries
    {
        public static UsersEntities ListarInformacoesUsuario(string userName)
        {
            var query = "SELECT * FROM bugtracker.mantis_user_table " +
                "WHERE username = '$USERNAME'".Replace("$USERNAME", userName);

            //FirstOrDefault pois o método chamado retorna um Inumerable
            return DatabaseHelper.ExecuteDbCommand<UsersEntities>(query).FirstOrDefault();
        } 

        public static UsersEntities ListarUltimoUsuarioCadastrado()
        {
            var query = "SELECT * FROM mantis_user_table ORDER BY ID DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<UsersEntities>(query).FirstOrDefault();
        }

        public static UsersEntities ListarAdministrador()
        {
            var query = "SELECT * FROM mantis_user_table WHERE access_level = '90' ORDER BY ID DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<UsersEntities>(query).FirstOrDefault();
        }

        public static UsersEntities ListarUsuarioInativo()
        {
            var query = "SELECT * FROM mantis_user_table WHERE enabled = '0' ORDER BY ID DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<UsersEntities>(query).FirstOrDefault();
        }
    }
}
