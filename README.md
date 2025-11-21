# Bernhoeft GRT - Teste Técnico C#


---

## Funcionalidades Implementadas

1. **Endpoints**

    * `GET /api/v1/avisos` - Lista todos os avisos ativos
    * `GET /api/v1/avisos/{id}` - Retorna um aviso específico pelo ID. Este endpoint retorna com o maximo de informações possíveis.
    * `POST /api/v1/avisos` - Cria um novo aviso
    * `PUT /api/v1/avisos/{id}` - Responsavel por editar a mensagem de um aviso.
    * `DELETE /api/v1/avisos/{id}` - Realiza **soft delete**, desativando o aviso.

2. **Controle de datas**

    * Cada aviso armazena a data de criação (`DataCriacao`) e a data de última alteração (`DataAlteracao`).

3. **OperationResult**

    * Todos os handlers retornam `IOperationResult<T>` usando `OperationResult<T>`, Seguindo o padrão do projeto.

---

## Decisões de Design

* **Soft Delete**:
  Exclusões não removem registros do banco, apenas alteram um campo `Ativo`, permitindo auditoria e controle histórico

* **Uso de `OperationResult<T>`**:
  Retorna códigos HTTP e mensagens de erro de forma padronizada, mantendo consistência em todos os endpoints.
* **Testes Unitarios**
    Não criei testes unitarios para evitar quebrar a arquitetura, já que seria necessario realizar referencia da camada de teste para a camada de Aplicação

---

## Considerações finais

* Segui a arquitetura existente e padrões de design do projeto original
* Rodei ferramentas de analise, como sonar qube para garantir que falhas criticas não passem

