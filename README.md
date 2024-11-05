# InsightAI

## Integrantes do Grupo

- **CAIO VITOR URBANO NEVES RM552392**
- **EMILE DE MOURA MAIA RM552235**
- **GUILHERME PEREIRA DE OLIVEIRA RM552238**
- **JULIA ANDRADE DIAS RM552332**
- **MARIA EDUARDA COSTA DE ARA�JO VIEIRA RM98760**

---

## Arquitetura do Projeto

Este projeto segue uma arquitetura multicamada (layered architecture), dividida em 6 projetos principais:

1. **InsightAI.Api**
- Camada de Apresentação: Responsável por expor a API RESTful da aplicação, recebendo e respondendo a requisições HTTP.
- Controllers: Controlam o fluxo de dados entre a camada de apresentação e a camada de serviços, delegando as solicitações às operações adequadas.

2. **InsightAI.MLModels**
- Camada de Modelos de Machine Learning: Contém a implementação dos modelos de machine learning utilizados pela aplicação, como algoritmos de classificação, regressão ou clustering.
- Serviços de Inferência: Exportam APIs para que os modelos possam ser utilizados pelas outras camadas da aplicação.

3. **InsightAI.Models**
- Camada de Modelos de Dados: Define as entidades que representam os dados da aplicação, como classes para representar empresas, produtos, clientes, etc. Essas entidades são utilizadas pelas outras camadas para trabalhar com os dados.

4. **InsightAI.Repositories**
- Camada de Acesso a Dados: Responsável por interagir com o banco de dados, realizando operações de criação, leitura, atualização e deleção de dados. Utiliza padrões como Repository Pattern para abstrair a implementação específica do banco de dados.

5. **InsightAI.Services**
- Camada de Serviços: Contém a lógica de negócio da aplicação, implementando as regras de negócio e as operações que manipulam os dados. Os serviços utilizam os repositórios para acessar o banco de dados e os modelos de machine learning para realizar predições e inferências.

6. **InsightAI.Tests**
- Camada de Testes: Dedicada a testes unitários e de integração, garantindo a qualidade do código e a correção do comportamento da aplicação.


### Depend�ncias Utilizadas:
- **ASP.NET Core**
- **Entity Framework Core**
- **XUnit**
- **Moq**
- **FluentAssertions**
- **FluentValidations**
- **Microsoft.ML**


---

## Design Patterns Utilizados

- **Repository Pattern**: Utilizado na camada de infraestrutura para abstrair a intera��o com o banco de dados e permitir uma comunica��o mais f�cil entre o Core e o banco de dados.
  
- **Dependency Injection (DI)**: Implementado em toda a aplica��o para permitir que as depend�ncias sejam resolvidas automaticamente, facilitando a testabilidade e a troca de implementa��es.
---

### Configura��es

1. Clone o reposit�rio do projeto:
   ```bash
   git clone https://github.com/seu-usuario/InsightAI.git

2. Abra o projeto em sua IDE de prefer�ncia (como Visual Studio ou VS Code).

3. Configura��o da Connection String:
    Se necess�rio, edite o arquivo appsettings.json e altere a Connection String para o banco de dados correto. Por padr�o, ela est� configurada para uma conta de estudante:

4. Rodando as Migrations:

    Para rodar as migrations corretamente, � necess�rio trocar o projeto de migra��es para InsightAI.Infraestructure, onde as classes de contexto e as migrations est�o configuradas. Voc� pode fazer isso de duas formas:

    Usando o Gerenciador de Pacotes do Visual Studio:
    No Visual Studio, v� em Ferramentas > Gerenciador de Pacotes NuGet > Console do Gerenciador de Pacotes.
    No console, altere o projeto padr�o para InsightAI.Infraestructure utilizando o menu suspenso localizado no canto superior direito do console.
    Depois, execute o seguinte comando para aplicar as migrations:
   ```bash
   Add-Migration FirstMigration
   Update-Database
   ```

### Comentários

O Model treinado não funciona dentro da API por erros de contexto CODA. O CODA é utilizado para o funcionamento da Model e variando de máquina para máquina, pode acabar não funcionando por questões da CPU/GPU e bem, não funcionou na minha durante a execução na API. O arquivo DDL gerado pela Model não estava reconhecendo os pacotes e nem tampouco a minha CPU/GPU. Mas tendo a extensão para o Visual Studio 2022 baixado através do link oficial: https://marketplace.visualstudio.com/items?itemName=MLNET.ModelBuilder2022&WT.mc_id=dotnet-35129-website - é possível utilizar a Model e testa-la adequadamente com as reclamações de uma empresa