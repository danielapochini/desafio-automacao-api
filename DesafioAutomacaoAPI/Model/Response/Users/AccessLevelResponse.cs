using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioAutomacaoAPI.Model.Users
{
    public class AccessLevelResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
