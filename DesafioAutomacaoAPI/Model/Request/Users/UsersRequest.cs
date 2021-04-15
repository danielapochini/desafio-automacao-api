using Newtonsoft.Json;
using System.Collections.Generic;

namespace DesafioAutomacaoAPI.Model.Request.Users
{
    public class UsersRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("access_level")]
        public AccessLevelRequest AccessLevel { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("protected")]
        public bool Protected { get; set; }
    } 
}
