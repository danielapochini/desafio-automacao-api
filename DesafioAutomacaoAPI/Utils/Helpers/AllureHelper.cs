using Allure.Commons;
using Allure.Xunit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoAPI.Utils.Helpers
{
    public class AllureHelper
    { 
        public static void SetupAllure()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

        public void AdicionarResultado(IRestResponse response)
        {
            Steps.Step("Resultado", () =>
            {
                Steps.Step("Url: " + response.Request.Resource);  
                Steps.Step("Status Code: " + (int)response.StatusCode);
                Steps.Step("Body Request: " + response.Content);
            });
        }
    }
}
