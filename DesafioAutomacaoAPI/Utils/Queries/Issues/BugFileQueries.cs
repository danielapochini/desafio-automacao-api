using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Queries.Issues
{
    public static class BugFileQueries
    {
        public static BugFileEntities ListarUltimoArquivoCadastrado()
        {
            var query = "SELECT * FROM mantis_bug_file_table ORDER BY ID DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<BugFileEntities>(query).FirstOrDefault();
        }

        public static BugFileEntities ListarArquivoCadastrado()
        {
            var query = "SELECT * FROM mantis_bug_file_table ORDER BY ID ASC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<BugFileEntities>(query).FirstOrDefault();
        }
    }
}
