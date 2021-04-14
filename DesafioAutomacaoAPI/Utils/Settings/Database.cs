using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic; 
using System.Text;

namespace DesafioAutomacaoAPI.Utils.Settings
{
    public class Database
    {
        public static IEnumerable<T> ExecuteDbCommand<T>(string query)
        {
            AppSettings appSettings = new AppSettings();
            var connectionString = appSettings.ConnectionString;

            //utilizando "using" para abrir e fechar a conexao
            using (var connection = new MySqlConnection(connectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

                return connection.Query<T>(query);
            };  
        } 
    }
}
