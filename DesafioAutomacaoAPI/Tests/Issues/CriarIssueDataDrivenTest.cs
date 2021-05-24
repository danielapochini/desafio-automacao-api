using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Request.Issues;
using DesafioAutomacaoAPI.Model.Response.Issues;
using DesafioAutomacaoAPI.Utils.Helpers;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Issues
{
    public class CriarIssueDataDrivenTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Issues";
        private const string subSuiteProjeto = "Criar Issues Data Driven Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#028dda86-2165-b74a-490b-7e0487eeb853";
         
        private static readonly RestManager restManager = new RestManager();
         
        [AllureXunitTheory, CsvData("Utils/Resources/DataDriven/IssuesTestData.csv")]
        [AllureDescription("Criação de usuários utilizando um arquivo .csv")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void CriarIssueDadosValidos(string summary, string description, string categoryName, string projectName)
        {

            var issuesBodyRequest = new IssuesRequest
            {
                Summary = summary,
                Description = description,
                Category = new Model.Request.Issues.Category
                {
                    Name = categoryName
                },
                Project = new Model.Request.Issues.Project
                {
                    Name = projectName
                }
            };

            string urlCriarIssue = "api/rest/issues/"; 

            var criarIssueRequest = restManager.PerformPostRequest<IssuesResponse, IssuesRequest>(urlCriarIssue, issuesBodyRequest);

            Steps.Step("Assertions", () => {
                using (new AssertionScope())
                {
                    criarIssueRequest.StatusCode.Should().Be(201);
                    criarIssueRequest.Data.Issue.Summary.Should().Be(issuesBodyRequest.Summary);
                    criarIssueRequest.Data.Issue.Description.Should().Be(issuesBodyRequest.Description);
                    criarIssueRequest.Data.Issue.Project.Name.Should().Be(issuesBodyRequest.Project.Name);
                    criarIssueRequest.Data.Issue.Category.Name.Should().Be(issuesBodyRequest.Category.Name);
                }
            });

            AllureHelper.AdicionarResultado(criarIssueRequest);
        }
    }
}
