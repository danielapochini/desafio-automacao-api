using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Queries.Projects
{
    public class SubProjectsQueries
    {
        public static SubProjectsEntities ListarInformacoesProjetoPai(int idProjeto)
        {
            var query = "SELECT * FROM bugtracker.mantis_project_hierarchy_table " +
                "WHERE parent_id = '$ID'".Replace("$ID", idProjeto.ToString());

            //FirstOrDefault pois o método chamado retorna um Inumerable
            return DatabaseHelper.ExecuteDbCommand<SubProjectsEntities>(query).FirstOrDefault();
        }
         

        public static SubProjectsEntities ListarUltimoSubProjetoCadastrado()
        {
            var query = "SELECT * FROM bugtracker.mantis_project_hierarchy_table ORDER BY parent_id DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<SubProjectsEntities>(query).FirstOrDefault();
        }
         

    }
}
