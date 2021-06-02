using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Model.Response.Issues;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Issues;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Issues
{
    public class ObterIssueArquivoTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Issues";
        private const string subSuiteProjeto = "Obter Arquivos Issues Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#4f9597c4-bc4b-4144-9660-f82a00bdca5a";

        private static readonly RestManager restManager = new RestManager();
         
        [AllureXunit]
        [AllureDescription("Obtém arquivos anexados em uma issue específica")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void ObterArquivosIssueIdValido()
        {
            var obterInformacoesIssuesBD = BugFileQueries.ListarUltimoArquivoCadastrado();
            string conteudoArquivoIssueConvertido = EncodeHelper.Base64Encode(obterInformacoesIssuesBD.Content);
            int idIssue = obterInformacoesIssuesBD.BugId;

            string urlArquivoIssue = $"api/rest/issues/{idIssue}/files";

            var obterArquivoIssueRequest = restManager.PerformGetRequest<IssuesFileResponse>(urlArquivoIssue);
             
            Steps.Step("Assertions", () => {
                using (new AssertionScope())
                {
                    obterArquivoIssueRequest.StatusCode.Should().Be(200);
                    obterArquivoIssueRequest.Data.Files.Should().Contain(x => x.Id == idIssue);
                    obterArquivoIssueRequest.Data.Files.Should().Contain(x => x.Filename == obterInformacoesIssuesBD.Filename);
                    obterArquivoIssueRequest.Data.Files.Should().Contain(x => x.ContentType == obterInformacoesIssuesBD.FileType);
                    obterArquivoIssueRequest.Data.Files.Should().Contain(x => x.Size == obterInformacoesIssuesBD.Filesize);
                    obterArquivoIssueRequest.Data.Files.Should().Contain(x => x.Content == conteudoArquivoIssueConvertido);
                }
            });

            AllureHelper.AdicionarResultado(obterArquivoIssueRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com id inválido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void ObterArquivosIssueIdInvalido()
        {  
            int idIssue = DadosFakeHelper.GerarId(); 

            int codigoEsperado = 1100;
            string mensagemEsperada = $"Issue #{idIssue} not found";
            
            string urlArquivoIssue = $"api/rest/issues/{idIssue}/files";

            var obterArquivoIssueRequest = restManager.PerformGetRequest<ErrorMessageResponse>(urlArquivoIssue);

            Steps.Step("Assertions", () => {
                using (new AssertionScope())
                {
                    obterArquivoIssueRequest.StatusCode.Should().Be(404);
                    obterArquivoIssueRequest.StatusDescription.Should().Be(mensagemEsperada);
                    obterArquivoIssueRequest.Data.Message.Should().Be(mensagemEsperada);
                    obterArquivoIssueRequest.Data.Code.Should().Be(codigoEsperado);
                    obterArquivoIssueRequest.Data.Localized.Should().Be(string.Concat(mensagemEsperada.Replace("#", string.Empty), "."));
                }
            });

            AllureHelper.AdicionarResultado(obterArquivoIssueRequest);
        }
    }
}
