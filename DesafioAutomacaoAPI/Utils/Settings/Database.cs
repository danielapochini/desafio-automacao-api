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
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            AppSettings appSettings = new AppSettings();
            var connectionString = appSettings.ConnectionString;

            using var connection = new MySqlConnection(connectionString); 
            
            return connection.Query<T>(query); 
        } 
    }
}
