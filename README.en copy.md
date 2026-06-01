# DotNetMvcAPI

This is a .NET 10 Web API project created for learning purposes.

## Project Environment
- **Framework**: .NET 10
- **Development Tools**: You can use IDEs such as Visual Studio, Visual Studio Code, or JetBrains Rider.

## How to Run the Project

You can run this project using the .NET CLI in your terminal. Please ensure you have the appropriate version of the .NET SDK installed.

1. **Navigate to the project directory**:
   ```bash
   cd DotNetMvcAPI
   ```

2. **Restore NuGet packages**:
   ```bash
   dotnet restore
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

4. **Browse the API Documentation**:
   Once the project is running, open the URL provided in the terminal (usually `http://localhost:5xxx` or `https://localhost:7xxx`) and append `/scalar/v1` to view the OpenAPI documentation in your browser.

## Cross-Platform IDE Development Guide

If you prefer using Visual Studio (Windows) or other full-featured Integrated Development Environments (IDEs) to open this project:
- Please open the project folder directly with your IDE, or load the project by opening `DotNetMvcAPI.csproj`.
- The project includes basic launch profiles (located in `Properties/launchSettings.json`). You can choose to run the application using IIS Express (on Windows) or the default Kestrel server.

## How This Project Was Created from Scratch

If you want to know how this project was initialized from scratch, below are the .NET CLI commands used:

### 1. Create the Web API Project
Run the following command in your terminal to create a Web API project named `DotNetMvcAPI`:
```bash
dotnet new webapi -n DotNetMvcAPI
```
*(Note: The `-n` parameter is used to specify the project name)*

### 2. Create the .gitignore File
To prevent temporary build files (like `bin/`, `obj/`) or local configuration files from being added to version control, you can generate the official standard `.gitignore` template after navigating into the project directory:
```bash
cd DotNetMvcAPI
dotnet new gitignore
```

## Architecture Concepts: Web API vs. MVC

In .NET web development, two common architectural patterns are **Web API** and **MVC (Model-View-Controller)**:

- **Web API**:
  - **Data-Focused**: Returns data (usually in JSON format) instead of HTML.
  - **When to use**: Ideal for building RESTful services that will be consumed by modern single-page applications (SPAs like React, Vue), mobile apps, or other backend services.
  
- **MVC (Model-View-Controller)**:
  - **UI-Focused**: Returns rendered HTML pages (using Razor Views).
  - **When to use**: When you need to build traditional web applications where the server renders the HTML and sends it directly to the browser.
