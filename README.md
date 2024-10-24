
# Sistema de Gerenciamento de Alunos e Turmas

## Descrição

Este projeto é uma API desenvolvida em .NET 8.0 para gerenciamento de alunos e turmas de cursos de idiomas. A API permite o cadastro, consulta, edição e exclusão de alunos e turmas, além de possibilitar a matrícula de alunos em turmas específicas, com validações de limites de alunos e matrículas únicas. O sistema utiliza o banco de dados MySQL para persistência das informações.

## Tecnologias Utilizadas

- **.NET 8.0**: Framework utilizado para o desenvolvimento da API.
- **MySQL**: Banco de dados relacional para armazenamento das informações de alunos e turmas.
- **Entity Framework Core**: ORM utilizado para mapeamento de objetos para o banco de dados.
- **Swagger**: Para documentação e teste dos endpoints da API.

## Regras de Negócio Implementadas

- **Restrição de Cadastro de Aluno Repetido**: Não é permitido cadastrar alunos com o mesmo CPF. Se um aluno já estiver registrado no sistema, o cadastro será bloqueado.
  
- **Matrícula Obrigatória na Turma**: Ao cadastrar um aluno, ele deve obrigatoriamente ser matriculado em uma turma.

- **Matrícula em Diversas Turmas**: Um aluno pode ser matriculado em várias turmas diferentes, mas a matrícula repetida na mesma turma é proibida.

- **Limite de 5 Alunos por Turma**: Cada turma tem um número máximo de 5 alunos. Ao atingir este limite, o sistema bloqueia novas matrículas na turma.

- **Exclusão de Turma Restringida**: Não é possível excluir uma turma que possui alunos matriculados. Para excluir uma turma, ela deve estar vazia.

## Endpoints

### AlunoController

- **POST /aluno/cadastrar_aluno**: Cadastra um novo aluno e o matricula em uma turma.
- **GET /aluno/{cpf}**: Obtém os dados de um aluno específico pelo CPF.
- **GET /aluno/buscar_alunos_por_numero_de_matriculas/{qtd}**: Retorna alunos que estão matriculados em uma quantidade específica de turmas.
- **GET /aluno/buscar_alunos_por_nome/{nome}**: Busca alunos pelo nome.
- **PUT /aluno/atualizar/{cpf}**: Atualiza os dados de um aluno existente.
- **DELETE /aluno/apagar/{cpf}**: Exclui um aluno do sistema.

### TurmaController

- **GET /turma/{codigo}**: Obtém uma turma pelo código, juntamente com a lista de alunos matriculados.
- **GET /turma/buscar_turma_por_numero_de_matriculas/{qtd}**: Retorna turmas com uma quantidade específica de alunos matriculados.
- **GET /turma/buscar_turma_por_nivel/{nivel}**: Busca turmas com base no nível de ensino.
- **POST /turma/cadastrar_turma**: Cadastra uma nova turma.
- **POST /turma/cadastrar_aluno_em_turma/{codigo}**: Matricula um aluno em uma turma existente.
- **DELETE /turma/apagar/{codigo}**: Exclui uma turma, desde que não tenha alunos matriculados.

## Estrutura do Banco de Dados

O projeto segue um modelo de relacionamento muitos-para-muitos entre as entidades de **Aluno** e **Turma**. A relação é mapeada através da tabela intermediária **AlunoTurma**, que armazena as matrículas dos alunos nas turmas.

## Como Executar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/JuniorBandeira7/Web-API-cursos-de-idiomas-dotnet.git
   ```
   
2. Navegue até o diretório do projeto:
   ```bash
   cd api
   ```

3. Configure o banco de dados MySQL no arquivo `appsettings.json` com suas credenciais.

4. Execute as migrações para criar as tabelas no banco de dados:
   ```bash
   dotnet ef database update
   ```

5. Execute a aplicação:
   ```bash
   dotnet watch run
   ```

6. Acesse o Swagger para testar os endpoints:
   ```
   http://localhost:5229/swagger/index.html
   ```


