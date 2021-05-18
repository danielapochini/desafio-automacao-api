using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Model.Response.Filters
{
    public partial class FiltersResponse
    {
        [JsonProperty("filters")]
        public List<Filter> Filters { get; set; }
    }

    public partial class Filter
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("criteria")]
        public Criteria Criteria { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Criteria
    {
        [JsonProperty("status")]
        public List<Status> Status { get; set; }

        [JsonProperty("hide_status")]
        public HideStatus HideStatus { get; set; }

        [JsonProperty("priority")]
        public List<Priority> Priority { get; set; }
    }

    public partial class Priority
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class HideStatus
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public partial class Status
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}
