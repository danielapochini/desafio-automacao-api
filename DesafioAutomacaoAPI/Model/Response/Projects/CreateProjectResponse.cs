using Newtonsoft.Json; 
using System.Collections.Generic; 

namespace DesafioAutomacaoAPI.Model.Response.Projects
{
    public class CreateProjectResponse
    {
        [JsonProperty("project")]
        public Project Project { get; set; }
    }

    public class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("view_state")]
        public ViewState ViewState { get; set; }

        [JsonProperty("access_level")]
        public AccessLevel AccessLevel { get; set; }

        [JsonProperty("custom_fields")]
        public List<object> CustomFields { get; set; }

        [JsonProperty("versions")]
        public List<object> Versions { get; set; }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }
    }

    public class Status
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class ViewState
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class AccessLevel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
