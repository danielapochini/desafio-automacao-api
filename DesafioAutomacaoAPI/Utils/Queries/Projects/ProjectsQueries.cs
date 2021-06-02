using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Queries.Projects
{
    public static class ProjectsQueries
    {
        public static ProjectsEntities ListarInformacoesProjeto(int idProjeto)
        {
            var query = "SELECT * FROM bugtracker.mantis_project_table " +
                "WHERE id = '$ID'".Replace("$ID", idProjeto.ToString());

            //FirstOrDefault pois o método chamado retorna um Inumerable
            return DatabaseHelper.ExecuteDbCommand<ProjectsEntities>(query).FirstOrDefault();
        }

        public static ProjectsEntities ListarInformacoesProjeto(string nomeProjeto)
        {
            var query = "SELECT * FROM bugtracker.mantis_project_table " +
                "WHERE name = '$NAME'".Replace("$NAME", nomeProjeto);
             
            return DatabaseHelper.ExecuteDbCommand<ProjectsEntities>(query).FirstOrDefault();
        }

        public static ProjectsEntities ListarUltimoProjetoCadastrado()
        {
            var query = "SELECT * FROM bugtracker.mantis_project_table ORDER BY ID DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<ProjectsEntities>(query).FirstOrDefault();
        }

        public static ProjectsEntities ListarProjetoInativo()
        {
            var query = "SELECT * FROM bugtracker.mantis_project_table WHERE enabled = '0' LIMIT 1";
             
            return DatabaseHelper.ExecuteDbCommand<ProjectsEntities>(query).FirstOrDefault();
        }

    }
}
