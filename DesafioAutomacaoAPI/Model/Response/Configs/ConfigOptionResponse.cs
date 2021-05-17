using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Model.Response.Config
{
    public partial class ConfigOptionResponse
    {
        [JsonProperty("configs")]
        public List<ConfigFields> Configs { get; set; }
    }

    public partial class ConfigFields
    {
        [JsonProperty("option")]
        public string Option { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }


}
