using Allure.Commons;
using Allure.Xunit;
using FluentAssertions.Execution;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Helpers
{
    public static class AllureHelper
    {
        public static void SetupAllure()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }


        public static void AdicionarResultado(IRestResponse response)
        {
            Steps.Step("Resultado", () =>
            {
                if (response.Request.Body != null)
                {
                    Steps.Step("Body Request: " + response.Request.Body.Value);
                }

                Steps.Step("Método: " + response.Request.Method);
                Steps.Step("Url: " + response.Request.Resource);
                Steps.Step("Status Code: " + (int)response.StatusCode + " - " + response.StatusDescription);

                if (!string.IsNullOrEmpty(response.Content))
                {
                        Steps.Step("Body Response: " + response.Content);
                }
            });
        }
    }
}
