using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Model.Response.Projects
{
    public class GetProjectResponse
    {
        [JsonProperty("projects")]
        public List<Project> Projects { get; set; }
    }
}
