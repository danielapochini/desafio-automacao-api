using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DesafioAutomacaoAPI.Model.Users
{ 
    public partial class UserResponse
    {
        [JsonProperty("user")]
        public UsersResponseAttributes User { get; set; }
    }

    public partial class UsersResponseAttributes
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = ("real_name"))]
        public string RealName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("access_level")]
        public AccessLevelResponse AccessLevel { get; set; }

        [JsonProperty("projects")]
        public List<object> Projects { get; set; }
    }

} 
