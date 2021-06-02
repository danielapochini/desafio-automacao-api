using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Response.Config;
using DesafioAutomacaoAPI.Utils.Helpers;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Config
{  
    public class ObterConfiguracaoSistemaTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Configurações";
        private const string subSuiteProjeto = "Obter Configurações do Sistema Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#b09a8b9d-5466-48f7-89dc-4be7414d7059";

        private readonly string valorOpcaoValido = "default_bug_priority";
        private readonly string valorOpcaoPrivado = "crypto_master_salt";
        private readonly string valorOpcaoInexistente = "does_not_exist";

        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Obtém configuração com valor válido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void ObterConfiguracaoValorValido()
        { 
            string urlConfiguracao = $"api/rest/config?option={valorOpcaoValido}";

            var ObterConfiguracaoRequest = restManager.PerformGetRequest<ConfigOptionResponse>(urlConfiguracao);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterConfiguracaoRequest.StatusCode.Should().Be(200);
                    ObterConfiguracaoRequest.Data.Configs.Should().Contain(x => x.Option == valorOpcaoValido && x.Value == "30"); 
                }
            });

            AllureHelper.AdicionarResultado(ObterConfiguracaoRequest);
        }

        [AllureXunit]
        [AllureDescription("Obtém configuração com valor inexistente")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void ObterConfiguracaoValorInexistente()
        { 
            string urlConfiguracao = $"api/rest/config?option={valorOpcaoInexistente}";

            var ObterConfiguracaoRequest = restManager.PerformGetRequest<ConfigOptionResponse>(urlConfiguracao);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterConfiguracaoRequest.StatusCode.Should().Be(200);
                    ObterConfiguracaoRequest.Data.Configs.Should().BeEmpty();
                }
            }); 

            AllureHelper.AdicionarResultado(ObterConfiguracaoRequest);
        }

        [AllureXunit]
        [AllureDescription("Obtém configuração com valor privado")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void ObterConfiguracaoValorPrivado()
        {
            
            string urlConfiguracao = $"api/rest/config?option={valorOpcaoPrivado}";

            var ObterConfiguracaoRequest = restManager.PerformGetRequest<ConfigOptionResponse>(urlConfiguracao);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterConfiguracaoRequest.StatusCode.Should().Be(200);
                    ObterConfiguracaoRequest.Data.Configs.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(ObterConfiguracaoRequest);
        }

        [AllureXunit]
        [AllureDescription("Obtém múltiplos valores de configurações")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Teste Exploratório")]
        [AllureLink(linkDocumentacao)]
        public void ObterConfiguracaoMultiplosValores()
        {
            string valorOpcaoValidoDois = "csv_separator";    

            string urlConfiguracao = $"api/rest/config?option[]={valorOpcaoValido}&option[]={valorOpcaoPrivado}&option[]={valorOpcaoInexistente}&option[]={valorOpcaoValidoDois}";

            var ObterConfiguracaoRequest = restManager.PerformGetRequest<ConfigOptionResponse>(urlConfiguracao);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterConfiguracaoRequest.StatusCode.Should().Be(200);
                    ObterConfiguracaoRequest.Data.Configs.Should().Contain(x => x.Option == valorOpcaoValido && x.Value == "30");
                    ObterConfiguracaoRequest.Data.Configs.Should().Contain(x => x.Option == valorOpcaoValidoDois && x.Value == ",");
                    ObterConfiguracaoRequest.Data.Configs.Should().NotContain(x => x.Option == valorOpcaoPrivado && x.Option == valorOpcaoInexistente);
                }
            });

            AllureHelper.AdicionarResultado(ObterConfiguracaoRequest);
        } 
    }
}
