using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Model.Request.Projects;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Projects;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Projects.SubProjects
{
    // Update the subproject relationship properties.
    // https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#f46c8545-b34a-b0f4-daa3-07fcadc81762

    public class AtualizarSubProjetoTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "SubProjetos";
        private const string subSuiteProjeto = "Atualizar SubProjetos Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#f46c8545-b34a-b0f4-daa3-07fcadc81762";

        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Atualiza o relacionamento de um subprojeto com um projeto válido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void AtualizarSubProjetoIdProjetoValido()
        {
            int idProjeto = 1;
            int idSubProjeto = 2;

            string mensagemEsperada = $"Subproject '{idSubProjeto}' updated"; 

            string urlPatchProjeto = $"api/rest/projects/{idProjeto}/subprojects/{idSubProjeto}";

            var atualizarSubProjetoBody = new SubProjectRequest
            {
                Project = new ProjectAttribute
                {
                    Name = "Projeto Teste"
                },
                InheritParent = true
            };

            var atualizarSubProjetoRequest = restManager.PerformPatchRequest<SubProjectRequest>(urlPatchProjeto, atualizarSubProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    atualizarSubProjetoRequest.StatusCode.Should().Be(204);
                    atualizarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    atualizarSubProjetoRequest.Content.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(atualizarSubProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste de subprojeto não vinculado ao projeto")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void AtualizarSubProjetoIdProjetoNaoVinculado()
        {
            int idProjeto = 2;
            int idSubProjeto = ProjectsQueries.ListarUltimoProjetoCadastrado().Id;

            string mensagemEsperada = $"Project '{idSubProjeto}' is not a subproject of '{idProjeto}'";
            string mensagemEsperadaLocalizedString = $"Project \"{idSubProjeto}\" is not a subproject of \"{idProjeto}\".";
            int codigoEsperado = 705;

            string urlPatchProjeto = $"api/rest/projects/{idProjeto}/subprojects/{idSubProjeto}";

            var atualizarSubProjetoBody = new SubProjectRequest
            {
                Project = new ProjectAttribute
                {
                    Name = "Projeto Teste"
                },
                InheritParent = false
            };

            var atualizarSubProjetoRequest = restManager.PerformPatchRequest<ErrorMessageResponse, SubProjectRequest>(urlPatchProjeto, atualizarSubProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    atualizarSubProjetoRequest.StatusCode.Should().Be(400);
                    atualizarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    atualizarSubProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    atualizarSubProjetoRequest.Data.Code.Should().Be(codigoEsperado);
                    atualizarSubProjetoRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(atualizarSubProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valor inexistente")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void AtualizarSubProjetoIdInexistente()
        {
            int gerarIds = DadosFakeHelper.GerarId();
            int idProjeto = gerarIds;
            int idSubProjeto = gerarIds;

            string mensagemEsperada = $"Project '{idProjeto}' not found";
            string mensagemEsperadaLocalizedString = $"Project \"{idProjeto}\" not found.";
            int codigoEsperado = 700;

            string urlPatchProjeto = $"api/rest/projects/{idProjeto}/subprojects/{idSubProjeto}";

            var atualizarSubProjetoBody = new SubProjectRequest
            {
                Project = new ProjectAttribute
                {
                    Name = "Projeto Teste"
                },
                InheritParent = true
            };

            var atualizarSubProjetoRequest = restManager.PerformPatchRequest<ErrorMessageResponse, SubProjectRequest>(urlPatchProjeto, atualizarSubProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    atualizarSubProjetoRequest.StatusCode.Should().Be(404);
                    atualizarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    atualizarSubProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    atualizarSubProjetoRequest.Data.Code.Should().Be(codigoEsperado);
                    atualizarSubProjetoRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(atualizarSubProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com projeto inválido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void AtualizarSubProjetoIdProjetoInvalido()
        {
            string idProjeto = DadosFakeHelper.GerarString();
            int idSubProjeto = DadosFakeHelper.GerarId();

            string mensagemEsperada = "'project_id' must be numeric";
            string mensagemEsperadaLocalizedString = "Invalid value for 'project_id'";
            int codigoEsperado = 29;

            string urlPatchProjeto = $"api/rest/projects/{idProjeto}/subprojects/{idSubProjeto}";

            var atualizarSubProjetoBody = new SubProjectRequest
            {
                Project = new ProjectAttribute
                {
                    Name = "Projeto Teste"
                },
                InheritParent = true
            };

            var atualizarSubProjetoRequest = restManager.PerformPatchRequest<ErrorMessageResponse, SubProjectRequest>(urlPatchProjeto, atualizarSubProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    atualizarSubProjetoRequest.StatusCode.Should().Be(400);
                    atualizarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    atualizarSubProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    atualizarSubProjetoRequest.Data.Code.Should().Be(codigoEsperado);
                    atualizarSubProjetoRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(atualizarSubProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com subprojeto inválido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void AtualizarSubProjetoIdSubProjetoInvalido()
        {
            int idProjeto = ProjectsQueries.ListarUltimoProjetoCadastrado().Id;
            string idSubProjeto = DadosFakeHelper.GerarString();

            string mensagemEsperada = "'subproject_id' must be numeric";
            string mensagemEsperadaLocalizedString = "Invalid value for 'subproject_id'";
            int codigoEsperado = 29;

            string urlPatchProjeto = $"api/rest/projects/{idProjeto}/subprojects/{idSubProjeto}";

            var atualizarSubProjetoBody = new SubProjectRequest
            {
                Project = new ProjectAttribute
                {
                    Name = "Projeto Teste"
                },
                InheritParent = true
            };

            var atualizarSubProjetoRequest = restManager.PerformPatchRequest<ErrorMessageResponse, SubProjectRequest>(urlPatchProjeto, atualizarSubProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    atualizarSubProjetoRequest.StatusCode.Should().Be(400);
                    atualizarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    atualizarSubProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    atualizarSubProjetoRequest.Data.Code.Should().Be(codigoEsperado);
                    atualizarSubProjetoRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(atualizarSubProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valor obrigatório")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void AtualizarSubProjetoIdProjetoObrigatorio()
        {
            int subProjetoId = DadosFakeHelper.GerarId();

            string mensagemEsperada = "Project id is missing.";

            string urlPostSubProjeto = Uri.EscapeUriString($"api/rest/projects/ /subprojects/{subProjetoId}");

            var adicionarSubProjetoRequest = restManager.PerformPatchRequest(urlPostSubProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    adicionarSubProjetoRequest.StatusCode.Should().Be(400);
                    adicionarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    adicionarSubProjetoRequest.Content.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(adicionarSubProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valor obrigatório")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void AtualizarSubProjetoIdSubProjetoObrigatorio()
        {
            int idProjeto = DadosFakeHelper.GerarId();

            string mensagemEsperada = "Subproject id is missing.";

            string urlPostSubProjeto = Uri.EscapeUriString($"api/rest/projects/{idProjeto}/subprojects/ ");

            var adicionarSubProjetoRequest = restManager.PerformPatchRequest(urlPostSubProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    adicionarSubProjetoRequest.StatusCode.Should().Be(400);
                    adicionarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    adicionarSubProjetoRequest.Content.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(adicionarSubProjetoRequest);
        }
    }

}
