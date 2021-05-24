using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Model.Request.Issues
{
    public partial class IssuesRequest
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }

        [JsonProperty("files")]
        public List<File> Files { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Field
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class CustomField
    {
        [JsonProperty("field")]
        public Field Field { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class File
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
