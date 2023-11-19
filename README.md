# GPS's Azure Resume

This is my Cloud Resume Challenge built on Azure. It's a static website hosted on Azure Storage, with a visitor counter built on Azure Functions. The website is built with HTML, CSS, and JavaScript. The visitor counter is built with .NET and Azure Functions. 

If you'd like to build your own, here is the template [GitHub Repo](https://github.com/madebygps/cgc-azure-resume) and YouTube video [video](https://youtu.be/ieYrBWmkfno)

![architecture](architecture.png)

## Demo

[View it live here](https://www.gwynethpena.com)

## Pre-requisites

I leverage [Dev Containers](https://code.visualstudio.com/docs/remote/containers) for my development environment. If you'd like to use it, you'll need to install [Docker](https://www.docker.com/products/docker-desktop) and the [Dev Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) for VS Code.

Once installed:

1. Make sure Docker is running.
2. Open the project in VS Code.
3. Ctrl/Cmd + Shift + P to open the command palette.
4. Type "Reopen in Container" and select:
    - [`.NET API`](.devcontainer/api/devcontainer.json) container is for working with Azure Functions backend.
    - [`JS Frontend`](.devcontainer/frontend/devcontainer.json) container is for working with the frontend.
5. VS Code will reload and you'll be in the container.

## Structure

- `frontend/`: Folder contains the website.
    - `main.js`: Folder contains visitor counter code.
- `api/`: Folder contains the dotnet API deployed on Azure Functions.
    - `Counter.cs`: Contains the visitor counter code.
- `.github/workflows/`: Folder contains CI/CD workflow configurations.
- `.devcontainer`: Folder contains the my container configuration for VS Code.
