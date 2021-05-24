using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Model.Response.Issues
{
    public partial class IssuesResponse
    {
        [JsonProperty("issue")]
        public Issue Issue { get; set; }
    }
     
    public partial class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Reporter
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
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

    public partial class Resolution
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class ViewState
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
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

    public partial class Severity
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class Reproducibility
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class Attachment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("reporter")]
        public Reporter Reporter { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }
    }

    public partial class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public partial class Type
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class File
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }
    }

    public partial class History
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("type")]
        public Type Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("file")]
        public File File { get; set; }
    }

    public partial class Issue
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("reporter")]
        public Reporter Reporter { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("resolution")]
        public Resolution Resolution { get; set; }

        [JsonProperty("view_state")]
        public ViewState ViewState { get; set; }

        [JsonProperty("priority")]
        public Priority Priority { get; set; }

        [JsonProperty("severity")]
        public Severity Severity { get; set; }

        [JsonProperty("reproducibility")]
        public Reproducibility Reproducibility { get; set; }

        [JsonProperty("sticky")]
        public bool Sticky { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("history")]
        public List<History> History { get; set; }
    } 
}
