# GPS's Azure Resume

This is my Cloud Resume Challenge built on Azure. It's a static website hosted on Azure Storage, with a visitor counter built on Azure Functions. The website is built with HTML, CSS, and JavaScript. The visitor counter is built with .NET and Azure Functions.

If you'd like to build your own, here is the YouTube video [video](https://youtu.be/ieYrBWmkfno).

![architecture](architecture.png)

## Table of Contents

- [Demo](#demo)
- [Pre-requisites](#pre-requisites)
- [Structure](#structure)
- [Setup](#setup)
- [Frontend Resources](#frontend-resources)
- [Backend Resources](#backend-resources)
- [Testing Resources](#testing-resources)
- [CI/CD Resources](#cicd-resources)
- [TO DO](#to-do)
- [Contributing](#contributing)
- [License](#license)

## Demo

[View it live here](https://www.gpsresume.com/)

## Pre-requisites

I leverage [Dev Containers](https://code.visualstudio.com/docs/remote/containers) for my development environment. If you'd like to use it, you'll need to install [Docker](https://www.docker.com/products/docker-desktop) and the [Dev Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) extension for VS Code.

## Structure

- `frontend/`: Contains the website.
    - `main.js`: Contains visitor counter code.
- `api/`: Contains the .NET API deployed on Azure Functions.
    - `GetVisitorCounter.cs`: Contains the visitor counter code.
- `.github/workflows/`: Contains CI/CD workflow configurations.
- `.devcontainer`: Contains the container configuration for VS Code.

## Setup

1. Make sure Docker is running.
2. Open the project in VS Code.
3. Press `Ctrl/Cmd + Shift + P` to open the command palette.
4. Type "Reopen in Container" and select:
    - [`.NET API`](.devcontainer/api/devcontainer.json) container for working with the Azure Functions backend.
    - [`JS Frontend`](.devcontainer/frontend/devcontainer.json) container for working with the frontend.
    - [`Tests`](.devcontainer/tests/devcontainer.json) container for running tests.
5. VS Code will reload and you'll be in the container.

## Frontend Resources

The front-end is a static site with HTML, CSS, and JavaScript. It includes a visitor counter that fetches data via an API call to an Azure Function.

- I used this [template](https://www.styleshout.com/free-templates/ceevee/) to create my site.
- This [article](https://www.digitalocean.com/community/tutorials/how-to-use-the-javascript-fetch-api-to-get-data) explains how to use the JavaScript Fetch API to make an API call.
- [Azure Storage Explorer](https://azure.microsoft.com/features/storage-explorer/) is a handy tool for working with Storage Accounts.
- This is how you can [deploy a static site to blob storage](https://docs.microsoft.com/azure/storage/blobs/storage-blob-static-website-host).

## Backend Resources

The back-end is an [HTTP triggered Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-bindings-http-webhook-trigger?tabs=csharp) with Cosmos DB input and output binding. The function retrieves a CosmosDB item, increments it, saves it, and returns its value to the caller.

- [Create a Cosmos DB account](https://docs.microsoft.com/azure/cosmos-db/create-cosmosdb-resources-portal)
- [Create an HTTP triggered Azure Function in Visual Studio Code](https://docs.microsoft.com/azure/azure-functions/functions-develop-vs-code?tabs=csharp)
- [Azure Functions Cosmos DB bindings](https://docs.microsoft.com/azure/azure-functions/functions-bindings-cosmosdb-v2)
- [Retrieve a Cosmos DB item with Functions binding](https://docs.microsoft.com/azure/azure-functions/functions-bindings-cosmosdb-v2-input?tabs=csharp)
- [Write to a Cosmos DB item with Functions binding](https://docs.microsoft.com/azure/azure-functions/functions-bindings-cosmosdb-v2-output?tabs=csharp)
- [Enable CORS with Azure Functions locally](https://learn.microsoft.com/azure/azure-functions/functions-develop-local#local-settings-file) and once it's [deployed to Azure](https://docs.microsoft.com/azure/azure-functions/functions-how-to-use-azure-function-app-settings?tabs=portal#cors).

## Testing Resources

[Testing is important](https://dev.to/flippedcoding/its-important-to-test-your-code-3lid). Though my tests are simple, they exist. I am using .NET but some of these resources will apply to any language.

- [Getting Started with xUnit.net](https://xunit.net/docs/getting-started/netcore/cmdline)
- [Testing Azure Functions](https://techcommunity.microsoft.com/t5/fasttrack-for-azure/azure-functions-part-2-unit-and-integration-testing/ba-p/3769764)

## CI/CD Resources

- [Deploy a blob storage static site with GitHub Actions](https://docs.microsoft.com/azure/storage/blobs/storage-blobs-static-site-github-actions)
- [Deploy an Azure Function to Azure with GitHub Actions](https://github.com/marketplace/actions/azure-functions-action)
- [Implement .NET testing in GitHub Actions](https://docs.github.com/en/actions/guides/building-and-testing-net)

## TO DO

- Implement tests into CI/CD.
- Create IaC files.
- Improve tests and tests documentation.

## Contributing

Contributions are welcome! Please read the [contributing guidelines](CONTRIBUTING.md) first.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.