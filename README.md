# Desafio de Automação API Rest - Base2 
  
Projeto realizado para atingir as metas propostas no Desafio API: API Rest da Base2.
## SUT (Software Under Test)
Os testes serão realizados na API Rest do Mantis Bug Tracker.  

Documentação Postman: 
- [Mantis Bug Tracker REST API](https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#intro)

Foi necessário instalar a seguinte Docker Image:
 - [MantisBT bug tracker Docker image](https://github.com/okainov/mantisbt-docker)

Optei por esta versão pois trata-se de uma imagem Docker mais atualizada do MantisBT e de fácil configuração.

## Tecnologias Utilizadas 
- [Docker](https://www.docker.com/) - Ferramenta para levantar containers através de imagens
- [Jenkins](https://www.jenkins.io/) - Ferramenta para realizar a integracão contínua do projeto na pipeline
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - Linguagem utilizada para o projeto
- [.NET 5](https://dotnet.microsoft.com/learn) - Plataforma de desenvolvimento
- [XUnit](https://xunit.net/) - Framework .NET que auxilia na construção de testes  
- [RestSharp](https://restsharp.dev/getting-started/) - Framework API Rest
- [RestSharp.Serializers.NewsoftJson](https://restsharp.dev/usage/serialization.html#newtonsoftjson-aka-json-net) - Package que substitui o deserilizador/serilizador padrão e que permite configurações personalizadas
- [FluentAssertions](https://fluentassertions.com/introduction) - Extensão de métodos de Assert de forma mais clara e natural
- [Dapper](https://www.learndapper.com/) - (Object Relational Mapping) voltado para o desenvolvimento .NET, ou seja, auxilia no mapeamento de objetos a partir de consulta SQL.
- [MySqlBackup.NET](https://www.nuget.org/packages/MySqlBackup.NET/) - Ferramenta que auxilia na realização do restore do banco de dados
- [Boggus](https://github.com/bchavez/Bogus) - Biblioteca para gerar dados fakes 
- [Allure Reports](https://github.com/allure-framework/allure-csharp) - Biblioteca para gerar relatórios de testes

## Arquitetura
 
| Diretório | Funcionalidade |
| ------ | ------ | 
 
 ## Metas
 - [x]  2) Alguns scripts devem ler dados de uma planilha Excel para implementar Data-Driven.
 > A classe de teste `Users/CriarUsuarioDataDrivenTest.cs` está realizando a leitura do arquivo .CSV que está no caminho `Utils/Resources/DataDriven/`
 - [x]  4) O projeto deve tratar autenticação.
 > O token para utilização da API é gerado pelo usuário administrador no painel `Tokens API` em sua conta e este Token é passado no Header da requisição, conforme a documentação da [API Mantis](https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#intro)
 - [x]  5) Pelo menos um teste deve fazer a validação usando REGEX (Expressões Regulares).
 > Os métodos `IsValidAddress()` e `IsValidUsername()` gerados na classe `RegexHelper` retornam através do assert `IsMatch` se o parâmetro passado atende ao Regex. Testes criados na classe `CriarUsuarioRegexTest` utilizam estes métodos.
 - [x]  7) O projeto deverá gerar um relatório de testes automaticamente.
 > Após a execução do build na pipeline, o relatório de teste é gerado automaticamente utilizando o plugin de `Allure Reports`, na pasta `allure-report` na raiz do workspace do Jenkins.
 - [x] 8) Implementar pelo menos dois ambientes (desenvolvimento / homologação)
  > Os ambientes de `DEV` e `HML` foram implementados no projeto. Há dois arquivos na raiz do projeto, `appsettings.DEV.json` e `appsettings.HML.json` que são compilados conforme o ambiente selecionado nas configurações de build.
 - [x] 9) A massa de testes deve ser preparada neste projeto, seja com scripts carregando massa nova no BD ou com restore de banco de dados.
 > A massa de dados está sendo tratada através do método `DatabaseHelper.ResetMantisDatabase()` que realiza o restore do BD antes da execução dos testes.
 - [x] 10) Executar testes em paralelo. Pelo menos duas threads (25 testes cada).
 > O arquivo `xunit.runner.json` na raiz do projeto está configurado para realizar a execução dos testes em paralelo e em duas threads.
 - [x]  11) Testes deverão ser agendados pelo Jenkins, CircleCI, TFS ou outra ferramenta de CI que preferir.
 > Os testes estão implementados na pipeline do Jenkins para o ambiente de `DEV`, o script de configuração da pipeline está disponível na raiz do projeto. O projeto possui um `webhook` que a cada push realizado no repositório do GitHub é disparado automaticamente um novo build no Jenkins.

## Referências
- [Configuration Files XUnit](https://xunit.net/docs/configuration-files): Documentação de configuração do runner do XUnit, utilizado para setar os testes em paralelo e em threads.
- [Default Config MantisBT](https://fossies.org/linux/mantisbt/config_defaults_inc.php): Documentação de configuração do MantisBT, utilizado para entender o padrão de e-mail e username válidos.
- [Using Environment Variables in XUnit](https://spicychillysoft.com/2019/10/03/using-environment-variables-in-xunit/): Utilização de múltiplas variáveis de ambiente no XUnit.
- [.NET SDK Support - Jenkins Plugin](https://plugins.jenkins.io/dotnet-sdk/): Plugin de apoio para utilização do SDK 5.0 .NET e utilzação de comandos `dotnet` na pipeline 
- [Allure XUnit](https://github.com/TinkoffCreditSystems/Allure.XUnit): Adapter do Allure Reports para execução no XUnit
- [Allure Plugin Jenkins](https://docs.qameta.io/allure/#_jenkins): Documentação de configuração do Allure Reports para execução no Jenkins
- [Triggering a Jenkins build on push using GitHub webhooks](https://faun.pub/triggering-jenkins-build-on-push-using-github-webhooks-52d4361542d4) / [Jenkins Tutorial: Configure (SCM) Github Triggers and Git Polling using Ngrok](https://www.cloudbees.com/blog/jenkins-tutorial-configure-scm-github-triggers-and-git-polling-using-ngrok): Configuração para que o build do Jenkins realize um trigger a cada push no repositório do projeto.
- [Data Driven Test in xUnit Using Custom Attribute](https://softwareautomationtest.home.blog/2019/04/07/data-driven-test-in-xunit-using-custom-attribute/): Criação de atributo personalizável para testes data driven utilizando .CSV
- [Backing up Database in MySQL using C#](https://stackoverflow.com/a/12311685): Utilização da ferramenta MySqlBackup.NET para restore do banco de dados
