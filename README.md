# DotNetMvcAPI

[繁體中文](#繁體中文) | [English](#english)

---

## 繁體中文

這是一個用於學習和練習的 .NET (ASP.NET Core Web API) 專案。

### 專案簡介
本專案的主要目的是作為學習 .NET API 開發的練習場。在這裡可以學習和測試如何建立 RESTful API、設計 Controller、處理 Model 等後端開發技巧。

### 環境要求
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (或專案對應版本的 .NET SDK)

### 如何啟動專案

要啟動這個專案，請按照以下步驟進行：

1. **開啟終端機 (Terminal)**
2. **切換到專案的根目錄**：
   ```bash
   cd /Users/parkerchen/Desktop/code/DotNetMvcAPI
   ```
3. **執行專案**：
   ```bash
   dotnet run
   ```

#### 啟動後的存取方式
專案成功啟動後，終端機會顯示伺服器正在監聽的 URL (例如：`http://localhost:5xxx`)。

在開發環境 (Development) 下，專案預設啟用了 Swagger。您可以打開瀏覽器，前往：
```
http://localhost:<您的port>/swagger
```
透過 Swagger UI，您可以方便地查看所有的 API 介面並進行測試。

### 開發工具推薦
您可以選擇以下任一編輯器來開發此專案：
- **Visual Studio Code** (推薦安裝 C# Dev Kit 擴充功能)
- **Visual Studio for Mac**
- **JetBrains Rider**

---

## English

This is a .NET (ASP.NET Core Web API) project used for learning and practice purposes.

### Project Overview
The main purpose of this project is to serve as a playground for learning .NET API development. Here you can learn and test backend development skills such as creating RESTful APIs, designing Controllers, handling Models, and more.

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (or the corresponding .NET SDK version for the project)

### How to Run the Project

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

#### Accessing the Application
Once the project successfully starts, the terminal will display the URL the server is listening on (e.g., `http://localhost:5xxx`).

In the Development environment, Swagger is enabled by default. You can open your browser and go to:
```
http://localhost:<your_port>/swagger
```
Through the Swagger UI, you can easily view all API endpoints and test them.

### Recommended Development Tools
You can choose any of the following editors to develop this project:
- **Visual Studio Code** (C# Dev Kit extension recommended)
- **Visual Studio for Mac**
- **JetBrains Rider**
