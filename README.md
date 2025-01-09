# Golden Raspberry Awards API

## Descrição
A Golden Raspberry Awards API é uma aplicação RESTful desenvolvida em .NET Core utilizando o conceito de Domain-Driven Design (DDD). A API permite a leitura da lista de indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards, e fornece informações sobre o produtor com maior intervalo entre dois prêmios consecutivos e o que obteve dois prêmios mais rápido.

## Estrutura do Projeto
O projeto está organizado nas seguintes camadas:
- **Domain**: Contém as entidades e interfaces de repositório.
- **Application**: Contém os serviços de aplicação.
- **Infrastructure**: Contém a implementação dos repositórios e o contexto do banco de dados.
- **Presentation**: Contém os controladores da API e a configuração do Swagger.

## Tecnologias Utilizadas
- .NET Core
- Entity Framework Core (InMemory Database)
- CsvHelper
- Swashbuckle (Swagger)
- xUnit (Testes de Integração)

## Requisitos do Sistema
1. Ler o arquivo CSV dos filmes e inserir os dados em uma base de dados ao iniciar a aplicação.
2. Obter o produtor com maior intervalo entre dois prêmios consecutivos, e o que obteve dois prêmios mais rápido.


## Instruções para Rodar o Projeto
1. Clone o repositório de https://github.com/macksonnn/GoldenRaspberryAwards

2. Execute o projeto:
   dotnet run

3. Acesse a documentação do Swagger em http://localhost:5133/index.html .

## Instruções para Rodar os Testes de Integração
1. Navegue até os projeto de test e execute:
  dotnet test
Os testes de integração garantirão que os dados obtidos estão de acordo com os dados fornecidos na proposta

## Endpoints da API
Obter Produtores com Maior e Menor Intervalo entre Prêmios:
  GET /api/movies/producers-awards
  Retorna o produtor com maior intervalo entre dois prêmios consecutivos e o que obteve dois prêmios mais rápido.

 
