# Introdução 
>.Net Core 3.1

>Projeto **ABCBrasil.OpenBanking.Pagamento.Api**

Esta Api Canal tem como objetivo disponibilizar serviço de Registro de Pagamento e ConsultaCIP.

# Desenvolvimento

Siga as intruções abaixo para clonar e executar modificações no projeto.
Clone o repositório, a partir do respositório OpenBanking neste exemplo estamos salvando na pasta "**ABC**", você pode salvar na sua pasta de destino:

```
cd Projetos
git clone https://abcbrasil.visualstudio.com/OpenBanking/_git/ABCBrasil.OpenBanking.Pagamento.Api

```
Mude para o respositório develop e baixe as atualizações:
```
cd ABCBrasil.OpenBanking.Pagamento.Api
git checkout develop 
git fetch
git pull
```

Crie o seu repositório a partir do develop e realize as suas alterações:

``` 
git checkout -b feature/_nome_da_nova_branche develop

```
>O padrão é tudo em minúsculo e separados com o símbolo underline "_"


Ao ***finalizar as suas alterações*** no projeto final você deve ***solicitar um pull request para que as modificações*** sejam enviadas ao branch develop. O pull request pode ser solicitado diretamente através do site do respositório.

Antes de realizar a solicitação de ***pull request*** ao ***branche develop*** não se esqueça de realizar um merge back com o branch ***develop***.

### Estrutura do Projeto

Os processos estão mapeados de forma dinâmica, com o objetivo de otimizar e reutilizar os processos já existentes, além de possibilitar que as manutenções sejam realizadas de forma simples. A única coisa que é única de cada processo é o ***CorrelationId*** para informações no log.

Verifique abaixo a arquitetura do projeto descrita no diagrama abaixo:

### Diagrama de Contexto
![C4Diagram](/Documentacao/C1_APICANALPAGAMENTO.png)

### Diagrama de Containers
![C4Diagram](/Documentacao/C2_APICANALPAGAMENTO.png)

### Diagrama de Componentes
![C4Diagram](/Documentacao/C3_APICANALPAGAMENTO.png)


#### Api de integração (Tibco)
``` Link para projeto Tibco``` 

#### Conectividade Tibco
```
    "CalendarioConfig": {
      "BaseAddress": "",
      "ApiKeyName": "",
      "ApiKey": ""
    },

    "CoreCalculoConfig": {
      "BaseAdress": "",
      "ApiKeyName": "",
      "ApiKey": ""
    },

    "CorePagamentoLoteConfig": {
      "BaseAdress": "",
      "ApiKeyName": "",
      "Apikey": ""
    }
```
Serviço | Processo | Tipo
-|-|-
Cip |Consulta Boleto | Síncrono
Cálculo |Valor | Síncrono
Calendário |proximo-dia-util | Síncrono

# Recursos e payload
Os recursos e exemplos de requisições estão disponíveis na pasta Documentos através de uma collection que pode ser importada utilizando o Postman.
Arquivo | Descrição
-|-
[ABCBrasil.OpenBanking.Pagamento.Api.postman_collection.json](/Documentacao/ABCBrasil.OpenBanking.Pagamento.Api.postman_collection.json) | Coleção com os exemplos de requisições ConsultaCIP que devem ser importadas no Postman


# ApiKey
* A API possui uma ApiKey que precisa ser passada via header. 
```csharp
...
  "ABCBrasilApiSettings": {
    "apiName": "ABCBrasil.OpenBanking.Pagamento.Api",
    "cacheActivated": true,
    "cacheTtl": 180, //SEGUNDOS
    "keyName": "ABC_KeyID",
    "keyValue": "174BABE4-8BA5-4FF1-A38F-CEB5F8CDFCC4"
	}
...
```

# Tibco
* A API se comunica com o Tibco, e o setup é feito em tempo de execução. 
```csharp
...
    "CalendarioConfig": {
      "BaseAddress": "http://svdweb01.abcbrasildev.local/ABCBrasil.Calendar.Api/api/v1",
      "ApiKeyName": "ABC_KeyID_Calendar",
      "ApiKey": "D95AD5CF-D6B0-41AE-AE78-93C69D9913F7"
    },
	"CoreCalculoConfig": {
      "BaseAddress": "http://svdtib01.abcbrasildev.local:7100/api/v1",
      "ApiKeyName": "ABC_KeyID_Pagamento",
      "ApiKey": "F50AAEB7-B08E-41E6-B54B-E74327FD7E5B"
    },
    "CorePagamentoLoteConfig": {
      "BaseAddress": "http://svdweb01.abcbrasildev.local/ABCBrasil.Core.Pagamento.API/api/v1",
      "ApiKeyName": "ApiKey",
      "Apikey": "3939A032-24E0-4F40-BA7B-CA9989694F6E"
    }
...
```

# Seq
* A API possui instrumentação de log utilizando Serilog via Seq.
* As configurações abaixo são do servidor de PRD. 
```csharp
...
    "Serilog": {
    "IsNotEncrypt": "true",
    "WriteTo": [
      {
        "Name": "Seq",
        "Args": {
          "serverUrl": "http://localhost:5341/",
          "apiKey": "cS1Kf4j6YcsCsPnSQ64F",
          "restrictedToMinimumLevel": "Information"
        }
      }
    ]
  },
...
```

# Helthcheck
* A API possui helthcheck. 
* https://localhost:5001/healthchecks


#

# Contribua
* Crie uma branch atualizada como feature/wi_9999 <--Número do workitem

#Histórico
Versão|Descrição
-|-
1.0.0.0 | Inicial