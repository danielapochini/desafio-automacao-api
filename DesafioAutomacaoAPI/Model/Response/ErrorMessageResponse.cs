using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioAutomacaoAPI.Model
{
    public class ErrorMessageResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("localized")]
        public string Localized { get; set; }
    }
}
