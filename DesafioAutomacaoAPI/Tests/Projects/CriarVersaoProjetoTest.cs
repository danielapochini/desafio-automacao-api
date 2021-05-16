using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Request.Projects;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Projects
{ 
    public class CriarVersaoProjetoTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager(); 

        [AllureXunit]
        public void CriarNovaVersaoDeProjeto()
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
