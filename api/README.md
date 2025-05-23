# API Function for Azure Resume

This directory contains the Azure Functions API code for the visitor counter functionality.

## Local Development Setup

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Azure Functions Core Tools](https://docs.microsoft.com/azure/azure-functions/functions-run-local#install-the-azure-functions-core-tools)
- Azure subscription with a Cosmos DB account
- [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli) or [Azure Developer CLI](https://learn.microsoft.com/azure/developer/azure-developer-cli/install-azd)

### Authentication

The function uses DefaultAzureCredential to authenticate with Azure Cosmos DB. When running locally, it will use credentials in the following order:

1. Environment variables (AZURE_TENANT_ID, AZURE_CLIENT_ID, AZURE_CLIENT_SECRET)
2. Azure CLI (`az login`)
3. Azure Developer CLI (`azd auth login`)
4. Visual Studio authentication
5. Azure PowerShell authentication

To use Azure CLI authentication (recommended for local development):

```bash
# Login to Azure
az login

# If you have multiple subscriptions, set the one you want to use
az account set --subscription <subscription-id>
```

To use Azure Developer CLI:

```bash
# Login to Azure
azd auth login
```

### Configuration

1. Create a local.settings.json file by copying local.settings.json.template
2. Update the `CosmosDbEndpoint` setting with your Cosmos DB URL

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "CosmosDbEndpoint": "https://<your-cosmosdb-account>.documents.azure.com:443/"
  },
  "Host": {
    "LocalHttpPort": 7071,
    "CORS": "*"
  }
}
```

### Running the Function

```bash
func start
```

The function will run on http://localhost:7071 by default.

### Changes from Binding Approach

This project originally used Azure Functions Cosmos DB bindings with connection strings. It now uses the CosmosClient directly with DefaultAzureCredential for improved security. This approach:

1. Eliminates the need for connection strings with sensitive keys
2. Provides more flexibility in how we interact with Cosmos DB
3. Makes it easier to use managed identities in production
4. Works well with local development using Azure CLI or Azure Developer CLI