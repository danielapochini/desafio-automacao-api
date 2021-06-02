using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Request.Issues;
using DesafioAutomacaoAPI.Model.Response.Issues;
using DesafioAutomacaoAPI.Utils.Helpers; 
using DesafioAutomacaoAPI.Utils.Queries.Issues;
using FluentAssertions;
using FluentAssertions.Execution; 
using System.Collections.Generic; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Issues
{
    public class CriarIssueComAnexoTest : IAssemblyFixture<TestBase>
    { 
        private const string suiteProjeto = "Issues";
        private const string subSuiteProjeto = "Criar Issues Data Driven Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#028dda86-2165-b74a-490b-7e0487eeb853";
         
        private static readonly RestManager restManager = new RestManager();

        [AllureXunit] 
        [AllureDescription("Criação de issues utilizando um arquivo .csv")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void CriarIssueUtilizandoAnexoBase64()
        { 
            string textoEncode = EncodeHelper.Base64Encode("Desafio API RestSharp");

            var issuesBodyRequest = new IssuesRequest
            {
                Summary = "Issue com anexo",
                Description = "Esta issue possui um anexo em texto",
                Project = new Model.Request.Issues.Project
                {
                    Id = 2,
                    Name = "Projeto Teste Mantis API REST"
                },
                Category = new Model.Request.Issues.Category
                {
                    Id = 1,
                    Name = "General"
                },
                Files = new List<Model.Request.Issues.File>()
                {
                    new Model.Request.Issues.File { Name = "desafioapi.txt" , Content = textoEncode}
                }
            };

            string urlCriarIssue = "api/rest/issues/";

            var criarIssueRequest = restManager.PerformPostRequest<IssuesResponse, IssuesRequest>(urlCriarIssue, issuesBodyRequest);
            var informacoesIssuesBD = BugFileQueries.ListarUltimoArquivoCadastrado(); 

            Steps.Step("Assertions", () => {
                using (new AssertionScope())
                {
                    criarIssueRequest.StatusCode.Should().Be(201);
                    criarIssueRequest.Data.Issue.Summary.Should().Be(issuesBodyRequest.Summary);
                    criarIssueRequest.Data.Issue.Description.Should().Be(issuesBodyRequest.Description);
                    criarIssueRequest.Data.Issue.Project.Name.Should().Be(issuesBodyRequest.Project.Name);
                    criarIssueRequest.Data.Issue.Category.Name.Should().Be(issuesBodyRequest.Category.Name);
                    criarIssueRequest.Data.Issue.Attachments.Should().Contain(x => x.Filename == informacoesIssuesBD.Filename);
                    criarIssueRequest.Data.Issue.Attachments.Should().Contain(x => x.ContentType == informacoesIssuesBD.FileType);
                }
            });

            AllureHelper.AdicionarResultado(criarIssueRequest);
        }
    }
}
