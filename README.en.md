# DotNetMvcAPI

This is a .NET (ASP.NET Core Web API) project used for learning and practice purposes.

## Project Overview
The main purpose of this project is to serve as a playground for learning .NET API development. Here you can learn and test backend development skills such as creating RESTful APIs, designing Controllers, handling Models, and more.

## MVC API vs Minimal API

When building a Web API project in .NET, there are two primary architectural choices: **MVC API (Controllers)** and **Minimal API**.

- **MVC API (Controller-based)**:
  This is the traditional and classic API development model in .NET. It adopts the Model-View-Controller architecture (though typically without the View in APIs). You create classes inheriting from `ControllerBase` and define endpoints using routing attributes. This model provides a complete lifecycle, Filters, advanced Model Binding, and a built-in Dependency Injection structure.
- **Minimal API**:
  This is a newer, lightweight development model designed to build HTTP APIs with minimal code and boilerplate. You can define routes and handlers directly in the `Program.cs` file using methods like `app.MapGet()` and `app.MapPost()`. It offers excellent performance and is ideal for microservices or simple, small-scale APIs.

### When to use MVC API?
- **Large Projects and Enterprise Applications**: When the project scale is large with numerous APIs, the Controller-based structure makes the codebase much easier to organize, categorize, and maintain.
- **Complex Filters and Middleware Needs**: If your endpoints require sharing complex authorization, validation, or exception handling logic.
- **Existing Team Expertise**: If the development team is already highly familiar with the traditional ASP.NET Core MVC architecture.
- **Advanced API Versioning and Documentation (Swagger) Integration**: MVC is often more mature and offers out-of-the-box support for these advanced features.

## Command to Create an MVC API Project

If you want to create an MVC API project similar to this one from scratch, you can use the following .NET CLI command:

```bash
# Create a Web API project named MyMvcApiProject (using Controller architecture)
dotnet new webapi -n MyMvcApiProject --use-controllers
```
*(Note: In newer .NET SDK versions, the default `webapi` template might use Minimal API. Adding the `--use-controllers` flag forces the creation of a Controller-based MVC API project.)*

## Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) (or the corresponding .NET SDK version for the project)

## How to Run the Project

> **💡 Note:** Microsoft has announced that there will be no new releases of Visual Studio for Mac after Visual Studio 2022 (the product is being retired). Therefore, for developing the latest .NET projects on macOS, it is highly recommended to use the `.NET CLI` (e.g., `dotnet run`) for building and running your applications.

To start this project, please follow these steps:

1. **Open your Terminal**
2. **Navigate to the project root directory**:
   ```bash
   cd /Users/parkerchen/Desktop/code/DotNetMvcAPI
   ```
3. **Run the project**:
   - **Normal Run Mode:**
   ```bash
   dotnet run
   ```

   - **Developer Mode (Hot Reload):**
   (Recommended for development. When you modify and save the code, the API server will automatically reload without requiring a manual restart.)
   ```bash
   dotnet watch run
   ```

### Accessing the Application
Once the project successfully starts, the terminal will display the URL the server is listening on (e.g., `http://localhost:5xxx`).

In the Development environment, Scalar is enabled by default as the API documentation client. You can open your browser and go to:
```
http://localhost:<your_port>/scalar/v1
```
Through the Scalar UI, you can easily view all API endpoints and test them.

> Note: Because this is a pure API project, there is no default web page at the root path (`/`). Visiting `http://localhost:<your_port>/` directly will result in a 404 Not Found. Please visit the API routes directly or navigate to `/scalar/v1`.

## Recommended Development Tools
You can choose any of the following editors to develop this project:
- **Visual Studio Code** (C# Dev Kit extension recommended)
- **JetBrains Rider**
- **Visual Studio (Windows)**
- ~~**Visual Studio for Mac**~~ (Retired by Microsoft; using VS Code or Rider is recommended)

## Cross-Platform Development: Notes on using Visual Studio (Windows)

Since this project was generated using the `.NET CLI`, if you are opening this project on Windows using the standard Visual Studio application, please keep the following in mind:

1. **Solution File (.sln)**:
   By default, the CLI might only generate a project file (`.csproj`) without a solution file (`.sln`). In Visual Studio, simply select "Open a project or solution" and choose `DotNetMvcAPI.csproj`. Visual Studio will automatically generate the corresponding `.sln` file for you when you save the project.
2. **Launch Profile**:
   When opened in Visual Studio on Windows, the default launch profile is usually set to `IIS Express`. To remain consistent with the execution behavior in cross-platform CLI environments (`dotnet run`), which uses the Kestrel server, it is highly recommended to change the launch profile from the dropdown menu in the top toolbar to the project name (**DotNetMvcAPI**).
