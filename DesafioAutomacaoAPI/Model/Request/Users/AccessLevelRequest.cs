using Newtonsoft.Json;

namespace DesafioAutomacaoAPI.Model.Request.Users
{
    public class AccessLevelRequest
    { 
        [JsonProperty("name")]
        public string Name { get; set; } 
    } 
}
