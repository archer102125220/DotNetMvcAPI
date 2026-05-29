# DotNetMvcAPI

This is a .NET (ASP.NET Core Web API) project used for learning and practice purposes.

## Project Overview
The main purpose of this project is to serve as a playground for learning .NET API development. Here you can learn and test backend development skills such as creating RESTful APIs, designing Controllers, handling Models, and more.

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
   ```bash
   dotnet run
   ```

### Accessing the Application
Once the project successfully starts, the terminal will display the URL the server is listening on (e.g., `http://localhost:5xxx`).

In the Development environment, Swagger is enabled by default. You can open your browser and go to:
```
http://localhost:<your_port>/swagger
```
Through the Swagger UI, you can easily view all API endpoints and test them.

## Recommended Development Tools
You can choose any of the following editors to develop this project:
- **Visual Studio Code** (C# Dev Kit extension recommended)
- **JetBrains Rider**
- ~~**Visual Studio for Mac**~~ (Retired by Microsoft; using VS Code or Rider is recommended)
