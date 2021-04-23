using Bogus;
using DesafioAutomacaoAPI.Model.Request.Users;
using DesafioAutomacaoAPI.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioAutomacaoAPI.Utils
{
    public class DadosFakeHelper
    {
        public static string GerarNome()
        {
            var faker = new Faker<UsersRequest>("pt_BR")
                                .RuleFor(p => p.RealName, f => f.Person.FullName)
                                .Generate();
            return faker.RealName;
        }

        public static string GerarEmail()
        {
            var faker = new Faker<UsersRequest>("pt_BR")
                                .RuleFor(p => p.Email, f => f.Person.Email.ToLower())
                                .Generate();
            return faker.Email;
        }

        public static string GerarNomeDeUsuario()
        {
            var faker = new Faker<UsersRequest>("pt_BR")
                                .RuleFor(p => p.Username, f => f.Internet.UserName().ToLower())
                                .Generate();
            return faker.Username;
        }

        public static string GerarSenha()
        {
            var faker = new Faker<UsersRequest>("pt_BR")
                                .RuleFor(p => p.Password, f => f.Internet.Password())
                                .Generate();
            return faker.Password;
        } 
    }
}
