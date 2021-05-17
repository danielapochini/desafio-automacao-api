using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Response.Langs;
using DesafioAutomacaoAPI.Utils.Helpers;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Langs
{
    // Get the specified localized string.
    // If string doesn't exist, it will be silently skipped.
    // https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#f4db831d-c31c-6c98-df8d-b16f490f4247

    public class ObterStringLocalizedTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager();

        const string parametroStringValido = "all_projects";
        const string parametroStringInvalido = "does_not_exist";

        [AllureXunit]
        public void ObterStringLocalizedValorValido()
        { 
            string urlObterString = $"api/rest/lang?string={parametroStringValido}";


            var ObterStringLocalizedRequest = restManager.PerformGetRequest<StringLocalizedResponse>(urlObterString);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterStringLocalizedRequest.StatusCode.Should().Be(200);
                    ObterStringLocalizedRequest.Data.Strings.Should().Contain(x => x.Name == "all_projects" && x.Localized == "All Projects");
                    ObterStringLocalizedRequest.Data.Language.Should().Be("english");
                }
            });

            AllureHelper.AdicionarResultado(ObterStringLocalizedRequest);
        }

        [AllureXunit]
        public void ObterStringLocalizedValorInexistente()
        { 
            string urlObterString = $"api/rest/lang?string={parametroStringInvalido}";

            var ObterStringLocalizedRequest = restManager.PerformGetRequest<StringLocalizedResponse>(urlObterString);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterStringLocalizedRequest.StatusCode.Should().Be(200);
                    ObterStringLocalizedRequest.Data.Strings.Should().BeEmpty();
                    ObterStringLocalizedRequest.Data.Language.Should().Be("english");
                }
            });

            AllureHelper.AdicionarResultado(ObterStringLocalizedRequest);
        }

        [AllureXunit]
        public void ObterStringLocalizedMultiplosValores()
        {
            string parametroStringValidoDois = "move_bugs";
            string urlObterString = $"api/rest/lang?string[]={parametroStringValido}&string[]={parametroStringInvalido}&string[]={parametroStringValidoDois}";


            var ObterStringLocalizedRequest = restManager.PerformGetRequest<StringLocalizedResponse>(urlObterString);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterStringLocalizedRequest.StatusCode.Should().Be(200);
                    ObterStringLocalizedRequest.Data.Strings.Should().Contain(x => x.Name == parametroStringValido && x.Localized == "All Projects");
                    ObterStringLocalizedRequest.Data.Strings.Should().Contain(x => x.Name == parametroStringValidoDois && x.Localized == "Move Issues");
                    ObterStringLocalizedRequest.Data.Strings.Should().NotContain(x => x.Name == parametroStringInvalido);
                    ObterStringLocalizedRequest.Data.Language.Should().Be("english");
                }
            });

            AllureHelper.AdicionarResultado(ObterStringLocalizedRequest);
        }
    }
}
