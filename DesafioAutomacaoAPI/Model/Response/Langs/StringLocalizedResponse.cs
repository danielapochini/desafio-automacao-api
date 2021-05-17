using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Model.Response.Langs
{
    public partial class StringLocalizedResponse
    {
        [JsonProperty("strings")]
        public List<StringFields> Strings { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public partial class StringFields
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("localized")]
        public string Localized { get; set; }
    }
}
