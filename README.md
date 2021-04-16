# Desafio de Automação API Rest - Base2 
  
Projeto realizado para atingir as metas propostas no Desafio API: API Rest da Base2.
## SUT (Software Under Testing)
Os testes serão realizados na API Rest do Mantis Bug Tracker.  

Documentação Postman: 
- [Mantis Bug Tracker REST API](https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#intro)

Foi necessário instalar o Docker e baixar a seguinte imagem:
 - [MantisBT bug tracker Docker image](https://github.com/okainov/mantisbt-docker)

Optei por esta versão pois trata-se de uma imagem Docker mais atualizada do MantisBT e de fácil configuração.

## Tecnologias Utilizadas 

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - Linguagem utilizada para o projeto
- [.NET 5](https://dotnet.microsoft.com/learn) - Plataforma de desenvolvimento
- [XUnit](https://xunit.net/) - Framework .NET que auxilia na construção de testes  
- [RestSharp](https://restsharp.dev/getting-started/) - Framework API Rest
- [RestSharp.Serializers.NewsoftJson](https://restsharp.dev/usage/serialization.html#newtonsoftjson-aka-json-net) - Package que substitui o deserilizador/serilizador padrão e que permite configurações personalizadas
- [FluentAssertions](https://fluentassertions.com/introduction) - Extensão de métodos de Assert de forma mais clara e natural
- [Dapper](https://www.learndapper.com/) - (Object Relational Mapping) voltado para o desenvolvimento .NET, ou seja, auxilia no mapeamento de objetos a partir de consulta SQL.
- [Boggus](https://github.com/bchavez/Bogus) - Biblioteca para gerar dados fakes 
 
## Arquitetura

 
| Diretório | Funcionalidade |
| ------ | ------ | 
 
 ## Metas
 
