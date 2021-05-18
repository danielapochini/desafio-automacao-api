using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Entities
{
    public class FiltersEntity
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ProjectId { get; set; }
		public byte IsPublic { get; set; }
		public string Name { get; set; }
		public string FilterString { get; set; }
	}
}
