using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Users;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Users; 
using FluentAssertions; 
using FluentAssertions.Execution;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Users
{ 
    public class ObterInfosMeuUsuarioTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager();  

        [AllureXunit]
        public void ObterMyUserInfo()
        {  
            string urlGetUserInfo = "api/rest/users/me";
             
            var obterDadosResponse = restManager.PerformGetRequest<UsersResponseAttributes>(urlGetUserInfo);             
            var resultadoListarUsers = UsersQueries.ListarInformacoesUsuario(obterDadosResponse.Data.Name);
             
            Steps.Step("Assertions", () =>
            {
                //usando fluent validations para realizar asserts multiplos 
                using (new AssertionScope()) 
                {
                    obterDadosResponse.StatusCode.Should().Be(200); 
                    resultadoListarUsers.Id.Should().Be(obterDadosResponse.Data.Id);
                    resultadoListarUsers.UserName.Should().Be(obterDadosResponse.Data.Name);
                    resultadoListarUsers.RealName.Should().Be(obterDadosResponse.Data.RealName);
                    resultadoListarUsers.Email.Should().Be(obterDadosResponse.Data.Email);
                    resultadoListarUsers.AccessLevel.Should().Be(obterDadosResponse.Data.AccessLevel.Id);
                }
            });

            AllureHelper.AdicionarResultado(obterDadosResponse);
        }
    }
}
