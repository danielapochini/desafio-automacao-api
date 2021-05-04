using Newtonsoft.Json; 

namespace DesafioAutomacaoAPI.Model.Request.Projects
{
    public class ProjectVersionRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("released")]
        public bool Released { get; set; }

        [JsonProperty("obsolete")]
        public bool Obsolete { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
