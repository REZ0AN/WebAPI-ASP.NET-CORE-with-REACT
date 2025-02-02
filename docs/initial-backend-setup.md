# Introduction
We want to build `REST API` for a `TODO` web-application. Where an user can sign-in, sign-up to the web-application, also can add, edit, delete and list todos. For this we have choosed **ASP.NET Core WebApi**. 

# Setup Procedure

## Step-1

Install `.NET` **SDK** [From Here](http://dotnet.microsoft.com/en-us/download)

Open-up your terminal or command prompt to verify the installation. And execute command below:

```bash
dotnet --version
```
This will show the version of `dotnet` installed on your `PC` otherwise the installation process was unsuccessfull.

## Step-2

Create a folder anywhere in your `PC` called `TODO`. Then open this folder with `VSCODE`.

## Step-3

Install some extensions on VSCODE for hastle free setup. Extensions are 

- C# (By Microsoft)
- C# Dev Kit (By Microsoft)
- C# Extensions (By JosKreativ)
- .NET Install Tool (By Microsoft)
- Rest Client (By Huachao Mao)

## Step-4 

Open-up the `VSCODE` terminal by pressing `Ctrl+j`. And initialize the project backend using below command: 

```bash
dotnet new webapi -o backend
```
This command will setup a boiler-palette for your web-application.

## Step-5 

Understanding the folder structure and the files which are in default setup is must needed step.

Folder Structure you will see

```bash
./backend
├── Program.cs
├── Properties
├── appsettings.Development.json
├── appsettings.json
├── backend.csproj
├── backend.http
├── bin
└── obj
```
**Description of each of the files and folders**

--- 

### Program.cs

This is the entry point of the application. In ASP.NET Core, it sets up the application host, configures services (dependency injection), middleware (such as routing, authentication, etc.), and defines how the HTTP request pipeline is managed. It essentially “boots up” the API's.

### Properties

This folder usually contains project-specific settings. A common file here is `launchSettings.json`, which provides configuration details for local development (e.g., environment variables, application URLs, and profiles for running or debugging the app).

Default configs in `launchSettings.json`

```json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://localhost:5181",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7287;http://localhost:5181",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```
But we only need one profile so we will update this

```json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "localhost": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://localhost:5181",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

When the application will start we can interact with the REST API through this url `http://localhost:5181`. And it is set to `Development` Environment.

### appsettings.Development.json

This configuration file contains settings that override the values in `appsettings.json` when the application is running in a **Development** environment. It’s useful for storing development-specific configuration details (like connection strings or logging levels) that shouldn’t be used in production.

### appsettings.json

This file holds **general** configuration settings for the application. It might include **connection strings**, **logging settings**, and other custom configurations. When the application starts, these settings are loaded and can be accessed throughout the app using ASP.NET Core’s configuration APIs.

**Note** By default, ASP.NET Core uses a layered approach to **load configuration settings** from multiple sources. When you use the default host builder (typically via the CreateDefaultBuilder method in `Program.cs`).`appsettings.json` this file is always loaded first. It provides the base configuration settings for the application. `appsettings.{Environment}.json` ff the `ASPNETCORE_ENVIRONMENT` variable is set (for example, to "**Development**" or "**Production**"), the corresponding file (e.g., `appsettings.Development.json`) is loaded next. Any settings in this file `override` those in `appsettings.json` if there is a **conflict**. Default `override` order `Base settings (appsettings.json) > Environment-specific settings (appsettings.{Environment}.json) > User secrets (if in Development) > Environment variables > Command-line arguments`.

### backend.csproj

This is the project file for the ASP.NET Core Web API. It **defines** the project’s metadata, dependencies (NuGet packages), build settings, and other configuration details required by the .NET build system.

### backend.http

This file is often used for testing HTTP endpoints directly from the development environment. Tools like Visual Studio Code’s` REST Client extension` allow us to write and send HTTP requests from this file, making it a convenient way to manually test the API routes during development.

### bin

The bin folder contains the compiled binaries of the project (e.g., DLL files and the executable). After building the project, the output is stored here. This folder is essential for running the application. This folder you will see after running the project once. This is how you can run (go to the directory in which you setup the project)

```bash
dotnet run
```
or you can run using this command

```bash
dotnet watch run
```
I will discuss this two commands later.

### obj
The obj folder holds temporary build artifacts and intermediate files generated during the build process. These files are used by the compiler and MSBuild, and while they are crucial for the build process, these files are not really big concern for us.

This is the basic setup for ASP.NET Core WebApi. And this documentation only focuses on the initial setup and make you familiar the folders and files of initial setup.