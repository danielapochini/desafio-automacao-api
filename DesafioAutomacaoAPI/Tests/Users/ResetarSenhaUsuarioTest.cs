﻿using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Users;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Users
{ 
    public class ResetarSenhaUsuarioTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        public void ResetSenhaUsuarioValido()
        { 
            int userId = UsersQueries.ListarUltimoUsuarioCadastrado().Id;

            string urlResetSenha = $"api/rest/users/{userId}/reset";

            var resetarSenhaRequest = restManager.PerformPutRequest(urlResetSenha);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    resetarSenhaRequest.StatusCode.Should().Be(204);
                    resetarSenhaRequest.Content.Should().BeEmpty();
                    resetarSenhaRequest.StatusDescription.Should().Be("No Content");
                }
            });

            AllureHelper.AdicionarResultado(resetarSenhaRequest);
        }

        [AllureXunit]
        public void ResetSenhaUsuarioInvalido()
        {
            int userId = DadosFakeHelper.GerarId();

            string urlResetSenha = $"api/rest/users/{userId}/reset";

            var resetarSenhaRequest = restManager.PerformPutRequest<ErrorMessageResponse>(urlResetSenha);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    resetarSenhaRequest.StatusCode.Should().Be(400);
                    resetarSenhaRequest.Data.Message.Should().Be("Invalid user id");
                    resetarSenhaRequest.Data.Code.Should().Be(29);
                    resetarSenhaRequest.Data.Localized.Should().Be("Invalid value for 'id'");
                    resetarSenhaRequest.StatusDescription.Should().Be("Invalid user id");
                }
            });

            AllureHelper.AdicionarResultado(resetarSenhaRequest);
        }

        [AllureXunit]
        public void ResetarUnicoAdministradorAtivo()
        {
            int userId = UsersQueries.ListarAdministrador().Id;

            string urlResetSenha = $"api/rest/users/{userId}/reset";

            var resetarSenhaRequest = restManager.PerformPutRequest<ErrorMessageResponse>(urlResetSenha);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    resetarSenhaRequest.StatusCode.Should().Be(400);
                    resetarSenhaRequest.StatusDescription.Should().Be("Resetting last administrator not allowed");
                    resetarSenhaRequest.Data.Message.Should().Be("Resetting last administrator not allowed");
                    resetarSenhaRequest.Data.Code.Should().Be(808);
                    resetarSenhaRequest.Data.Localized.Should().Be("You cannot remove or demote the last administrator account. To perform the action you requested, " +
                        "you first need to create another administrator account.");
                }
            });

            AllureHelper.AdicionarResultado(resetarSenhaRequest);
        }

    }
}