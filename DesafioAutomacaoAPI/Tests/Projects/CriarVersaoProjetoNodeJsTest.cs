using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Request.Projects;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Projects
{ 
    public class CriarVersaoProjetoNodeJsTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Projetos";
        private const string subSuiteProjeto = "Criar Versão de Projeto Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#d9c16d5e-cae9-20e6-2b46-556c68c451cc";

        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Teste utilizando NodeJs para criar nova versão de projeto")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void CriarNovaVersaoDeProjetoNodeJs()
        {
            int idProjeto = DadosFakeHelper.GerarId();

            string urlPostProjeto = $"api/rest/projects/{idProjeto}/versions/";

            var versaoProjetoBody = new ProjectVersionRequest
            {
                Name = "v1.1.0",
                Description = "Major new version",
                Released = true,
                Obsolete = false,
                Timestamp = NodeJsHelper.RetornaDataAleatoriaEmTrintaDias()
            };

            var criarVersaoProjetoRequest = restManager.PerformPostRequest(urlPostProjeto, versaoProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    criarVersaoProjetoRequest.StatusCode.Should().Be(204);
                    criarVersaoProjetoRequest.Content.Should().BeEmpty();
                    criarVersaoProjetoRequest.StatusDescription.Should().Be("Version created with id 1");
                }
            });

            AllureHelper.AdicionarResultado(criarVersaoProjetoRequest);
        }
    }
}
