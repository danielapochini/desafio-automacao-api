using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Entities
{
    public class SubProjectsEntities
    {
        public int ChildId { get; set; }
        public int ParentId { get; set; }
        public bool InheritParent { get; set; }
    }
} 
