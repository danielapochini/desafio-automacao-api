using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Entities
{
    public class ProjectsEntities
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public int Status { get; set; }
		public bool Enabled { get; set; }
		public int ViewState { get; set; }
		public int AccessMin { get; set; }
		public string FilePath { get; set; }
		public string Description { get; set; }
		public int CategoryId { get; set; }
		public byte InheritGlobal { get; set; }

	}
}
