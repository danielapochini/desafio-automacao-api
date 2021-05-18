using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Queries.Filters
{
    public class FiltersQueries
    {
        public static FiltersEntity ListarUltimoFiltroCadastrado()
        {
            var query = "SELECT * FROM mantis_filters_table ORDER BY ID DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<FiltersEntity>(query).FirstOrDefault();
        }
        public static FiltersEntity ListarUltimoFiltroPublicoCadastrado()
        {
            var query = "SELECT * FROM mantis_filters_table WHERE IS_PUBLIC = 1 ORDER BY ID DESC LIMIT 1";
            return DatabaseHelper.ExecuteDbCommand<FiltersEntity>(query).FirstOrDefault();
        }
    }
}
