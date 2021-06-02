using Newtonsoft.Json; 
using System.Collections.Generic; 

namespace DesafioAutomacaoAPI.Model.Response.Issues
{
    public partial class IssuesFileResponse
    {
        [JsonProperty("files")]
        public List<File> Files { get; set; }
    } 


}
