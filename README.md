# TaxAPI

Conjunto de APIs de geração de impostos fictícios a partir do envio de notas fiscais via HTTP. Desenvolvido em .NET 8.0, apresentando 2 APIs principais.

### Funcionalidades ⚙️

- Cadastro e gerenciamento de empresas, notas fiscais e impostos em banco de dados SQLServer;
- Serviços mensais de geração de impostos ou recolhimento de empresas com dados cadastrais incompletos;
- Envio de e-mail ao usuário após cadastro de empresa e outras condições especiais.

## Composição

### RegistroNF.API 📃

API responsável pelo gerenciamento e cadastro de notas fiscais fornecidas pelo usuário, junto com avaliação mensal de empresas com dados cadastrais incompletos, notificando usuários por meio de e-mail.

### CalculadoraImposto.API 💰

API responsável pelo cálculo de impostos a partir do recebimento de notas fiscais, realizando requisições por meio de HTTP mensalmente, a partir das configurações no Hangfire.

#### Componentes Compartilhados 🔗

- Serviço SMTP para envio de e-mails, utilizando da biblioteca MimeMessage;
- Hangfire para configuração de funcionalidades mensais recorrentes;
- Middleware de gerenciamento de exceções nativas (Exception) ou customizadas (BusinessRuleException);
- Classes de configuração para geração de tabelas por meio do Entity Framework;
- Testes unitários com branch coverage completo avaliado por Fine Code Coverage;
- Logger para envio de mensagens em Console durante a execução do sistema;
- Interfaces para Injeção de Dependência;
- FluentValidation para validação de objetos no cadastro ou atualização;
- Swagger para testes manuais das APIs.