# 🚀 GPS's Azure Resume

This is my Cloud Resume Challenge built on Azure. It's a static website hosted on Azure Storage, with a visitor counter built on Azure Functions. The website is built with HTML, CSS, and JavaScript. The visitor counter is built with .NET 8.0 and Azure Functions using the isolated process model.

If you'd like to build your own, here is the YouTube video [video](https://youtu.be/ieYrBWmkfno).

![architecture](architecture.png)

## 📋 Table of Contents

- [🔍 Demo](#-demo)
- [📝 Pre-requisites](#-pre-requisites)
- [🏗️ Structure](#️-structure)
- [⚙️ Setup](#️-setup)
- [🌐 Frontend Resources](#-frontend-resources)
- [⚡ Backend Resources](#-backend-resources)
- [🧪 Testing Resources](#-testing-resources)
- [🔄 CI/CD Resources](#-cicd-resources)
- [📌 TO DO](#-to-do)
- [👥 Contributing](#-contributing)
- [📜 License](#-license)

## 🔍 Demo

[View it live here](https://www.gpsresume.com/)

## 📝 Pre-requisites

I leverage [Dev Containers](https://code.visualstudio.com/docs/remote/containers) for my development environment. If you'd like to use it, you'll need to install [Docker](https://www.docker.com/products/docker-desktop) and the [Dev Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) extension for VS Code.

## 🏗️ Structure

- `frontend/`: Contains the website.
    - `main.js`: Contains visitor counter code.
- `api/`: Contains the .NET 8.0 API deployed on Azure Functions.
    - `Program.cs`: Contains the dependency injection setup and DefaultAzureCredential configuration.
    - `CosmosDbService.cs`: Service for interacting with Cosmos DB.
    - `GetVisitorCounter.cs`: Contains the visitor counter code.
- `.github/workflows/`: Contains CI/CD workflow configurations.
- `.devcontainer`: Contains the container configuration for VS Code.

## ⚙️ Setup

1. Make sure Docker is running.
2. Open the project in VS Code.
3. Press `Ctrl/Cmd + Shift + P` to open the command palette.
4. Type "Reopen in Container" and select:
    - [`.NET API`](.devcontainer/api/devcontainer.json) container for working with the Azure Functions backend.
    - [`JS Frontend`](.devcontainer/frontend/devcontainer.json) container for working with the frontend.
    - [`Tests`](.devcontainer/tests/devcontainer.json) container for running tests.
5. VS Code will reload and you'll be in the container.

## 🌐 Frontend Resources

The front-end is a static site with HTML, CSS, and JavaScript. It includes a visitor counter that fetches data via an API call to an Azure Function.

- 🎨 I used this [template](https://www.styleshout.com/free-templates/ceevee/) to create my site.
- 📡 This [article](https://www.digitalocean.com/community/tutorials/how-to-use-the-javascript-fetch-api-to-get-data) explains how to use the JavaScript Fetch API to make an API call.
- 🗄️ [Azure Storage Explorer](https://azure.microsoft.com/features/storage-explorer/) is a handy tool for working with Storage Accounts.
- 🌍 This is how you can [deploy a static site to blob storage](https://docs.microsoft.com/azure/storage/blobs/storage-blob-static-website-host).

## ⚡ Backend Resources

The back-end is an [HTTP triggered Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-bindings-http-webhook-trigger?tabs=csharp) with Azure Cosmos DB integration. The function is built using .NET 8.0 with the isolated process model and uses DefaultAzureCredential for secure authentication to Cosmos DB.

- 🔧 [Create a Cosmos DB account](https://docs.microsoft.com/azure/cosmos-db/create-cosmosdb-resources-portal)
- 🛠️ [Create an HTTP triggered Azure Function in Visual Studio Code](https://docs.microsoft.com/azure/azure-functions/functions-develop-vs-code?tabs=csharp)
- 🔌 [Azure Functions Cosmos DB bindings](https://docs.microsoft.com/azure/azure-functions/functions-bindings-cosmosdb-v2)
- 📤 [Enable CORS with Azure Functions locally](https://learn.microsoft.com/azure/azure-functions/functions-develop-local#local-settings-file) and once it's [deployed to Azure](https://docs.microsoft.com/azure/azure-functions/functions-how-to-use-azure-function-app-settings?tabs=portal#cors).
- 🔐 [DefaultAzureCredential documentation](https://learn.microsoft.com/dotnet/api/azure.identity.defaultazurecredential)

### 🔑 Authentication

The application uses DefaultAzureCredential to authenticate with Azure Cosmos DB. This simplifies credential management by supporting multiple authentication methods and improves security by eliminating the need for connection strings with sensitive keys.

#### 💻 Local Development

1. Copy `api/local.settings.json.template` to `api/local.settings.json`
2. Set the `CosmosDbEndpoint` to your Cosmos DB endpoint URL
3. For local authentication, you can use:
   - **Azure CLI**: Sign in with `az login` before running the application
   - **Azure Developer CLI**: Sign in with `azd auth login` before running the application
   - **Visual Studio**: Use Visual Studio authentication
   - **Service Principal**: Set `AZURE_TENANT_ID`, `AZURE_CLIENT_ID`, and `AZURE_CLIENT_SECRET` environment variables

The DefaultAzureCredential will automatically detect and use credentials from the development environment.

#### ☁️ Azure Deployment

When deployed to Azure Functions, the app will use the Function App's managed identity:

1. Enable system-assigned managed identity on your Function App
2. Grant the managed identity appropriate permissions on your Cosmos DB account
3. Configure the app setting in your Function App:
   - `CosmosDbEndpoint`: Your Cosmos DB endpoint URL
   - `CosmosDbDatabaseName`: Your database name (e.g., "AzureResume")
   - `CosmosDbContainerName`: Your container name (e.g., "Counter")

## 🧪 Testing Resources

[Testing is important](https://dev.to/flippedcoding/its-important-to-test-your-code-3lid). Though my tests are simple, they exist. I am using .NET but some of these resources will apply to any language.

- 📚 [Getting Started with xUnit.net](https://xunit.net/docs/getting-started/netcore/cmdline)
- 🧩 [Testing Azure Functions](https://techcommunity.microsoft.com/t5/fasttrack-for-azure/azure-functions-part-2-unit-and-integration-testing/ba-p/3769764)

## 🔄 CI/CD Resources

- 🚢 [Deploy a blob storage static site with GitHub Actions](https://docs.microsoft.com/azure/storage/blobs/storage-blobs-static-site-github-actions)
- 🔄 [Deploy an Azure Function to Azure with GitHub Actions](https://github.com/marketplace/actions/azure-functions-action)
- ✅ [Implement .NET testing in GitHub Actions](https://docs.github.com/en/actions/guides/building-and-testing-net)

## 📌 TO DO

- 🔄 Implement tests into CI/CD.
- 🏗️ Create IaC files.
- 📝 Improve tests and tests documentation.

## 👥 Contributing

Contributions are welcome! Please read the [contributing guidelines](CONTRIBUTING.md) first.

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.