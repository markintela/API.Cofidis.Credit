# API.Credit.Cofidis

Bem-vindo ao repositório da API.Credit.Cofidis. Este documento fornece instruções sobre como configurar e testar a API.

## Passo a Passo

### Passo 1: Configuração da Solução

1. **Abrir a Solução:**
   - Abra a solução no Visual Studio.

2. **Configurar Múltiplos Projetos de Startup:**
   - Escolha a opção **Multiple startup projects**.
   - Configure os seguintes projetos para iniciar:
     - **[External]API.DigitalKey**
     - **API.Cofidis.Credit**
   - Certifique-se de definir a ação desejada (Start ou Start without debugging) para cada projeto.

### Passo 2: Criar a Procedure

Para criar a procedure basta correr o código abaixo no sql managment studio para na base de dados criada pelo entity framework.

CREATE PROCEDURE SP_DetermineCreditLimit
    @baseSalary DECIMAL(18, 2),
    @creditLimit DECIMAL(18, 2) OUTPUT
AS
BEGIN
    IF @baseSalary <= 1000
    BEGIN
        SET @creditLimit = 1000;
    END
    ELSE IF @baseSalary > 1000 AND @baseSalary <= 2000
    BEGIN
        SET @creditLimit = 2000;
    END
    ELSE
    BEGIN
        SET @creditLimit = 5000;
    END
END;


### Passo 3: Executar Test Cases

Para executar selecione o projeto de testes
**[External]API.Cofidis.Credit.Tests**



