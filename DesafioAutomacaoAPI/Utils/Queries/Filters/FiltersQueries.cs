using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Settings; 
using System.Linq; 

namespace DesafioAutomacaoAPI.Utils.Queries.Filters
{
    public static class FiltersQueries
    {
        public static FiltersEntities ListarUltimoFiltroCadastrado()
        {
            var query = "SELECT * FROM mantis_filters_table ORDER BY ID DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<FiltersEntities>(query).FirstOrDefault();
        }
        public static FiltersEntities ListarUltimoFiltroPublicoCadastrado()
        {
            var query = "SELECT * FROM mantis_filters_table WHERE IS_PUBLIC = 1 ORDER BY ID DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<FiltersEntities>(query).FirstOrDefault();
        }
    }
}
