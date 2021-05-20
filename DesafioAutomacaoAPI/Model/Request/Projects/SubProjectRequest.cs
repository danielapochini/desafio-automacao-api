using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Model.Request.Projects
{
    public partial class SubProjectRequest
    {
        [JsonProperty("project")]
        public ProjectAttribute Project { get; set; }

        [JsonProperty("inherit_parent")]
        public bool InheritParent { get; set; }
    }

    public partial class ProjectAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
